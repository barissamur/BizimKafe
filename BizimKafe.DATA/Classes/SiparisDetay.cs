using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizimKafe.DATA.Classes
{
    public class SiparisDetay
    {
        public string UrunAd { get; set; }
        public decimal BirimFiyat { get; set; }
        public int Adet { get; set; }
        public string TutarTL { get { return Tutar().ToString("c2"); } }

        public decimal Tutar()
        {
            return BirimFiyat * Adet;
        }

        public override string ToString()
        {
            return "";
        }
    }
}
