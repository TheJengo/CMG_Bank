using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CMG_Bank
{
    public partial class personel_Yonet : UserControl
    {
        Banka CMG = Banka.BankaBilgisiGetir();

        public personel_Yonet() 
        {
            InitializeComponent();
        }
        public void BilgiGetir()
        { 
            /*
            mlvPersonel.Items.Clear();
            foreach (Personel _Personel in CMG.SeciliSube().PersonelListesi())
            {
                ListViewItem eleman = new ListViewItem(_Personel.PersonelNo);
                eleman.SubItems.Add(_Personel.Ad + " " + _Personel.Soyad.ToUpper());
                if (_Personel is Calisan)
                {
                    eleman.SubItems.Add("Çalışan");
                }
                if (_Personel is Mudur)
                {
                    eleman.SubItems.Add("Yönetici");
                }
                if (_Personel is Ceo)
                {
                    eleman.SubItems.Add("CEO");
                }
                eleman.SubItems.Add(_Personel.IseAlimTarihi.Date.ToShortDateString());
                mlvPersonel.Items.Add(eleman);
            }
             */
             
        }
        private void btnYenile_Click(object sender, EventArgs e)
        {
            BilgiGetir();
            lblOlumsuzSonuc.Visible = false;
            lblOlumluSonuc.Visible = false;
        }
        int indeks;
        private void mlvPersonel_SelectedIndexChanged(object sender, EventArgs e)
        {
            indeks = Convert.ToInt32(mlvPersonel.FocusedItem.Index);
            txtPerAd.Text = CMG.SeciliSube().PersonelListesi().ElementAt(indeks).Ad;
            txtPerSoyad.Text = CMG.SeciliSube().PersonelListesi().ElementAt(indeks).Soyad;
            txtPerTCKNO.Text = CMG.SeciliSube().PersonelListesi().ElementAt(indeks).TCKNO.ToString();
            txtPerMaas.Text = CMG.SeciliSube().PersonelListesi().ElementAt(indeks).Maas.ToString();
            if (CMG.SeciliSube().PersonelListesi().ElementAt(indeks) is Calisan)
            {
                rbCalisan.Checked = true;
            }
            else
            {
                rbMudur.Checked = true;
            }
        }

        private void btnPerGuncelle_Click(object sender, EventArgs e)
        {
            Personel _Personel = CMG.SeciliSube().PersonelListesi().ElementAt(indeks);
            if (txtPerAd.Text == "" || txtPerSoyad.Text == "" || txtPerTCKNO.Text == "" || txtPerMaas.Text == "")
            {
                lblOlumluSonuc.Visible = false;
                lblOlumsuzSonuc.Visible = true;
            }
            else
            {
                _Personel.BilgileriGuncelle(txtPerAd.Text, txtPerSoyad.Text, Convert.ToInt64(txtPerTCKNO.Text), Convert.ToDecimal(txtPerMaas.Text));
                if (txtPerSifre.Text == "" || txtPerSifreOnay.Text == "")
                {
                    if (txtPerSifre.Text == txtPerSifreOnay.Text)
                    {
                        _Personel.SifreDegistir(txtPerSifre.Text);
                        lblOlumsuzSonuc.Visible = false;
                        lblOlumluSonuc.Visible = true;
                    }
                    else
                    {
                        lblOlumsuzSonuc.Visible = false;
                        lblOlumluSonuc.Visible = true;
                    }
                    lblOlumsuzSonuc.Visible = true;
                    lblOlumluSonuc.Visible = false;
                }
                if (_Personel is Calisan)
                {
                    rbCalisan.Checked = true;
                }
                else
                {
                    rbMudur.Checked = true;
                }
                lblOlumsuzSonuc.Visible = false;
                lblOlumluSonuc.Visible = true;
            }
        }

        private void btnMaasOde_Click(object sender, EventArgs e)
        {
        }
    }
}
