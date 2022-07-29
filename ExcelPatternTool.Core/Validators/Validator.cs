using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using ExcelPatternTool.Core.Excel.Attributes;
using ExcelPatternTool.Core.Linq.Models;
using ExcelPatternTool.Core.Patterns;
using ExcelPatternTool.Core.Linq.Core;
using ExcelPatternTool.Core.Excel.Models.Interfaces;

namespace ExcelPatternTool.Core.Validators
{
    public class Validator
    {
        private IValidatorProvider validatorProvider;
        private Dictionary<string, string> aliasDictionary;


        private IEnumerable<PatternItem> _patternItems;

        public IEnumerable<PatternItem> PatternItems
        {
            get
            {
                if (_patternItems==null)
                {
                    _patternItems = validatorProvider.GetPatternItems();

                }
                return _patternItems;
            }
            set { _patternItems = value; }
        }

        public IEnumerable<ProcessResult> Validate(object obj)
        {


            var result = new List<ProcessResult>();

            foreach (var validator in PatternItems)
            {
                if (validator.ValidationPattern==null)
                {
                    continue;
                }
                var currentConvention = validatorProvider.GetConvention(validator.ValidationPattern.Convention.ToString()).Convention;
                if (currentConvention==null)
                {
                    continue;
                }
                validator.ValidationPattern.Expression = ValidateItem(validator.ValidationPattern.Expression);
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
            this.validatorProvider = validatorProvider;

        }

        public Validator()
        {
            aliasDictionary = new Dictionary<string, string>();

        }

        public void SetValidatorProvider<T>(IValidatorProvider validatorProvider)
        {
            this.SetValidatorProvider(typeof(T), validatorProvider);
        }

        public void SetValidatorProvider(Type entityType, IValidatorProvider validatorProvider)
        {
            this.validatorProvider = validatorProvider;
            InitAliasDictionary(entityType);
            this.validatorProvider.PropertyTypeMaper = this.GetOriginalPropertyName;

        }
    }
}
