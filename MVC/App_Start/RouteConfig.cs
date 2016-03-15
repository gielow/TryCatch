using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TC.Helper;

namespace TC_WebShopCaseMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Article", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute("OrderDetailsApi",
                "api/Order/{protocol}/Details",
                new { controller = "Order", action = "DetailsJson" });

            routes.MapRoute("OrderApi",
                "api/Order/Index",
                new { controller = "Order", action = "IndexJson" });
            
            routes.MapRoute("ArticleApi",
                "api/Article/Index/{number}",
                new { controller = "Article", action = "IndexJson" });

            routes.MapRoute("CartNewApi",
                "api/Cart/Index/New",
                new { controller = "Cart", action = "NewJson" });

            routes.MapRoute("CartApi",
                "api/Cart/Index/{guid}",
                new { controller = "Cart", action = "IndexJson" });

            routes.MapRoute("CartItemsApi",
                "api/Cart/{guid}/Items",
                new { controller = "Cart", action = "ItemsJson" });

            routes.MapRoute("CartICheckoutApi",
                "api/Cart/{guid}/Checkout",
                new { controller = "Cart", action = "CheckoutJson" });

            routes.MapRoute("CustomerLoginApi",
                "api/Customer/Login/{customer}",
                new { controller = "Customer", action = "LoginJson" });

            routes.MapRoute(null,
                "api/Cart/{guid}/Items/{articleId}/{quantity}",
                new { controller = "Cart", action = "AddItemJson" },
                new { httpMethod = new HttpMethodOverrideConstraint("POST", "PUT")
            });

            routes.MapRoute(null,
                "api/Cart/{guid}/Items/{articleId}/{quantity}",
                new { controller = "Cart", action = "RemoveItemJson" },
                new { httpMethod = new HttpMethodOverrideConstraint("POST", "DELETE")
            });

            /*routes.MapRoute(
                name: "Api",
                url: "api/{controller}/{action}",
                defaults: new {  });*/

        }
    }
}
