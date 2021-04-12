using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Workshop.Model
{
    public class ProcedureTypeInfo : InfoBase
    {
        public ProcedureTypeInfo()
        {
        }
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));

            }
        }
        private string _description;

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged(nameof(Description));

            }
        }

        private string _func;

        public string Func
        {
            get { return _func; }
            set
            {
                _func = value;
                RaisePropertyChanged(nameof(Func));
            }
        }


    }
}
