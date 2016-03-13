using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TC_WebShopCaseMVC.DAO;
using TC.Models;

namespace TC_WebShopCaseMVC.Controllers
{
    [RoutePrefix("api/Cart")]
    public class CartApiController : ApiController
    {
        // GET: api/Cart
        [HttpGet]
        [Route("{guid}")]
        public Cart Get(string guid)
        {
            if (string.IsNullOrEmpty(guid) || guid == "0")
            {
                guid = Guid.NewGuid().ToString();
                DB.Instance.PutCart(new Cart(guid));
            }

            return DB.Instance.Carts.FirstOrDefault(c => c.Guid == guid);
        }

        [HttpPut]
        [Route("{guid}/Items/{itemId:int}/{quantity:int}")]
        public void AddItem(string guid, int itemId, int quantity)
        {
            var cart = this.Get(guid);
            var article = DB.Instance.Articles.FirstOrDefault(a => a.Id == itemId);
            if (article == null)
                throw new Exception(string.Format("Article id {0} not found.", itemId));
            cart.AddArticle(article, quantity);
            DB.Instance.PutCart(cart);
        }

        [HttpDelete]
        [Route("{guid}/Items/{itemId:int}/{quantity:int}")]
        public void RemoveItem(string guid, int itemId, int quantity)
        {
            var cart = this.Get(guid);
            cart.RemoveArticle(itemId, quantity);
            DB.Instance.PutCart(cart);
        }

        [Route("{guid}/Items")]
        [HttpGet]
        public IEnumerable<OrderItem> Items(string guid)
        {
            return this.Get(guid).Items;
        }
        
        [HttpPost]
        [Route("{guid}/Checkout")]
        public void Checkout(string customerLogin)
        {

        }
    }
}
