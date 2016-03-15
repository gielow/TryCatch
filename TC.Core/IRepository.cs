using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Models;

namespace TC.Core
{
    public interface IRepository
    {
        List<Article> Articles { get; }

        List<Customer> Customers { get; }

        void PutCustomer(Customer customer);
        
        List<Cart> Carts { get; }
        
        List<Order> Orders { get; }
        
        void PutOrder(Order order);

        void PutCart(Cart cart);

        void DeleteCart(Cart cart);        
    }
}
