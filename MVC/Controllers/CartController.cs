using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TC_WebShopCaseMVC.DAO;
using TC.Models;

namespace TC_WebShopCaseMVC.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index(string guid)
        {
            if (string.IsNullOrEmpty(guid))
                guid = (HttpContext.Session["CartGuid"] as string);

            if (string.IsNullOrEmpty(guid))
            {
                guid = Guid.NewGuid().ToString();
                DB.Instance.PutCart(new Cart(guid));
            }

            HttpContext.Session["CartGuid"] = guid;
            return View(DB.Instance.Carts.FirstOrDefault(c => c.Guid == guid));
        }

        [Authorize]
        public ActionResult Checkout()
        {
            string guid = (HttpContext.Session["CartGuid"] as string);
            var cart = DB.Instance.Carts.FirstOrDefault(c => c.Guid == guid);

            if (cart == null)
            {
                ModelState.AddModelError("", "Cart not found.");
                RedirectToAction("Index");
            }

            var customer = DB.Instance.Customers.FirstOrDefault(c => c.Email == User.Identity.Name);

            var order = new Order();
            order.Customer = customer;
            order.Items = cart.Items;
            order.Status = OrderStatus.WaitingPayment;
            order.DateTime = DateTime.Now;

            DB.Instance.PutOrder(order);
            HttpContext.Session["CartGuid"] = string.Empty;
            DB.Instance.DeleteCart(cart);

            //RedirectToAction("Details", "Order", new { protocol = order.Protocol });
            return View(order);
        }
    }
}
