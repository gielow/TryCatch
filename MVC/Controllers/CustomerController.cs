using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TC.Models;
using TC_WebShopCaseMVC.DAO;

namespace TC_WebShopCaseMVC.Controllers
{
    public class CustomerController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(CustomerLoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (DB.Instance.Customers.Exists(c => c.Email == model.Email && c.Password == model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password.");
                }
            }

            return View(model);
        }

        [Authorize]
        public ActionResult Details()
        {
            var customer = DB.Instance.Customers.FirstOrDefault(c => c.Email == User.Identity.Name);
            return View(customer);
        }

        /*[HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            //if (ModelState.IsValid)
            //{
            //var usuario = repositorio.Buscar<Usuario>(x => x.Email == model.Email && x.Senha == model.Password);
            if (model.Email == "andre.gielow@gmail.com")
            {
                FormsAuthentication.SetAuthCookie(model.Email, false);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                ModelState.AddModelError("", "Usuário ou senha invalido.");
            }
            //}

            return View(model);
        }*/
        
        
        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Create(Customer model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (DB.Instance.Customers.Exists(c => c.Email.Equals(model.Email)))
                    {
                        ModelState.AddModelError("", "This email address is already registered.");
                    }
                    else
                    {
                        DB.Instance.PutCustomer(model);
                        FormsAuthentication.SetAuthCookie(model.Email, false);
                    }

                    return RedirectToAction("Details");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", string.Format("Error at creating customer: {0}", ex.Message));
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        
        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion
    }
}
