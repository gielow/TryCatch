using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using TC.Models;

namespace WPF.DAO
{
    public class EcommerceAPI
    {
        private HttpClient Client()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost/TC/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        public async void GetArticles(Func<List<Article>, int> loadMethod, int page)
        {
            //var url = "http://localhost/TC/api/Article/Index/1";
            //var webClient = new WebClient();
            //var articles = webClient.DownloadData(url);
            //DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Article>));

            using (var client = Client())
            {
                HttpResponseMessage response = await client.GetAsync(string.Format("api/Article/Index/{0}", page));

                if (response.IsSuccessStatusCode)
                {
                    var articles = await response.Content.ReadAsAsync<List<Article>>();
                    loadMethod(articles);
                }
            }
        }

        public async void GetCart(Func<Cart, int> loadMethod, string guid)
        {
            using (var client = Client())
            {
                HttpResponseMessage response = await client.GetAsync(
                    string.Format("api/Cart/{0}", string.IsNullOrEmpty(guid) ? "0" : guid));

                if (response.IsSuccessStatusCode)
                {
                    var cart = await response.Content.ReadAsAsync<Cart>();
                    guid = cart.Guid;

                    loadMethod(cart);
                }
            }
        }

        public async void AddCartItem(Func<Cart, int> loadMethod, string guid, int articleId)
        {
            using (var client = Client())
            {
                HttpResponseMessage response = await client.PutAsync(
                    string.Format("api/Cart/{0}/Items/{1}/1", string.IsNullOrEmpty(guid) ? "0" : guid, articleId), null);

                if (response.IsSuccessStatusCode)
                {
                    GetCart(loadMethod, guid);
                }
            }
        }

        public async void Authenticate()
        {
            /*var request = new RegisterBindingModel();
            request.Email = "andre.gielow@gmail.com";
            request.Password = "test123";
            request.ConfirmPassword = "test123";

            using (var client = Client())
            {
                var response = await client.PostAsJsonAsync("api/Account/Register", request);

                if (response.IsSuccessStatusCode)
                {
                    
                }
            }*/
        }

        public async void RemoveCartItem(Func<Cart, int> loadMethod, string guid, int articleId)
        {
            using (var client = Client())
            {
                HttpResponseMessage response = await client.DeleteAsync(
                    string.Format("api/Cart/{0}/Items/{1}/1", string.IsNullOrEmpty(guid) ? "0" : guid, articleId));

                if (response.IsSuccessStatusCode)
                {
                    GetCart(loadMethod, guid);
                }
            }
        }

        /*public async void Register(RegisterBindingModel customer)
        {
            /*using (var client = Client())
            {
                HttpResponseMessage response = await client.PutAsync(
                    "api/Account/Register", customer);

                if (response.IsSuccessStatusCode)
                {
                    
                }
            }
        }*/
    }
}
