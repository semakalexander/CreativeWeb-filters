using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CreativeWeb.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account     

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(string Name, string Password)
        {
            if (FormsAuthentication.Authenticate(Name, Password))
                FormsAuthentication.RedirectFromLoginPage(Name, false);
            TempData["error"] = "Неправильно введено дані";
            return View();
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}