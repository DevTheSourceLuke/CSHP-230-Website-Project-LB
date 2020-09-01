using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LearningCenter.Business;
using LearningCenter.WebSite.Models;

namespace LearningCenter.WebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClassManager classManager;
        private readonly IUserManager userManager;

        public HomeController(IClassManager classManager, IUserManager userManager)
        {
            this.classManager = classManager;
            this.userManager = userManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult LogIn(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LoginModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.LogIn(loginModel.Email, loginModel.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "Email and password do not match.");
                }
                else
                {
                    Session["User"] = new Models.UserModel(user.Id, user.Email);

                    FormsAuthentication.SetAuthCookie(loginModel.Email, false);

                    return Redirect(returnUrl ?? "~/");
                }
            }

            return View(loginModel);
        }
        public ActionResult LogOff()
        {
            Session["User"] = null;
            System.Web.Security.FormsAuthentication.SignOut();

            return Redirect("~/");
        }

        [HttpGet]
        public ActionResult Register(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel registerModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.Register(registerModel.Email, registerModel.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "User name and password do not match.");
                }
                else
                {
                    Session["User"] = new Models.UserModel(user.Id, user.Email);

                    System.Web.Security.FormsAuthentication.SetAuthCookie(registerModel.Email, false);

                    return Redirect(returnUrl ?? "~/");
                }
            }

            return View(registerModel);
        }

        public ActionResult ClassList()
        {
            ViewBag.Message = "Class Listing.";

            var classes = classManager.Classes
                .Select(t => new Models.ClassModel(t.Id, t.Name, t.Description, t.Price))
                .ToArray();

            return View(classes);
        }
    }
}