using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TC.Models;

namespace WPF.DAO
{
    public class Authentication
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
            using (var client = Client())
            {
                client.BaseAddress = new Uri("http://localhost/TC/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(string.Format("api/Article/page/{0}", page));

                if (response.IsSuccessStatusCode)
                {
                    var articles = await response.Content.ReadAsAsync<List<Article>>();
                    loadMethod(articles);
                }

                //return new List<Article>();
            }
        }

        public async void Authenticate()
        {
            var request = new RegisterBindingModel();
            request.Email = "andre.gielow@gmail.com";
            request.Password = "test123";
            request.ConfirmPassword = "test123";

            using (var client = Client())
            {
                var response = await client.PostAsJsonAsync("api/Account/Register", request);

                if (response.IsSuccessStatusCode)
                {
                    
                }
            }
        }
    }
}
