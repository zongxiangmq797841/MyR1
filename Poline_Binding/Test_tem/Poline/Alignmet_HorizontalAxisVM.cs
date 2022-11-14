using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Test_tem.Poline
{
    public class Alignmet_HorizontalAxisVM : Alignment_ViewModel
    {
         #region Variables
        private int horizontalDivideCount = 4 * 8;
        private string endTimeLabel;
        private string startTimeLabel;
        #endregion

        #region Events
        public event EventHandler<Test_tem.Poline.Alignment_PolyLinesVM.HistoryTimeChangedEventArgs> HistoryTimeChanged;
        #endregion

        #region Properties

        public ObservableCollection<string> HorizontalLabelList
        {
            get;
            private set;
        }

        public string HistoryEndTimeLabel
        {
            get
            {
                return endTimeLabel;
            }
            set
            {
                if (value.Substring(0, 2) == "24")
                {
                    this.OnPropertyChanged("HistoryEndTimeLabel");
                    return;
                }
                DateTime time1;
                try
                {
                    string s1 = this.CurrentDateTime.ToShortDateString() + " " + value;
                    time1 = DateTime.Parse(s1);
                    UpdateHorizontalAxis(time1, this.StartDateTime);
                    this.OnHistoryTimeChanged(time1, this.StartDateTime);
                    this.EndDateTime = time1;
                }
                catch (Exception)
                {
                    this.OnPropertyChanged("HistoryEndTimeLabel");
                }

            }
        }

        public string HistoryStartTimeLabel
        {
            get
            {
                return startTimeLabel;
            }
            set
            {
                if (value.Substring(0, 2) == "24")
                {
                    this.OnPropertyChanged("HistoryStartTimeLabel");
                    return;
                }
                DateTime time2;
                try
                {
                    string s2 = this.CurrentDateTime.ToShortDateString() + " " + value;
                    time2 = DateTime.Parse(s2);
                    UpdateHorizontalAxis(this.EndDateTime, time2);
                    this.OnHistoryTimeChanged(this.EndDateTime, time2);
                    this.StartDateTime = time2;
                }
                catch (Exception)
                {
                    this.OnPropertyChanged("HistoryStartTimeLabel");
                }
            }
        }

        public DateTime AbsoluteStartDateTime { get; set; }//AbsoluteStartDateTime may earlier than CurrentDateTime 1 day

        public DateTime CurrentDateTime { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        #endregion

        #region Constructor
        public Alignmet_HorizontalAxisVM()
        {
            this.HorizontalLabelList = new ObservableCollection<string>();
            this.AbsoluteStartDateTime = DateTime.Now;
            this.SetTimeToZero();
        }
        #endregion

        #region PrivateFunctions
        public void UpdateHorizontalAxis(DateTime endTime, DateTime startTime)
        {
            if ((endTime - startTime).TotalSeconds <= 0)
            {
                throw new Exception();
            }
            endTimeLabel = endTime.ToString("HH:mm:ss");
            startTimeLabel = startTime.ToString("HH:mm:ss");
            UpdateHorizontalLabelList(endTime, startTime);
            this.OnPropertyChanged("HistoryEndTimeLabel");
            this.OnPropertyChanged("HistoryStartTimeLabel");
            this.OnPropertyChanged("HorizontalLabelList");
        }

        private void UpdateHorizontalLabelList(DateTime endTime, DateTime startTime)
        {
            ObservableCollection<string> _tmp = new ObservableCollection<string>();
            double timeInterval = (endTime - startTime).TotalSeconds / (horizontalDivideCount + 1);
            for (int i = 0; i < horizontalDivideCount/ 2; i++)
            {
                TimeSpan span = new TimeSpan(0, 0, Convert.ToInt32(2 * (i + 1) * timeInterval));
                DateTime time = startTime + span;
                _tmp.Add(time.ToString("HH:mm:ss"));
            }
            this.HorizontalLabelList = _tmp;
        }

        private void OnHistoryTimeChanged(DateTime endTime, DateTime startTime)
        {
            if (this.HistoryTimeChanged != null)
            {
                this.HistoryTimeChanged(null, new Test_tem.Poline.Alignment_PolyLinesVM.HistoryTimeChangedEventArgs(endTime, startTime));
            }
        }

        #endregion

        #region PublicFunctions

        public void SetTimeToZero()
        {
            this.endTimeLabel = "00:00:00";
            this.startTimeLabel = "00:00:00";
            DateTime zeroDateTime;
            DateTime.TryParse(DateTime.Now.ToShortDateString() + this.endTimeLabel, out zeroDateTime);
            UpdateHorizontalLabelList(zeroDateTime, zeroDateTime);
            this.OnPropertyChanged("HistoryStartTimeLabel");
            this.OnPropertyChanged("HistoryEndTimeLabel");
            this.OnPropertyChanged("HorizontalLabelList");
        }

        #endregion
    }
}
