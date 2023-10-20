using ExcelPatternTool.Contracts;
using ExcelPatternTool.Core.EntityProxy;
using ExcelPatternTool.Validation;
using ExcelPatternTool.Validators.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelPatternTool
{
    public class ValidateProcessor
    {
        private static Validator validator;

        public static void Init()
        {
            EntityProxyContainer.Current.Init(CliProcessor.patternFilePath);
            ProcessResultList=new List<ProcessResultDto>();
            validator = new Validator();
            validator.SetValidatorProvider(EntityProxyContainer.Current.EntityType, new CliValidatorProvider());

        }
        public static List<ProcessResultDto> ProcessResultList { get; set; }

        public static void GetDataAction(List<IExcelEntity> entities)
        {
            ProcessResultList.Clear();
            foreach (var item in entities)
            {

                var row = (item as IExcelEntity).RowNumber + 1;
                var id = ProcessResultList.Count + 1;
                var level = 1;


                var validateResult = validator.Validate(item);
                var result = validateResult.Where(c => c.IsValidated == false)
                    .Select(c => new ProcessResultDto()
                    {
                        Id = id,
                        Row = row,
                        Column = c.Column,
                        Level = level,
                        Content = c.Content,
                        KeyName = c.KeyName,
                    });


                foreach (var processResultDto in result)
                {
                    ProcessResultList.Add(processResultDto);

                }


            }
            var currentCount = ProcessResultList.Count();

        }

    }
}
