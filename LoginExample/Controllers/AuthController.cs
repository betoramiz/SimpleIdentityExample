using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoginExample.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LoginExample.Controllers
{
    public class AuthController : Controller
    {
        private UserManager<UserProfile> userManager;
        private AppContext db;

        public AuthController()
        {
            db = new AppContext();
            userManager = new UserManager<UserProfile>(new UserStore<UserProfile>(db));
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            if(ModelState.IsValid)
            {
                var userFinded = userManager.Find(login.Mail,login.Password);
                if(userFinded != null){
                    var authenticationManager = HttpContext.GetOwinContext().Authentication;
                    var identity = userManager.CreateIdentity(userFinded, DefaultAuthenticationTypes.ApplicationCookie);

                    authenticationManager.SignOut();
                    authenticationManager.SignIn(new Microsoft.Owin.Security.AuthenticationProperties() { IsPersistent = true }, identity);

                    return RedirectToAction("Index");
                }
                return View();
                
            }

            return View();
            
        }

        [Authorize]
        public ActionResult SingOut()
        {
            var AutheticationManager = HttpContext.GetOwinContext().Authentication;
            AutheticationManager.SignOut();
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Create(RegisterVM newUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new UserProfile() { Name = newUser.Name, LastName = newUser.LastName, UserName = newUser.Email, Email = newUser.Email };
                    var result = userManager.Create(user, newUser.password);
                    if (result.Succeeded)
                        return RedirectToAction("Index");

                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error);
                }
                catch(Exception ex)
                {
                    string err = ex.Message;
                }
                
            }
            return View();
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult CrearEstado()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult CrearEstado(Estado estado)
        {
            try
            {
                db.Estado.Add(estado);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
            
        }
    }
}