using BizimKafe.DATA.Classes;
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
    public partial class UrunlerForm : Form
    {
        private readonly KafeVeri _db;

        public UrunlerForm(KafeVeri db)
        {
            InitializeComponent();
            _db=db;
            UrunleriListele();
        }

        private void UrunleriListele()
        {
            dgvUrunler.DataSource = _db.Urunler.ToList();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (btnEkle.Text == "EKLE")
            {
                if (txtUrunAd.Text == "")
                {
                    MessageBox.Show("Ürün adını boş giremezsiniz");
                    return;
                }

                Urun urun = new Urun()
                {
                    UrunAd = txtUrunAd.Text,
                    BirimFiyat = nudBirimFiyat.Value
                };

                _db.Urunler.Add(urun);
                UrunleriListele();
            }

            else
            {
                DataGridViewRow satir = dgvUrunler.SelectedRows[0];
                Urun urun = (Urun)satir.DataBoundItem;

                urun.BirimFiyat = nudBirimFiyat.Value;
                urun.UrunAd = txtUrunAd.Text;

                UrunleriListele();
                DuzenleIptal();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dgvUrunler.SelectedRows.Count == 0)
            {
                MessageBox.Show("Önce ürün seçiniz");
                return;
            }

            DialogResult dr = MessageBox.Show("Seçili ürünü silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (dr == DialogResult.No)
                return;

            DataGridViewRow satir = dgvUrunler.SelectedRows[0];
            Urun urun = (Urun)satir.DataBoundItem;

            _db.Urunler.Remove(urun);

            UrunleriListele();
        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            UrunDuzenle();

            DataGridViewRow satir = dgvUrunler.SelectedRows[0];
            Urun urun = (Urun)satir.DataBoundItem;
            txtUrunAd.Text = urun.UrunAd;
            nudBirimFiyat.Value = urun.BirimFiyat;
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            DuzenleIptal();
        }
        private void UrunDuzenle()
        {
            btnEkle.Text = "KAYDET";
            btnIptal.Visible = true;
        }


        private void DuzenleIptal()
        {
            btnIptal.Visible = false;
            btnEkle.Text = "EKLE";
        }
    }
}
