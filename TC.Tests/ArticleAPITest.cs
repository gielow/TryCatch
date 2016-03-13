using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Collections.Generic;
using TC.Models;

namespace TC_WebShopCaseMVC.Tests
{
    [TestClass]
    public class ArticleAPITest
    {
        [TestMethod]
        public async void GetArticles()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost/TC/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Article/page/1");

                if (response.IsSuccessStatusCode)
                {
                    List<Article> articles = await response.Content.ReadAsAsync<List<Article>>();

                    Assert.AreEqual(10, articles.Count);
                }
            }
        }
    }
}
