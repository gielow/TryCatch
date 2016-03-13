using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using TC_WebShopCaseMVC.DAO;
using TC_WebShopCaseMVC.Models;
using TC.Models;

namespace TC_WebShopCaseMVC.Tests
{
    /// <summary>
    /// Summary description for SerializationTest
    /// </summary>
    [TestClass]
    public class SerializationTest
    {
        private const string rootPath = @"E:\VSWorkspace\TC_WebShopCaseMVC\TC_WebShopCaseMVC\TC_WebShopCaseMVC\App_Data";
        private readonly string articlePath = string.Format(@"{0}\articles.xml", rootPath);
        private readonly string customersPath = string.Format(@"{0}\customers.xml", rootPath);
        public SerializationTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        
        [TestMethod]
        public void SerializeArticles()
        {
            var articles = new List<Article>();

            for (int i = 1; i <= 50; i++)
            {
                articles.Add(new Article()
                {
                    Id = i,
                    Description = string.Format("Article {0}", i),
                    Price = 10
                });
            }

            Serializer.SerializeObject<List<Article>>(articles, articlePath);
            Assert.IsTrue(File.Exists(articlePath));
        }

        [TestMethod]
        public void DeserializeArticles()
        {
            var customers = Serializer.DeSerializeObject<List<Article>>(articlePath);

            Assert.AreEqual(50, customers.Count);
        }

        [TestMethod]
        public void SerializeCustomers()
        {
            var customers = new List<Customer>();

            for (int i = 1; i <= 10; i++)
            {
                customers.Add(new Customer()
                {
                    Id = i,
                    Address = "Primavera st",
                    City = "Blumenau",
                    Email = "andre.gielow@gmail.com",
                    FirstName = "André " + i.ToString("0#"),
                    LastName = "Gielow",
                    HouseNumber = 291,
                    ZipCode = "89057-036",
                    Title = "Mr."
                });
            }

            Serializer.SerializeObject<List<Customer>>(customers, customersPath);
            Assert.IsTrue(File.Exists(customersPath));
        }

        [TestMethod]
        public void DeserializeCustomers()
        {
            var customers = Serializer.DeSerializeObject<List<Customer>>(customersPath);

            Assert.AreEqual(10, customers.Count);
        }
    }
}
