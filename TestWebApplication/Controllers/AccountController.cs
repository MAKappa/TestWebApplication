using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestWebApplication.Models;
using System.Web.Security;

namespace TestWebApplication.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.Membership model)
        {
            using (var context = new TestAppDbEntities())
            {
                bool isValid = context.User.Any(x=>x.Username == model.Password && x.Password == model.Password );
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    return RedirectToAction("Index", "Patients");
                }
                ModelState.AddModelError("","Invalid username and password");
               
            }
            return View();
        }

        // GET: Account
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(User model)
        {
            using (var context=new TestAppDbEntities())
            {
                context.User.Add(model);
                context.SaveChanges();
            }
                return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}