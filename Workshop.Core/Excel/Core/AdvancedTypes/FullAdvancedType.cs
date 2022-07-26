using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Workshop.Core.Domains;
using Workshop.Core.Excel.Attributes;
using Workshop.Core.Excel.Models;

namespace Workshop.Core.Excel.Core.AdvancedTypes
{
    public class FullAdvancedType<T> : IFullAdvancedType, IEntity<Guid>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Exportable(ignore: true)]
        public Guid Id { get; set; }
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
