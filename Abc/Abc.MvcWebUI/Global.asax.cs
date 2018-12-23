using Abc.MvcWebUI.Entity;
using Abc.MvcWebUI.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Abc.MvcWebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // uygulamanın çalıştırılma yeri burası 

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Database.SetInitializer(new DataInitializer()); // oluşturduğumuz DataInitializer i devreye sokuyoruz. uygulama çalıştığı anda DataInitializer daki test veriler otomatik olarak veritabanına eklenecek
            Database.SetInitializer(new IdentityInitializer()); // oluşturduğumuz IdentityInitializer i devreye sokuyoruz.uygulama çalıştığı anda IdentityInitializer daki test veriler otomatik olarak veritabanına eklenecek

            // tabi hepsinin çalıştırılması gerekmiyor. yani DataInitializer ın çalışması için database model de değişiklik olması gerekiyor.
            // IdentityInitializer ın çalışması için ise eğer veritabanı yoksa oluşturuluyor
        }
    }
}
