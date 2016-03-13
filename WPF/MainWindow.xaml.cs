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
using TC.Models;
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

            LoadArticles(0);

            btnNext.Click += delegate(object sender, RoutedEventArgs e){ LoadArticles(1); };
            btnPrevious.Click += delegate (object sender, RoutedEventArgs e) { LoadArticles(-1); };
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var obj = new Authentication();
            obj.Authenticate();
        }

        private void LoadArticles(int factor)
        {
            var pageNumber = Convert.ToInt32(actualPage.Content) + factor;
            if (pageNumber < 1)
                return;

            var obj = new Authentication();
            obj.GetArticles(LoadArticlesGrid, pageNumber);
            actualPage.Content = pageNumber;
        }

        private int LoadArticlesGrid(List<Article> articles)
        {
            dgArticles.Items.Clear();
            foreach (var article in articles)
            {
                dgArticles.Items.Add(article);
            }

            return 0;
        }

        private void AddToCart(object sender, RoutedEventArgs e)
        {
            Article article = null;
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    article = dgArticles.Items[row.GetIndex()] as Article;
                    break;
                }


        }
        
        private void RemoveFromCart(object sender, RoutedEventArgs e)
        {
            Article article = null;
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    article = dgArticles.Items[row.GetIndex()] as Article;
                    break;
                }


        }
    }
}
