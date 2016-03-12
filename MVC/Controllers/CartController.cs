using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TC_WebShopCaseMVC.DAO;
using TC_WebShopCaseMVC.Models;

namespace TC_WebShopCaseMVC.Controllers
{
    [RoutePrefix("api/Cart")]
    public class CartController : ApiController
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

            return DB.Instance.GetCart(guid);
        }

        [HttpPut]
        [Route("{guid}/Items/{itemId:int}/{quantity:int}")]
        public void AddItem(string guid, int itemId, int quantity)
        {
            var cart = this.Get(guid);
            cart.AddArticle(itemId, quantity);
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
        public IEnumerable<CartItem> Items(string guid)
        {
            return this.Get(guid).Items;
        }
        
        // POST: api/Cart
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Cart/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Cart/5
        public void Delete(int id)
        {
        }
    }
}
