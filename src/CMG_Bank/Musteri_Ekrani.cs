using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CMG_Bank
{
    public partial class Musteri_Ekrani : MaterialSkin.Controls.MaterialForm
    {
        Banka CMG = Banka.BankaBilgisiGetir();
        public Musteri aktifMusteri;

        public Musteri_Ekrani()
        {
            InitializeComponent();
            foreach(Musteri _Musteri in CMG.MusteriListele())
            {
                if(_Musteri.MusteriNo == Giris_Ekrani.girilenNumara)
                {
                    aktifMusteri = _Musteri;
                    break;
                }
            }
            mlvHesaplar.Items.Clear();
            foreach (Hesap _Hesap in aktifMusteri.Hesaplarim())
            {
                ListViewItem eleman = new ListViewItem(_Hesap.HesapNo);
                eleman.SubItems.Add(_Hesap.ParaBirimi);
                eleman.SubItems.Add(String.Format("{0:0.00}", _Hesap.Bakiye));
                mlvHesaplar.Items.Add(eleman);
            }
            List<string> HesapTuru = new List<string>();
            foreach (Kur _Kur in CMG.KurListesi())
            {
                HesapTuru.Add(_Kur.BirimAdi);
            }
            string[] hesapTuru = HesapTuru.ToArray();
            drpHesapTuru.Items = hesapTuru;
            if (aktifMusteri.Hesaplarim().Count == 0)
            {
                pnlBilgi.Visible = true;
                pnlBilgi.BringToFront();
            }
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            GorunmezYap();
            if( mlvHesaplar.Visible == false)
            {
                pnlGiris.Visible = true;
                pnlGiris.BringToFront();
                pnlMenu.Visible = true;
                pnlMenu.BringToFront();
                mlvHesaplar.Visible = true;
                mlvHesaplar.Items.Clear();
                foreach (Hesap _Hesap in aktifMusteri.Hesaplarim())
                {
                    ListViewItem eleman = new ListViewItem(_Hesap.HesapNo);
                    eleman.SubItems.Add(_Hesap.ParaBirimi);
                    eleman.SubItems.Add(String.Format("{0:0.00}", _Hesap.Bakiye));
                    mlvHesaplar.Items.Add(eleman);
                }
            }
            else
            {
                pnlMenu.Visible = false;
                if (pnlIslemler.Visible != false)
                {
                    pnlGiris.Visible = false;
                }
            }
           
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void drpHesapTuru_onItemSelected(object sender, EventArgs e)
        {
            btnHesapAc.Visible = true;
        }
 
        
        private void btnHesapAc_Click(object sender, EventArgs e)
        {
            if (drpHesapTuru.selectedValue == "Türk Lirası")
            {
                aktifMusteri.HesapEkle(new TRY());
            }
            else
            {
                foreach (Kur _Kur in CMG.KurListesi())
                {
                    if (_Kur.BirimAdi == drpHesapTuru.selectedValue)
                    {
                        aktifMusteri.HesapEkle(new Doviz(_Kur.BirimKodu));
                    }
                }
            }
            mlvHesaplar.Items.Clear();
            foreach (Hesap _Hesap in aktifMusteri.Hesaplarim())
            {
                ListViewItem eleman = new ListViewItem(_Hesap.HesapNo);
                eleman.SubItems.Add(_Hesap.ParaBirimi);
                eleman.SubItems.Add(String.Format("{0:0.00}", _Hesap.Bakiye));
                mlvHesaplar.Items.Add(eleman);
            } 
        }

        private void mlvHesaplar_SelectedIndexChanged(object sender, EventArgs e)
        {
            int hesapIndeksi = 0;
            hesapIndeksi = Convert.ToInt32(mlvHesaplar.FocusedItem.Index);
            aktifMusteri.HesapIndeksi(aktifMusteri.Hesaplarim().ElementAt(hesapIndeksi).HesapNo);
            pnlGiris.Visible = false;
            if(aktifMusteri.SeciliHesap().Status == "Aktif")
            {
                MenuDurumu(true);
                pnlIslemler.Visible = true;
                pnlIslemler.BringToFront();
                if(aktifMusteri.SeciliHesap() is Doviz)
                {
                    btnEkHesap.Enabled = false;
                    imgEkHesap.Enabled = false;
                }
            }
            else
            {
                MenuDurumu(false);
            }

        }
        public void MenuDurumu(bool Durum)
        {
                btnEkHesap.Enabled = Durum;
                btnParaTransferi.Enabled = Durum;
                btnParaCekme.Enabled = Durum;
                btnOdemeler.Enabled = Durum;
                imgEkHesap.Visible = Durum;
                imgOdemeler.Visible = Durum;
                imgParaCekme.Visible = Durum;
                imgTransfer.Visible = Durum;
        }

        private void btnAyarlar_Click(object sender, EventArgs e)
        {
            if (pnlAyarlarArkaPlan.Visible == false)
            {
                pnlAyarlarArkaPlan.Visible = true;
                pnlAyarlarArkaPlan.BringToFront();
                lblIsim.Text = "Sn." + aktifMusteri.Ad + " " + aktifMusteri.Soyad.ToUpper().ToString();
                lblAd.Text = aktifMusteri.Ad;
                lblSoyad.Text = aktifMusteri.Soyad.ToUpper();
                lblMusNo.Text = aktifMusteri.MusteriNo;
                lblTCKNO.Text = aktifMusteri.TCKNO.ToString();
                lblKatilimTarihi.Text = aktifMusteri.OlusturulmaTarihi.ToShortDateString();
                lblAktifHesapSayisi.Text = aktifMusteri.Hesaplarim().Count.ToString();
                lblGizliSoru.Text = aktifMusteri.GizliSoru;
            }
            else
            {
                pnlAyarlarArkaPlan.Visible = false;
            }
            
        }

        private void btnSifreDegistir_Click(object sender, EventArgs e)
        {
            if(txtGizliSoruCevap.Text == "" || txtSifre.Text == "" || txtSifreTekrar.Text == "")
            {
                lblOlumsuzSonuc.Visible = true;
                lblOlumluSonuc.Visible = false;
            }
            else
            {
                if(aktifMusteri.GizliSoruKontrol(txtGizliSoruCevap.Text))
                {
                    if(txtSifreTekrar.Text == txtSifre.Text)
                    {
                        aktifMusteri.SifreDegistir(txtSifre.Text);
                        lblOlumsuzSonuc.Visible = false;
                        lblOlumluSonuc.Visible = true;
                    }
                }
                else
                {
                    lblOlumsuzSonuc.Visible = true;
                    lblOlumluSonuc.Visible = false;
                }
            }
        }

        private void btnParaCekme_Click(object sender, EventArgs e)
        {
            GorunmezYap();
            if(pnlParaCekme.Visible == false)
            {
                pnlParaCekme.Visible = true;
                pnlParaCekme.BringToFront();
                lblSeciliHesapBakiye.Text = String.Format("{0:0.00}", aktifMusteri.SeciliHesap().Bakiye);
                lblSeciliHesapNo.Text = aktifMusteri.SeciliHesap().HesapNo;
                lblGunlukLimit.Text = String.Format("{0:0.00}", aktifMusteri.SeciliHesap().GunlukLimit);
                lblUyari.Visible = false;
            }
            else
            {
                lblSeciliHesapBakiye.Text = String.Format("{0:0.00}", aktifMusteri.SeciliHesap().Bakiye);
                lblSeciliHesapNo.Text = aktifMusteri.SeciliHesap().HesapNo;
                lblGunlukLimit.Text = String.Format("{0:0.00}", aktifMusteri.SeciliHesap().GunlukLimit);
                pnlIslemRapor.Visible = false;
                txtParaCekmeMiktar.Visible = false;
                lblMiktar.Visible = false;
                btnParaCek.Visible = false;
                btnParaCekmeDiger.Visible = true;
                btnParaCekmeHepsi.Visible = true;
                btnParaCekmeIkiYuz.Visible = true;
                btnParaCekmeYuz.Visible = true;
                btnParaCekmeElli.Visible = true;
                btnParaCekmeYirmi.Visible = true;
            }

        }
        public void HesaptanParaCek(decimal Miktar)
        {
            pnlIslemRapor.Visible = true;
            pnlIslemRapor.BringToFront();
            Cek _Cek = new Cek(aktifMusteri.SeciliHesap().HesapNo, Miktar);
            aktifMusteri.SeciliHesap().IslemYap(_Cek);
            lblIslemSonucu.Text = _Cek.Rapor();          
        }
        public void EkHesaptanParaCek(decimal Miktar)
        {
            pnlIslemRapor.Visible = true;
            pnlIslemRapor.BringToFront();
            Cek _Cek = new Cek(aktifMusteri.SeciliHesap().ArtiHesap.HesapNo, Miktar);
            aktifMusteri.SeciliHesap().ArtiHesap.IslemYap(_Cek);
            lblIslemSonucu.Text = _Cek.Rapor();
        }

        private void btnParaCekmeDiger_Click(object sender, EventArgs e)
        {
            if (pnlParaCekme.Visible == true)
            {
                txtParaCekmeMiktar.Visible = true;
                lblMiktar.Visible = true;
                btnParaCek.Visible = true;
                btnParaCekmeDiger.Visible = false;
                btnParaCekmeHepsi.Visible = false;
                btnParaCekmeIkiYuz.Visible = false;
                btnParaCekmeYuz.Visible = false;
                btnParaCekmeElli.Visible = false;
                btnParaCekmeYirmi.Visible = false;
            }
            if(pnlEkHesapParaCekme.Visible == true)
            {
                txtEkHesapParaCekmeMiktar.Visible = true;
                lblEkHesapParaCekmeMiktar.Visible = true;
                btnEkHesapParaCek.Visible = true;
                btnEkHesapParaCekmeDiger.Visible = false;
                btnEkHesapParaCekmeHepsi.Visible = false;
                btnEkHesapParaCekmeIkiYuz.Visible = false;
                btnEkHesapParaCekmeYuz.Visible = false;
                btnEkHesapParaCekmeElli.Visible = false;
                btnEkHesapParaCekmeYirmi.Visible = false;
            }
        }

        private void btnParaCek_Click(object sender, EventArgs e)
        {
            if(pnlParaCekme.Visible == true)
            {
                if (txtParaCekmeMiktar.Text == "")
                {
                    lblUyari.Visible = true;
                }
                else
                {
                    HesaptanParaCek(Convert.ToDecimal(txtParaCekmeMiktar.Text));
                }
            }
            if(pnlEkHesapParaCekme.Visible == true)
            {
                if (txtEkHesapParaCekmeMiktar.Text == "")
                {
                    lblEkHesapParaCekmeUyarisi.Visible = true;
                }
                else
                {
                    EkHesaptanParaCek(Convert.ToDecimal(txtEkHesapParaCekmeMiktar.Text));
                }
            }

        }

        private void btnParaCekmeYirmi_Click(object sender, EventArgs e)
        {
            if(pnlParaCekme.Visible == true)
            {
                HesaptanParaCek(Convert.ToDecimal(20));
            }
            if(pnlEkHesapParaCekme.Visible == true)
            {
                EkHesaptanParaCek(Convert.ToDecimal(20));          
            }
        }
        private void btnParaCekmeElli_Click(object sender, EventArgs e)
        {
            if (pnlParaCekme.Visible == true)
            {
                HesaptanParaCek(Convert.ToDecimal(50));
            }
            if (pnlEkHesapParaCekme.Visible == true)
            {
                EkHesaptanParaCek(Convert.ToDecimal(50));
            }
        }
        private void btnParaCekmeYuz_Click(object sender, EventArgs e)
        {
            if (pnlParaCekme.Visible == true)
            {
                HesaptanParaCek(Convert.ToDecimal(100));
            }
            if (pnlEkHesapParaCekme.Visible == true)
            {
                EkHesaptanParaCek(Convert.ToDecimal(100));
            }
        }
        private void btnParaCekmeIkiYuz_Click(object sender, EventArgs e)
        {
            if (pnlParaCekme.Visible == true)
            {
                HesaptanParaCek(Convert.ToDecimal(200));
            }
            if (pnlEkHesapParaCekme.Visible == true)
            {
                EkHesaptanParaCek(Convert.ToDecimal(200));
            }
        }
        private void btnParaCekmeHepsi_Click(object sender, EventArgs e)
        {
            if (pnlParaCekme.Visible == true)
            {
                HesaptanParaCek(aktifMusteri.SeciliHesap().GunlukLimit);
            }
            if (pnlEkHesapParaCekme.Visible == true)
            {
                EkHesaptanParaCek(Convert.ToDecimal(aktifMusteri.SeciliHesap().ArtiHesap.Bakiye));
            }
        }
        public void GorunmezYap()
        {
            txtAliciHesapNo.Text = "";
            txtGizliSoruCevap.Text = "";
            txtParaCekmeMiktar.Text = "";
            txtSifre.Text = "";
            txtSifreTekrar.Text = "";
            txtTransferMiktar.Text = "";
            txtYatirMiktar.Text = "";
            txtEkHesapParaCekmeMiktar.Text = "";
            txtEkHesabaOdemeMiktari.Text = "";
            txtEkHesapMiktar.Text = "";
            drpSubeler.Clear();
            pnlEkHesapOzeti.Visible = false;
            pnlAyarlarArkaPlan.Visible = false;
            pnlOdemeIslemleri.Visible = false;
            pnlParaCekme.Visible = false;
            pnlParaYatirma.Visible = false;
            pnlParaTransferi.Visible = false;
            pnlEkHesapAc.Visible = false;
            pnlIslemRapor.Visible = false;
            pnlEkHesapOdeme.Visible = false;
            pnlEkHesapEkrani.Visible = false;
            pnlHesapOzeti.Visible = false;
        }
        /** Para Yatırma **/
        private void btnParaYatirma_Click(object sender, EventArgs e)
        {
            GorunmezYap();
            if (pnlParaCekme.Visible == false)
            {
                pnlParaYatirma.Visible = true;
                pnlParaYatirma.BringToFront();
                lblYatirmaBakiye.Text = String.Format("{0:0.00}", aktifMusteri.SeciliHesap().Bakiye) + aktifMusteri.SeciliHesap().ParaBirimi;
                lblYatirHesapNo.Text = aktifMusteri.SeciliHesap().HesapNo;
                lblYatirmaBirim.Text = aktifMusteri.SeciliHesap().ParaBirimi;
                lblUyari.Visible = false;
            }
            else
            {
                lblYatirmaBakiye.Text = String.Format("{0:0.00}", aktifMusteri.SeciliHesap().Bakiye) + aktifMusteri.SeciliHesap().ParaBirimi;
                lblYatirHesapNo.Text = aktifMusteri.SeciliHesap().HesapNo;
                lblYatirmaBirim.Text = aktifMusteri.SeciliHesap().ParaBirimi;
            }
        }

        private void btnParaYatir_Click(object sender, EventArgs e)
        {
            if (txtYatirMiktar.Text == "")
            {
                lblYatirmaUyarisi.Visible = true;
            }
            else
            {
                pnlIslemRapor.Visible = true;
                pnlIslemRapor.BringToFront();
                Yatir _Yatir = new Yatir(aktifMusteri.SeciliHesap().HesapNo, Convert.ToDecimal(txtYatirMiktar.Text));
                aktifMusteri.SeciliHesap().IslemYap(_Yatir);
                lblIslemSonucu.Text = _Yatir.Rapor();
                MenuDurumu(true);
            }
        }
        /** Havale **/
        private void btnParaTransferi_Click(object sender, EventArgs e)
        {
            GorunmezYap();
            lblTransferUyarisi.Visible = false;
            List<string> Subeler = new List<string>();
            foreach (Sube _Sube in CMG.SubeListesi())
            {
                Subeler.Add(_Sube.Ad+"-"+_Sube.SubeKodu);
            }
            string[] strSubeler = Subeler.ToArray();
            drpSubeler.Items = strSubeler;
            if (pnlParaCekme.Visible == false)
            {
                pnlParaTransferi.Visible = true;
                pnlParaTransferi.BringToFront();
                lblParaTransferBakiye.Text = String.Format("{0:0.00}", aktifMusteri.SeciliHesap().Bakiye) + aktifMusteri.SeciliHesap().ParaBirimi;
                lblParaTransferHesapNo.Text = aktifMusteri.SeciliHesap().HesapNo;
                lblParaTransferBirim.Text = aktifMusteri.SeciliHesap().ParaBirimi;
                lblUyari.Visible = false;
            }
            else
            {
                lblParaTransferBakiye.Text = String.Format("{0:0.00}", aktifMusteri.SeciliHesap().Bakiye) + aktifMusteri.SeciliHesap().ParaBirimi;
                lblParaTransferHesapNo.Text = aktifMusteri.SeciliHesap().HesapNo;
                lblParaTransferBirim.Text = aktifMusteri.SeciliHesap().ParaBirimi;
            }
        }

        private void btnHavaleYap_Click(object sender, EventArgs e)
        {
           
            if (txtTransferMiktar.Text == "" || drpSubeler.selectedValue == "" || txtAliciHesapNo.Text == "")
            {
                lblTransferUyarisi.Visible = true;
            }
            else
            {
                foreach (Musteri _Musteri in CMG.MusteriListele())
	            {
		            foreach (Hesap _Hesap in _Musteri.Hesaplarim())
	               {
                       if (_Hesap.HesapNo == txtAliciHesapNo.Text)
                       {
                         string[] subeKodu = drpSubeler.selectedValue.Split('-');
                          foreach(Sube _Sube in CMG.SubeListesi())
	                      {
		                     if (subeKodu[1] == _Sube.SubeKodu)
                             {
                                 pnlIslemRapor.Visible = true;
                                 pnlIslemRapor.BringToFront();
                                 Havale _Havale;
                                 decimal havaleKomisyonu = 0M;
                                 if (_Musteri is Bireysel)
                                 {
                                     havaleKomisyonu = Convert.ToDecimal(txtTransferMiktar.Text) * 0.02M;
                                     _Havale = new Havale(aktifMusteri.SeciliHesap().HesapNo, Convert.ToDecimal(txtTransferMiktar.Text) - havaleKomisyonu, _Hesap);
                                     if(aktifMusteri.SeciliHesap().IslemYap(_Havale))
                                     {
                                         aktifMusteri.SeciliHesap().IslemYap(new Havale(aktifMusteri.SeciliHesap().HesapNo, havaleKomisyonu, _Sube.SeciliHesap()));                                   
                                     }
                                     lblIslemSonucu.Text = _Havale.Rapor();
                                 }
                                 if(_Musteri is Ticari)
                                 {
                                     _Havale = new Havale(aktifMusteri.SeciliHesap().HesapNo, Convert.ToDecimal(txtTransferMiktar.Text) - havaleKomisyonu, _Hesap);
                                     aktifMusteri.SeciliHesap().IslemYap(_Havale);
                                     lblIslemSonucu.Text = _Havale.Rapor();
                                 }
                             }
	                      }
                       }
	               }
	            }

            }
        }
        /** Ödemeler **/
        private void btnOdemeler_Click(object sender, EventArgs e)
        {
            GorunmezYap();
            if (aktifMusteri.SeciliHesap() is Doviz || aktifMusteri.SeciliHesap().ArtiHesap == null)
            {
                btnEkHesapBorcuOdeme.Visible = false;
            }
            else
            {
                btnEkHesapBorcuOdeme.Visible = true;
            }
            if(pnlOdemeIslemleri.Visible == true)
            {
                lblOdemeBakiye.Text = String.Format("{0:0.00}", aktifMusteri.SeciliHesap().Bakiye) + aktifMusteri.SeciliHesap().ParaBirimi;
                lblOdemeHesapNo.Text = aktifMusteri.SeciliHesap().HesapNo;
                lblOdemeBirim.Text = String.Format("{0:0.00}", aktifMusteri.SeciliHesap().GunlukLimit);
            }
            else
            {
                pnlOdemeIslemleri.Visible = true;
                pnlOdemeIslemleri.BringToFront();
                lblOdemeBakiye.Text = String.Format("{0:0.00}", aktifMusteri.SeciliHesap().Bakiye)+aktifMusteri.SeciliHesap().ParaBirimi;
                lblOdemeHesapNo.Text = aktifMusteri.SeciliHesap().HesapNo;
                lblOdemeBirim.Text = "TRY";
            }
        }

        private void btnHesapKapatma_Click(object sender, EventArgs e)
        {
            pnlIslemRapor.Visible = true;
            pnlIslemRapor.BringToFront();
            if(pnlOdemeIslemleri.Visible == true)
            {
                if (aktifMusteri.SeciliHesap().HesapKapama())
                {
                    lblIslemSonucu.Text = "Hesap Kapatma işleminiz " + Environment.NewLine + "başarıyla tamamlandı!" + Environment.NewLine + "Hesabınızı tekrar aktif etmek için" + Environment.NewLine + "Para Yatırma işlemi yapmanız" + Environment.NewLine + "yeterli olacaktır.";
                    MenuDurumu(false);
                }
                else
                {
                    if (aktifMusteri.SeciliHesap() is Doviz)
                        lblIslemSonucu.Text = "Hesabınız kapatılamadı." + Environment.NewLine + "Hesap Kapatma işlemi" + Environment.NewLine + "için bakiyeniz 0.00" + aktifMusteri.SeciliHesap().ParaBirimi +Environment.NewLine + "olmalıdır.";
                    if (aktifMusteri.SeciliHesap() is TRY)
                        lblIslemSonucu.Text = "Hesabınız kapatılamadı." + Environment.NewLine + "Hesabınızın ve varsa Ek" + Environment.NewLine + "Hesap bakiyeniz 0.00 " + aktifMusteri.SeciliHesap().ParaBirimi + Environment.NewLine + "olmalıdır.";
                }
            }
            if(pnlEkHesapEkrani.Visible == true)
            {
                if (aktifMusteri.SeciliHesap().ArtiHesap.HesapKapama())
                {
                    lblIslemSonucu.Text = "Ek Hesap Kapatma işleminiz " + Environment.NewLine + "başarıyla tamamlandı!" + Environment.NewLine + "Ek Hesabınızı tekrar aktif etmek için" + Environment.NewLine + "Ek Hesap Açma işlemi yapmanız" + Environment.NewLine + "yeterli olacaktır.";
                    aktifMusteri.SeciliHesap().ArtiHesap = null;
                }
                else
                {
                    lblIslemSonucu.Text = "Ek Hesabınız kapatılamadı." + Environment.NewLine + "Ek Hesap Kapatma işlemi" + Environment.NewLine + "için Ödenecek Tutariniz 0.00" + aktifMusteri.SeciliHesap().ParaBirimi +Environment.NewLine + "olmalıdır.";                 
                }
            }
        }

        int flagOne;
        private void btnEkHesapBorcuOdeme_Click(object sender, EventArgs e)
        {
            lblEkHesapOdemeUyarisi.Visible = false;
            pnlEkHesapOdeme.Visible = true;
            pnlEkHesapOdeme.BringToFront();
            lblEkHesapBakiyesi.Text = String.Format("{0:0.00}", aktifMusteri.SeciliHesap().ArtiHesap.Bakiye);
            lblEkHesapNo.Text = aktifMusteri.SeciliHesap().ArtiHesap.HesapNo;
            lblEkHesapAnaBakiye.Text = String.Format("{0:0.00}", aktifMusteri.SeciliHesap().Bakiye);
            btnEkHesabaOdemeYap.Text = "Ödeme Yap";
            flagOne = 1;
        }

        private void btnEkHesabaOdemeYap_Click(object sender, EventArgs e)
        {
            if (txtEkHesabaOdemeMiktari.Text == "")
            {
                lblEkHesapOdemeUyarisi.Visible = true;
            }
            else
            {
                if (flagOne == 1)
                {
                    Havale _Havale = new Havale(aktifMusteri.SeciliHesap().HesapNo, Convert.ToDecimal(txtEkHesabaOdemeMiktari.Text), aktifMusteri.SeciliHesap().ArtiHesap);
                    if (aktifMusteri.SeciliHesap().IslemYap(_Havale))
                    {
                        aktifMusteri.SeciliHesap().ArtiHesap.IslemYap(new Yatir(aktifMusteri.SeciliHesap().ArtiHesap.HesapNo, Convert.ToDecimal(txtEkHesabaOdemeMiktari.Text)));
                        lblIslemSonucu.Text = _Havale.Miktar + " TL Ödeme işleminiz" + Environment.NewLine + "başarılı. İyi günler dileriz :)";
                    }
                    else
                    {
                        lblIslemSonucu.Text = "Para Aktarma işleminiz başarısız!" + Environment.NewLine + "Lütfen tekrar işlem yapınız.";
                    }
                }
                if (flagOne == 2)
                {
                    Cek _Cek = new Cek(aktifMusteri.SeciliHesap().ArtiHesap.HesapNo, Convert.ToDecimal(txtEkHesabaOdemeMiktari.Text));
                    if (aktifMusteri.SeciliHesap().ArtiHesap.IslemYap(_Cek))
                    {
                        aktifMusteri.SeciliHesap().IslemYap(new Yatir(aktifMusteri.SeciliHesap().HesapNo, Convert.ToDecimal(txtEkHesabaOdemeMiktari.Text)));
                        lblIslemSonucu.Text = "Para Aktarma işleminiz başarılı!" + Environment.NewLine + "İyi günler dileriz :)";
                    }
                    else
                    {
                        lblIslemSonucu.Text = "Para Aktarma işleminiz başarısız!" + Environment.NewLine + "Lütfen tekrar işlem yapınız.";
                    }

                }
                pnlIslemRapor.Visible = true;
                pnlIslemRapor.BringToFront();
                flagOne = 0;
            }
        }
        private void btnAnaHesabaParaAktar_Click(object sender, EventArgs e)
        {
            lblEkHesapOdemeUyarisi.Visible = false;
            pnlEkHesapOdeme.Visible = true;
            pnlEkHesapOdeme.BringToFront();
            lblEkHesapBakiyesi.Text = String.Format("{0:0.00}", aktifMusteri.SeciliHesap().ArtiHesap.Bakiye);
            lblEkHesapNo.Text = aktifMusteri.SeciliHesap().ArtiHesap.HesapNo;
            lblEkHesapAnaBakiye.Text = String.Format("{0:0.00}", aktifMusteri.SeciliHesap().Bakiye);
            btnEkHesabaOdemeYap.Text = "Havale Yap";
            flagOne = 2;
        }
        
        /** Ek Hesap Açma **/
        decimal Miktar;
        public decimal FaizHesapla(decimal Miktar)
        {
            return dtpEkHesapVadeTarihi.Value.Subtract(DateTime.Now).Days * EkHesap.GunlukFaizOrani * (Miktar / 100);
        }
        private void dtpEkHesapVadeTarihi_onValueChanged(object sender, EventArgs e)
        {
            if(dtpEkHesapVadeTarihi.Value < DateTime.Now)
            {
                lblEkHesapUyarisi.Visible = true;
                pnlEkHesapBildirisi.Visible = false;
            }
            if (txtEkHesapMiktar.Text != "")
            {
                Miktar = Convert.ToDecimal(txtEkHesapMiktar.Text);
                if (Miktar > 0 && Miktar <= 10000)
                {
                    pnlEkHesapBildirisi.Visible = true;
                    pnlEkHesapBildirisi.BringToFront();
                    lblKrediTutari.Text = String.Format("{0:0.00}", Miktar) + " TL";
                    lblEkHesapTutari.Text = String.Format("{0:0.00}", (Miktar + FaizHesapla(Miktar) + (FaizHesapla(Miktar) * EkHesap.VergiOrani))) + " TL";
                    lblFaizTutari.Text = String.Format("{0:0.00}", FaizHesapla(Miktar)) + " TL";
                    lblVergiler.Text = String.Format("{0:0.00}", (FaizHesapla(Miktar) * EkHesap.VergiOrani)) + " TL";
                }
            }
        }

        private void txtEkHesapMiktar_OnValueChanged(object sender, EventArgs e)
        {
            if(txtEkHesapMiktar.Text != "")
            {
                Miktar = Convert.ToDecimal(txtEkHesapMiktar.Text);
                if(Miktar > 0 && Miktar <= 10000)
                {
                    pnlEkHesapBildirisi.Visible = true;
                    pnlEkHesapBildirisi.BringToFront();
                    lblKrediTutari.Text = String.Format("{0:0.00}", Miktar) + " TL";
                    lblEkHesapTutari.Text = String.Format("{0:0.00}", (Miktar + FaizHesapla(Miktar) +(FaizHesapla(Miktar) * EkHesap.VergiOrani))) + " TL";
                    lblFaizTutari.Text = String.Format("{0:0.00}", FaizHesapla(Miktar)) + " TL";
                    lblVergiler.Text = String.Format("{0:0.00}", (FaizHesapla(Miktar) * EkHesap.VergiOrani)) + " TL";
                }
            }
        }

        private void btnEkHesap_Click(object sender, EventArgs e)
        {
            GorunmezYap();
            dtpEkHesapVadeTarihi.Value = DateTime.Now.AddDays(1);
            if(aktifMusteri.SeciliHesap().ArtiHesap == null)
            {
                pnlEkHesapAc.Visible = true;
                pnlEkHesapAc.BringToFront();
                pnlEkHesapBildirisi.Visible = false;
            }
            else
            {
                lblEkHesapEkranBakiye.Text = String.Format("{0:0.00}", aktifMusteri.SeciliHesap().ArtiHesap.Bakiye)+"TL";
                lblEkHesapEkranDurum.Text = "TRY";
                lblEkHesapEkranHesapNo.Text = aktifMusteri.SeciliHesap().ArtiHesap.HesapNo;
                pnlEkHesapParaCekme.Visible = false;               
                pnlEkHesapEkrani.Visible = true;
                pnlEkHesapEkrani.BringToFront();
            }
        }

        private void btnEkHesapAc_Click(object sender, EventArgs e)
        {
            if(txtEkHesapMiktar.Text == "")
            {
                lblEkHesapUyarisi.Visible = true;
            }
            else
            {
                aktifMusteri.SeciliHesap().EkHesapAc(dtpEkHesapVadeTarihi.Value, Convert.ToDecimal(txtEkHesapMiktar.Text));
                pnlIslemRapor.Visible = true;
                pnlIslemRapor.BringToFront();
                if (aktifMusteri.SeciliHesap().ArtiHesap != null)
                {
                    lblIslemSonucu.Text = "Ek Hesap Açma işlemi başarılı! " + Environment.NewLine + "Ek Hesap Numaranız:" + aktifMusteri.SeciliHesap().ArtiHesap.HesapNo + Environment.NewLine + "\tİyi harcamalar :)";
                }
                else
                {
                    lblIslemSonucu.Text = "Ek Hesap Açma isteği başarısız!" + Environment.NewLine + "Lütfen daha sonra tekrar" + Environment.NewLine + "Ek Hesap Açma isteğinde bulununuz.";
                }
            }
       }

        private void btnEkHesaptanParaCek_Click(object sender, EventArgs e)
        {
            lblEkHesapParaCekmeBakiye.Text = String.Format("{0:0.00}", aktifMusteri.SeciliHesap().ArtiHesap.Bakiye);
            lblEkHesapParaCekmeHesapNo.Text = aktifMusteri.SeciliHesap().ArtiHesap.HesapNo;
            lblEkHesapParaCekmeBirim.Text = "TRY";
            pnlEkHesapParaCekme.Visible = true;
            pnlEkHesapParaCekme.BringToFront();
            pnlIslemRapor.Visible = false;
            txtEkHesapParaCekmeMiktar.Visible = false;
            lblEkHesapParaCekmeMiktar.Visible = false;
            btnEkHesapParaCek.Visible = false;
            btnEkHesapParaCekmeDiger.Visible = true;
            btnEkHesapParaCekmeHepsi.Visible = true;
            btnEkHesapParaCekmeIkiYuz.Visible = true;
            btnEkHesapParaCekmeYuz.Visible = true;
            btnEkHesapParaCekmeElli.Visible = true;
            btnEkHesapParaCekmeYirmi.Visible = true;
        }

        private void btnEkHesapOzeti_Click(object sender, EventArgs e)
        {
            int flagTwo = 0;
            if (aktifMusteri.SeciliHesap().ArtiHesap.Bakiye > Banka.IslemTutari)
            {
                flagTwo = 1;
                Banka.BankaBilgisiGetir().SeciliSube().SeciliHesap().IslemYap(new Yatir(Banka.BankaBilgisiGetir().SeciliSube().SeciliHesap().HesapNo, Banka.IslemTutari));
                aktifMusteri.SeciliHesap().ArtiHesap.IslemYap(new Cek(aktifMusteri.SeciliHesap().ArtiHesap.HesapNo, Banka.IslemTutari));
            }
            if (aktifMusteri.SeciliHesap().Bakiye > Banka.IslemTutari)
            {
                flagTwo = 2;
                aktifMusteri.SeciliHesap().IslemYap(new Havale(aktifMusteri.SeciliHesap().HesapNo, Banka.IslemTutari, Banka.BankaBilgisiGetir().SeciliSube().SeciliHesap()));
            }
            if (flagTwo != 0)
            {   
                pnlEkHesapOzeti.Visible = true;
                pnlEkHesapOzeti.BringToFront();
                mlvEkHesapOzeti.Items.Clear();
                foreach (Islem _Islem in aktifMusteri.SeciliHesap().ArtiHesap.HesapOzeti())
                {
                    ListViewItem eleman = new ListViewItem(_Islem.IslemTarihi.ToShortDateString());
                    eleman.SubItems.Add(String.Format("{0:0.00}", _Islem.Miktar));
                    if (_Islem.islemSonucu)
                    {
                        if(_Islem is Yatir)
                        {
                          eleman.SubItems.Add("Ödeme");
                        } 
                        if (_Islem is Cek)
                        {
                            eleman.SubItems.Add("Para Çekme");
                        }
                    }
                    mlvEkHesapOzeti.Items.Add(eleman);
                }
                lblEkHesapOzetBakiyesi.Text = String.Format("{0:0.00}", aktifMusteri.SeciliHesap().ArtiHesap.Bakiye)+" TL";
                lblEkHesapOzetHesapNo.Text = aktifMusteri.SeciliHesap().ArtiHesap.HesapNo;
                lblOdenecekTutar.Text = String.Format("{0:0.00}", aktifMusteri.SeciliHesap().ArtiHesap.OdenecekTutar) + " TL";
            }
            else
            {
                pnlIslemRapor.Visible = true;
                pnlIslemRapor.BringToFront();
                lblIslemSonucu.Text = "Hesap Özeti görüntüleme" + Environment.NewLine + "işlem maliyeti 2.30 TL'dır." + Environment.NewLine + "Yetersiz Bakiye sebebiyle" + Environment.NewLine + "Hesap Özeti görüntüleme işlemi" + Environment.NewLine + "yapılamamaktadır.";
            }
        }

        private void btnHesapOzeti_Click(object sender, EventArgs e)
        {
            GorunmezYap();
            if(pnlHesapOzeti.Visible == false)
            {
                pnlHesapOzeti.Visible = true;
                pnlHesapOzeti.BringToFront();
                mlvHesapOzetIslemDetay.Visible = false;
                lblHesapOzetiBakiye.Text = String.Format("{0:0.00}", aktifMusteri.SeciliHesap().Bakiye) +" "+ aktifMusteri.SeciliHesap().ParaBirimi;
                lblHesapOzetiHesapNo.Text = aktifMusteri.SeciliHesap().HesapNo;
                if (aktifMusteri.SeciliHesap().ArtiHesap == null)
                {
                    lblHesapOzetiDurum.Text = "Yok";
                }
                else
                {
                    lblHesapOzetiDurum.Text = "Var";
                }
                mlvHesapOzetiIslemleri.Items.Clear();
                if(aktifMusteri.SeciliHesap().HesapOzeti().Count > 0)
                {
                    dtpHesapOzetBaslangici.Value = aktifMusteri.SeciliHesap().HesapOzeti().First().IslemTarihi.AddDays(-1);
                    dtpHesapOzetBitisi.Value = aktifMusteri.SeciliHesap().HesapOzeti().Last().IslemTarihi.AddDays(+1);
                    
                }
            //    foreach (Islem _Islem in aktifMusteri.SeciliHesap().HesapOzeti())
            //    {  
            //        ListViewItem eleman = new ListViewItem(_Islem.IslemTarihi.ToShortDateString());
            //        eleman.SubItems.Add(String.Format("{0:0.00}", _Islem.Miktar) + aktifMusteri.SeciliHesap().ParaBirimi);
            //        if (_Islem.islemSonucu)
            //        {
            //            if (_Islem is Yatir)
            //            {
            //                eleman.SubItems.Add("Para Yatırma");
            //            }
            //            if (_Islem is Cek)
            //            {
            //                eleman.SubItems.Add("Para Çekme");
            //            }
            //            if(_Islem is Havale)
            //            {
            //                Havale _Havale =(Havale) _Islem;
            //                if(_Havale.aliciHesap != null )
            //                {
            //                    eleman.SubItems.Add("Hesaba Havale");
            //                }
            //                if (_Havale.aliciEkHesap != null)
            //                {
            //                    eleman.SubItems.Add("Ek Hesaba Havale");
            //                }
            //            }
            //        }
            //        else
            //        {
            //            eleman.SubItems.Add("Başarısız");
            //        }
            //        mlvHesapOzetiIslemleri.Items.Add(eleman);
            //    }
            }
            else
            {
                pnlHesapOzeti.Visible = false;
            }

        }
        public void TarihAraliginaGoreGetir(DateTime baslangicTarihi, DateTime bitisTarihi)
        {
            TimeSpan baslangicZamani,bitisZamani;
            mlvHesapOzetiIslemleri.Visible = true;
            mlvHesapOzetiIslemleri.BringToFront();
            foreach (Islem _Islem in aktifMusteri.SeciliHesap().HesapOzeti())
            {
                baslangicZamani = _Islem.IslemTarihi.Subtract(baslangicTarihi);
                bitisZamani = bitisTarihi.Subtract(_Islem.IslemTarihi);
                if (baslangicZamani.Days >= 0 && bitisZamani.Days >= 0)
                {
                    ListViewItem eleman = new ListViewItem(_Islem.IslemTarihi.ToShortDateString());
                    eleman.SubItems.Add(String.Format("{0:0.00}", _Islem.Miktar) + aktifMusteri.SeciliHesap().ParaBirimi);
                    if (_Islem.islemSonucu)
                    {
                        if (_Islem is Yatir)
                        {
                            eleman.SubItems.Add("Para Yatırma");
                        }
                        if (_Islem is Cek)
                        {
                            eleman.SubItems.Add("Para Çekme");
                        }
                        if (_Islem is Havale)
                        {
                            Havale _Havale = (Havale)_Islem;
                            if (_Havale.aliciHesap != null)
                            {
                                eleman.SubItems.Add("Hesaba Havale");
                            }
                            if (_Havale.aliciEkHesap != null)
                            {
                                eleman.SubItems.Add("Ek Hesaba Havale");
                            }
                        }
                    }
                    else
                    {
                        eleman.SubItems.Add("Başarısız");
                    }
                    mlvHesapOzetiIslemleri.Items.Add(eleman);
                }
            }
        }

        private void dtpHesapOzetBitisi_onValueChanged(object sender, EventArgs e)
        {
            mlvHesapOzetiIslemleri.Items.Clear();
            TarihAraliginaGoreGetir(dtpHesapOzetBaslangici.Value, dtpHesapOzetBitisi.Value);
        }
        private void dtpHesapOzetBaslangici_onValueChanged(object sender, EventArgs e)
        {
            mlvHesapOzetiIslemleri.Items.Clear();
            TarihAraliginaGoreGetir(dtpHesapOzetBaslangici.Value, dtpHesapOzetBitisi.Value);
        }
        private void mlvHesapOzetiIslemleri_SelectedIndexChanged(object sender, EventArgs e)
        {
            int seciliIslem;
            seciliIslem = Convert.ToInt32(mlvHesapOzetiIslemleri.FocusedItem.Index);
            if(aktifMusteri.SeciliHesap().HesapOzeti().ElementAt(seciliIslem) is Havale)
            {
                mlvHesapOzetIslemDetay.Items.Clear();
                mlvHesapOzetIslemDetay.Visible = true;
                mlvHesapOzetIslemDetay.BringToFront();
                Havale _Havale = (Havale)aktifMusteri.SeciliHesap().HesapOzeti().ElementAt(seciliIslem);
                if(_Havale.aliciHesap != null)
                {
                    if(_Havale.aliciAdi != null)
                    {
                        if (aktifMusteri.SeciliHesap().HesapNo == _Havale.HesapNo)
                        {
                            ListViewItem eleman = new ListViewItem("Siz");
                            eleman.SubItems.Add(_Havale.aliciAdi);
                            mlvHesapOzetIslemDetay.Items.Add(eleman);
                        }
                        if (aktifMusteri.SeciliHesap().HesapNo == _Havale.aliciHesap.HesapNo)
                        {
                            ListViewItem eleman = new ListViewItem(_Havale.gonderenAdi);
                            eleman.SubItems.Add("Siz");
                            mlvHesapOzetIslemDetay.Items.Add(eleman);
                        }
                    }
                    else
                    {
                        if (aktifMusteri.SeciliHesap().HesapNo == _Havale.HesapNo)
                        {
                            ListViewItem eleman = new ListViewItem("Siz");
                            eleman.SubItems.Add(CMG.Adi);
                            mlvHesapOzetIslemDetay.Items.Add(eleman);
                        }
                        if (aktifMusteri.SeciliHesap().HesapNo == _Havale.aliciHesap.HesapNo)
                        {
                            ListViewItem eleman = new ListViewItem(CMG.Adi);
                            eleman.SubItems.Add("Siz");
                            mlvHesapOzetIslemDetay.Items.Add(eleman);
                        }
                    }
                }
                else
                {
                    if (aktifMusteri.SeciliHesap().HesapNo == _Havale.HesapNo)
                    {
                        ListViewItem eleman = new ListViewItem("Siz");
                        eleman.SubItems.Add("Ek Hesabınız");
                        mlvHesapOzetIslemDetay.Items.Add(eleman);
                    }
                    if (aktifMusteri.SeciliHesap().HesapNo == _Havale.aliciEkHesap.HesapNo)
                    {
                        ListViewItem eleman = new ListViewItem("Ek Hesabınız");
                        eleman.SubItems.Add("Siz");
                        mlvHesapOzetIslemDetay.Items.Add(eleman);
                    }
                }

            }
        }

        private void mlvHesapOzetIslemDetay_SelectedIndexChanged(object sender, EventArgs e)
        {
            mlvHesapOzetIslemDetay.Visible = false;
        }


    }
}

