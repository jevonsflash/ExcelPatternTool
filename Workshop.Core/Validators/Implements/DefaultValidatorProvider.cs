using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Workshop.Core.Entites;
using Workshop.Infrastructure.Core;
using Workshop.Infrastructure.Helper;
using Workshop.Infrastructure.Linq.Core;
using Workshop.Infrastructure.Models;
using Workshop.Infrastructure.Services;

namespace Workshop.Core.Validators
{
    public class DefaultValidatorProvider<T> : IValidatorProvider
    {
        public Dictionary<string, ValidateConvention> Conventions { get; set; }
        public DefaultValidatorProvider()
        {
            Conventions = new Dictionary<string, ValidateConvention>();
            Init();
        }

        public Func<string, string> PropertyTypeMaper { get; set; }
        public void Init()
        {
            var generalOne = new Func<ValidatorInfoItem, object, ProcessResult>((c, e) =>
            {

                var lambdaParser = new LambdaParser();
                var propName = PropertyTypeMaper?.Invoke(c.PropName);

                c.Expression = c.Expression.Replace("{value}", propName);
                var lambdaResult = (lambdaParser.Eval(c.Expression, (varName) =>
                {
                    object input = null;
                    object val = TryGetValue(varName, e);
                    if (c.TargetName == "Value")
                    {
                        if (val is IFormulatedType formulatedType)
                        {
                            input = formulatedType.GetValue();
                        }
                        else
                        {
                            input = val;
                        }
                    }

                    else if (c.TargetName == "Formula")
                    {
                        if (val is IFormulatedType formulatedType)
                        {
                            input = formulatedType.Formula;
                        }
                        else
                        {
                            throw new ArgumentException($"设置TargetName为'Formula'时，指定的对象不是包含公式的类型");

                        }
                    }
                    return input;
                })); // --> 5
                if (lambdaResult is bool)
                {
                    c.ProcessResult.IsValidated = (bool)lambdaResult;
                }
                else
                {
                    c.ProcessResult.IsValidated = false;
                    throw new ArgumentException($"普通表达式返回值类型为{lambdaResult.GetType()},应该为bool类型");
                }
                return c.ProcessResult;

            });

            var regularOne = new Func<ValidatorInfoItem, object, ProcessResult>((c, e) =>
            {


                object val = TryGetValue(c.PropName, e);

                var input = string.Empty;

                if (c.TargetName == "Value")
                {
                    var propValue = val.ToString();
                    input = propValue;

                }

                else if (c.TargetName == "Formula")
                {
                    if (val is IFormulatedType)
                    {
                        input = (val as IFormulatedType).Formula;
                    }
                    else
                    {
                        throw new ArgumentException($"设置TargetName为'Formula'时，指定的对象不是包含公式的类型");

                    }
                }

                else
                {
                    throw new ArgumentException($"未知的TargetName类型{c.TargetName}");

                }

                var pattern = c.Expression;

                var regularResult = Regex.IsMatch(input, pattern);
                c.ProcessResult.IsValidated = (bool)regularResult;

                return c.ProcessResult;

            });

            Conventions.Add("DefaultGeneral", new ValidateConvention(generalOne));
            Conventions.Add("DefaultRegular", new ValidateConvention(regularOne));
        }

        private object TryGetValue(string varName, object e)
        {
            var propName = PropertyTypeMaper?.Invoke(varName);
            PropertyInfo propertyInfo;
            if (!string.IsNullOrEmpty(propName))
            {
                propertyInfo = e.GetType().GetProperty(propName);
            }
            else
            {
                propertyInfo = e.GetType().GetProperty(varName);
            }
            if (propertyInfo != null)
            {
                var result = propertyInfo.GetValue(e);
                return result;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<ValidatorInfoItem> GetValidatorInfos()
        {
            var result = LocalDataHelper.ReadObjectLocal<ValidatorInfo>();
            return result.List;
        }

        public ValidateConvention GetConvention(string type)
        {
            ValidateConvention result;
            this.Conventions.TryGetValue(type, out result);
            return result;
        }
    }
}
