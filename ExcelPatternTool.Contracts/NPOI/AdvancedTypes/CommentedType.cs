using System;

namespace ExcelPatternTool.Contracts.NPOI.AdvancedTypes
{
    public class CommentedType<T> : ICommentedType
    {

        public T Value { get; set; }
        public string Comment { get; set; }

        public CommentedType()
        {

        }
        public CommentedType(T value)
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
