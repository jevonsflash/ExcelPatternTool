using System;
using System.Collections.Generic;
using System.Text;

namespace Workshop.Infrastructure.ExcelHandler
{
    public interface IFormulatedType
    {
        string Formula { get; set; }
    }

    public class FormulatedType<T> : IFormulatedType
    {
        public T Value { get; set; }
        public string Formula { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }


    public class LabeledType<T>
    {
        public T Value { get; set; }
        public string Label { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
