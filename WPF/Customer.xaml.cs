using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TC.Models;
using WPF.DAO;

namespace WPF
{
    /// <summary>
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class Customer : Window
    {
        public Customer()
        {
            InitializeComponent();
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(edEmail.Text, @"^((([\w]+\.[\w]+)+)|([\w]+))@(([\w]+\.)+)([A-Za-z]{1,3})$"))
            {
                MessageBox.Show("Please input valid email address ");
                return;
            }

            /*var customer = new RegisterBindingModel();
            customer.Title = edTitle.Text;
            customer.FirstName = edFirstName.Text;
            customer.LastName = edFirstName.Text;
            customer.Address = edAddress.Text;
            customer.ZipCode = edZipCode.Text;
            customer.City = edCity.Text;
            customer.Email = edEmail.Text;

            var api = new EcommerceAPI();
            api.Register(customer);*/
        }
    }
}
