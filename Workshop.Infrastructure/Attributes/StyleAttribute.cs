using System;

namespace Workshop.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class StyleAttribute : System.Attribute
    {
        public string FontColor { get; set; }

        public string FontName { get; set; }

        public short FontSize { get; set; }

        public string BorderColor { get; set; }

        public string BackColor { get; set; }

        public StyleAttribute()
        {

        }
    }
}