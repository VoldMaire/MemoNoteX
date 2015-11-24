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
using MemoNoteModel;

namespace MemoNote
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

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            if (tblregauto.Text == "Реєстрація")
            {
                Controls.RegControl rgc = new Controls.RegControl();
                grdControl.Children.Clear();
                grdControl.Children.Add(rgc);
                tblregauto.Text = "Авторизація";
            }
            else
            {
                Controls.AutoControl atc = new Controls.AutoControl();
                grdControl.Children.Clear();
                grdControl.Children.Add(atc);
                tblregauto.Text = "Реєстрація";
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Controls.AutoControl atc = new Controls.AutoControl();
            grdControl.Children.Clear();
            grdControl.Children.Add(atc);
        }
    }
}
