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
using WFAWater.Entity;
using WFAWater.Helper;
using WFAWater.Model;

namespace WFAWater
{
    public partial class AddOrder : Form
    {
        public AddOrder(int siparisMusterisi)
        {
            InitializeComponent();
            this.siparisMusterisi = siparisMusterisi;
        }
        int siparisMusterisi;
        private void AddOrder_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmMain fm = new frmMain();
            fm.Show();
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddOrder_Load(object sender, EventArgs e)
        {
           MusteriyiYaz(CustomerHelper.GetCustomerByID(siparisMusterisi));

            
        }
        public void MusteriyiYaz(Customer c)
        {
            txtAd.Text = c.adi;
            txtSoyad.Text = c.soyadi;
            txtAdres.Text = c.adres;
           
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                OrderHelper.AddOrder(OrderHelper.GetOrderToAdd(siparisMusterisi, Convert.ToDecimal(txtTutar.Text)).ConvertToOrder());
                MessageBox.Show("Sipariş eklendi.");
            }
            catch (Exception)
            {
                MessageBox.Show("Sipariş eklenirken hata oluştu.");
                throw;
            }
            
        }
    }
}
