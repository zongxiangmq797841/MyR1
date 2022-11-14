using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Test_tem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ShowGridLines = true;

            //Line myLine = new Line();
            //myLine.Stroke = System.Windows.Media.Brushes.LightSteelBlue;
            //myLine.X1 = 1;
            //myLine.X2 = 50;
            //myLine.Y1 = 1;
            //myLine.Y2 = 50;
            //myLine.HorizontalAlignment = HorizontalAlignment.Left;
            //myLine.VerticalAlignment = VerticalAlignment.Center;
            //myLine.StrokeThickness = 2;
            //grid.Children.Add(myLine);
            //Grid.SetRow(myLine, 1);
            //Grid.SetColumn(myLine, 1);
            //this.Content = grid;
            //Point point = new Point(this.canvas1.Width, this.canvas1.Height);
            //Point xypoint = new Point(point.X / 2, point.Y / 2);//新坐标原点  

            ////x轴坐标起点  
            //Point xstartpoint = new Point(0, point.Y / 2);
            ////x轴坐标终点  
            //Point xendpoint = new Point(point.X, point.Y / 2);

            ////y轴坐标起点  
            //Point ystartpoint = new Point(point.X / 2, point.Y);
            ////y轴坐标终点  
            //Point yendpoint = new Point(point.X / 2, 0);

            //Line xline = new Line();
            //xline.Stroke = System.Windows.Media.Brushes.LightSteelBlue;

            //xline.X1 = 0;
            //xline.Y1 = this.canvas1.Height / 2;

            //xline.X2 = this.canvas1.Width;
            //xline.Y2 = this.canvas1.Height / 2;

            //this.canvas1.Children.Add(xline);


            //Line yline = new Line();
            //yline.Stroke = System.Windows.Media.Brushes.LightSteelBlue;

            //yline.X1 = this.canvas1.Width / 2;
            //yline.Y1 = this.canvas1.Height;

            //yline.X2 = this.canvas1.Width / 2;
            //yline.Y2 = 0;

            //this.canvas1.Children.Add(yline);
            //Point[] points = new Point[1000];

            ////绘制sin曲线,从原点（0,0）开始  
            //Point zpoint = new Point(0, 0);
            //zpoint = XYTransf(zpoint, xypoint);
            //points[0] = zpoint;//sin曲线的起点  


            //for (int i = 1; i < 1000; i++)
            //{
            //    //计算sin（x，y）  
            //    point.X = 10 * i;//x  
            //    point.Y = 10 * Math.Sin(i);//y  

            //    //坐标转换  
            //    point = XYTransf(point, xypoint);
            //    points[i] = point;

            //}

            //Path myPath = new Path();
            //myPath.Stroke = Brushes.Black;
            //myPath.StrokeThickness = 1;
            //StreamGeometry theGeometry = BuildRegularPolygon(points, true, false);
            //// Create a StreamGeometry to use to specify myPath.  
            //theGeometry.FillRule = FillRule.EvenOdd;

            //// Freeze the geometry (make it unmodifiable)  
            //// for additional performance benefits.  
            //theGeometry.Freeze();

            //// Use the StreamGeometry returned by the BuildRegularPolygon to   
            //// specify the shape of the path.  
            //myPath.Data = theGeometry;  

            //// Add path shape to the UI.  
            ////this.Content=myPath;
            //grid.Children.Add(myPath);
            //Grid.SetRow(myPath, 1);
            //Grid.SetColumn(myPath, 1);
            //this.Content = grid;
           
        }
        private StreamGeometry BuildRegularPolygon(Point[] values, bool isClosed, bool isfilled)
        {
            // c is the center, r is the radius,  
            // numSides the number of sides, offsetDegree the offset in Degrees.  
            // Do not add the last point.  

            StreamGeometry geometry = new StreamGeometry();

            using (StreamGeometryContext ctx = geometry.Open())
            {
                ctx.BeginFigure(values[0], isfilled /* is filled */, isClosed /* is closed */);

                for (int i = 1; i < values.Length; i++)
                {
                    ctx.LineTo(values[i], true /* is stroked */, false /* is smooth join */);
                }
            }

            return geometry;

        }
        public Point XYTransf(Point point, Point xypoint)
        {
            point.X += xypoint.X;
            point.Y = xypoint.Y - point.Y;

            return point;//显示屏幕坐标系的位置  
        }

    }
}
