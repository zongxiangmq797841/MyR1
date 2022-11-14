using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test_tem.Poline
{
    public class Alignment_VerticalAxisVM : Alignment_ViewModel
    {
         #region Variables
        private int verticalDivideCount = 24;
        private double YmaxValue;
        private double YminValue;
        #endregion

        #region Events
        public event EventHandler<Test_tem.Poline.Alignment_PolyLinesVM.YValueChangedEventArgs> YValueChanged;
        #endregion

        #region Constructor
        public Alignment_VerticalAxisVM()
        {
            this.VerticalLabelList = new double[verticalDivideCount];
            this.VerticalStringList = new string[verticalDivideCount];
            //this.Unit = PETGantrySystemStatus.Common.ConfigParameter.TemperatureUnit;
            UpdateVerticalLabelList(0f, 0f);
        }
        #endregion

        #region Properties
        public double[] VerticalLabelList { get; private set; }

        public string MaxValueLabel
        {
            get
            {
                string tmp;
                tmp =YmaxValue.ToString();
                //if (Unit == PETGantrySystemStatus.Common.ConfigParameter.CountRateUnit)
                //{
                //    tmp = string.Format("{0:F1}", YmaxValue / 1000);
                //}
                //else
                //{
                //    tmp = string.Format("{0:F1}", YmaxValue);
                //}
                return tmp;
            }
            set
            {
                try
                {
                    double tmp = YmaxValue;
                    YmaxValue = Convert.ToSingle(value);
                    //if (Unit == PETGantrySystemStatus.Common.ConfigParameter.CountRateUnit)
                    //{
                    //    YmaxValue = Convert.ToSingle(value) * 1000;
                    //}
                    //else
                    //{
                    //    YmaxValue = Convert.ToSingle(value);
                    //}
                    if (YmaxValue < YminValue)
                    {
                        YmaxValue = tmp;
                        return;
                    }
                    this.UpdateVerticalLabelList(YmaxValue, YminValue);
                    this.OnYValueChanged(YmaxValue, YminValue);
                }
                catch (Exception)
                {
                    this.OnPropertyChanged("MaxValueLabel");
                }
            }
        }

        public double MaxValue
        {
            get
            {
                return this.YmaxValue;
            }
        }

        public string MinValueLabel
        {
            get
            {
                string tmp;
                tmp = string.Format("{0:F1}", YminValue / 1000);
                //if (Unit == PETGantrySystemStatus.Common.ConfigParameter.CountRateUnit)
                //{
                //    tmp = string.Format("{0:F1}", YminValue / 1000);
                //}
                //else
                //{
                //    tmp = string.Format("{0:F1}", YminValue);
                //}
                return tmp;
            }
            set
            {
                try
                {
                    double tmp = YminValue;
                    YminValue = Convert.ToSingle(value) * 1000;
                    //if (Unit == PETGantrySystemStatus.Common.ConfigParameter.CountRateUnit)
                    //{
                    //    YminValue = Convert.ToSingle(value) * 1000;
                    //}
                    //else
                    //{
                    //    YminValue = Convert.ToSingle(value);
                    //}
                    if (YminValue > YmaxValue)
                    {
                        YminValue = tmp;
                        return;
                    }
                    this.UpdateVerticalLabelList(YmaxValue, YminValue);
                    this.OnYValueChanged(YmaxValue, YminValue);
                }
                catch (Exception)
                {
                    this.OnPropertyChanged("MinValueLabel");
                }
            }
        }

        public double MinValue
        {
            get
            {
                return this.YminValue;
            }
        }

        public string Unit { get; private set; }

        public string[] VerticalStringList { get; private set; }
        #endregion

        #region Functions
        public void UpdateVerticalLabelList(double maxValue, double minValue)
        {
            string[] tmp = new string[verticalDivideCount];
            this.YmaxValue = maxValue;
            this.YminValue = minValue;
            double valueInterval = (maxValue - minValue) / (verticalDivideCount+1);
            for (int i = 0; i < verticalDivideCount; i++)
            {
                this.VerticalLabelList[verticalDivideCount-i-1] = minValue + (i + 1) * valueInterval;
            }
            for (int i = 0; i < verticalDivideCount; i++)
            {
                tmp[i] = string.Format("{0:F1}", this.VerticalLabelList[i]);
            }
            
            this.VerticalStringList = tmp;
            this.OnPropertyChanged("VerticalStringList");
            this.OnPropertyChanged("MinValueLabel");
            this.OnPropertyChanged("MaxValueLabel");
        }

        private void OnYValueChanged(double maxYValue, double minYValue)
        {
            if (this.YValueChanged != null)
            {
                this.YValueChanged(null, new Test_tem.Poline.Alignment_PolyLinesVM.YValueChangedEventArgs(maxYValue, minYValue));
            }
        }

        public void UpdateUnitProperty(string unit)
        {
            this.Unit = unit;
            this.OnPropertyChanged("Unit");
        }
        #endregion
    }
}
