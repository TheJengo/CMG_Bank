using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bunifu.Framework;
using BunifuAnimatorNS;
using MaterialSkin.Animations;
using MaterialSkin.Controls;
using MaterialSkin;

namespace CMG_Bank
{
    public partial class Personel_Ekrani : MaterialSkin.Controls.MaterialForm
    {
        Banka CMG = Banka.BankaBilgisiGetir();
        public static Personel aktifPersonel;

        public Personel_Ekrani()
        {
            InitializeComponent(); 
            MaterialSkin.MaterialSkinManager skinManager = MaterialSkin.MaterialSkinManager.Instance;
            skinManager.AddFormToManage(this);
            skinManager.Theme = MaterialSkin.MaterialSkinManager.Themes.DARK;
            skinManager.ColorScheme = new MaterialSkin.ColorScheme(MaterialSkin.Primary.Grey600, MaterialSkin.Primary.BlueGrey900, MaterialSkin.Primary.Blue500, MaterialSkin.Accent.Orange700, MaterialSkin.TextShade.WHITE);
            CMG.SubeIndeksi(new String(Giris_Ekrani.girilenNumara.ToCharArray(0, 5)));
            aktifPersonel = CMG.SeciliSube().PersonelGetir(Giris_Ekrani.girilenNumara);
            GorunmezYap();
        }
        /** Bileşenleri Gizle **/
        public void GorunmezYap()
        {
            if(aktifPersonel is Calisan)
            {
                btnMenuPersonelEkle.Visible = false;
                btnMenuSubeEkle.Visible = false;
                btnMenuSubeHesaplari.Visible = false;
                btnMenuBankaRaporu.Visible = false;
            }
            else if( aktifPersonel is Mudur)
            {
                btnMenuSubeEkle.Visible = false;
                btnMenuBankaRaporu.Visible = false;
            }
            pnlSeciliSubeHesaplari.Visible = false;
            pnlSeciliSubeIslemleri.Visible = false;
            pnlHesaplar.Visible = false;
            pnlSubeIslemleri.Visible = false;
            pnlAyarlarArkaPlan.Visible = false;
            doviz_Islem1.Visible = false;
            musteri_Ekle1.Visible = false;
            musteri_Listele1.Visible = false;
        }
        /** Oturumu Sonlandır **/
        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /** AYARLAR */
        private void btnAyarlar_Click(object sender, EventArgs e)
        {
            GorunmezYap();
            pnlAyarlarArkaPlan.Visible = true;
            lblIsim.Text = "Sn." + aktifPersonel.Soyad.ToUpper();
            lblAd.Text = aktifPersonel.Ad;
            lblAlimTarihi.Text = aktifPersonel.IseAlimTarihi.Date.ToString();
            lblMaasTarihi.Text = (30 - (aktifPersonel.IseAlimTarihi - DateTime.Now).Days).ToString();
            lblPerNo.Text = aktifPersonel.PersonelNo;
            lblSoyad.Text = aktifPersonel.Soyad.ToUpper();
            lblTCKNO.Text = aktifPersonel.TCKNO.ToString();
            pnlAyarlarArkaPlan.BringToFront();
        }
        private void btnSifreDegistir_Click(object sender, EventArgs e)
        {
            if (txtSifre.Text == "" || txtSifreTekrar.Text == "")
            {
                lblOlumluSonuc.Visible = false;
                lblOlumsuzSonuc.Visible = true;
            }
            else
            {
                if (txtSifre.Text == txtSifreTekrar.Text)
                {
                    aktifPersonel.SifreDegistir(txtSifre.Text);
                    lblOlumsuzSonuc.Visible = false;
                    lblOlumluSonuc.Visible = true;
                }
                else
                {
                    lblOlumluSonuc.Visible = false;
                    lblOlumsuzSonuc.Visible = true;
                }
            }
        }
        /** Ayalar -Bitişi **/

        /** DOVİZ ISLEMLERİ -Başlangıcı **/
        private void btnMenuDoviz_Click(object sender, EventArgs e)
        {
            aktifButonAyiraci.Height = ((Bunifu.Framework.UI.BunifuFlatButton)sender).Height;
            aktifButonAyiraci.Top = ((Bunifu.Framework.UI.BunifuFlatButton)sender).Top;
            doviz_Islem1.Visible = true;
            doviz_Islem1.BringToFront();
        }
        /** DOVİZ ISLEMLERİ -Bitişi **/

        /** MUSTERİ EKLE -Başlangıcı **/
        private void btnMenuMusteriEkle_Click(object sender, EventArgs e)
        {
            aktifButonAyiraci.Height = ((Bunifu.Framework.UI.BunifuFlatButton)sender).Height;
            aktifButonAyiraci.Top = ((Bunifu.Framework.UI.BunifuFlatButton)sender).Top;
            musteri_Ekle1.Visible = true;
            musteri_Ekle1.BringToFront();
        }
        /** MUSTERİ EKLE -Bitişi **/

        /** MUSTERI LISTELEME -Başlangıcı **/
        private void btnMenuMusteriListele_Click(object sender, EventArgs e)
        {
            aktifButonAyiraci.Height = ((Bunifu.Framework.UI.BunifuFlatButton)sender).Height;
            aktifButonAyiraci.Top = ((Bunifu.Framework.UI.BunifuFlatButton)sender).Top;
            musteri_Listele1.Visible = true;
            musteri_Listele1.BringToFront();
        }
        /** MUSTERI LISTELEME -Bitişi**/

        /** PERSONEL YONETİMi -Başlangıcı **/
        private void btnMenuPersonelEkle_Click(object sender, EventArgs e)
        {
            aktifButonAyiraci.Height = ((Bunifu.Framework.UI.BunifuFlatButton)sender).Height;
            aktifButonAyiraci.Top = ((Bunifu.Framework.UI.BunifuFlatButton)sender).Top;
            pnlPersonelYonetim.Visible = true;
            pnlPersonelYonetim.BringToFront();
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
        }
        int indeksPersonel;
        private void mlvPersonel_SelectedIndexChanged(object sender, EventArgs e)
        {
            indeksPersonel = Convert.ToInt32(mlvPersonel.FocusedItem.Index);
            txtPerAd.Text = CMG.SeciliSube().PersonelListesi().ElementAt(indeksPersonel).Ad;
            txtPerSoyad.Text = CMG.SeciliSube().PersonelListesi().ElementAt(indeksPersonel).Soyad;
            txtPerTCKNO.Text = CMG.SeciliSube().PersonelListesi().ElementAt(indeksPersonel).TCKNO.ToString();
            txtPerMaas.Text = CMG.SeciliSube().PersonelListesi().ElementAt(indeksPersonel).Maas.ToString();
            if (CMG.SeciliSube().PersonelListesi().ElementAt(indeksPersonel) is Calisan)
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
            Personel _Personel = CMG.SeciliSube().PersonelListesi().ElementAt(indeksPersonel);
            if (txtPerAd.Text == "" || txtPerSoyad.Text == "" || txtPerTCKNO.Text == "" || txtPerMaas.Text == "")
            {
                lblPerOlumluSonuc.Visible = false;
                lblPerOlumsuzSonuc.Visible = true;
            }
            else
            {
                _Personel.BilgileriGuncelle(txtPerAd.Text, txtPerSoyad.Text, Convert.ToInt64(txtPerTCKNO.Text), Convert.ToDecimal(txtPerMaas.Text));
                if (txtPerSifre.Text == "" || txtPerSifreOnay.Text == "")
                {
                    if (txtPerSifre.Text == txtPerSifreOnay.Text)
                    {
                        _Personel.SifreDegistir(txtPerSifre.Text);
                        lblPerOlumsuzSonuc.Visible = false;
                        lblPerOlumluSonuc.Visible = true;
                    }
                    else
                    {
                        lblPerOlumsuzSonuc.Visible = false;
                        lblOlumluSonuc.Visible = true;
                    }
                    lblPerOlumsuzSonuc.Visible = true;
                    lblPerOlumluSonuc.Visible = false;
                }
                if (_Personel is Calisan)
                {
                    rbCalisan.Checked = true;
                }
                else
                {
                    rbMudur.Checked = true;
                }
                lblPerOlumsuzSonuc.Visible = false;
                lblPerOlumluSonuc.Visible = true;
            }
        }

        private void btnPerYenile_Click(object sender, EventArgs e)
        {
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
           
        }
        private void btnPerCikar_Click(object sender, EventArgs e)
        {
            CMG.SeciliSube().PersonelCikar(CMG.SeciliSube().PersonelListesi().ElementAt(indeksPersonel));
        }
        private void btnPerEkle_Click(object sender, EventArgs e)
        {
            if (txtPerAd.Text == "" || txtPerSoyad.Text == "" || txtPerTCKNO.Text == "" || txtPerMaas.Text == "")
            {
                lblPerOlumluSonuc.Visible = false;
                lblPerOlumsuzSonuc.Visible = true;
            }
            else
            {
                if (txtPerSifre.Text == "" || txtPerSifreOnay.Text == "")
                {
                    if (rbCalisan.Checked == true)
                    {
                        Calisan _Calisan = new Calisan(txtPerAd.Text, txtPerSoyad.Text, Convert.ToInt64(txtPerTCKNO.Text), Convert.ToDecimal(txtPerMaas.Text), "1234");
                        CMG.SeciliSube().PersonelEkle(_Calisan);
                    }
                    else
                    {
                        Mudur _Mudur = new Mudur(txtPerAd.Text, txtPerSoyad.Text, Convert.ToInt64(txtPerTCKNO.Text), Convert.ToDecimal(txtPerMaas.Text), "1234");
                        CMG.SeciliSube().PersonelEkle(_Mudur);
                    }
                }
                else
                {
                    if (txtPerSifre.Text == txtPerSifreOnay.Text)
                    {
                        if (rbCalisan.Checked == true)
                        {
                            Calisan _Calisan = new Calisan(txtPerAd.Text, txtPerSoyad.Text, Convert.ToInt64(txtPerTCKNO.Text), Convert.ToDecimal(txtPerMaas.Text),txtPerSifre.Text);
                            CMG.SeciliSube().PersonelEkle(_Calisan);
                        }
                        else
                        {
                            Mudur _Mudur = new Mudur(txtPerAd.Text, txtPerSoyad.Text, Convert.ToInt64(txtPerTCKNO.Text), Convert.ToDecimal(txtPerMaas.Text), txtPerSifre.Text);
                            CMG.SeciliSube().PersonelEkle(_Mudur);
                        }
                    }
                    else
                    {
                        lblPerOlumsuzSonuc.Visible = true; ;
                        lblPerOlumluSonuc.Visible = false;
                    }
                }
               
                lblPerOlumsuzSonuc.Visible = false;
                lblPerOlumluSonuc.Visible = true;
            }

        }
        /** PERSONEL YONETİMİ -Bitişi **/

        /** AKTİF ŞUBE HESAPLARI **/
        private void btnMenuSubeHesaplari_Click(object sender, EventArgs e)
        {
            GorunmezYap();
            aktifButonAyiraci.Height = ((Bunifu.Framework.UI.BunifuFlatButton)sender).Height;
            aktifButonAyiraci.Top = ((Bunifu.Framework.UI.BunifuFlatButton)sender).Top;
            pnlSeciliSubeHesaplari.Visible = true;
            pnlSeciliSubeHesaplari.BringToFront();
            drpHesapTuru.Clear();
            List<string> HesapTuru = new List<string>();
            foreach (Kur _Kur in CMG.KurListesi())
            {
                HesapTuru.Add(_Kur.BirimAdi);
            }
            string[] hesapTuru = HesapTuru.ToArray();
            drpHesapTuru.Items = hesapTuru;
            mlvSeciliSubeHesaplari.Items.Clear();
            foreach (Hesap _Hesap in CMG.SeciliSube().Hesaplar)
            {
                ListViewItem eleman = new ListViewItem(_Hesap.HesapNo);
                if (_Hesap is Doviz)
                {
                    eleman.SubItems.Add("Döviz");
                }
                else
                {
                    if (CMG.SeciliSube().SeciliHesap() == null)
                    {
                        CMG.SeciliSube().HesapIndeksi(_Hesap.HesapNo);
                        CMG.SeciliSube().SeciliHesap();
                    }
                    eleman.SubItems.Add("TRY");
                }
                eleman.SubItems.Add(String.Format("{0:0.00}",_Hesap.Bakiye));
                eleman.SubItems.Add(_Hesap.ParaBirimi);
                mlvSeciliSubeHesaplari.Items.Add(eleman);
            }
        }
        private void btnHesapEkle_Click(object sender, EventArgs e)
        {
            if (drpHesapTuru.selectedIndex != -1)
            {
                if (drpHesapTuru.selectedValue == "Türk Lirası")
                {
                    CMG.SeciliSube().HesapEkle(new TRY());
                }
                else
                {
                    foreach (Kur _Kur in CMG.KurListesi())
                    {
                        if (_Kur.BirimAdi == drpHesapTuru.selectedValue)
                        {
                            CMG.SeciliSube().HesapEkle(new Doviz(_Kur.BirimKodu));
                        }
                    }
                }
                mlvHesaplar.Items.Clear();
                foreach (Hesap _Hesap in CMG.SeciliSube().Hesaplar)
                {
                    ListViewItem eleman = new ListViewItem(_Hesap.HesapNo);
                    if (_Hesap is Doviz)
                    {
                        eleman.SubItems.Add("Döviz");
                    }
                    else
                    {
                        eleman.SubItems.Add("TRY");
                    }
                    eleman.SubItems.Add(String.Format("{0:0.00}",_Hesap.Bakiye));
                    eleman.SubItems.Add( _Hesap.ParaBirimi);
                    mlvHesaplar.Items.Add(eleman);
                }
            }
        }
        private void btnSeciliSubeHesaplariYenile_Click(object sender, EventArgs e)
        {
            mlvSeciliSubeHesaplari.Items.Clear();
            foreach (Hesap _Hesap in CMG.SeciliSube().Hesaplar)
            {
                ListViewItem eleman = new ListViewItem(_Hesap.HesapNo);
                if (_Hesap is Doviz)
                {
                    eleman.SubItems.Add("Döviz");
                }
                else
                {
                    if (CMG.SeciliSube().SeciliHesap() == null)
                    {
                        CMG.SeciliSube().HesapIndeksi(_Hesap.HesapNo);
                        CMG.SeciliSube().SeciliHesap();
                    }
                    eleman.SubItems.Add("TRY");
                }
                eleman.SubItems.Add(String.Format("{0:0.00}",_Hesap.Bakiye));
                eleman.SubItems.Add(_Hesap.ParaBirimi);
                mlvSeciliSubeHesaplari.Items.Add(eleman);
            }
        }
        private void mlvSeciliSubeHesaplari_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indeks = 0;
            indeks = Convert.ToInt32(mlvSeciliSubeHesaplari.FocusedItem.Index);
            pnlSeciliSubeIslemleri.Visible = true;
            pnlSeciliSubeIslemleri.BringToFront();
            CMG.SeciliSube().HesapIndeksi(CMG.SeciliSube().Hesaplar.ElementAt(indeks).HesapNo);
            mlvSeciliSubeHesapIslemleri.Items.Clear();
            foreach (Islem _Islem in CMG.SeciliSube().Hesaplar.ElementAt(indeks).HesapIslemleri)
            {
                ListViewItem eleman = new ListViewItem(_Islem.IslemTarihi.ToShortDateString());
                if (_Islem is Cek)
                {
                    eleman.SubItems.Add("Para Çekme");
                }
                else if (_Islem is Yatir)
                {
                    eleman.SubItems.Add("Para Yatırma");
                }
                else
                {
                    eleman.SubItems.Add("Havale");
                }
                eleman.SubItems.Add(_Islem.Miktar.ToString());
                if (_Islem.islemSonucu)
                {
                    eleman.SubItems.Add("Başarılı");
                }
                else
                {
                    eleman.SubItems.Add("Başarısız");
                }
                mlvSeciliSubeHesapIslemleri.Items.Add(eleman);
            }
        }
        private void btnSubeHesaplarinaGit_Click(object sender, EventArgs e)
        {
            pnlSeciliSubeIslemleri.Visible = false;
        }
        /** AKTİF ŞUBE HESAPLARI -Bişiti **/

        /** TUM SUBE ISLEMLERI **/
        private void btnMenuSubeEkle_Click(object sender, EventArgs e)
        {
            aktifButonAyiraci.Height = ((Bunifu.Framework.UI.BunifuFlatButton)sender).Height;
            aktifButonAyiraci.Top = ((Bunifu.Framework.UI.BunifuFlatButton)sender).Top;
            pnlSubeIslemleri.Visible = true;
            pnlSubeIslemleri.BringToFront();
            mlvSubeler.Items.Clear();
            foreach (Sube _Sube in CMG.SubeListesi())
            {
                ListViewItem eleman = new ListViewItem(_Sube.SubeKodu);
                eleman.SubItems.Add(_Sube.Ad);
                eleman.SubItems.Add(_Sube.Adres);
                eleman.SubItems.Add(_Sube.OlusturulmaTarihi.ToShortDateString());
                mlvSubeler.Items.Add(eleman);
            }
        }
        int indeks = 0;
        private void mlvSubeler_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpHesapTuru.Clear();
            indeks = Convert.ToInt32(mlvSubeler.FocusedItem.Index);
            txtSubeAdiGuncelle.Text = CMG.SubeListesi().ElementAt(indeks).Ad;
            txtSubeAdresiGuncelle.Text = CMG.SubeListesi().ElementAt(indeks).Adres;
            CMG.SubeIndeksi(CMG.SubeListesi().ElementAt(indeks).SubeKodu);
            pnlHesaplar.Visible = true;
            pnlHesaplar.BringToFront();
            mlvHesaplar.Items.Clear();
            foreach (Hesap _Hesap in CMG.SubeListesi().ElementAt(indeks).Hesaplar)
            {
                ListViewItem eleman = new ListViewItem(_Hesap.HesapNo);
                if (_Hesap is Doviz)
                {
                    eleman.SubItems.Add("Döviz");
                }
                else
                {
                    eleman.SubItems.Add("TRY");
                }
                eleman.SubItems.Add(String.Format("{0:0.00}",_Hesap.Bakiye));
                eleman.SubItems.Add(_Hesap.ParaBirimi);
                mlvHesaplar.Items.Add(eleman);
            }
        }
        private void btnSubelereGit_Click(object sender, EventArgs e)
        {
            pnlHesaplar.Visible = false;
        }
        int hesapIndeksi;
        string hesapNo;
        private void mlvHesaplar_SelectedIndexChanged(object sender, EventArgs e)
        {
            hesapIndeksi = Convert.ToInt32(mlvHesaplar.FocusedItem.Index);
            hesapNo = CMG.SubeListesi().ElementAt(indeks).Hesaplar.ElementAt(hesapIndeksi).HesapNo;
            mlvIslemler.Items.Clear();
            pnlIslemler.Visible = true;
            pnlIslemler.BringToFront();
            foreach (Islem _Islem in CMG.SubeListesi().ElementAt(indeks).Hesaplar.ElementAt(hesapIndeksi).HesapIslemleri)
            {
                ListViewItem eleman = new ListViewItem(_Islem.IslemTarihi.ToShortDateString());
                if (_Islem is Cek)
                {
                    eleman.SubItems.Add("Para Çekme");
                }
                else if (_Islem is Yatir)
                {
                    eleman.SubItems.Add("Para Yatırma");
                }
                else
                {
                    eleman.SubItems.Add("Havale");
                }
                eleman.SubItems.Add(_Islem.Miktar.ToString());
                if (_Islem.islemSonucu)
                {
                    eleman.SubItems.Add("Başarılı");
                }
                else
                {
                    eleman.SubItems.Add("Başarısız");
                }
                mlvIslemler.Items.Add(eleman);
            }
        }
        private void btnHesaplaraGit_Click(object sender, EventArgs e)
        {
            pnlIslemler.Visible = false;
        }
        private void btnSubeEkle_Click(object sender, EventArgs e)
        {
            if (txtSubeAdi.Text == "" || txtSubeAdresi.Text == "")
            {
                lblSubeOlumsuzSonuc.Visible = true;
                lblSubeOlumluSonuc.Visible = false;
            }
            else
            {
                Sube yeniSube = new Sube(txtSubeAdi.Text, txtSubeAdresi.Text);
                CMG.SubeEkle(yeniSube);
                lblSubeOlumsuzSonuc.Visible = false;
                lblSubeOlumluSonuc.Visible = true;
            }
        }
        private void btnSubeGuncelle_Click(object sender, EventArgs e)
        {
            Sube _Sube = CMG.SubeListesi().ElementAt(indeks);
            if (txtSubeAdiGuncelle.Text == "" || txtSubeAdresiGuncelle.Text == "")
            {
                lblSubeOlumsuzSonuc.Visible = false;
                lblSubeOlumluSonuc.Visible = true;
            }
            else
            {
                _Sube.Guncelle(txtSubeAdiGuncelle.Text, txtSubeAdresiGuncelle.Text);
                lblSubeOlumsuzSonuc.Visible = false;
                lblSubeOlumluSonuc.Visible = true;
            }
        }
        private void btnYenileSubeEkle_Click(object sender, EventArgs e)
        {
            mlvSubeler.Items.Clear();
            foreach (Sube _Sube in CMG.SubeListesi())
            {
                ListViewItem eleman = new ListViewItem(_Sube.SubeKodu);
                eleman.SubItems.Add(_Sube.Ad);
                eleman.SubItems.Add(_Sube.Adres);
                eleman.SubItems.Add(_Sube.OlusturulmaTarihi.ToShortDateString());
                mlvSubeler.Items.Add(eleman);
            }
            lblSubeOlumsuzSonuc.Visible = false;
            lblSubeOlumluSonuc.Visible = false;
        }
        /** Tüm Şube İşlemleri -Bitişi **/

        /** Banka Rapor Ekrani -Başlangıcı **/
        private void btnMenuBankaRaporu_Click(object sender, EventArgs e)
        {
            pnlBankaRapor.Visible = true;
            pnlBankaRapor.BringToFront();
            aktifButonAyiraci.Height = ((Bunifu.Framework.UI.BunifuFlatButton)sender).Height;
            aktifButonAyiraci.Top = ((Bunifu.Framework.UI.BunifuFlatButton)sender).Top;
            mlvGelir.Items.Clear();
            mlvGider.Items.Clear();
            int xGider = 0;
            int xGelir = 0;
            if (CMG.Rapor() != null)
            {
                xGelir++;
                xGider++;
                foreach (Sube _Sube in CMG.SubeListesi())
                {
                    foreach (Hesap _Hesap in _Sube.Hesaplar)
                    {
                        foreach (Islem _Islem in _Hesap.HesapIslemleri)
                        {
                            decimal Miktar = _Islem.Miktar;
                            if (_Hesap is Doviz)
                            {
                                Miktar *= CMG.KurGetir(_Hesap.ParaBirimi);
                            }
                            ListViewItem eleman = new ListViewItem(String.Format("{0:0.00}", Miktar));
                            eleman.SubItems.Add(_Sube.SubeKodu);
                            eleman.SubItems.Add(_Hesap.ParaBirimi);
                            if (_Islem is Yatir)
                            {
                                CMG.Gelirler(Miktar);
                                mlvGelir.Items.Add(eleman);
                                xGelir++;
                            }
                            if (_Islem is Cek)
                            {
                                CMG.Giderler(Miktar);
                                mlvGider.Items.Add(eleman);
                            }
                            if (_Islem is Havale)
                            {
                                Havale _Havale = (Havale)_Islem;
                                if (_Havale.aliciHesap.HesapNo == _Hesap.HesapNo)
                                {
                                    CMG.Gelirler(Miktar);
                                    mlvGelir.Items.Add(eleman);
                                    xGelir++;
                                }
                                else
                                {
                                    CMG.Giderler(Miktar);
                                    mlvGider.Items.Add(eleman);
                                    xGider++;
                                }
                            }
                        }
                    }
                }
                lblGelir.Text = "Toplam Gelir: " + String.Format("{0:0.00}", CMG.Gelir) + "TL";
                lblGider.Text = "Toplam Gider: " + String.Format("{0:0.00}", CMG.Gider) + "TL";
                if (CMG.Gelir == 0 || CMG.Gider == 0)
                {
                    lblKar.Text = "Kar Oranı : %0";
                }
                else
                {
                    lblKar.Text = "Kar Oranı: %" + Convert.ToInt32(((CMG.Gelir - CMG.Gider) / CMG.Gider) * 100).ToString();
                }
            }
        }
        /** Banka Rapor Ekrani -Bitişi**/

    }
}
