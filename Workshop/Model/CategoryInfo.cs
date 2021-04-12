using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Workshop.Model
{
    public class CategoryInfo : ViewModelBase
    {
        public string Id { get; set; }
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _code;

        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }


        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }



        public DateTime CreateTime { get; set; }
    }
}
