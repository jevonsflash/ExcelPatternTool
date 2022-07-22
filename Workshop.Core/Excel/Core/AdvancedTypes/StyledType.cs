using Workshop.Core.Excel.Models;

namespace Workshop.Core.Excel.Core.AdvancedTypes
{
    public class StyledType<T> : IStyledType
    {
        public T Value { get; set; }
        public string Comment { get; set; }
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
