namespace Workshop.Infrastructure.Core
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
            this.Value = value;
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
