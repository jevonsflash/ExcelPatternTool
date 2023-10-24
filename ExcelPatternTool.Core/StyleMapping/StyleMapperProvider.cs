using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using ExcelPatternTool.Common.Linq;
using ExcelPatternTool.Contracts;
using ExcelPatternTool.Contracts.Models;
using ExcelPatternTool.Contracts.NPOI.AdvancedTypes;
using ExcelPatternTool.Contracts.Patterns;

namespace ExcelPatternTool.Core.StyleMapping
{
    public abstract class StyleMapperProvider
    {
        protected Dictionary<string, StyleConvention> Conventions { get; set; }
        public StyleMapperProvider()
        {
            Conventions = InitConventions();
        }

        virtual public Func<string, string> PropertyTypeMapper { get; set; }
        virtual public Dictionary<string, StyleConvention> InitConventions()
        {

            var conventions = new Dictionary<string, StyleConvention>();

            var generalOne = new Func<string, StyleMapping, object, StyleMetadata>((key, c, e) =>
            {
                StyleMetadata result;
                var lambdaParser = new LambdaParser();
                var propName = PropertyTypeMapper?.Invoke(key);
                if (c == null)
                {
                    return null;
                }
                if (!string.IsNullOrEmpty(propName))
                {
                    c.Expression = c.Expression.Replace("{value}", propName);
                }
                else
                {
                    c.Expression = c.Expression.Replace("{value}", key);
                }
                var lambdaResult = lambdaParser.Eval(c.Expression, (varName) =>
                {
                    object input = null;
                    object val = TryGetValue(varName, e);
                    if (c.Target == Target.Value)
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
                result = c.MappingConfig[lambdaResult];
                return result;

            });

            var regularOne = new Func<string, StyleMapping, object, StyleMetadata>((key, c, e) =>
            {

                StyleMetadata result = null;

                object val = TryGetValue(key, e);
                if (c == null)
                {
                    return null;
                }
                if (val == null)
                {
                    return result;

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
                if (input != null)
                {
                    var regularResult = Regex.IsMatch(input, pattern);
                    result = c.MappingConfig[regularResult];
                }

                return result;

            });

            conventions.Add("LambdaExpression", new StyleConvention(generalOne));
            conventions.Add("RegularExpression", new StyleConvention(regularOne));
            return conventions;
        }

        virtual public object TryGetValue(string varName, object e)
        {
            var propName = PropertyTypeMapper?.Invoke(varName);
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

        virtual public Dictionary<string, StyleMapping> GetStyleMappingContainers()
        {
            return new Dictionary<string, StyleMapping>();
        }

        virtual public StyleConvention GetConvention(string type)
        {
            StyleConvention result;
            Conventions.TryGetValue(type, out result);
            return result;
        }



    }
}
