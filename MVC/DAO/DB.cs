using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TC_WebShopCaseMVC.Models;

namespace TC_WebShopCaseMVC.DAO
{
    public sealed class DB
    {
        private static readonly DB _instance = new DB(HttpContext.Current.Server.MapPath("~/App_Data"));

        public static DB Instance
        {
            get { return _instance; }
        }

        private DB(string rootPath)
        {
            DataPath = rootPath;
        }

        private string DataPath { get; set; }
        private string ArticlesPath { get { return string.Format("{0}/articles.xml", DataPath); } }
        private string CartsPath { get { return string.Format("{0}/carts.xml", DataPath); } }
        private string CustomersPath { get { return string.Format("{0}/customers.xml", DataPath); } }

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

        public Cart GetCart(string guid)
        {
            try
            {
                return Carts.FirstOrDefault(c => c.Guid.Equals(guid));
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error at GetCart: {0}", ex.Message), ex);
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