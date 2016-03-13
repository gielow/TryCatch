using System;
using System.Collections.Generic;
using System.Data;
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
using WPF.DAO;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            loadArticlesGrid();

            button.Click += Button_Click;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var obj = new Authentication();
            obj.Authenticate();
        }

        private void loadArticlesGrid()
        {
            var obj = new Authentication();
            obj.GetArticles();
            DataTable dt = new DataTable();
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Price", typeof(decimal));

            for (int i = 1; i <= 10; i++)
            {
                /*var a = new DataGridRow();
                a.
                row["Description"] = string.Format("Article {0}", i);
                row["Price"] = 10;
                dt.Rows.Add(row);*/
            }

            dgArticles.Items.Clear();
            //dgArticles.Items.Add()
        }
    }
}
