using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        public string Name { get; set; } // ürün ismi
        public string Description { get; set; } // ürün açıklama
        public double Price { get; set; } // ürün fiyatı
        public int Stock { get; set; } // ürün stok miktarı
        public string Image { get; set; }

        public int CategoryId { get; set; } // foreing key
    }
}