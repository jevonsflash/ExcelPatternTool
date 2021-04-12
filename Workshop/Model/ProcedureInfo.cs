using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;

namespace Workshop.Model
{
    public class ProcedureInfo : ViewModelBase
    {
        public ProcedureInfo()
        {
            this.PropertyChanged += ProcedureInfo_PropertyChanged;
        }

        private void ProcedureInfo_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Duration))
            {
                this.Price = GetStandardPrice(this.Duration);
            }
        }

        private double _duration;

        public double Duration
        {
            get { return _duration; }
            set
            {
                _duration = value;
                RaisePropertyChanged(nameof(Duration));

            }
        }

        private double _price;

        public double Price
        {
            get { return _price; }
            set
            {
                _price = value;
                RaisePropertyChanged(nameof(Price));
            }
        }

        private string _gourp;

        public string Group
        {
            get { return _gourp; }
            set
            {
                _gourp = value;
                RaisePropertyChanged(nameof(Group));
            }
        }


        private ProcedureTypeInfo _procedureTypeInfo;

        public ProcedureTypeInfo ProcedureTypeInfo
        {
            get { return _procedureTypeInfo; }
            set
            {
                _procedureTypeInfo = value;
                RaisePropertyChanged(nameof(ProcedureTypeInfo));

            }
        }

        public DateTime CreateTime { get; set; }

        private double GetStandardPrice(double duration)
        {
            return Math.Round((double)duration * 19 / 60, 2);
        }

    }
}
