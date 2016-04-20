using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MemoNote.Controls
{
    /// <summary>
    /// Interaction logic for DragTextBoxControl.xaml
    /// </summary>
    public partial class DragTextBoxControl : UserControl
    {
        private int prevLength = 7;

        public Point Location;

        private Point CorectionPoint;

        public DragTextBoxControl()
        {
            InitializeComponent();
        }

        private void tbMain_TextChanged(object sender, TextChangedEventArgs e)
        {
            int actualTextLength = tbMain.Text.Length;
            
            if (actualTextLength > 7 && actualTextLength > prevLength)
            {
                textControl.Width += 7;
                prevLength = tbMain.Text.Length;
            }

            if (actualTextLength > 7 && actualTextLength < prevLength)
            {
                textControl.Width -= 7;
                prevLength = tbMain.Text.Length;
            }
        }

        private void moveElement_MouseDown(object sender, MouseButtonEventArgs e)
        {
            moveElement.PreviewMouseMove += moveElement_MouseMove;
            if (!IsEnabled) return;
            e.Handled = true;
            moveElement.CaptureMouse();
            CorectionPoint = Mouse.GetPosition(this);
        }

        private void moveElement_MouseUp(object sender, MouseButtonEventArgs e)
        {
            moveElement.PreviewMouseMove -= moveElement_MouseMove;
            if (!IsEnabled) return;
            e.Handled = true;
            moveElement.ReleaseMouseCapture();           
        }

        private void moveElement_MouseMove(object sender, MouseEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            Point temp = new Point();
            temp.X = Mouse.GetPosition(parentWindow).X-View.MainNoteWindow.WinCanvasRelation.X;
            temp.Y = Mouse.GetPosition(parentWindow).Y-View.MainNoteWindow.WinCanvasRelation.Y;
            Location = temp;
            Location.X -= CorectionPoint.X;
            Location.Y -= CorectionPoint.Y;
            this.SetValue(Canvas.LeftProperty, Location.X);
            this.SetValue(Canvas.TopProperty, Location.Y);
        }
    }
}