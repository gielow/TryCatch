using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TC.Models
{
    public class CartItem
    {
        public Article Article { get; set; }
        public int Quantity { get; set; }
        public decimal Total
        {
            get
            {
                return this.Quantity * Article.Price;
            }
        }
    }
}