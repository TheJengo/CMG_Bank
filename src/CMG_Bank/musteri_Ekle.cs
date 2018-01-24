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
    public partial class musteri_Ekle : UserControl
    {
    
        Banka CMG = Banka.BankaBilgisiGetir();
        string[] GizliSoru = {
                                 "İlk evcil hayvanınızın adı nedir?",
                                 "İlk aracınızın modeli nedir?",
                                 "Hangi şehirde doğdunuz?",
                                 "Babanızın ortanca ismi nedir?",
                                 "Çocukluk lakabınız nedir?"
                             };
        public musteri_Ekle()
        {
            InitializeComponent();
            for (int i = 0; i < GizliSoru.Length; i++ )
            {
                dpdMusGizliSoru.AddItem(GizliSoru[i]);
            }
        }


        private void btnMusteriEkle_Click(object sender, EventArgs e)
        {
            
            if (txtMusAdi.Text == "" || txtMusSoyadi.Text == "" || txtMusTCKNO.Text == "" || txtMusKizlik.Text == ""
                || dpdMusGizliSoru.selectedValue == "" || txtMusGizliCevap.Text == "" || txtMusTelNo.Text == "" || txtMusAdres.Text == "")
            {
                lblOlumsuzSonuc.Visible = true;
                lblOlumluSonuc.Visible = false;
            }
            else
            {
                if (rbBireyselMusteri.Checked == true)
                {
                    Bireysel _Bireysel = new Bireysel(txtMusAdi.Text, txtMusSoyadi.Text, Convert.ToInt64(txtMusTCKNO.Text), Convert.ToInt64(txtMusTelNo.Text), txtMusAdres.Text, txtMusKizlik.Text, dpdMusGizliSoru.selectedValue, txtMusGizliCevap.Text, "0000");
                    lblOlumsuzSonuc.Visible = false;
                    lblOlumluSonuc.Visible = true;
                    CMG.MusteriEkle(_Bireysel);

                }
                if (rbTicariMusteri.Checked == true)
                {
                    if (txtVergiNo.Text != "")
                    {
                        Ticari _Ticari = new Ticari(txtMusAdi.Text, txtMusSoyadi.Text, Convert.ToInt64(txtMusTCKNO.Text), Convert.ToInt64(txtMusTelNo.Text), txtMusAdres.Text, txtMusKizlik.Text, dpdMusGizliSoru.selectedValue, txtMusGizliCevap.Text, Convert.ToInt64(txtVergiNo.Text), "0000");                 
                        lblOlumsuzSonuc.Visible = false;
                        lblOlumluSonuc.Visible = true;
                        CMG.MusteriEkle(_Ticari);
                    }
                    else
                    {
                        lblOlumsuzSonuc.Visible = true;
                        lblOlumluSonuc.Visible = false;
                    }
                }
            }
        }

        private void rbTicariMusteri_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTicariMusteri.Checked == true)
            {
                lblVergiNo.Visible = true;
                txtVergiNo.Visible = true;
            }
            else
            {
                lblVergiNo.Visible = false;
                txtVergiNo.Visible = false;
            }
        }
        

    }
}
