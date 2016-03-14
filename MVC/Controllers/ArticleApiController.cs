using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TC_WebShopCaseMVC.DAO;
using TC.Models;

namespace TC_WebShopCaseMVC.Controllers
{
    [RoutePrefix("api/Article")]
    public class ArticleApiController : ApiController
    {
        // GET: api/Artcile
        public IEnumerable<Article> Get(int? pageNumber)
        {
            if (!pageNumber.HasValue)
                pageNumber = 1;

            return DB.Instance.Articles.Skip((pageNumber.Value - 1) * 10).Take(10);
        }
        
        [HttpGet]
        [Route("{page}/{number:int}")]
        public IEnumerable<Article> Page(int? number)
        {
            if (!number.HasValue)
                number = 1;

            return DB.Instance.Articles.Skip((number.Value - 1) * 10).Take(10);
        }

        // GET: api/Artcile/5
        public IEnumerable<Article> Get(int? pageNumber, int? pageSize)
        {
            if (!pageNumber.HasValue)
                pageNumber = 1;

            if (!pageSize.HasValue)
                pageSize = 10;

            return DB.Instance.Articles.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
        }
    }
}
