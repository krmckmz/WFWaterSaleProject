using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFAWater.Entity;
using WFAWater.Model;

namespace WFAWater.Helper
{
    public static class Ext
    {
        public static CustomerModel ConvertToCustomerModel(this Customer c)
        {
            CustomerModel cm = new CustomerModel();
            cm.ID = c.ID;
            cm.adi = c.adi;
            cm.soyadi = c.soyadi;
            cm.telefon = c.telefon;
            cm.adres = c.adres;
            cm.aktifMi = c.aktifMi;
            
            return cm;
           
        }
        public static Customer ConvertToCustomer(this CustomerModel cm)
        {
            Customer c = new Customer();
            c.ID = cm.ID;
            c.adi = cm.adi;
            c.soyadi = cm.soyadi;
            c.telefon = cm.telefon;
            c.adres = cm.adres;
            c.aktifMi = cm.aktifMi;
            return c;
        }
        public static OrderModel ConvertToOrderModel(this Order o)
        {
            OrderModel om = new OrderModel();
            om.ID = o.ID;
            om.Durum = o.Durum;
            om.CustomerID = o.CustomerID;
            om.Customer = o.Customer.ConvertToCustomerModel();
            om.Customer.ID = o.Customer.ID;
            om.Tutar = o.Tutar;
            om.KayitTarihi = o.KayitTarihi;
            om.isDeleted = o.isDeleted;
          
            return om;
        }
        public static Order ConvertToOrder(this OrderModel om)
        {
            Order o = new Order();
            o.ID = om.ID;
            o.Durum = om.Durum;
            o.CustomerID = om.CustomerID;
            //o.Customer = om.Customer.ConvertToCustomer();
            //o.Customer.ID = om.Customer.ID;
            o.Tutar = om.Tutar;
            o.KayitTarihi = om.KayitTarihi;
            o.isDeleted = om.isDeleted;
           
            return o;
        }
    }
}
