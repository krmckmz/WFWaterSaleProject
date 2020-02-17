using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFAWater.Entity;

namespace WFAWater.Model
{
    public class CustomerModel
    {
        public int ID { get; set; }
        public string adi { get; set; }
        public string soyadi { get; set; }
        public string telefon { get; set; }
        public string adres { get; set; }
        public bool aktifMi { get; set; }
    }
}
