using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Windows;

namespace Test_tem.Poline
{
    public class Alignment_PolyLinesVM : Alignment_ViewModel
    {
        
        public Alignment_Pointline PolyLines { get; private set; }
        public Alignmet_HorizontalAxisVM HorizontalAxis { get; private set; }
        public Alignment_VerticalAxisVM VerticalAxis { get; private set; }
        public DateTime StartDateTime { get; private set; }
        public TimeSpan TimeInterval { get; private set; }
        public double YMaxValue { get; private set; }
        public double YMinValue { get; private set; }


        public class HistoryTimeChangedEventArgs : EventArgs
        {
            public DateTime EndTime { get; set; }

            public DateTime StartTime { get; set; }

            public HistoryTimeChangedEventArgs(DateTime endTime, DateTime startTime)
            {
                this.EndTime = endTime;
                this.StartTime = startTime;
            }
        }
        public class YValueChangedEventArgs : EventArgs
        {
            public double YMaxValue { get; set; }

            public double YMinValue { get; set; }

            public YValueChangedEventArgs(double maxYValue, double minYValue)
            {
                this.YMaxValue = maxYValue;
                this.YMinValue = minYValue;
            }
        }
        public Alignment_PolyLinesVM()
        {
            this.HorizontalAxis = new Alignmet_HorizontalAxisVM();
            this.HorizontalAxis.HistoryTimeChanged += new EventHandler<HistoryTimeChangedEventArgs>(HorizontalAxis_HistoryTimeChanged);
            this.VerticalAxis = new Alignment_VerticalAxisVM();
            this.VerticalAxis.YValueChanged += new EventHandler<YValueChangedEventArgs>(VerticalAxis_YValueChanged);
            this.PolyLines = new Alignment_Pointline();
            
        }
       
        private Point CreatePoint(double value, DateTime time)
        {
            TimeSpan span = time - this.StartDateTime;
            double pointY = GetPointY(value);
            double pointX = GetPointX(span);
            int x = (int)pointX;
    
            return new Point(pointX, pointY);
        }
        private double GetPointY(double value)
        {
            double length = 0f;
            length = 400 * (value - YMaxValue) / (YMinValue - YMaxValue);
            return length;
        }

        private double GetPointX(TimeSpan span)
        {
            double seconds = span.TotalSeconds;
            double width = 0f;
            if (this.TimeInterval.TotalSeconds == 0f)
            {
                return 0;
            }
            width = (seconds / (this.TimeInterval.TotalSeconds)) * 500;
            return width;
        }
        private void HorizontalAxis_HistoryTimeChanged(object sender, HistoryTimeChangedEventArgs e)
        {
            this.HorizontalAxis.UpdateHorizontalAxis(e.EndTime, e.StartTime);
            this.StartDateTime = e.StartTime;
            this.TimeInterval = e.EndTime - e.StartTime;
         
        }
        private void VerticalAxis_YValueChanged(object sender, YValueChangedEventArgs e)
        {
            this.VerticalAxis.UpdateVerticalLabelList(e.YMaxValue, e.YMinValue);
            this.YMaxValue = e.YMaxValue;
            this.YMinValue = e.YMinValue;
        }
    }
    public class PolyLineViewModel : Alignment_ViewModel
    {
        #region Variables
        private PointCollection _polyLineCollection;
        
        private bool _isSelected = true;
        private float _currentValue = 0f;
        #endregion

       

        public PointCollection PolyLineCollection
        {
            get { return _polyLineCollection; }
            set
            {
                _polyLineCollection = value;
                this.OnPropertyChanged("PolyLineCollection");
            }
        }
        public Visibility Visibility
        {
            get
            {
                if (IsSelected)
                    return System.Windows.Visibility.Visible;
                else
                    return System.Windows.Visibility.Hidden;
            }
        }

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                this.OnPropertyChanged("Visibility");
            }
        }

        public SolidColorBrush Brush { get; private set; }

        public float CurrentValue
        {
            get
            {
                return this._currentValue;
            }
            set
            {
                this._currentValue = value;
                this.OnPropertyChanged("CurrentValue");
            }
        }

        public PolyLineViewModel(SolidColorBrush brush)
        {
            this.Brush = brush;
        }
    }
}
