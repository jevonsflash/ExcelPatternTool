using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private ValidatorProvider validatorProvider;

        public IEnumerable<ValidatorInfo> ValidatorInfos { get; set; }
        public IEnumerable<ProcessResult> Validate(EmpoyeeEntity empoyeeEntity)
        {

            ValidatorInfos = validatorProvider.GetValidatorInfos();

            var result = new List<ProcessResult>();

            foreach (var validator in ValidatorInfos)
            {
                var currentConvention = validatorProvider.GetConvention(validator.Convention);
                var currentResult = ValidateItem(validator, empoyeeEntity, currentConvention);
                currentResult.Column = validator.PropName;
                result.Add(currentResult);
            }

            return result;
        }


        private ProcessResult ValidateItem(ValidatorInfo c, EmpoyeeEntity e, Func<ValidatorInfo, EmpoyeeEntity, ProcessResult> convention)
        {
            var type = c.EntityType;

            var aliasDictionary = new Dictionary<string, string>();
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
                    aliasDictionary.Add(propName, propNameAlias);
                }


            }

            foreach (var pare in aliasDictionary)
            {
                var propName = pare.Key;
                var propNameAlias = pare.Value;

                if (!string.IsNullOrEmpty(propNameAlias))
                {
                    c.Expression.Replace(propNameAlias, propName);
                }
            }

            return convention?.Invoke(c, e);
        }

        public Validator()
        {
            validatorProvider = new ValidatorProvider();
        }

    }
}
