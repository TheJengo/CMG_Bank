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
        Sube Izmir = new Sube("İzmir","Gaziemir/Izmir");
        public Form1()
        {
            InitializeComponent();
            CMG.SubeEkle(Izmir);
        }
    
        Bireysel A1 = new Bireysel("Cengiz", "Cebeci", 30151295680, 5071278994, "Turgutlu/Manisa", "Çalışkan", "En Sevdiğin Renk", "Fuşya","1234");
        Ticari A2 = new Ticari("Coşkun", "Cebeci", 30151295680, 5071278994, "Turgutlu/Manisa", "Çalışkan", "En Sevdiğin Renk", "Fuşya", 1234,"1234");

        Hesap h1, h2, h3, h4, h5, h6;
        private void mtrlIlerle_Click(object sender, EventArgs e)
        {
            h3 = new Doviz("USD");
            h1 = new TRY();
            h4 = new Doviz("USD");
            h2 = new TRY();
            h5 = new TRY();
            h6 = new TRY();
            A1.HesapEkle(h1);
            A1.HesapEkle(h4);
            A2.HesapEkle(h2);
            A1.HesapEkle(h6);
            Izmir.HesapEkle(h3);
            h3.IslemYap(new Yatir(h3.HesapNo, 2500M));
            h4.IslemYap(new Yatir(h1.HesapNo, 3000M));
            Izmir.HesapEkle(h5);
            Izmir.HesapIndeksi(h5.HesapNo);
            Izmir.SeciliHesap().IslemYap(new Yatir(Izmir.SeciliHesap().HesapNo,10000M));
            CMG.MusteriEkle(A1);
           
            CMG.MusteriEkle(A2);
            A1.Hesaplarim().ElementAt(0).EkHesapAc(DateTime.Now.AddDays(1), 3000M);
            txtGizli.Text = A2.MusteriNo;
            txtPass.Text = A1.MusteriNo.ToString();
            MessageBox.Show(A1.Hesaplarim().ElementAt(0).ArtiHesap.Limit.ToString());
            A1.Hesaplarim().ElementAt(0).ArtiHesap.IslemYap(new Yatir(CMG.SeciliSube().SeciliHesap().HesapNo,2000));
            Ceo A3 = new Ceo("Cengiz", "Cebeci", 30151295680, 3000, "1234");
            A1.Hesaplarim().ElementAt(0).ArtiHesap.HesapOzeti();
            Izmir.PersonelEkle(A3);
            txtNo.Text = A3.PersonelNo.ToString();
            
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            Kur dolar = new Kur("Dolar","USD","$" ,3.85545M);
            Kur pound = new Kur("Pound", "GBP", "£",4.53245M);
            CMG.KurEkle(dolar);
            CMG.KurEkle(pound); 
            A1.HesapIndeksi(A1.Hesaplarim().ElementAt(0).HesapNo);
            //A1.SeciliHesap().IslemYap(new Yatir(A1.Hesaplarim().ElementAt(0).HesapNo, 1000));
            A1.SeciliHesap().IslemYap(new Havale(A1.SeciliHesap().HesapNo,100,A2.Hesaplarim().ElementAt(0)));
            MessageBox.Show(A2.Hesaplarim().ElementAt(0).Bakiye.ToString());
            CMG.SubeIndeksi(txtGizli.Text);
            MessageBox.Show(CMG.SeciliSube().SeciliHesap().Bakiye.ToString());
            MessageBox.Show(CMG.Gelir.ToString()); 
            Giris_Ekrani frm = new Giris_Ekrani();
            frm.ShowDialog(this);
            this.Close();
            
        }
        
    }
}
