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
using System.Windows.Shapes;

namespace MemoNote.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainNoteWindow : Window
    {
        public static Point WinCanvasRelation;

        public List<string> lblist = new List<string>
        {
            "Ukraine",
            "UsA",
            "Germany",
            "France",
            "Swizerland",
            "Spain",
            "Italia",
            "UsA",
            //"Germany",
            //"France",
            //"Swizerland",
            //"Spain",
            //"Italia",
            //"UsA",
            //"Germany",
            //"France",
            //"Swizerland",
            //"Spain",
            //"Italia"
        };

        public MainNoteWindow()
        {
            InitializeComponent();      
            
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            WinCanvasRelation = MainCanvas.TranslatePoint(new Point(0, 0), MainWindow);
        }

        private void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void TreeView_Loaded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = new TreeViewItem();
            item.Header = "Computer";
            item.ItemsSource = new string[] { "Monitor", "CPU", "Mouse" };

            // ... Create a second TreeViewItem.
            TreeViewItem item2 = new TreeViewItem();
            item2.Header = "Outfit";
            item2.ItemsSource = new string[] { "Pants", "Shirt", "Hat", "Socks" };

            // ... Get TreeView reference and add both items.
            var tree = sender as TreeView;
            tree.Items.Add(item);
            tree.Items.Add(item2);
        }

        private void ListBox_Loaded(object sender, RoutedEventArgs e)
        {
            ((ListBox)sender).ItemsSource = lblist;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAddNotepad_Click(object sender, RoutedEventArgs e)
        {
            NewNotebookWindow wnd = new NewNotebookWindow();
            wnd.Show();
        }    
    }
}
