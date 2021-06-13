using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Workshop.Core.Entites;
using Workshop.Infrastructure.Attributes;
using Workshop.Infrastructure.Linq.Core;
using Workshop.Infrastructure.Models;

namespace Workshop.Core.Validators
{
    public class Validator
    {
        private IValidatorProvider validatorProvider;
        private Dictionary<string, string> aliasDictionary;

        public IEnumerable<ValidatorInfoItem> ValidatorInfos { get; set; }
        public IEnumerable<ProcessResult> Validate(object obj)
        {

            ValidatorInfos = validatorProvider.GetValidatorInfos();

            var result = new List<ProcessResult>();

            foreach (var validator in ValidatorInfos)
            {
                var currentConvention = validatorProvider.GetConvention(validator.Convention).Convention;
                var genericType = validatorProvider.GetType().GetGenericTypeDefinition();
                validator.Expression = ValidateItem(genericType, validator.Expression);
                var currentResult = currentConvention?.Invoke(validator, obj);
                currentResult.KeyName = $"{(obj as EmployeeEntity).Name} 的 {validator.PropName}" ;
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

        public Validator(IValidatorProvider validatorProvider):this()
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
