using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Entity
{
    public class DataContext: DbContext
    {
        public DataContext() : base("dataConnection") // buraya base vermezsek veritabanına gidip varsayılan konumda ve isimde database oluşturur. dolayısıyla base ile kendimiz isim veriyoruz
        {
            // Database.SetInitializer(new DataInitializer()); // oluşturduğumuz datainitializer i devreye sokuyoruz. uygulama çalıştığı anda datainitializer daki test veriler otomatik olarak veritabanına eklenecek. Lakin bunu Global.asax ta da yapabiliriz
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
    }
}