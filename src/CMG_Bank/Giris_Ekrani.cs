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
using System.Drawing.Drawing2D;



namespace CMG_Bank
{
    public partial class Giris_Ekrani : MaterialSkin.Controls.MaterialForm
    {
        /** Başlatma Verileri **/
        Banka CMG = Banka.BankaBilgisiGetir();
        public Giris_Ekrani()
        {
            InitializeComponent();
            MaterialSkin.MaterialSkinManager skinMenager = MaterialSkin.MaterialSkinManager.Instance;
            skinMenager.AddFormToManage(this);
            skinMenager.Theme = MaterialSkin.MaterialSkinManager.Themes.DARK;
            skinMenager.ColorScheme = new MaterialSkin.ColorScheme(MaterialSkin.Primary.Grey600, MaterialSkin.Primary.BlueGrey900, MaterialSkin.Primary.Blue500, MaterialSkin.Accent.Orange700, MaterialSkin.TextShade.WHITE);
        }
        public static string girilenNumara = "";

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /** Ceo Personel Numarasını Elde Etmek ve Başlatma Verilerini Aktif etmek için Yapılmıştır **/
        private void btnKapat_MouseHover(object sender, EventArgs e)
        {
            txtPersonelNo.Text = CMG.SeciliSube().PersonelListesi().ElementAt(0).PersonelNo;
        } 

        /** Bireysel Müşteri Giriş **/
        private void btnMusteriGiris_Click(object sender, EventArgs e)
        {

            foreach (Musteri _Musteri in CMG.MusteriListele())
            {
                if (_Musteri.MusteriNo == txtBireyselNo.Text && _Musteri is Bireysel)
                {
                    if (_Musteri.GirisYap(txtBireyselNo.Text, txtBireyselSifre.Text))
                    {
                        girilenNumara = txtBireyselNo.Text;
                        Musteri_Ekrani frmMusteri = new Musteri_Ekrani();
                        frmMusteri.ShowDialog();
                        break;
                    }
                    else
                    {
                        lblBireyselSonuc.Visible = true;
                    }
                }
            }
        }
        /** Ticari Müşteri Giriş **/
        private void btnTicariGiris_Click(object sender, EventArgs e)
        {
            foreach (Musteri _Musteri in CMG.MusteriListele())
            {
                if (_Musteri.MusteriNo == txtTicariNo.Text && _Musteri is Ticari)
                {
                    if (_Musteri.GirisYap(txtTicariNo.Text, txtTicariSifre.Text))
                    {
                        girilenNumara = txtTicariNo.Text;
                        Musteri_Ekrani frmMusteri = new Musteri_Ekrani();
                        frmMusteri.ShowDialog();
                        break;
                    }
                    else
                    {
                        lblTicariSonuc.Visible = true;
                    }
                }
            }
        }
        /** Personel Giriş **/
        private void btnPersonelGiris_Click(object sender, EventArgs e)
        {
            int count = CMG.SubeListesi().Count;
            foreach (Sube _Sube in CMG.SubeListesi().ToList())
            {
                foreach (Personel _Personel in _Sube.PersonelListesi())
                {
                    if (_Personel.PersonelNo == txtPersonelNo.Text)
                    {
                        if (_Personel.GirisYap(txtPersonelNo.Text, txtPersonelSifre.Text))
                        {
                            girilenNumara = txtPersonelNo.Text;
                            Personel_Ekrani frmPersonel = new Personel_Ekrani();
                            frmPersonel.ShowDialog();
                            break;
                        }
                        else
                        {
                            lblPersonelSonuc.Visible = true;
                        }
                        if(CMG.SubeListesi().Count != count)
                        {
                            break;
                        }
                    }
                }
             }
        }
    }
}
