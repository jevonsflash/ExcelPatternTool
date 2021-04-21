using Microsoft.VisualStudio.TestTools.UnitTesting;
using Workshop.Infrastructure.Linq.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop.Infrastructure.Linq.Core.Tests
{
    [TestClass()]
    public class LambdaParserTests
    {
        [TestMethod()]
        public void LambdaParserTest()
        {

            var lambdaParser = new LambdaParser();

            var varContext = new Dictionary<string, object>();
            varContext["pi"] = 3.14M;
            varContext["one"] = 1M;
            varContext["two"] = 2M;
            varContext["test"] = "test";
            var o1 = (lambdaParser.Eval("pi>one && 0<one ? (1+8)/3+1*two : 0", varContext)); // --> 5
            var o2 = (lambdaParser.Eval("test.ToUpper()", varContext)); // --> TEST
            var o3 = (lambdaParser.Eval("pi>1", varContext)); // --> 5

            Console.WriteLine(o1);
            Console.WriteLine(o2);
        }


    }
}