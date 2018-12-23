using Abc.MvcWebUI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Models
{
    public class Cart // alışveriş sepetinin tamamını temsil ediyor ve sepet işlemleri için metotlar oluşturuyoruz
    {
        private List<CartLine> _cardLines = new List<CartLine>();
        public List<CartLine> CartLines
        {
            get
            {
                return _cardLines;
            }
        }

        public void AddProduct(Product product, int quantity)
        {
            var line = _cardLines.Where(x => x.Product.Id == product.Id).FirstOrDefault(); // eklenecek ürün var mı yok mu kontrol ediyoruz

            if (line == null) // eğer ürün varsa sadece sayısını artır yoksa ekle
            {
                _cardLines.Add(new CartLine()
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }

        }

        public void DeleteProduct(Product product) // gönderilen ürünü silmek için
        {
            _cardLines.RemoveAll(x => x.Product.Id == product.Id);
        }

        public double Total() // sepetteki ürünlerin toplam fiyatı
        {
            return _cardLines.Sum(x => x.Product.Price * x.Quantity);
        }

        public void Clear() // sepeti boşaltmak için
        {
            _cardLines.Clear();
        }
    }

    public class CartLine // alışveriş sepetindeki her bir satırı temsil ediyor
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}