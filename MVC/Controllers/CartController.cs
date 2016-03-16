using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TC_WebShopCaseMVC.DAO;
using TC.Models;
using TC.Core;
using TC.Helper;

namespace TC_WebShopCaseMVC.Controllers
{
    public class CartController : Controller
    {
        private IRepository _repository;

        public CartController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet, EnableJson]
        public ActionResult New()
        {
            var guid = Guid.NewGuid().ToString();
            _repository.PutCart(new Cart(guid));
            HttpContext.Session["CartGuid"] = guid;

            return View(_repository.Carts.FirstOrDefault(c => c.Guid == guid));
        }
        
        [HttpGet, EnableJson]
        public ActionResult Index(string guid)
        {
            return View(GetCart(guid));
        }
        

        private Cart GetCart(string guid)
        {
            if (string.IsNullOrEmpty(guid))
                guid = HttpContext.Session["CartGuid"] as string;
            else if (string.IsNullOrEmpty(HttpContext.Session["CartGuid"] as string))
                HttpContext.Session["CartGuid"] = guid;

            return _repository.Carts.FirstOrDefault(c => c.Guid == guid) ?? new Cart();
        }

        [HttpGet, EnableJson]
        public ActionResult Items(string guid)
        {
            return View(GetCart(guid).Items);
        }
        
        [HttpPut, EnableJson]
        [AcceptVerbs("PUT", "POST")]
        public ActionResult AddItem(string guid, int articleId, int quantity)
        {
            var cart = this.GetCart(guid);
            var article = _repository.Articles.FirstOrDefault(a => a.Id == articleId);
            if (article == null)
                throw new Exception(string.Format("Article id {0} not found.", articleId));

            cart.AddArticle(article, quantity);
            _repository.PutCart(cart);

            return Index(guid);
        }

        [HttpDelete, EnableJson]
        [AcceptVerbs("DELETE", "POST")]
        public ActionResult RemoveItem(string guid, int articleId, int quantity)
        {
            var cart = this.GetCart(guid);
            cart.RemoveArticle(articleId, quantity);
            _repository.PutCart(cart);

            return Index(guid);
        }
        
        [Authorize]
        [HttpGet, EnableJson]
        public ActionResult Checkout(string guid)
        {
            var cart = _repository.Carts.FirstOrDefault(c => c.Guid == guid);

            if (cart == null)
            {
                ModelState.AddModelError("", "Cart not found.");
                RedirectToAction("Index", "Cart");

                return View(new Order());
            }

            var customer = _repository.Customers.FirstOrDefault(c => c.Email == User.Identity.Name);

            var order = new Order();
            order.Customer = customer;
            order.Items = cart.Items;
            order.Status = OrderStatus.WaitingPayment;
            order.DateTime = DateTime.Now;

            _repository.PutOrder(order);
            _repository.DeleteCart(cart);

            return View(order);
        }
    }
}
