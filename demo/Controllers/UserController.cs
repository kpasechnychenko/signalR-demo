using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Model.Interfaces.Domain;
using Model.Model;
using System.Web.Security;
using System.ComponentModel.DataAnnotations;

namespace demo.Controllers
{
    public class UserController : Controller
    {
        public IUserModel UserModel { get; set; }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [ActionName("Login"), HttpPost]
        [AllowAnonymous]
        public ActionResult Login(ViewLogin info, string returnUrl)
        {
            if (!UserModel.LoginUser(info))
            {
                ModelState.AddModelError("", "Incorrect login or password");
                info.Password = "";
                return View(info);
            }

            FormsAuthentication.SetAuthCookie(info.UserName, false);
            if (string.IsNullOrEmpty(returnUrl) || returnUrl == "/")
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToRoute(returnUrl);  
        }
        
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View("CreateUser");
        }

        [ActionName("Register"), HttpPost]
        [AllowAnonymous]
        public ActionResult Register(ViewRegisterUser user)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    UserModel.CreateUser(user);
                }
                catch
                {
                    ModelState.AddModelError("", "User already exists");
                    user.Password = "";
                    return View("CreateUser", user);
                }
            }
            FormsAuthentication.SetAuthCookie(user.UserName, false);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit()
        {
            var usr = UserModel.GetUserByName(User.Identity.Name);
            return View("EditUser", usr);
        }

        [ActionName("Edit"), HttpPost]
        [Authorize]
        public ActionResult Edit(ViewRegisterUser user)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    UserModel.UpdateUser(user);
                }
                catch
                {
                    ModelState.AddModelError("", "Something went wrong. Please try again.");
                    user.Password = "";
                    return View("EditUser", user);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }

    }
}
