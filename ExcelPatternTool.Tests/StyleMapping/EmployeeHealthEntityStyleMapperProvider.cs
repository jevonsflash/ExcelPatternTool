using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using ExcelPatternTool.Contracts;
using ExcelPatternTool.Contracts.Models;
using ExcelPatternTool.Contracts.NPOI.AdvancedTypes;
using ExcelPatternTool.Contracts.Patterns;
using ExcelPatternTool.Validation.Linq;

namespace ExcelPatternTool.Core.StyleMapping
{
    public class EmployeeHealthEntityStyleMapperProvider : StyleMapperProvider
    {
        public override Dictionary<string, StyleMapping> GetStyleMappingContainers()
        {
            var result = new Dictionary<string, StyleMapping>
            {
                {
                    "收缩压",
                    new StyleMapping()
                    {
                        Target = Target.Value,
                        Convention = Convention.LambdaExpression,
                        Expression = "{value}>=130||{value}<40",
                        MappingConfig = new Dictionary<object, StyleMetadata>
                        {
                            { true, new StyleMetadata(){  FontColor="Red",  BackColor="#FFFFFE"} } ,
                            { false, new StyleMetadata(){  FontColor="Black"} }
                        }
                    }
                },
                {
                    "舒张压",
                    new StyleMapping()
                    {
                        Target = Target.Value,
                        Convention = Convention.LambdaExpression,
                        Expression = "{value}>=80 ",
                        MappingConfig = new Dictionary<object, StyleMetadata>
                        {
                            { true, new StyleMetadata(){  FontColor="Green",  BackColor="#FFFFFE"} } ,
                            { false, new StyleMetadata(){  FontColor="Black"} }
                        }
                    }
                }
            };
            return result;
        }

    }
}
