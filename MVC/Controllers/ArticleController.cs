using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TC_WebShopCaseMVC.DAO;

namespace TC_WebShopCaseMVC.Controllers
{
    public class ArticleController : Controller
    {
        // GET: Article
        public ActionResult Index(int? pageNumber)
        {
            if (!pageNumber.HasValue)
                pageNumber = 1;

            return View(DB.Instance.Articles.Skip((pageNumber.Value - 1) * 10).Take(10));
        }
    }
}
