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
        Banka CMG = Banka.BankaBilgisiGetir();
        Sube Izmir = new Sube();
        public Giris_Ekrani()
        {
            InitializeComponent();
            MaterialSkin.MaterialSkinManager skinMenager = MaterialSkin.MaterialSkinManager.Instance;
            skinMenager.AddFormToManage(this);
            skinMenager.Theme = MaterialSkin.MaterialSkinManager.Themes.DARK;
            skinMenager.ColorScheme = new MaterialSkin.ColorScheme(MaterialSkin.Primary.Grey600, MaterialSkin.Primary.BlueGrey900, MaterialSkin.Primary.Blue500, MaterialSkin.Accent.Orange700, MaterialSkin.TextShade.WHITE);
        }

        
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMusteriGiris_Click(object sender, EventArgs e)
        {

            foreach (Musteri _Musteri in CMG.MusteriListele())
            {
                if (_Musteri.MusteriNo == txtBireyselNo.Text && _Musteri is Bireysel)
                {
                    if (_Musteri.GirisYap(txtBireyselNo.Text, txtBireyselSifre.Text))
                    {
                        //MusteriPanelini AçM
                        MessageBox.Show("Test");
                    }
                }
            }
            lblBireyselSonuc.Visible = true;
        }
   
        private void btnTicariGiris_Click(object sender, EventArgs e)
        {
            foreach (Musteri _Musteri in CMG.MusteriListele())
            {
                if (_Musteri.MusteriNo == txtTicariNo.Text && _Musteri is Ticari)
                {
                    if (_Musteri.GirisYap(txtTicariNo.Text, txtTicariSifre.Text))
                    {
                        //MusteriPanelini Aç

                        MessageBox.Show("Test");
                    }
                }
            }
            lblTicariSonuc.Visible = true;
        } 
    }
}
