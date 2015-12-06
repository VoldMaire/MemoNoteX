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
    /// Interaction logic for RegControl.xaml
    /// </summary>
    public partial class RegControl : UserControl
    {
        public RegControl()
        {
            InitializeComponent();
        }

        private void tbLogin_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.SelectAll();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            tbLogin.SelectAll();
            tbName.SelectAll();
            tbPassword.SelectAll();
            tbRepeatPassword.SelectAll();
        }
    }
}
