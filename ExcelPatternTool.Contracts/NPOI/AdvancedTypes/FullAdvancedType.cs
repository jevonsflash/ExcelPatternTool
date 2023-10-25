using System;
using ExcelPatternTool.Contracts.Models;

namespace ExcelPatternTool.Contracts.NPOI.AdvancedTypes
{
    public class FullAdvancedType<T> : ICommentedType, IFormulatedType, IStyledType
    {

        public T Value { get; set; }
        public string Comment { get; set; }
        public string Formula { get; set; }
        public StyleMetadata StyleMetadata { get; set; }
        public override string ToString()
        {
            return Value.ToString();
        }

        public FullAdvancedType()
        {

        }
        public FullAdvancedType(T value)
        {
            Value = value;
        }

        public object GetValue()
        {
            return Value;
        }
        public void SetValue(object value)
        {
            Value = (T)value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Value.Equals(obj);
        }
    }
}
