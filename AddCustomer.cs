using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WFAWater.Helper;
namespace WFAWater
{
    public partial class AddCustomer : Form
    {
        public AddCustomer()
        {
            InitializeComponent();
          
        }
    
        private void AddOrUpdateCustomer_Load(object sender, EventArgs e)
        {

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            bool sonuc = MusteriEkle(txtAd.Text, txtSoyad.Text, txtTel.Text, txtAdres.Text);
            if (sonuc)
            {
                MessageBox.Show("Müşteri eklendi.");
               
            }
            else
            {
                MessageBox.Show("Müşteri eklenemedi.");
            }
            

        }
        public bool MusteriEkle(string ad,string soyad,string telefon,string adres)
        {
          
          
             bool   sonuc = CustomerHelper.CRUD(CustomerHelper.GetCustomer(ad,soyad,telefon,adres).ConvertToCustomer(), EntityState.Added);
             


      
            return sonuc;
        }

        private void AddOrUpdateCustomer_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmMain fm = new frmMain();
            fm.Show();

        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
