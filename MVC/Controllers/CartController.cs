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

        [HttpGet]
        public ActionResult New()
        {
            return View(GetNew());
        }

        [EnableJson]
        public JsonResult NewJson()
        {
            return Json(GetNew(), JsonRequestBehavior.AllowGet);
        }

        private Cart GetNew()
        {
            var guid = Guid.NewGuid().ToString();
            _repository.PutCart(new Cart(guid));
            HttpContext.Session["CartGuid"] = guid;
            return _repository.Carts.FirstOrDefault(c => c.Guid == guid);
        }

        [HttpGet]
        public ActionResult Index(string guid)
        {
            return View(GetCart(guid));
        }

        [EnableJson]
        public JsonResult IndexJson(string guid)
        {
            return Json(GetCart(guid), JsonRequestBehavior.AllowGet);
        }

        private Cart GetCart(string guid)
        {
            if (string.IsNullOrEmpty(guid))
                guid = HttpContext.Session["CartGuid"] as string;
            else if (string.IsNullOrEmpty(HttpContext.Session["CartGuid"] as string))
                HttpContext.Session["CartGuid"] = guid;

            return _repository.Carts.FirstOrDefault(c => c.Guid == guid) ?? new Cart();
        }

        [HttpGet]
        public ActionResult Items(string guid)
        {
            return View(GetItems(guid));
        }

        [EnableJson]
        public JsonResult ItemsJson(string guid)
        {
            return Json(GetItems(guid), JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<OrderItem> GetItems(string guid)
        {
            return GetCart(guid).Items;
        }

        [HttpPut]
        [AcceptVerbs("PUT", "POST")]
        public ActionResult AddItem(string guid, int articleId, int quantity)
        {
            AddItemCart(guid, articleId, quantity);

            return Index(guid);
        }

        [EnableJson]
        [HttpPut]
        [AcceptVerbs("PUT", "POST")]
        public JsonResult AddItemJson(string guid, int articleId, int quantity)
        {
            AddItemCart(guid, articleId, quantity);

            return IndexJson(guid);
        }

        private void AddItemCart(string guid, int articleId, int quantity)
        {
            var cart = this.GetCart(guid);
            var article = _repository.Articles.FirstOrDefault(a => a.Id == articleId);
            if (article == null)
                throw new Exception(string.Format("Article id {0} not found.", articleId));

            cart.AddArticle(article, quantity);
            _repository.PutCart(cart);
        }

        [HttpDelete]
        [AcceptVerbs("DELETE", "POST")]
        public ActionResult RemoveItem(string guid, int articleId, int quantity)
        {
            return View(RemoveItemCart(guid, articleId, quantity));
        }

        [EnableJson]
        [HttpDelete]
        [AcceptVerbs("DELETE")]
        public JsonResult RemoveItemJson(string guid, int articleId, int quantity)
        {
            return Json(RemoveItemCart(guid, articleId, quantity), JsonRequestBehavior.AllowGet);
        }

        private Cart RemoveItemCart(string guid, int articleId, int quantity)
        {
            var cart = this.GetCart(guid);
            cart.RemoveArticle(articleId, quantity);
            _repository.PutCart(cart);

            return cart;
        }

        [Authorize]
        public ActionResult Checkout(string guid)
        {
            return View(CartCheckout(guid));
        }

        [Authorize]
        [HttpPost]
        [EnableJson]
        public JsonResult CheckoutJson(string guid)
        {
            return Json(CartCheckout(guid), JsonRequestBehavior.AllowGet);
        }

        private Order CartCheckout(string guid)
        {
            var cart = _repository.Carts.FirstOrDefault(c => c.Guid == guid);

            if (cart == null)
            {
                ModelState.AddModelError("", "Cart not found.");
                RedirectToAction("Index", "Cart");

                return new Order();
            }

            var customer = _repository.Customers.FirstOrDefault(c => c.Email == User.Identity.Name);

            var order = new Order();
            order.Customer = customer;
            order.Items = cart.Items;
            order.Status = OrderStatus.WaitingPayment;
            order.DateTime = DateTime.Now;

            _repository.PutOrder(order);
            _repository.DeleteCart(cart);

            return order;
        }
    }
}
