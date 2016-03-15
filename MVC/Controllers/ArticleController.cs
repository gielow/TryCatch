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
    public class ArticleController : Controller
    {
        private IRepository _repository;

        public ArticleController(IRepository repository)
	    {
            _repository = repository;
	    }
        
        [HttpGet]
        public ActionResult Index(int? number)
        {
            return View(GetPage(number));
        }
        
        [EnableJson]
        public JsonResult IndexJson(int? number)
        {
            return Json(GetPage(number), JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<Article> GetPage(int? number)
        {
            if (!number.HasValue)
                number = 1;

            return _repository.Articles.Skip((number.Value - 1) * 10).Take(10);
        }
    }
}
