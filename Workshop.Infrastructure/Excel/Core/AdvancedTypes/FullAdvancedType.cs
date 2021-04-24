using System;
using System.Collections.Generic;
using System.Text;
using Workshop.Infrastructure.Models;

namespace Workshop.Infrastructure.Core
{
    public class FullAdvancedType<T> : IFullAdvancedType
    {
        public T Value { get; set; }
        public string Comment { get; set; }
        public string Formula { get; set; }
        public StyleMetadata StyleMetadata { get; set; }
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
