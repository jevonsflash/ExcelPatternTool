using ExcelPatternTool.Contracts.NPOI.AdvancedTypes;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExcelPatternTool.Odbc
{
    public class AdvancedTypeConverter : ValueConverter
    {
        private readonly Type modelClrType;
        private readonly Type providerClrType;

        public AdvancedTypeConverter(Type modelClrType, Type providerClrType) : base(

           (IAdvancedType v) => "", (string v) => default(IAdvancedType))
        {
            this.modelClrType = modelClrType;
            this.providerClrType = providerClrType;
        }
        public override Func<object, object> ConvertToProvider => (c) => { return (c as IAdvancedType).GetValue(); };

        public override Func<object, object> ConvertFromProvider => (c) => { return null; };

        public override Type ModelClrType => modelClrType;

        public override Type ProviderClrType => providerClrType;

        public override Expression ConstructorExpression => Expression.New(GetType());
    }
}
