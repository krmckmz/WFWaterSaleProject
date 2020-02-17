using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFAWater.Entity;
using WFAWater.Model;

namespace WFAWater.Helper
{
    public static class OrderHelper
    {
        public static List<OrderModel> GetOrders()
        {
            List<OrderModel> oml=new List<OrderModel>();
            using (SuEntities db=new SuEntities())
            {
                OrderModel om=new OrderModel();
                var liste = db.Order.Where(o=>o.isDeleted==false).ToList();
                foreach (var item in liste)
                {
                    om = item.ConvertToOrderModel();
                    oml.Add(om);
                }
         
            }
            return oml;
        }
        public static List<OrderModel>GetTodaysOrders()
        {
            List<OrderModel> oml = new List<OrderModel>();
            using (SuEntities db=new SuEntities())
            {
                OrderModel om=new OrderModel();
                //var liste = db.Order.Where(x => x.KayitTarihi == DateTime.Today&&x.isDeleted==false).ToList();
                var liste = db.Order.Where(x => x.isDeleted == false).ToList();
                foreach (var item in liste)
                {
                    if (item.KayitTarihi.Date==DateTime.Today.Date)
                    {
                        om = item.ConvertToOrderModel();
                        oml.Add(om);
                    }
                  

                }
               
            }
            return oml;
        }
        public static bool CRUD(Order o,EntityState state)
        {
            bool sonuc;
            using (SuEntities db=new SuEntities())
            {
                db.Entry(o).State = state;
                if (db.SaveChanges()>0)
                {
                    sonuc = true;
                }
                else
                {
                    sonuc = false;
                }
            }
            return sonuc;
        }
        public static bool AddOrder(Order o)
        {
            bool sonuc;
            using (SuEntities db=new SuEntities())
            {
                db.Order.Add(o);
                if (db.SaveChanges()>0)
                {
                    sonuc = true;
                }
                else
                {
                    sonuc = false;
                }
            }
            return sonuc;
        }
        public static OrderModel GetOrderByID(int ID)
        {
            using (SuEntities db=new SuEntities())
            {
                return db.Order.Find(ID).ConvertToOrderModel();

            }
        }
        public static OrderModel GetOrderToAdd(int customerID,decimal tutar)
        {
            OrderModel om = new OrderModel();
           
            om.Tutar = tutar;
            om.Durum = 0;
            om.isDeleted = false;
            om.KayitTarihi = DateTime.Now;
            using (SuEntities db = new SuEntities())
            {
                var x = db.Customer.Where(y => y.ID == customerID).First();
                om.Customer = x.ConvertToCustomerModel();
                om.CustomerID = x.ID;
            }
            return om;
        }
    }
}
