﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TC.Helper
{
    // Source: https://code.msdn.microsoft.com/Build-truly-RESTful-API-194a6253
    public class EnableJsonAttribute : ActionFilterAttribute
    {
        private readonly static string[] _jsonTypes = new string[] { "application/json", "text/json" };

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (typeof(RedirectToRouteResult).IsInstanceOfType(filterContext.Result))
                return;

            //var acceptTypes = new string[] { filterContext.HttpContext.Request.ContentType };
            var acceptTypes = filterContext.HttpContext.Request.AcceptTypes;
            //var acceptTypes = filterContext.HttpContext.Request.AcceptTypes ?? _jsonTypes;

            var model = filterContext.Controller.ViewData.Model;

            var contentEncoding = filterContext.HttpContext.Request.ContentEncoding ?? Encoding.UTF8;

            if (_jsonTypes.Any(type => acceptTypes.Contains(type)))
                filterContext.Result = new JsonResult2()
                {
                    Data = model,
                    ContentEncoding = contentEncoding,
                    ContentType = filterContext.HttpContext.Request.ContentType
                };
        }
    }
}
