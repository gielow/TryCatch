using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TC.Core;
using TC.Models;
using TC_WebShopCaseMVC.DAO;

namespace TC_WebShopCaseMVC.Controllers
{
    public class CustomerController : Controller
    {
        private IRepository _repository;

        public CustomerController(IRepository repository)
        {
            _repository = repository;
        }

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
                if (_repository.Customers.Exists(c => c.Email == model.Email && c.Password == model.Password))
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
            var customer = _repository.Customers.FirstOrDefault(c => c.Email == User.Identity.Name);
            return View(customer);
        }
        
        // GET: Customer/Create
        public ActionResult Create(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Create(Customer model, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_repository.Customers.Exists(c => c.Email.Equals(model.Email)))
                    {
                        ModelState.AddModelError("", "This email address is already registered.");
                    }
                    else
                    {
                        _repository.PutCustomer(model);
                        FormsAuthentication.SetAuthCookie(model.Email, false);
                        return RedirectToLocal(returnUrl);
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
            return RedirectToAction("Index", "Article");
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
