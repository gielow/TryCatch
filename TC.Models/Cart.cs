﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TC.Models
{
    public class Cart
    {
        public Cart()
        {
            Items = new List<OrderItem>();
        }
        public Cart(string guid)
        {
            Guid = guid;
            Items = new List<OrderItem>();
        }

        public void AddArticle(Article article, int quantity)
        {
            var cartItem = Items.FirstOrDefault(i => i.Article == article) ?? null;

            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                cartItem = new OrderItem()
                {
                    Quantity = quantity,
                    Article = article
                };

                Items.Add(cartItem);
            }
        }

        public string Guid { get; set; }
        public List<OrderItem> Items { get; set; }
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

            return string.Equals(this.Guid, obj2.Guid);
        }

        public void RemoveArticle(int itemId, int quantity)
        {
            var item = Items.FirstOrDefault(i => i.Article.Id == itemId);

            if (item != null)
            {
                item.Quantity -= quantity;

                if (item.Quantity <= 0)
                    Items.Remove(item);
            }
        }
    }
}