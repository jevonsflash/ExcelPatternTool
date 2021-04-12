using GalaSoft.MvvmLight;
using Workshop.Model;

namespace Workshop.Model
{
    public class WorkValidateInfo : ViewModelBase
    {

        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private JobStatusType _jobStatus;

        public JobStatusType JobStatus
        {
            get { return _jobStatus; }
            set
            {
                _jobStatus = value;
                RaisePropertyChanged(nameof(JobStatus));
            }
        }


    }
}
