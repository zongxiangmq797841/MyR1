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
using Test_tem.Poline;

namespace Test_tem
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
            this.DataContext = new Alignment_PolyLinesVM();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CurveScale curveScale = new CurveScale();
            curveScale.SetValue(Grid.RowProperty, 0);
            curveScale.SetValue(Grid.RowSpanProperty, 3);
            curveScale.SetValue(Grid.ColumnProperty, 0);
            curveScale.SetValue(Grid.ColumnSpanProperty, 3);

            LayoutRoot.Children.Add(curveScale);
          
            textbox1.KeyDown += new KeyEventHandler(textbox_KeyDown);
            textbox2.KeyDown += new KeyEventHandler(textbox_KeyDown);
            //textbox3.KeyDown += new KeyEventHandler(textbox_KeyDown);
            //textbox4.KeyDown += new KeyEventHandler(textbox_KeyDown);
        }
        private void textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Decimal || e.Key == Key.OemPeriod)
            {
                e.Handled = false;
            }
            else if ((e.Key >= Key.D0 && e.Key <= Key.D9) && e.KeyboardDevice.Modifiers != ModifierKeys.Shift)
            {
                e.Handled = false;
            }
            else if (e.Key == Key.Enter)
            {
                UIElement element = Keyboard.FocusedElement as UIElement;
                if (element != null)
                {
                    element.MoveFocus(new TraversalRequest(FocusNavigationDirection.Down));
                }
                Keyboard.ClearFocus();
                e.Handled = true;
            }
            //else
            //{
            //    e.Handled = false;
            //}
        }
        private void textbox_KeyDownNormal(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                UIElement element = Keyboard.FocusedElement as UIElement;
                if (element != null)
                {
                    element.MoveFocus(new TraversalRequest(FocusNavigationDirection.Down));
                }
                Keyboard.ClearFocus();
                e.Handled = true;
            }
        }
        private void Myline_MouseMove(object sender, MouseEventArgs e)
        {
            double x = e.GetPosition(Myline).X - 1;
            scaleLine.SetValue(Canvas.LeftProperty, x);
        }
    }
    public class CurveScale : UIElement
    {
        //左上角为原点，向右为正，向下为正
        private int baseX = 0;
        private int baseY = 0;
        static private Pen scalePen = new Pen(Brushes.Yellow, 1);

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            Color whitecolor = new Color();
            whitecolor.A = System.Convert.ToByte("ff", 16);
            whitecolor.R = System.Convert.ToByte("06", 16);
            whitecolor.G = System.Convert.ToByte("13", 16);
            whitecolor.B = System.Convert.ToByte("11", 16);
            Brush cellBrush = new SolidColorBrush(whitecolor) as Brush;
            Pen cellPen = new Pen(cellBrush, 1);
            //Pen cellPen = new Pen(Brushes.Red, 1);

            GuidelineSet guidelines = null;
            Point point0 = new Point();
            Point point1 = new Point();
            double halfPenWidth = 0.5;

            //画横轴
            guidelines = new GuidelineSet();
            point0 = new Point(baseX + 30 + 15, baseY + this.RenderSize.Height - 30);
            guidelines.GuidelinesX.Add(point0.X + halfPenWidth);
            guidelines.GuidelinesX.Add(point0.X - halfPenWidth);
            point1 = new Point(baseX + this.RenderSize.Width - 10, baseY + this.RenderSize.Height - 30);
            drawingContext.PushGuidelineSet(guidelines);
            drawingContext.DrawLine(scalePen, point0, point1);
            drawingContext.Pop();

            //画横轴箭头
            guidelines = new GuidelineSet();
            point0 = new Point(baseX + this.RenderSize.Width - 10, baseY + this.RenderSize.Height - 30);
            guidelines.GuidelinesX.Add(point0.X + halfPenWidth);
            guidelines.GuidelinesX.Add(point0.X - halfPenWidth);
            point1 = new Point(baseX + this.RenderSize.Width - 15, baseY + this.RenderSize.Height - 25);
            drawingContext.PushGuidelineSet(guidelines);
            drawingContext.DrawLine(scalePen, point0, point1);
            drawingContext.Pop();
            point0 = new Point(baseX + this.RenderSize.Width - 10, baseY + this.RenderSize.Height - 30);
            guidelines.GuidelinesX.Add(point0.X + halfPenWidth);
            guidelines.GuidelinesX.Add(point0.X - halfPenWidth);
            point1 = new Point(baseX + this.RenderSize.Width - 15, baseY + this.RenderSize.Height - 35);
            drawingContext.PushGuidelineSet(guidelines);
            drawingContext.DrawLine(scalePen, point0, point1);
            drawingContext.Pop();


            //画纵轴
            guidelines = new GuidelineSet();
            point0 = new Point(baseX + 30 + 15, baseY + this.RenderSize.Height - 30);
            guidelines.GuidelinesX.Add(point0.X + halfPenWidth);
            guidelines.GuidelinesX.Add(point0.X - halfPenWidth);
            point1 = new Point(baseX + 30 + 15, baseY + 10);
            drawingContext.PushGuidelineSet(guidelines);
            drawingContext.DrawLine(scalePen, point0, point1);
            drawingContext.Pop();

            //画纵轴箭头
            guidelines = new GuidelineSet();
            point0 = new Point(baseX + 30 + 15, baseY + 10);
            guidelines.GuidelinesX.Add(point0.X + halfPenWidth);
            guidelines.GuidelinesX.Add(point0.X - halfPenWidth);
            point1 = new Point(baseX + 25 + 15, baseY + 15);
            drawingContext.PushGuidelineSet(guidelines);
            drawingContext.DrawLine(scalePen, point0, point1);
            drawingContext.Pop();
            point0 = new Point(baseX + 30 + 15, baseY + 10);
            guidelines.GuidelinesX.Add(point0.X + halfPenWidth);
            guidelines.GuidelinesX.Add(point0.X - halfPenWidth);
            point1 = new Point(baseX + 35 + 15, baseY + 15);
            drawingContext.PushGuidelineSet(guidelines);
            drawingContext.DrawLine(scalePen, point0, point1);
            drawingContext.Pop();


            //int j = 0;
            double inteval_X = (this.RenderSize.Width - 30 - 30 - 15) / 32;
            double i = baseX + 30 + 15 + inteval_X / 2;
            //画横轴刻度
            for (int j = 0; j < 32 * 2; j++)
            {
                //j++;
                if (j % 2 != 0)//大刻度
                {
                    guidelines = new GuidelineSet();
                    point0 = new Point(baseX + i, baseY + baseY + this.RenderSize.Height - 30);
                    guidelines.GuidelinesX.Add(point0.X + halfPenWidth);
                    guidelines.GuidelinesX.Add(point0.X - halfPenWidth);
                    point1 = new Point(baseX + i, baseY + baseY + this.RenderSize.Height - 30 + 10);
                    drawingContext.PushGuidelineSet(guidelines);
                    drawingContext.DrawLine(scalePen, point0, point1);
                    drawingContext.Pop();
                }
                else
                {
                    guidelines = new GuidelineSet();
                    point0 = new Point(baseX + i, baseY + baseY + this.RenderSize.Height - 30);
                    guidelines.GuidelinesX.Add(point0.X + halfPenWidth);
                    guidelines.GuidelinesX.Add(point0.X - halfPenWidth);
                    point1 = new Point(baseX + i, baseY + this.RenderSize.Height - 30 + 5);
                    drawingContext.PushGuidelineSet(guidelines);
                    drawingContext.DrawLine(scalePen, point0, point1);
                    drawingContext.Pop();
                }
                i += inteval_X / 2;
            }

            //画纵轴刻度
            double inteval_Y = (this.RenderSize.Height - 30 - 30) / 32;
            double t = (this.RenderSize.Height - 30 - inteval_Y);
            for (int k = 0; k < 32; k++)
            {
                guidelines = new GuidelineSet();
                point0 = new Point(baseX + 30 + 15, baseY + t);
                guidelines.GuidelinesX.Add(point0.X + halfPenWidth);
                guidelines.GuidelinesX.Add(point0.X - halfPenWidth);
                point1 = new Point(baseX + 30 + 15 - 5, baseY + t);
                drawingContext.PushGuidelineSet(guidelines);
                drawingContext.DrawLine(scalePen, point0, point1);
                drawingContext.Pop();
                t -= inteval_Y;
            }
        }
    }
}
