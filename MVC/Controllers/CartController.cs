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
            return View(DB.Instance.Carts.FirstOrDefault(c => c.Guid == guid).Items);
        }
        
    }
}
