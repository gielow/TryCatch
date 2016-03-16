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
        
        [HttpGet, EnableJson]
        public ActionResult Index(int? number)
        {
            if (!number.HasValue)
                number = 1;

            return View(_repository.Articles.Skip((number.Value - 1) * 10).Take(10).ToList());
        }
    }
}
