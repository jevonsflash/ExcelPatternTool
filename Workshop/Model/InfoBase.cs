using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Workshop.Model
{
    public class InfoBase : ViewModelBase
    {
        public string Id { get; set; }
        private CategoryInfo _category;

        public CategoryInfo Category
        {
            get { return _category; }
            set
            {
                _category = value;
                RaisePropertyChanged(nameof(Category));
            }
        }

        public DateTime CreateTime { get; set; }
    }
}
