using System;
using System.Collections.Generic;
using System.Text;
using ExcelPatternTool.Core.Helper;
using ExcelPatternTool.Core.Patterns;
using Newtonsoft.Json;

namespace ExcelPatternTool.Core.Validators.Implements
{
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
    }
}
