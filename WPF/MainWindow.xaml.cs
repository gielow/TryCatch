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
        private string CartGuid = string.Empty;
        private string User = string.Empty;

        public MainWindow()
        {
            InitializeComponent();

            LoadArticles(0);
            InitializeCart();
            LoadCart();

            btnNext.Click += delegate(object sender, RoutedEventArgs e){ LoadArticles(1); };
            btnPrevious.Click += delegate (object sender, RoutedEventArgs e) { LoadArticles(-1); };
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var obj = new EcommerceAPI();
            obj.Authenticate();
       } 

        private void LoadArticles(int factor)
        {
            var pageNumber = Convert.ToInt32(actualPage.Content) + factor;
            if (pageNumber < 1)
                return;

            var obj = new EcommerceAPI();
            obj.GetArticles(LoadArticlesGrid, pageNumber);
            actualPage.Content = pageNumber;
        }

        private int LoadArticlesGrid(List<Article> articles)
        {
            dgArticles.Items.Clear();
            articles.ForEach(a => dgArticles.Items.Add(a));

            return 0;
        }

        private void LoadCart()
        {
            var obj = new EcommerceAPI();
            obj.GetCart(LoadCartGrid, CartGuid);
        }

        private int LoadCartGrid(Cart cart)
        {
            dgCart.Items.Clear();
            CartGuid = cart.Guid;
            cart.Items.ForEach(i => dgCart.Items.Add(i));

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

            var obj = new EcommerceAPI();
            obj.AddCartItem(LoadCartGrid, this.CartGuid, article.Id);
        }

        private void InitializeCart()
        {
            var obj = new EcommerceAPI();
            obj.NewCart().ContinueWith((t) => CartGuid = t.Result);

        }

        private void RemoveFromCart(object sender, RoutedEventArgs e)
        {
            OrderItem item = null;
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    item = dgCart.Items[row.GetIndex()] as OrderItem;
                    break;
                }

            var obj = new EcommerceAPI();
            obj.RemoveCartItem(LoadCartGrid, this.CartGuid, item.Article.Id);
        }

        private void btnCheckout_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.User))
            {
                var frmCustomer = new CustomerForm();
                frmCustomer.ShowDialog();
            }
        }
    }
}
