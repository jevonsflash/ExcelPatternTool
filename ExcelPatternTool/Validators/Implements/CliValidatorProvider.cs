using System;
using System.Collections.Generic;
using System.Text;
using ExcelPatternTool.Core.Helper;
using ExcelPatternTool.Core.Patterns;
using Newtonsoft.Json;

namespace ExcelPatternTool.Core.Validators.Implements
{
    /// <summary>
    /// 可实现一个自定义的ValidatorProvider
    /// </summary>
    public class CliValidatorProvider : ValidatorProvider
    {
        public override IEnumerable<PatternItem> GetPatternItems()
        {
            if (DirFileHelper.IsExistFile(CliProcessor.patternFilePath))
            {
                var serializedstr = DirFileHelper.ReadFile(CliProcessor.patternFilePath);
                var _pattern = JsonConvert.DeserializeObject<Pattern>(serializedstr);

                return _pattern.Patterns;
            }

            return default;
        }

        public override Dictionary<string, ValidateConvention> InitConventions()
        {

            var defaultConventions = base.InitConventions();
            //x 为当前列轮询的字段规则PatternItem对象，
            //e 为当前行轮询的Entity对象
            //返回ProcessResult作为校验结果
            defaultConventions.Add("MyExpression", new ValidateConvention((x, e) =>
            {
                //再此编写自定义校验功能
                //可用 x.PropName（或PropertyTypeMaper(x.PropName)） 获取当前列轮询的字段（Excel表头）名称
                //返回ProcessResult作为校验结果,IsValidated设置为true表示校验通过
                x.ValidationPattern.ProcessResult.IsValidated = true;
                return x.ValidationPattern.ProcessResult;
            }));

            return defaultConventions;
        }
    }
}
