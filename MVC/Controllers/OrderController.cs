using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TC.Models;
using TC_WebShopCaseMVC.DAO;

namespace TC_WebShopCaseMVC.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        // GET: ORder
        public ActionResult Index()
        {
            var customerOrders = DB.Instance.Orders.Where(o => o.Customer.Email == User.Identity.Name);
            return View(customerOrders);
        }

        // GET: ORder/Details/5
        public ActionResult Details(int protocol)
        {
            var order = DB.Instance.Orders.FirstOrDefault(o => o.Protocol == protocol);

            if (order == null)
                return View(new Order());

            if (order.Customer.Email != User.Identity.Name)
            {
                ModelState.AddModelError("", "This order does not belong to you");
                return View(new Order());
            }
            else
            {
                return View(order);
            }
        }
    }
}
