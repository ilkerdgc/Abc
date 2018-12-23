using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Entity
{
    public class Order // sipariş temel bilgileri tablosu
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public double Total { get; set; }
        public DateTime OrderDate { get; set; }
        public EnumOrderState OrderState { get; set; }

        public string UserName { get; set; }
        public string AdresBasligi { get; set; }
        public string Adres { get; set; }
        public string Sehir { get; set; }
        public string Semt { get; set; }
        public string Mahalle { get; set; }
        public string PostaKodu { get; set; }

        public List<OrderLine> OrderLines { get; set; }
    }

    public class OrderLine // sipariş içindeki her bir eleman tablosu
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        public virtual Order Order { get; set; } // lazy loading kavramını etkin hale getirmek için virtual diyoruz

        public int Quantity { get; set; }
        public double Price { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}