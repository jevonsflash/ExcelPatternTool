using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Workshop.Core.Entites;
using Workshop.Infrastructure.Linq.Core;
using Workshop.Infrastructure.Models;

namespace Workshop.Core.Validators
{
    public class ValidatorProvider
    {
        public Dictionary<string, Func<ValidatorInfo, EmpoyeeEntity, ProcessResult>> Conventions { get; set; }
        public ValidatorProvider()
        {
            Init();
        }

        public void Init()
        {
            var generalOne = new Func<ValidatorInfo, EmpoyeeEntity, ProcessResult>((c, e) =>
            {


                var lambdaParser = new LambdaParser();
                var lambdaResult = (lambdaParser.Eval(c.Expression, (varName) =>
                {
                    object val = TryGetValue(varName, e);
                    return val;
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

            var regularOne = new Func<ValidatorInfo, EmpoyeeEntity, ProcessResult>((c, e) =>
            {


                object val = TryGetValue(c.PropName, e);
                var propValue = val.ToString();
                var pattern = c.Expression;

                var regularResult = Regex.IsMatch(propValue, pattern);
                c.ProcessResult.IsValidated = (bool)regularResult;

                return c.ProcessResult;

            });

            Conventions.Add("DefaultGeneral", generalOne);
            Conventions.Add("DefaultRegular", regularOne);
        }

        private object TryGetValue(string varName, EmpoyeeEntity e)
        {
            var propertyInfo = e.GetType().GetProperty(varName);


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

        public IEnumerable<ValidatorInfo> GetValidatorInfos()
        {
            var result=new List<ValidatorInfo>();
            result.Add(new ValidatorInfo()
            {
                Description = "需要满足表达式",
                EntityType = typeof(EmpoyeeEntity),
                Expression = "",
                Convention = "DefaultRegular",
                PropName = "应发合计"

            });
            return result;
        }

        public Func<ValidatorInfo, EmpoyeeEntity, ProcessResult> GetConvention(string type)
        {
            Func<ValidatorInfo, EmpoyeeEntity, ProcessResult> result;
            this.Conventions.TryGetValue(type, out result);
            return result;
        }
    }
}
