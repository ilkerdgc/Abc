using Abc.MvcWebUI.Entity;
using Abc.MvcWebUI.Identity;
using Abc.MvcWebUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Abc.MvcWebUI.Controllers
{
    public class AccountController : Controller
    {
        private DataContext db = new DataContext();

        private UserManager<ApplicationUser> userManager;
        private RoleManager<ApplicationRole> roleManager;

        public AccountController()
        {
            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            userManager = new UserManager<ApplicationUser>(userStore);
            RoleStore<ApplicationRole> roleStore = new RoleStore<ApplicationRole>(new IdentityDataContext());
            roleManager = new RoleManager<ApplicationRole>(roleStore);
        }
        [Authorize]
        public ActionResult Index()
        {
            var orders = db.Orders.Where(x => x.UserName == User.Identity.Name).Select(x => new UserOrderModel()
            {
                Id = x.Id,
                OrderNumber = x.OrderNumber,
                OrderDate = x.OrderDate,
                OrderState = x.OrderState,
                Total = x.Total
            }).OrderByDescending(x => x.OrderDate).ToList();

            return View(orders);
        }

        [Authorize]
        public ActionResult OrderDetails(int id)
        {
            var entity = db.Orders.Where(x => x.Id == id).Select(x => new OrderDetailsModel()
            {
                OrderId = x.Id,
                OrderNumber = x.OrderNumber,
                Total = x.Total,
                OrderDate = x.OrderDate,
                OrderState = x.OrderState,
                AdresBasligi = x.AdresBasligi,
                Adres = x.Adres,
                Sehir = x.Sehir,
                Semt = x.Semt,
                Mahalle = x.Mahalle,
                PostaKodu = x.PostaKodu,
                OrderLines = x.OrderLines.Select(i => new OrderDetailsLineModel()
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product.Name.Length > 50 ? i.Product.Name.Substring(0,47) + "..." : i.Product.Name,
                    Image = i.Product.Image,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            }).FirstOrDefault();

            return View(entity);
        }

        // GET: Account
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register registerModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.Name = registerModel.Name;
                user.Surname = registerModel.SurName;
                user.Email = registerModel.Email;
                user.UserName = registerModel.UserName;

                IdentityResult identityResult = userManager.Create(user, registerModel.Password);

                if (identityResult.Succeeded)
                {
                    //Kullanıcı oluştu kulanıcıyı bşr role atıyabilirsiniz.
                    if (roleManager.RoleExists("user"))
                    {
                        userManager.AddToRole(user.Id, "user");
                    }
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("RegisterUserError", "Kullanıcı oluşturma hatası");
                }
            }
            return View(registerModel);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login loginModel, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                // Login işlemleri
                var user = userManager.Find(loginModel.UserName, loginModel.Password);

                if (user != null)
                {
                    // varolan kullanıcıyı sisteme dahil et
                    // applicationCookie oluşturup sisteme bırak
                    var autManager = HttpContext.GetOwinContext().Authentication;
                    var identityclaims = userManager.CreateIdentity(user, "ApplicationCookie");
                    var autProperties = new AuthenticationProperties();
                    autProperties.IsPersistent = loginModel.RememberMe;

                    autManager.SignIn(autProperties, identityclaims);

                    if (!String.IsNullOrEmpty(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("LoginUserError", "Böyle bir kullanıcı yok");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            var autManager = HttpContext.GetOwinContext().Authentication;
            autManager.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}