using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFAWater.Entity;

namespace WFAWater.Model
{
    public class OrderModel
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public int Durum { get; set; }
        public decimal Tutar { get; set; }
        public DateTime KayitTarihi { get; set; }
        public CustomerModel Customer = new CustomerModel();
        public bool isDeleted { get; set; }
    }
}
