using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Animations;
using MaterialSkin.Controls;

namespace CMG_Bank
{
    public partial class Bilgi_Ekrani : MaterialSkin.Controls.MaterialForm
    {
        Banka CMG = Banka.BankaBilgisiGetir();

        public Bilgi_Ekrani()
        {
            InitializeComponent();
            MaterialSkin.MaterialSkinManager skinMenager = MaterialSkin.MaterialSkinManager.Instance;
            skinMenager.AddFormToManage(this);
            skinMenager.Theme = MaterialSkin.MaterialSkinManager.Themes.DARK;
            skinMenager.ColorScheme = new MaterialSkin.ColorScheme(MaterialSkin.Primary.Grey600, MaterialSkin.Primary.BlueGrey900, MaterialSkin.Primary.Blue500, MaterialSkin.Accent.Orange700, MaterialSkin.TextShade.WHITE);     
      
         }
        /** 
         * Bankada Yapılacak işlemlerin başarılı olması için 
         * yapılan kurucu/başlatıcı işlemler.
         **/
        private void btnKurulumuTamamla_Click(object sender, EventArgs e)
        {
            if(cbOnay.Checked == false || txtBankaAdi.Text == "" || txtBankaKodu.Text == "" || txtKaynakPara.Text == "" || txtCeoAdi.Text == ""  || txtKurucuSoyad.Text == "" || txtTCKNO.Text == "" || txtSifre.Text == "")
            {
                lblKurUyarisi.Visible = true;
            }
            else
            {
                CMG.BilgileriDuzenle(txtBankaAdi.Text, txtBankaKodu.Text, Convert.ToDecimal(txtKaynakPara.Text));
                Ceo _CEO = new Ceo(txtCeoAdi.Text, txtKurucuSoyad.Text, Convert.ToInt64(txtTCKNO.Text), 7500, txtSifre.Text);
                Sube MerkezSube = new Sube("Cebeci", "Ankara");
                TRY SubeHesabi = new TRY();
                Kur TRYKur = new Kur("Türk Lirası", "TRY", "₺", 1);
                CMG.SubeEkle(MerkezSube);
                CMG.SubeIndeksi(MerkezSube.SubeKodu);
                CMG.SeciliSube().HesapEkle(SubeHesabi);
                CMG.KurEkle(TRYKur);
                CMG.SubeIndeksi(SubeHesabi.HesapNo);
                SubeHesabi.IslemYap(new Yatir(SubeHesabi.HesapNo,Convert.ToDecimal(txtKaynakPara.Text)));
                CMG.SeciliSube().PersonelEkle(_CEO);
                Giris_Ekrani frmGirisEkrani = new Giris_Ekrani();
                frmGirisEkrani.ShowDialog();
                this.Close();
            }

        }
    }
}
