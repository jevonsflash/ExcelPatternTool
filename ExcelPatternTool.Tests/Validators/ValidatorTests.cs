using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ExcelPatternTool.Tests.Entites;
using ExcelPatternTool.Core.NPOI;
using ExcelPatternTool.Contracts;
using ExcelPatternTool.Contracts.Models;
using ExcelPatternTool.Validation;
using ExcelPatternTool.Validation.Validators.Implements;

namespace ExcelPatternTool.Tests.Validators
{
    [TestClass()]
    public class ValidatorTests
    {
        [TestMethod()]
        public void ValidateTest()
        {
            ProcessResultList = new List<ProcessResult>();

            Importer import = new Importer();
            var filePath = @"D:\test.xlsx";
            var data1 = new byte[0];

            data1 = File.ReadAllBytes(filePath);
            import.LoadXlsx(data1);
            var importOption = new ImportOption<EmployeeEntity>(0, 2);
            importOption.SheetName = "全职";
            Employees = import.Process<EmployeeEntity>(importOption).ToList();

            var validator = new Validator();
            validator.SetValidatorProvider<EmployeeEntity>(new DefaultValidatorProvider());
            foreach (var item in Employees)
            {

                var row = Employees.IndexOf(item);
                var id = ProcessResultList.Count + 1;
                var level = 1;


                var validateResult = validator.Validate(item);
                var result = validateResult.Where(c => c.IsValidated == false)
                    .Select(c => new ProcessResult()
                    {
                        Id = id,
                        Row = row,
                        Level = level,
                        Content = c.Content,
                        KeyName = c.KeyName,
                    });


                foreach (var processResultDto in result)
                {
                    ProcessResultList.Add(processResultDto);

                }

            }

            Assert.IsNotNull(ProcessResultList);

        }
        public List<ProcessResult> ProcessResultList { get; set; }

        public List<EmployeeEntity> Employees { get; set; }


    }
}