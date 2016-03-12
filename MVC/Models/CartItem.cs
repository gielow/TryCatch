using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TC_WebShopCaseMVC.Models
{
    public class CartItem
    {
        public Article Product { get; set; }
        public int Quantity { get; set; }
        public decimal Total
        {
            get
            {
                return this.Quantity * Product.Price;
            }
        }
    }
}