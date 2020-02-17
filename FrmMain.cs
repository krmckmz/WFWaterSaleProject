using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WFAWater.Model;
using WFAWater.Helper;
using System.Data.Entity;
using WFAWater.Entity;

namespace WFAWater
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

        }
        int islemYapilacak;
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtAdi.Text.Length > 0 && txtSoyadi.Text.Length > 0)
            {
                var musteri = CustomerHelper.GetCustomerByName(txtAdi.Text.ToLower(), txtSoyadi.Text.ToLower());
                MusteriBas(musteri);

            }
            else
            {
                MusterileriBas(CustomerHelper.GetCustomers());
            }

        }
        public void SiparisleriBas(List<OrderModel>oml)
        {  
            dgvSiparis.Rows.Clear();
            foreach (var item in oml)
            {
                string durum="";
                switch (item.Durum)
                {
                    case 0:durum = Common.siparisDurumu.Hazırlanıyor.ToString();
                        break;
                    case 1:durum = Common.siparisDurumu.Yolda.ToString();
                        break;
                    case 2:durum = Common.siparisDurumu.TeslimEdildi.ToString();
                        break;
                    default:
                        break;
                }
                dgvSiparis.Rows.Add(item.ID, item.Customer.adi, item.Customer.soyadi, durum, item.Customer.adres, item.Tutar);
            }
            //for (int i = dgvSiparis.Rows.Count-1; i >-1; i--)
            //{
            //    DataGridViewRow row = dgvSiparis.Rows[i];
            //    if (!row.IsNewRow&&row.Cells[0].Value==null)
            //    {
            //        dgvSiparis.Rows.RemoveAt(i);
            //    }
            //}
        }
       
        public void MusteriBas(CustomerModel cm)
        {
            dgvMusteriler.Rows.Clear();
         
                dgvMusteriler.Rows.Add(cm.ID, cm.adi, cm.soyadi, cm.telefon, cm.adres);
        

        }

        public void MusterileriBas(List<CustomerModel> cml)
        {
            dgvMusteriler.Rows.Clear();
            foreach (var item in cml)
            {
                
                    dgvMusteriler.Rows.Add(item.ID, item.adi, item.soyadi, item.telefon, item.adres);
              

            }
            //for (int i = 1; i <dgvMusteriler.Rows.Count-1; i++)
            //{
                
            //    if (dgvMusteriler.Rows[i].Cells[0].Value.ToString() == "")
            //    {
            //        dgvMusteriler.Rows.RemoveAt(i);
            //    }
            //}
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {

            this.Hide();
            AddCustomer f = new AddCustomer();

            f.Show();


        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            MusterileriBas(CustomerHelper.GetCustomers());
            SiparisleriBas(OrderHelper.GetOrders());

        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {

            islemYapilacak = (int)dgvMusteriler.CurrentRow.Cells["customerID"].Value;
            this.Hide();
            UpdateCustomer f = new UpdateCustomer(islemYapilacak);

            f.Show();

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            islemYapilacak = (int)dgvMusteriler.CurrentRow.Cells["customerID"].Value;
            bool sonuc = CustomerHelper.CRUD(SilinecekOlustur(islemYapilacak), EntityState.Modified);
            if (sonuc)
            {
                MessageBox.Show("Müşteri silindi.");
                MusterileriBas(CustomerHelper.GetCustomers());
            }
            else
            {
                MessageBox.Show("Müşteri silinemedi.");
            }
        }
        public Customer SilinecekOlustur(int islemYapilacak)
        {
            var c = CustomerHelper.GetCustomerByID(islemYapilacak);
            c.aktifMi = false;
            return c;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SiparisleriBas(OrderHelper.GetTodaysOrders());
        }

        private void btnSiparisSil_Click(object sender, EventArgs e)
        {
            int silinecek = (int)dgvSiparis.CurrentRow.Cells["siparisID"].Value;
            bool sonuc = OrderHelper.CRUD(SilinecekAl(silinecek).ConvertToOrder(), EntityState.Modified);
            if (sonuc)
            {
                MessageBox.Show("Sipariş silindi");
            }
            else
            {
                MessageBox.Show("Sipariş silinemedi.");
            }
            SiparisleriBas(OrderHelper.GetOrders());
        }
        public OrderModel SilinecekAl(int silinecek)
        {
            var x= OrderHelper.GetOrderByID(silinecek);
            x.isDeleted = true;
            return x;
            
        }

        private void btnTumuSil_Click(object sender, EventArgs e)
        {
        
            var liste = GetOrdersToDelete(OrderHelper.GetOrders());
            try
            {
                foreach (var item in liste)
                {
                    OrderHelper.CRUD(item.ConvertToOrder(), EntityState.Modified);                 
                }
                MessageBox.Show("Tüm siparişler silindi.");
                SiparisleriBas(OrderHelper.GetOrders());

            }
            catch (Exception)
            {
                MessageBox.Show("Siparişler silinirken hata oluştu.");
                throw;
            }
              
            
            
        }
        public List<OrderModel> GetOrdersToDelete(List<OrderModel>oml)
        {
           
            foreach (var item in oml)
            {
                item.isDeleted = true;

            }
            return oml;
        }

        private void btnSiparisEkle_Click(object sender, EventArgs e)
        {
            int siparisMusterisi = (int)dgvMusteriler.CurrentRow.Cells["customerID"].Value;
            this.Hide();
            AddOrder fao = new AddOrder(siparisMusterisi);
            fao.Show();

        }

        private void btnYolda_Click(object sender, EventArgs e)
        {
            int islemYapilacak =(int) dgvSiparis.CurrentRow.Cells["siparisID"].Value;          
            if (SiparisYolda(islemYapilacak))
            {
                MessageBox.Show("Sipariş durumu güncellendi.");
            }
            else
            {
                MessageBox.Show("Sipariş durumu güncellenemedi.");
            }
            SiparisleriBas(OrderHelper.GetOrders());
        }
        public bool SiparisYolda(int islemYapilacak)
        {
            var x = OrderHelper.GetOrderByID(islemYapilacak);
            x.Durum = 1;
            return OrderHelper.CRUD(x.ConvertToOrder(), EntityState.Modified);
        }

        private void btnTeslimEdildi_Click(object sender, EventArgs e)
        {
            int islemYapilacak = (int)dgvSiparis.CurrentRow.Cells["siparisID"].Value;
            if (SiparisTeslimEdildi(islemYapilacak))
            {
                MessageBox.Show("Sipariş durumu güncellendi.");
            }
            else
            {
                MessageBox.Show("Sipariş durumu güncellenemedi.");

            }
            SiparisleriBas(OrderHelper.GetOrders());
        }
        public bool SiparisTeslimEdildi(int islemYapilacak)
        {
            var x = OrderHelper.GetOrderByID(islemYapilacak);
            x.Durum = 2;
            return OrderHelper.CRUD(x.ConvertToOrder(), EntityState.Modified);
        }
    }
}
