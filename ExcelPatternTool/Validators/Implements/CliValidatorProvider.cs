using System;
using System.Collections.Generic;
using System.Text;
using ExcelPatternTool.Contracts.Validations;
using ExcelPatternTool.Core.Patterns;
using ExcelPatternTool.Validation;
using Newtonsoft.Json;
using DirFileHelper = ExcelPatternTool.Common.Helper.DirFileHelper;

namespace ExcelPatternTool.Validators.Implements
{
    /// <summary>
    /// 可实现一个自定义的ValidatorProvider
    /// </summary>
    public class CliValidatorProvider : ValidatorProvider
    {
        public override Dictionary<string, IValidation> GetValidationContainers(Type entityType)
        {
            if (DirFileHelper.IsExistFile(CliProcessor.patternFilePath))
            {
                var serializedstr = DirFileHelper.ReadFile(CliProcessor.patternFilePath);
                var _pattern = JsonConvert.DeserializeObject<Pattern>(serializedstr);
                if (_pattern != null)
                {
                    return new Dictionary<string, IValidation>(
                        _pattern.Patterns.Select(c => new KeyValuePair<string, IValidation>(c.PropName, c.Validation)));

                }
            }

            return default;
        }

        public override Dictionary<string, IValidateConvention> InitConventions()
        {

            var defaultConventions = base.InitConventions();
            //x 为当前列轮询的字段规则PatternItem对象，
            //e 为当前行轮询的Entity对象
            //返回ProcessResult作为校验结果
            defaultConventions.Add("MyExpression", new ValidateConvention((key, c, e) =>
            {
                //再此编写自定义校验功能
                //可用 key（或PropertyTypeMapper(key)） 获取当前列轮询的字段（Excel表头）名称
                //返回ProcessResult作为校验结果,IsValidated设置为true表示校验通过
                c.ProcessResult.IsValidated = true;
                return c.ProcessResult;
            }));

            return defaultConventions;
        }
    }
}
