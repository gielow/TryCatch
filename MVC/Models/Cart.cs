using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TC_WebShopCaseMVC.DAO;

namespace TC_WebShopCaseMVC.Models
{
    public class Cart
    {
        public Cart()
        {

        }
        public Cart(string guid)
        {
            Guid = guid;
            Items = new List<CartItem>();
        }

        public void AddArticle(int productId, int quantity)
        {
            var product = DB.Instance.Articles.FirstOrDefault(a => a.Id == productId);

            if (product == null)
                throw new Exception(string.Format("Product id {0} not found.", productId));

            var cartItem = Items.FirstOrDefault(i => i.Product.Id == productId) ?? null;

            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                cartItem = new CartItem()
                {
                    Quantity = quantity,
                    Product = product
                };

                Items.Add(cartItem);
            }
        }

        public string Guid { get; set; }
        public List<CartItem> Items { get; set; }
        public decimal Total
        {
            get
            {
                return Items.Sum(i => i.Total);
            }
        }

        public override bool Equals(object obj)
        {
            var obj2 = obj as Cart;
            if (obj2 == null)
                return false;

            return this.Guid.Equals(obj2.Guid);
        }

        internal void RemoveArticle(int itemId, int quantity)
        {
            var item = Items.FirstOrDefault(i => i.Product.Id == itemId);

            if (item != null)
            {
                item.Quantity -= quantity;

                if (item.Quantity <= 0)
                    Items.Remove(item);
            }
        }
    }
}