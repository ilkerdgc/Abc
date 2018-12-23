using Abc.MvcWebUI.Entity;
using Abc.MvcWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Abc.MvcWebUI.Controllers
{
    public class HomeController : Controller
    {
        DataContext _context = new DataContext();

        // GET: Home
        public ActionResult Index()
        {
            var urunler = _context.Products.Where(x => x.IsHome && x.IsApproved).Select(x => new ProductModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description.Length > 50 ? x.Description.Substring(0,47) + "..." : x.Description,
                Price = x.Price,
                Stock = x.Stock,
                Image = x.Image,
                CategoryId = x.CategoryId

            });

            return View(urunler);
        }

        public ActionResult Details(int id)
        {
            return View(_context.Products.Where(x => x.Id == id).FirstOrDefault());
        }

        public ActionResult List(int? id) // nullablle demek yani id gönderilmesi zorunlu değil. isteğe bağlı
        {
            var urunler = _context.Products.Where(x => x.IsApproved).Select(x => new ProductModel()
            {
                Id = x.Id,
                Name = x.Name.Length > 50 ? x.Name.Substring(0, 47) + "..." : x.Name,
                Description = x.Description.Length > 50 ? x.Description.Substring(0, 47) + "..." : x.Description,
                Price = x.Price,
                Stock = x.Stock,
                Image = x.Image ?? "1.jpg", // burada ki soru işaretlerinin anlamı aslında Ternary if i daha da kısalmaktır. yani image true ise databaseden gelen değeri atayacak eğer false ise varsayılan resmi 1.jpg yapacak
                CategoryId = x.CategoryId

            }).AsQueryable(); // AsQueryable mantığı veritabanındaki sorgunun şuanda gerçekten çalıştırılmamış olması. dolayısıyla tolist metodu kullanıldığında sorgu çalışacak. bunu yapmanın amacı sorgunun üzerine filtreleme eklemek

            if (id != null)
            {
                urunler = urunler.Where(x => x.CategoryId == id); // burdan kullanıcının id boş değilse bu filtreleme burada oluşturulup sorgunun üzerine eklenecek ve daha sonra view e gönderilecek. yani id null ise tüm ürünler listelencek, id dolu ise seçilen kategoriye ait ürünler listelencek
            }

            return View(urunler);
        }

        public PartialViewResult _GetCategories()
        {
            return PartialView(_context.Categories.ToList());
        }
    }
}