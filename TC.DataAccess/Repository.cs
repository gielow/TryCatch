﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TC.Core;
using TC.Models;

namespace TC.DataAccess
{
    public class Repository : IRepository
    {
        private string DataPath { get { return HttpContext.Current.Server.MapPath("~/App_Data"); } }
        private string ArticlesPath { get { return string.Format("{0}/articles.xml", DataPath); } }
        private string CartsPath { get { return string.Format("{0}/carts.xml", DataPath); } }
        private string CustomersPath { get { return string.Format("{0}/customers.xml", DataPath); } }
        private string OrdersPath { get { return string.Format("{0}/orders.xml", DataPath); } }

        private List<Article> _articles;

        public List<Article> Articles
        {
            get
            {
                if (_articles == null)
                    _articles = Serializer.DeSerializeObject<List<Article>>(ArticlesPath);

                return _articles;
            }
        }

        private List<Customer> _customers;
        public List<Customer> Customers
        {
            get
            {
                if (_customers == null)
                    _customers = Serializer.DeSerializeObject<List<Customer>>(CustomersPath);

                return _customers;
            }
        }

        public void PutCustomer(Customer customer)
        {
            try
            {
                if (Customers.Contains(customer))
                    Customers.Remove(customer);

                Customers.Add(customer);
                Serializer.SerializeObject<List<Customer>>(Customers, CustomersPath);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error at PutCustomer: {0}", ex.Message), ex);
            }
        }

        private List<Cart> _carts;

        public List<Cart> Carts
        {
            get
            {
                if (_carts == null)
                    _carts = Serializer.DeSerializeObject<List<Cart>>(CartsPath);
                return _carts;
            }
        }

        private List<Order> _orders;

        public List<Order> Orders
        {
            get
            {
                if (_orders == null)
                    _orders = Serializer.DeSerializeObject<List<Order>>(OrdersPath);

                return _orders;
            }
        }

        public void PutOrder(Order order)
        {
            try
            {
                if (order.Protocol <= 0)
                {
                    if (Orders.Count > 0)
                        order.Protocol = Orders.Max(o => o.Protocol) + 1;
                    else
                        order.Protocol = 1;
                }

                Orders.Add(order);
                Serializer.SerializeObject<List<Order>>(Orders, OrdersPath);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error at PutOrder: {0}", ex.Message), ex);
            }
        }

        public void PutCart(Cart cart)
        {
            try
            {
                if (Carts.Contains(cart))
                    Carts.Remove(cart);

                Carts.Add(cart);
                Serializer.SerializeObject<List<Cart>>(Carts, CartsPath);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error at PutCart: {0}", ex.Message), ex);
            }
        }

        public void DeleteCart(Cart cart)
        {
            try
            {
                Carts.Remove(cart);
                Serializer.SerializeObject<List<Cart>>(Carts, CartsPath);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error at DeleteCart: {0}", ex.Message), ex);
            }
        }
    }
}
