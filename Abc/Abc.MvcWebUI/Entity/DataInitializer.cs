using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Entity
{
    public class DataInitializer: DropCreateDatabaseIfModelChanges<DataContext> // eğer model değişmişse veritabanımı oluştur diyoruz. hangi veritabanını oluşturacak: datacontext in bize gösterdiği veritabanını oluşturacak yani base e yazdığımız
    {
        protected override void Seed(DataContext context) // test verileri oluşturuyoruz
        {
            List<Category> kategoriler = new List<Category>()
            {
                new Category() {Name="Kamera", Description="Kamera ürünleri"},
                new Category() {Name="Bilgisayar", Description="Bilgisayar ürünleri"},
                new Category() {Name="Elektronik", Description="Elektronik ürünleri"},
                new Category() {Name="Telefon", Description="Telefon ürünleri"},
                new Category() {Name="Beyaz Eşya", Description="Beyaz Eşya ürünleri"},
            };

            foreach (var item in kategoriler)
            {
                context.Categories.Add(item);
            }

            context.SaveChanges();

            List<Product> urunler = new List<Product>()
            {
                new Product(){Name ="Kamera Ürün 1",Description="Açıklama Kamera ürün 1",Price=2300,Stock=10,IsApproved=true,CategoryId=1, IsHome=true,Image="1.jpg"},
                new Product(){Name ="Kamera Ürün 2",Description="Açıklama Kamera ürün 2",Price=1300,Stock=8,IsApproved=true,CategoryId=1,Image="2.jpg"},
                new Product(){Name ="Kamera Ürün 3",Description="Açıklama Kamera ürün 3",Price=2500,Stock=6,IsApproved=true,CategoryId=1,Image="3.jpg"},

                new Product(){Name ="Bilgisayar Ürün 1",Description="Açıklama Bilgisayar ürün 1",Price=2300,Stock=10,IsApproved=true,CategoryId=2, IsHome=true,Image="4.jpg"},
                new Product(){Name ="Bilgisayar Ürün 2",Description="Açıklama Bilgisayar ürün 2",Price=1300,Stock=8,IsApproved=true,CategoryId=2,Image="5.jpg"},
                new Product(){Name ="Bilgisayar Ürün 3",Description="Açıklama Bilgisayar ürün 3",Price=2500,Stock=6,IsApproved=true,CategoryId=2,Image="6.jpg"},

                new Product(){Name ="Elektronik Ürün 1",Description="Açıklama Elektronik ürün 1",Price=2300,Stock=10,IsApproved=true,CategoryId=3, IsHome=true,Image="7.jpg"},
                new Product(){Name ="Elektronik Ürün 2",Description="Açıklama Elektronik ürün 2",Price=1300,Stock=8,IsApproved=true,CategoryId=3,Image="8.jpg"},
                new Product(){Name ="Elektronik Ürün 3",Description="Açıklama Elektronik ürün 3",Price=2500,Stock=6,IsApproved=true,CategoryId=3,Image="9.jpg"},

                new Product(){Name ="Telefon Ürün 1",Description="Açıklama Telefon ürün 1",Price=2300,Stock=10,IsApproved=true,CategoryId=4, IsHome=true,Image="10.jpg"},
                new Product(){Name ="Telefon Ürün 2",Description="Açıklama Telefon ürün 2",Price=1300,Stock=8,IsApproved=true,CategoryId=4,Image="11.jpg"},
                new Product(){Name ="Telefon Ürün 3",Description="Açıklama Telefon ürün 3",Price=2500,Stock=6,IsApproved=true,CategoryId=4,Image="12.jpg"},

                new Product(){Name ="Beyaz Eşya Ürün 1",Description="Açıklama Beyaz Eşya ürün 1",Price=2300,Stock=10,IsApproved=true,CategoryId=5, IsHome=true,Image="13.jpg"},
                new Product(){Name ="Beyaz Eşya Ürün 2",Description="Açıklama Beyaz Eşya ürün 2",Price=1300,Stock=8,IsApproved=true,CategoryId=5,Image="14.jpg"},
                new Product(){Name ="Beyaz Eşya Ürün 3",Description="Açıklama Beyaz Eşya ürün 3",Price=2500,Stock=6,IsApproved=true,CategoryId=5,Image="15.jpg"},

            };

            foreach (var item in urunler)
            {
                context.Products.Add(item);
            }

            context.SaveChanges();

            base.Seed(context);
        }
    }
}