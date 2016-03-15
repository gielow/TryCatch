using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TC.Core;
using TC.Helper;
using TC.Models;
using TC_WebShopCaseMVC.DAO;

namespace TC_WebShopCaseMVC.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private IRepository _repository;

        public OrderController(IRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet]
        public ActionResult Index()
        {
            return View(GetOrders());
        }

        [EnableJson]
        public JsonResult IndexJson()
        {
            return Json(GetOrders(), JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<Order> GetOrders()
        {
            return _repository.Orders.Where(o => o.Customer.Email == User.Identity.Name);
        }

        [HttpGet]
        public ActionResult Details(int protocol)
        {
            return View(GetOrder(protocol));
        }
        
        [EnableJson]
        public JsonResult DetailsJson(int protocol)
        {
            return Json(GetOrder(protocol), JsonRequestBehavior.AllowGet);
        }

        private Order GetOrder(int protocol)
        {
            var order = _repository.Orders.FirstOrDefault(o => o.Protocol == protocol);

            if (order == null)
                return new Order();

            if (order.Customer.Email != User.Identity.Name)
            {
                ModelState.AddModelError("", "This order does not belong to you");
                return new Order();
            }
            else
            {
                return order;
            }
        }
    }
}
