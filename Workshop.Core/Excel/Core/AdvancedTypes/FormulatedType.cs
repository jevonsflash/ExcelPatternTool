using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Workshop.Core.Domains;
using Workshop.Core.Excel.Attributes;

namespace Workshop.Core.Excel.Core.AdvancedTypes
{

    public class FormulatedType<T> : IFormulatedType, IEntity<Guid>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Exportable(ignore: true)]
        public Guid Id { get; set; }
        public T Value { get; set; }
        public string Formula { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }

        public FormulatedType()
        {

        }

        public FormulatedType(T value)
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
