using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ExcelPatternTool.Contracts.Validations;
using ExcelPatternTool.Contracts;
using ExcelPatternTool.Contracts.Attributes;

namespace ExcelPatternTool.Validation
{
    public class Validator
    {
        private Type _entityType;
        private IValidatorProvider validatorProvider;
        private Dictionary<string, string> aliasDictionary;


        private Dictionary<string, IValidation> _validationContainers;

        public Dictionary<string, IValidation> ValidationContainers
        {
            get
            {
                if (_validationContainers == null)
                {
                    _validationContainers = validatorProvider.GetValidationContainers(_entityType);

                }
                return _validationContainers;
            }
            set { _validationContainers = value; }
        }

        public IEnumerable<ProcessResult> Validate(object obj)
        {
            if (_entityType == null)
            {
                throw new Exception("还没有为实体指定类型，是否忘记调用SetValidatorProvider?");
            }
            var result = new List<ProcessResult>();

            foreach (var validator in ValidationContainers)
            {
                if (validator.Value == null)
                {
                    continue;
                }
                var currentConvention = validatorProvider.GetConvention(validator.Value.Convention.ToString()).Convention;
                if (currentConvention == null)
                {
                    continue;
                }
                validator.Value.Expression = ValidateItem(validator.Value.Expression);
                var currentResult = currentConvention?.Invoke(validator.Key, validator.Value, obj);
                if (currentResult == null)
                {
                    continue;
                }
                currentResult.KeyName = $"RowNumber 为 {(obj as IExcelEntity).RowNumber} 的 {validator.Key}";
                result.Add(currentResult);
            }

            return result;
        }

        private string ValidateItem(string originalString)
        {
            var result = originalString;


            foreach (var pare in aliasDictionary)
            {
                var propName = pare.Value;
                var propNameAlias = pare.Key;

                if (!string.IsNullOrEmpty(propNameAlias))
                {
                    result = originalString.Replace(propNameAlias, propName);
                }
            }

            return result;

        }

        public virtual void InitAliasDictionary(Type entityType)
        {
            var type = entityType;
            foreach (var prop in type.GetProperties())
            {
                var attrs = Attribute.GetCustomAttributes(prop);
                var propName = prop.Name;
                var propNameAlias = string.Empty;

                ImportableAttribute attribute = default;
                foreach (var attr in attrs)
                {
                    if (attr is ImportableAttribute)
                    {
                        attribute = attr as ImportableAttribute;
                    }
                }

                if (attribute != null && attribute.Ignore == false && !string.IsNullOrEmpty(attribute.Name))
                {
                    propNameAlias = attribute.Name;
                    aliasDictionary.Add(propNameAlias, propName);
                }
            }
        }

        public string GetOriginalPropertyName(string alias)
        {
            var result = string.Empty;
            aliasDictionary.TryGetValue(alias, out result);
            return result;
        }

        public Validator(IValidatorProvider validatorProvider) : this()
        {
            this.validatorProvider = validatorProvider;

        }

        public Validator()
        {
            aliasDictionary = new Dictionary<string, string>();

        }

        public void SetValidatorProvider<T>(IValidatorProvider validatorProvider)
        {
            SetValidatorProvider(typeof(T), validatorProvider);
        }

        public void SetValidatorProvider(Type entityType, IValidatorProvider validatorProvider)
        {
            if (entityType == null)
            {
                throw new ArgumentNullException("EntityType为空");
            }
            _entityType = entityType;
            this.validatorProvider = validatorProvider;
            InitAliasDictionary(entityType);
            this.validatorProvider.PropertyTypeMaper = GetOriginalPropertyName;

        }
    }
}
