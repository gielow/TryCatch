using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;
using TC.Helper;
using TC.IoC;

namespace TC_WebShopCaseMVC
{
    public class WebApiApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            IoCConfig.Register();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Source: http://haacked.com/archive/2010/04/15/sending-json-to-an-asp-net-mvc-action-method-argument.aspx 
            // This must be added to accept JSON as request 
            ValueProviderFactories.Factories.Add(new JsonValueProviderFactory());
            // This must be added to accept XML as request 
            // Source: http://www.nogginbox.co.uk/blog/xml-to-asp.net-mvc-action-method 
            ValueProviderFactories.Factories.Add(new XmlValueProviderFactory());
        }
        
    }
}
