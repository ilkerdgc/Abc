using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Entity
{
    public class Product
    {
        public int Id { get; set; }

        [DisplayName("Ürün Adı")]
        public string Name { get; set; } // ürün ismi
        [DisplayName("Ürün Açıklaması")]
        public string Description { get; set; } // ürün açıklama
        [DisplayName("Ürün Fİyatı")]
        public double Price { get; set; } // ürün fiyatı
        public int Stock { get; set; } // ürün stok miktarı
        public string Image { get; set; }
        public bool IsHome { get; set; } // anasayfada gösterilecek ürünler
        public bool IsApproved { get; set; } // ürün onaylı ürün mü yani true ise ürün satışta eğer false ise ürün listelenmeyecek kullanıcı göremeyecek

        public int CategoryId { get; set; } // foreing key
        public Category Category { get; set; } // her ürünün bir kategorisi olabilir
    } 
}