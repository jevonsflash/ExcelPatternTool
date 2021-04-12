using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Workshop.Model
{
    public class ProductTypeInfo : InfoBase
    {
        public ProductTypeInfo()
        {
        }
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }


        private IList<ProcedureInfo> _procedureInfos;

        public IList<ProcedureInfo> ProcedureInfos
        {
            get { return _procedureInfos; }
            set
            {
                _procedureInfos = value;
                RaisePropertyChanged(nameof(ProcedureInfos));
            }
        }


    }
}
