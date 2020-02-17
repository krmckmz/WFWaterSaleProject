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
    public partial class UpdateCustomer : Form
    {
        public UpdateCustomer(int islemYapilacak)
        {
            InitializeComponent();
            this.islemYapilacak = islemYapilacak;
        }
        int islemYapilacak;
        private void UpdateCustomer_Load(object sender, EventArgs e)
        {

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            bool sonuc=Guncelle(islemYapilacak, txtAd.Text, txtSoyad.Text, txtTel.Text, txtAdres.Text);
            if (sonuc)
            {
                MessageBox.Show("Ürün güncellendi.");
            }
            else
            {
                MessageBox.Show("Ürün güncellenemedi.");
            }
        }
        public bool Guncelle(int islemYapilacak,string ad,string soyad,string telefon,string adres)
        {
            bool sonuc = CustomerHelper.CRUD(CustomerHelper.GetCustomerToUpdate(islemYapilacak, ad, soyad, telefon, adres).ConvertToCustomer(), EntityState.Modified);
            return sonuc;
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
          
        }

        private void UpdateCustomer_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmMain fm = new frmMain();
            fm.Show();
        }
    }
}
