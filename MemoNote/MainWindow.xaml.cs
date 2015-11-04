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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            User usrVova = new User("Vova", "Pass");
            Notepad notepad = new Notepad("My notebook", usrVova);
            for (int i = 0; i < 10; i++)
            {
                new Note("#" + i, "cycle1", Convert.ToString(i));
            }

            for (int i = 11; i < 20; i++)
            {
                new Note("#" + i, "cycle2", Convert.ToString(i));
            }

            notepad.ChangeStrategy(new TagSearchingStrategy());
            lbNote.ItemsSource = notepad.GetSearchResult(tbSearh.Text);
        }

        private void btnChangeStrategy_Click(object sender, RoutedEventArgs e)
        {
            Notepad notepad = Notepad.Objects.Values.ElementAt(0);
            notepad.ChangeStrategy(new NameSearchingStrategy());
            lbNote.ItemsSource = notepad.GetSearchResult(tbSearh.Text);
        }
    }
}
