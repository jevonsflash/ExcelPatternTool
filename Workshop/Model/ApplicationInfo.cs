using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Workshop.Model
{
    public class ApplicationInfo
    {
        public ApplicationInfo()
        {

        }
        private double _version;

        public double Version
        {
            get { return _version; }
            set { _version = value; }
        }

        public DateTime RealeaseDate { get; set; }
    }
}
