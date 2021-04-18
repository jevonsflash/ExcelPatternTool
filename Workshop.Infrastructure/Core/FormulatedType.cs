namespace Workshop.Infrastructure.Core
{
    public interface IFormulatedType
    {
        string Formula { get; set; }
        object GetValue();
    }

    public class FormulatedType<T> : IFormulatedType
    {
        public T Value { get; set; }
        public string Formula { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }

        public object GetValue()
        {
            return Value;
        }
    }


    public class CommentedType<T>
    {
        public T Value { get; set; }
        public string Label { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }
        public object GetValue()
        {
            return Value;
        }

    }
}
