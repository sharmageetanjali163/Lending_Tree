using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity;
using System.Net;
using System.Dynamic;
using LendingTree.Models;

namespace LendingTree.Controllers
{
    public class AdminController : Controller
    {
        private LendingContext db = new LendingContext();

        [HttpGet]
        public ActionResult AdminLogin()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminLogin(Admin adminLogin, string returnUrl = "")
        {
            string message = "";
            ViewBag.Message = message;
            using (var context = new LendingContext())
            {
                var entity = context.Admins.FirstOrDefault(x => x.AdminId == adminLogin.AdminId);

                if (entity != null)
                {
                    if (string.Compare(entity.Password, adminLogin.Password) == 0)
                    {
                        int timeout = adminLogin.RememberMe ? 525600 : 120;
                        var ticket = new FormsAuthenticationTicket(adminLogin.AdminId, adminLogin.RememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);

                        if (Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("AdminIndex", "Admins");
                        }
                    }
                    else
                    {
                        message = "Password not matching";
                    }
                }
                else
                {
                    message = "Admin ID not Present";
                }
            }

            ViewBag.Message = message;

            return View();
        }


        public ActionResult AdminIndex()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Admin");
        }
    }
}
