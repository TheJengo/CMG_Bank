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
    public partial class Form1 : Form
    {
        Banka CMG = Banka.BankaBilgisiGetir();
        Sube Izmir = new Sube();
        public Form1()
        {
            InitializeComponent();
            CMG.SubeEkle(Izmir);
        }
    
        Bireysel A1 = new Bireysel("Cengiz", "Cebeci", 30151295680, 5071278994, "Turgutlu/Manisa", "Çalışkan", "En Sevdiğin Renk", "Fuşya","1234");
        Ticari A2 = new Ticari("Cengiz", "Cebeci", 30151295680, 5071278994, "Turgutlu/Manisa", "Çalışkan", "En Sevdiğin Renk", "Fuşya", 1234,"1234");
        
        Hesap h1;
        Hesap h3;
        private void mtrlIlerle_Click(object sender, EventArgs e)
        {
            h3 = new Hesap();
            h1 = new Hesap();
            Izmir.HesapEkle(h3);
            A1.HesapEkle(h1);
            CMG.MusteriEkle(A1);

            CMG.MusteriEkle(A2);
            A1.Hesaplarim().ElementAt(0).EkHesapAc(DateTime.Now, 3000M);
            txtGizli.Text = CMG.SubeListesi().ElementAt(0).SubeKodu;
            txtPass.Text = A1.MusteriNo.ToString();
            MessageBox.Show(A1.Hesaplarim().ElementAt(0).ArtiHesap.Limit.ToString());
            A1.Hesaplarim().ElementAt(0).ArtiHesap.IslemYap(new Yatir(CMG.SeciliSube().SeciliHesap().HesapNo,2000));
            
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            CMG.SubeIndeksi(txtNo.Text);
            //MessageBox.Show(CMG.SeciliSube().SeciliHesap().Bakiye.ToString());
            Giris_Ekrani frm = new Giris_Ekrani();
            frm.ShowDialog();
            this.Close();
            
        }
        
    }
}
