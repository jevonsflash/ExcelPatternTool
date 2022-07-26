using Workshop.Core.Excel.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Workshop.Core.Excel.Attributes;

namespace Workshop.Core.Excel.Core.AdvancedTypes
{
    public class StyledType<T> : IStyledType
    {
        public T Value { get; set; }
        public StyleMetadata StyleMetadata { get; set; }

        public StyledType()
        {

        }

        public StyledType(T value)
        {
            Value = value;
        }
        public override string ToString()
        {
            return Value.ToString();
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
