using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using ExcelPatternTool.Core.Excel.Core.AdvancedTypes;
using ExcelPatternTool.Core.Helper;
using ExcelPatternTool.Core.Linq.Core;
using ExcelPatternTool.Core.Linq.Models;
using ExcelPatternTool.Core.Patterns;

namespace ExcelPatternTool.Core.Validators
{
    public abstract class ValidatorProvider : IValidatorProvider
    {
        protected Dictionary<string, ValidateConvention> Conventions { get; set; }
        public ValidatorProvider()
        {
            Conventions = this.InitConventions();
        }

        virtual public Func<string, string> PropertyTypeMaper { get; set; }
        virtual public Dictionary<string, ValidateConvention> InitConventions()
        {

            var conventions = new Dictionary<string, ValidateConvention>();

            var generalOne = new Func<PatternItem, object, ProcessResult>((x, e) =>
            {

                var lambdaParser = new LambdaParser();
                var propName = PropertyTypeMaper?.Invoke(x.PropName);
                var c = x.Validation;
                if (c==null)
                {
                    return null;
                }
                if (!string.IsNullOrEmpty(propName))
                {
                    c.Expression = c.Expression.Replace("{value}", propName);
                }
                else
                {
                    c.Expression = c.Expression.Replace("{value}", x.PropName);
                }
                var lambdaResult = lambdaParser.Eval(c.Expression, (varName) =>
                {
                    object input = null;
                    object val = TryGetValue(varName, e);
                    if (c.Target ==Target.Value)
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

                    else if (c.Target == Target.Formula)
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
                }); // --> 5
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

            var regularOne = new Func<PatternItem, object, ProcessResult>((x, e) =>
            {


                object val = TryGetValue(x.PropName, e);
                var c = x.Validation;
                if (c==null)
                {
                    return null;
                }
                if (val == null)
                {
                    c.ProcessResult.Content += "(值为空)";
                    c.ProcessResult.IsValidated = false;
                    return c.ProcessResult;

                }
                var input = string.Empty;

                if (c.Target == Target.Value)
                {
                    var propValue = val.ToString();
                    input = propValue;

                }

                else if (c.Target == Target.Formula)
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
                    throw new ArgumentException($"未知的TargetName类型{c.Target}");

                }

                var pattern = c.Expression;
                if (input == null)
                {
                    c.ProcessResult.Content += "(Formula为空)";
                    c.ProcessResult.IsValidated = false;

                }
                else
                {
                    var regularResult = Regex.IsMatch(input, pattern);
                    c.ProcessResult.IsValidated = regularResult;

                }

                return c.ProcessResult;

            });

            conventions.Add("LambdaExpression", new ValidateConvention(generalOne));
            conventions.Add("RegularExpression", new ValidateConvention(regularOne));
            return conventions;
        }

        virtual public object TryGetValue(string varName, object e)
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

        abstract public IEnumerable<PatternItem> GetPatternItems();

        virtual public ValidateConvention GetConvention(string type)
        {
            ValidateConvention result;
            Conventions.TryGetValue(type, out result);
            return result;
        }



    }
}
