using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WarmCallApp.Controllers
{
    /// <summary>
    /// Create an ActionResult and PartialView for each angular partial view you want to attatch to a route in the angular app.js file.
    /// </summary>
    public class AppController : Controller
    {
        public ActionResult Register(int IsBuyer)
        {
            ViewBag.IsBuyer = IsBuyer;
            return PartialView("Register");
        }
        public ActionResult SignIn(int IsBuyer)
        {
            ViewBag.IsBuyer = IsBuyer;
            return PartialView("SignIn");
        }
        public ActionResult Home()
        {
            return PartialView();
        }

        [Authorize]
        public ActionResult TodoManager()
        {
            return PartialView();
        }
    }
}