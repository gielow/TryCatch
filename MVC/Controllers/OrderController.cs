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
        
        [HttpGet, EnableJson]
        public ActionResult Index()
        {
            return View(_repository.Orders.Where(o => o.Customer.Email == User.Identity.Name));
        }

        [HttpGet, EnableJson]
        public ActionResult Details(int protocol)
        {
            var order = _repository.Orders.FirstOrDefault(o => o.Protocol == protocol);

            if (order == null)
                return View(new Order());

            if (order.Customer.Email != User.Identity.Name)
            {
                ModelState.AddModelError("", "This order does not belong to you");
                return View(new Order());
            }

            return View(order);

        }
    }
}
