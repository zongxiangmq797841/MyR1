using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Test_tem
{
    public class Class1 : INotifyPropertyChanged
    {
        private string _isCTResetEnable = "false";
        public string IsCTResetEnable
        {
            get { return _isCTResetEnable; }
            set
            {
                _isCTResetEnable = value;
                OnPropertyChanged("IsCTResetEnable");
            }
        }
        public Class1()
        {
            int a = 0;
            int b = a;
            IsCTResetEnable = "true";
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.BeginInvoke(this, new PropertyChangedEventArgs(propertyName), null, null);
            }
        }
    }
}
