using BizimKafe.DATA.Classes;
using BizimKafe.DATA.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BizimKafe.UI.Forms
{
    public partial class SiparisForm : Form
    {
        private readonly KafeVeri _db;
        private readonly Siparis _siparis;
        public SiparisForm(KafeVeri db, Siparis siparis)
        {
            InitializeComponent();
            _db = db;
            _siparis = siparis;
            BilgileriGuncelle();
            UrunleriListele();
        }

        private void UrunleriListele()
        {
            cboxUrun.DataSource = _db.Urunler; // DataSource her property için ayrı ayrı işlem yapar
        }

        private void BilgileriGuncelle()
        {
            Text = $"Masa {_siparis.MasaNo}";
            lblMasaNo.Text = _siparis.MasaNo.ToString("00");
            lblOdemeTutari.Text = _siparis.ToplamTutarTL;
            dgvSiparisDetaylar.DataSource = _siparis.SiparisDetaylar.ToList();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            Urun urun = (Urun)cboxUrun.SelectedItem;

            if (urun == null)
                return;

            SiparisDetay sd = _siparis.SiparisDetaylar.FirstOrDefault(x => x.UrunAd == urun.UrunAd);

            if (sd != null)
                sd.Adet += (int)nudAdet.Value;

            else
            {
                sd = new SiparisDetay()
                {
                    UrunAd = urun.UrunAd,
                    BirimFiyat= urun.BirimFiyat,
                    Adet = (int)nudAdet.Value,
                };

                _siparis.SiparisDetaylar.Add(sd);
            }

            BilgileriGuncelle();
        }

        private void btnAnaSayfa_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOde_Click(object sender, EventArgs e)
        {
            SiparisiKapat(_siparis.ToplamTutar(), SiparisDurum.Odendi);
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            SiparisiKapat(0, SiparisDurum.Iptal);
        }   

        private void SiparisiKapat(decimal odenenTutar, SiparisDurum yeniDurum)
        {
            _siparis.KapanisZamani = DateTime.Now;
            _siparis.OdenenTutar = odenenTutar;
            _siparis.Durum = yeniDurum;

            _db.AktifSiparisler.Remove(_siparis);
            _db.GecmisSiparisler.Add(_siparis);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
