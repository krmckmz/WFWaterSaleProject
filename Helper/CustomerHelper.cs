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
    public class CustomerHelper
    {
        public static CustomerModel GetCustomerByName(string ad, string soyad)
        {
            CustomerModel cm = new CustomerModel();
            using (SuEntities db = new SuEntities())
            {

                cm = db.Customer.Where(c => c.adi == ad && c.soyadi == soyad&&c.aktifMi==true).First().ConvertToCustomerModel();

            }
            return cm;
        }
        public static CustomerModel GetCustomerToUpdate(int islemYapilacak,string adi,string soyadi,string tel,string adres)
        {
            CustomerModel cm = new CustomerModel();
          
          
            cm.ID = islemYapilacak;
            cm.adi = adi;
            cm.soyadi = soyadi;
            cm.telefon = tel;
            cm.adres = adres;
            cm.aktifMi =true;
            return cm;
        }    

     
        public static Customer GetCustomerByID(int ID)
        {
            Customer c;
            using (SuEntities db = new SuEntities())
            {
                 c=db.Customer.Find(ID);
             

            }
            return c;

        }
        public static bool CRUD(Customer c,EntityState state)
        {
            bool sonuc;
            using (SuEntities db=new SuEntities())
            {
                db.Entry(c).State = state;
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
        public static CustomerModel GetCustomer(string ad, string soyad, string tel, string adres)
        {
            CustomerModel cm = new CustomerModel();
            cm.adi = ad.ToLower();
            cm.soyadi = soyad.ToLower();
            cm.telefon = tel;
            cm.adres = adres.ToLower();
            cm.aktifMi = true;
            return cm;
        }
        public static List<CustomerModel> GetCustomers()
        {
            List<CustomerModel> cml = new List<CustomerModel>();
            using (SuEntities db=new SuEntities())
            {
                var x = db.Customer.Where(c=>c.aktifMi==true);
                foreach (var item in x)
                {
                    CustomerModel cm = item.ConvertToCustomerModel();
                    cml.Add(cm);
                }

            }
            return cml;
        }

    }
}
