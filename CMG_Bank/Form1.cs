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
        Hesap h4;
        private void mtrlIlerle_Click(object sender, EventArgs e)
        {
            h3 = new Doviz("USD");
            h1 = new TRY();
            h4 = new Doviz("Dolar");
            A2.HesapEkle(h4);
            Izmir.HesapEkle(h3);
            A1.HesapEkle(h1);
            CMG.MusteriEkle(A1);

            CMG.MusteriEkle(A2);
            A1.Hesaplarim().ElementAt(0).EkHesapAc(DateTime.Now, 3000M);
            txtGizli.Text = CMG.SubeListesi().ElementAt(0).SubeKodu;
            txtPass.Text = A1.MusteriNo.ToString();
            MessageBox.Show(A1.Hesaplarim().ElementAt(0).ArtiHesap.Limit.ToString());
            A1.Hesaplarim().ElementAt(0).ArtiHesap.IslemYap(new Yatir(CMG.SeciliSube().SeciliHesap().HesapNo,2000));
            Personel A3 = new Personel("Cengiz", "Cebeci", 30151295680, 3000, "1234");
            A1.Hesaplarim().ElementAt(0).ArtiHesap.HesapOzeti();
            Izmir.PersonelEkle(A3);
            txtNo.Text = A3.PersonelNo.ToString();
            
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            Kur dolar = new Kur("Dolar","USD",'$' ,3.85545M);
            Kur pound = new Kur("Pound", "GBP", '£',4.53245M);
            CMG.KurEkle(dolar);
            CMG.KurEkle(pound); 
            A1.HesapIndeksi(A1.Hesaplarim().ElementAt(0).HesapNo);
            //A1.SeciliHesap().IslemYap(new Yatir(A1.Hesaplarim().ElementAt(0).HesapNo, 1000));
            A1.SeciliHesap().IslemYap(new Havale(A1.SeciliHesap().HesapNo,100,A2.Hesaplarim().ElementAt(0)));
            MessageBox.Show(A2.Hesaplarim().ElementAt(0).Bakiye.ToString());
            CMG.SubeIndeksi(txtGizli.Text);
            //MessageBox.Show(CMG.SeciliSube().SeciliHesap().Bakiye.ToString());
            MessageBox.Show(CMG.SeciliSube().SeciliHesap().Bakiye.ToString());
            MessageBox.Show(CMG.Rapor().ElementAt(3).Miktar.ToString());
            MessageBox.Show(CMG.Gelir.ToString()); 
            Giris_Ekrani frm = new Giris_Ekrani();
           // frm.ShowDialog();
            this.Close();
            
        }
        
    }
}
