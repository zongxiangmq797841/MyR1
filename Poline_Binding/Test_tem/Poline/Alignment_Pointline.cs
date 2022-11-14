using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows;

namespace Test_tem.Poline
{
    public class Alignment_Pointline : Alignment_ViewModel
    {
        public ObservableCollection<PolyLineViewModel> PointLineList { get; set; }
        public Alignment_Pointline()
        {
            this.PointLineList = new ObservableCollection<PolyLineViewModel>();
            this.PointLineList.Add(new PolyLineViewModel(Brushes.Red));
            UpdatePolyLineCollection();
            this.PointLineList[0].IsSelected = true;
        }
        public void UpdatePolyLineCollection()
        {
            PointCollection MyPointCollection = new PointCollection();
            for (int i = 0; i < 700; i++)
            {
                MyPointCollection.Add(new Point(i, 300));
            }
            this.PointLineList[0].PolyLineCollection = MyPointCollection;
        }

    }
}
