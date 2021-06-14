using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Workshop.Core.DataBase;
using Workshop.Core.Domains;
using Workshop.Helper;

namespace Workshop.ViewModel
{
    public class ToolsPageViewModel : ViewModelBase
    {
        private readonly WorkshopDbContext _dbContext;
        public ToolsPageViewModel(WorkshopDbContext dbContext)
        {
            this.GoPageCommand = new RelayCommand<string>(GoPageAction);
            this._dbContext = dbContext;
            InitData();
        }
        private void InitData()
        {
            var task = InvokeHelper.InvokeOnUi<IList<Log>>(null, () =>
            {
                var result = this._dbContext.Log.Where(c => true).ToList();
                return result;


            }, (t) =>
            {
                LogInfos = new List<Log>(t);

            });


        }

        private List<Log> _logInfos;

        public List<Log> LogInfos
        {
            get { return _logInfos; }
            set
            {
                _logInfos = value;
                RaisePropertyChanged();
            }
        }


        private void GoPageAction(string obj)
        {
        }

        public RelayCommand<string> GoPageCommand { get; set; }

    }
}
