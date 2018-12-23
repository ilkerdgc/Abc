using Abc.MvcWebUI.Entity;
using Abc.MvcWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Abc.MvcWebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class OrderController : Controller
    {
        // Admin sipariş yötetim işlemleri için

        private DataContext db = new DataContext();
        // GET: Order
        public ActionResult Index()
        {
            var orders = db.Orders.Select(x => new AdminOrderModel()
            {
                Id = x.Id,
                OrderNumber = x.OrderNumber,
                OrderDate = x.OrderDate,
                OrderState = x.OrderState,
                Total = x.Total,
                Count = x.OrderLines.Count // siparişteki toplam ürün
            }).OrderByDescending(x => x.OrderDate).ToList();

            return View(orders);
        }

        public ActionResult Details(int id)
        {
            var entity = db.Orders.Where(x => x.Id == id).Select(x => new OrderDetailsModel()
            {
                OrderId = x.Id,
                UseName = x.UserName,
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
                    ProductName = i.Product.Name.Length > 50 ? i.Product.Name.Substring(0, 47) + "..." : i.Product.Name,
                    Image = i.Product.Image,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            }).FirstOrDefault();

            return View(entity);
        }

        public ActionResult UpdateOrderState(int orderId, EnumOrderState orderState)
        {
            var order = db.Orders.Where(x => x.Id == orderId).FirstOrDefault();

            if (order != null)
            {
                order.OrderState = orderState;
                db.SaveChanges();

                TempData["message"] = "Bilgileriniz kayıt edildi.";

                return RedirectToAction("Details", new { id = orderId }); // geldiğimiz sipariş detay sayfasına yönlendiriyoruz
            }

            return RedirectToAction("Index");

        }
    }
}