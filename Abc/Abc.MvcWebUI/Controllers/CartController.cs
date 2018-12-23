using Abc.MvcWebUI.Entity;
using Abc.MvcWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Abc.MvcWebUI.Controllers
{
    public class CartController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Cart
        public ActionResult Index()
        {
            return View(GetCart());
        }

        public ActionResult AddToCart(int id) // sepete ürün ekliyoruz
        {
            var product = db.Products.Where(x => x.Id == id).FirstOrDefault();

            if (product != null)
            {
                GetCart().AddProduct(product, 1);
            }

            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int id) // sepeteki ürünü siliyoruz
        {
            var product = db.Products.Where(x => x.Id == id).FirstOrDefault();

            if (product != null)
            {
                GetCart().DeleteProduct(product);
            }

            return RedirectToAction("Index");
        }

        public Cart GetCart() // session her kullanıcıya oluştrulan bir depo. yani kullanıcıya özel sepet oluşturuyoruz
        {
            var cart = (Cart)Session["Cart"];

            if (cart == null) // kulanıcıya ait bir session yoksa yeni oluştur diyoruz
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }

            return cart;
        }

        public PartialViewResult _Summary()
        {
            return PartialView(GetCart());
        }

        public ActionResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ActionResult Checkout(ShippingDetails shippingModel)
        {
            var cart = GetCart(); // sepet bilgisi

            if (cart.CartLines.Count == 0) // sepette ürün yoksa
            {
                ModelState.AddModelError("", "Sepetinizde ürün bulunmamaktadır");
            }

            if (ModelState.IsValid)
            {
                // siparişi veritabanına kayıt ediyoruz
                // kayıt işleminden sonra sepeti sıfırlıyoruz. bunarı cart ta oluşturduğumuz metotla yapıyoruz
                SaveOrder(cart, shippingModel);

                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingModel);
            }
        }

        private void SaveOrder(Cart cart, ShippingDetails shippingModel)
        {
            var order = new Order()
            {
                OrderNumber = "A" + (new Random().Next(111111,999999).ToString()),
                Total = cart.Total(),
                OrderDate = DateTime.Now,
                OrderState = EnumOrderState.Waiting,
                UserName = User.Identity.Name,
                AdresBasligi = shippingModel.AdresBasligi,
                Adres = shippingModel.Adres,
                Sehir = shippingModel.Sehir,
                Semt = shippingModel.Semt,
                Mahalle = shippingModel.Mahalle,
                PostaKodu = shippingModel.PostaKodu,
                
                OrderLines = new List<OrderLine>()
            };
            
            foreach (var item in cart.CartLines)
            {
                var orderLine = new OrderLine()
                {
                    Quantity = item.Quantity,
                    Price = item.Quantity * item.Product.Price,
                    ProductId = item.Product.Id,

                };

                order.OrderLines.Add(orderLine);
            }

            db.Orders.Add(order);
            db.SaveChanges();
        }
    }
}