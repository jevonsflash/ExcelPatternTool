using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using ExcelPatternTool.Common.Linq;
using ExcelPatternTool.Contracts;
using ExcelPatternTool.Contracts.Models;
using ExcelPatternTool.Contracts.NPOI.AdvancedTypes;
using ExcelPatternTool.Contracts.Patterns;
using ExcelPatternTool.Core.StyleMapping;
using ExcelPatternTool.Tests.Entites;

namespace ExcelPatternTool.Core.StyleMapping
{
    public class EmployeeHealthEntityStyleMapperProvider : StyleMapperProvider
    {
        public override Dictionary<string, StyleMapping> GetStyleMappingContainers()
        {
            var result = new Dictionary<string, StyleMapping>
            {
                {
                    "体温",
                    new StyleMapping()
                    {
                        Target = Target.Value,
                        Convention = "LambdaExpression",
                        Expression = "{value}>=36.5",
                        MappingConfig = new Dictionary<object, StyleMetadata>
                        {
                            { true, new StyleMetadata(){  FontColor="Red"} } ,
                            { false, new StyleMetadata(){  FontColor="Black"} }
                        }
                    }
                },
                 {
                    "收缩压",
                    new StyleMapping()
                    {
                        Target = Target.Value,
                        Convention = "BloodPressureResultExpression",
                        MappingConfig = new Dictionary<object, StyleMetadata>
                        {
                            { "偏低异常", new StyleMetadata(){  FontColor="Orange"} } ,
                            { "偏高异常", new StyleMetadata(){  FontColor="Red"} },
                            { "正常", new StyleMetadata(){  FontColor="Black"} }
                        }
                    }

                },
                 {
                    "舒张压",
                    new StyleMapping()
                    {
                        Target = Target.Value,
                        Convention = "BloodPressureResultExpression",
                        MappingConfig = new Dictionary<object, StyleMetadata>
                        {
                            { "偏低异常", new StyleMetadata(){  FontColor="Orange"} } ,
                            { "偏高异常", new StyleMetadata(){  FontColor="Red"} },
                            { "正常", new StyleMetadata(){  FontColor="Black"} }
                        }
                    }

                },


            };
            return result;
        }

        public override Dictionary<string, StyleConvention> InitConventions()
        {

            var baseOne = base.InitConventions();
            baseOne.Add("BloodPressureResultExpression", new StyleConvention(new Func<string, StyleMapping, object, StyleMetadata>((key, c, e) =>
            {
                StyleMetadata result = null;
                var lambdaParser = new LambdaParser();
                if (c == null)
                {
                    return null;
                }
                var val = double.Parse((string)TryGetValue(key, e));
                if (key == nameof(EmployeeHealthEntity.BloodPressure2))
                {
                    if (val > 140)
                    {
                        result = c.MappingConfig["偏高异常"];

                    }
                    else if (val < 90)
                    {
                        result = c.MappingConfig["偏低异常"];

                    }
                    else
                    {
                        result = c.MappingConfig["正常"];
                    }
                }

                else if (key == nameof(EmployeeHealthEntity.BloodPressure1))
                {
                    if (val > 90)
                    {
                        result = c.MappingConfig["偏高异常"];

                    }
                    else if (val < 60)
                    {
                        result = c.MappingConfig["偏低异常"];

                    }
                    else
                    {
                        result = c.MappingConfig["正常"];
                    }
                }



                return result;

            })));
            return baseOne;

        }
    }
}
