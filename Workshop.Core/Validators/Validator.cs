using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Workshop.Core.Excel.Attributes;
using Workshop.Core.Linq.Models;
using Workshop.Core.Patterns;
using Workshop.Core.Linq.Core;
using Workshop.Core.Excel.Models.Interfaces;

namespace Workshop.Core.Validators
{
    public class Validator
    {
        private IValidatorProvider validatorProvider;
        private Dictionary<string, string> aliasDictionary;


        private IEnumerable<PatternItem> validatorInfos;

        public IEnumerable<PatternItem> ValidatorInfos
        {
            get
            {
                if (validatorInfos==null)
                {
                    validatorInfos = validatorProvider.GetPatternItems();

                }
                return validatorInfos;
            }
            set { validatorInfos = value; }
        }

        public IEnumerable<ProcessResult> Validate(object obj)
        {


            var result = new List<ProcessResult>();

            foreach (var validator in ValidatorInfos)
            {
                var currentConvention = validatorProvider.GetConvention(validator.ValidationPattern.Convention.ToString()).Convention;
                if (currentConvention==null)
                {
                    continue;
                }
                var genericType = validatorProvider.GetType().GetGenericTypeDefinition();
                validator.ValidationPattern.Expression = ValidateItem(genericType, validator.ValidationPattern.Expression);
                var currentResult = currentConvention?.Invoke(validator, obj);
                if (currentResult==null)
                {
                    continue;
                }
                currentResult.KeyName = $"RowNumber 为 {(obj as IExcelEntity).RowNumber} 的 {validator.PropName}";
                result.Add(currentResult);
            }

            return result;
        }

        private string ValidateItem(Type entityType, string originalString)
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

        private void InitAliasDictionary(Type entityType)
        {
            var type = entityType;
            foreach (var prop in type.GetProperties())
            {
                var attrs = System.Attribute.GetCustomAttributes(prop);
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
            SetValidatorProvider(validatorProvider);

        }

        public Validator()
        {
            aliasDictionary = new Dictionary<string, string>();

        }

        public void SetValidatorProvider(IValidatorProvider validatorProvider)
        {
            this.validatorProvider = validatorProvider;
            var genericType = validatorProvider.GetType().GetGenericArguments().FirstOrDefault();
            InitAliasDictionary(genericType);

            this.validatorProvider.PropertyTypeMaper = this.GetOriginalPropertyName;

        }
    }
}
