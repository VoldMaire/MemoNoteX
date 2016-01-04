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

        public Point Location
        {
            get
            {
                return new Point(0, 0);
            }
        }

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
            moveElement.MouseMove += moveElement_MouseMove;
            if (!IsEnabled) return;
            e.Handled = true;
            moveElement.CaptureMouse();
        }

        private void moveElement_MouseUp(object sender, MouseButtonEventArgs e)
        {
            moveElement.MouseMove -= moveElement_MouseMove;
            if (!IsEnabled) return;
            e.Handled = true;
            moveElement.ReleaseMouseCapture();
        }

        private void moveElement_MouseMove(object sender, MouseEventArgs e)
        {
            
            
        }
    }
}