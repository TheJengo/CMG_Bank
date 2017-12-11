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
        public Form1()
        {
            InitializeComponent();
        }
        Banka CMG = Banka.BankaBilgisiGetir();
        Bireysel A1 = new Bireysel("Cengiz", "Cebeci", 30151295680, 5071278994, "Turgutlu/Manisa", "Çalışkan", "En Sevdiğin Renk", "Fuşya","1234");
        Ticari A2 = new Ticari("Cengiz", "Cebeci", 30151295680, 5071278994, "Turgutlu/Manisa", "Çalışkan", "En Sevdiğin Renk", "Fuşya", 1234,"1234");
       
        Hesap h1 = new Hesap();
        Hesap h3 = new Hesap();
        
        private void mtrlIlerle_Click(object sender, EventArgs e)
        {
            A1.HesapEkle(h1);
            h1.IslemYap(new Yatir(h1.HesapNo, 1000)); //MessageBox.Show(h1.Bakiye.ToString());
            A1.HesapEkle(h3);
            Havale havale1 = new Havale(h1.HesapNo,1000,h3);
            //MessageBox.Show(h3.Bakiye.ToString());
            //MessageBox.Show(havale1.Rapor(h1.IslemYap(havale1)));
            h3.IslemYap(new Yatir(h3.HesapNo, 1000));
            //h3.IslemYap(new Cek(h3.HesapNo, 400));
            //h3.IslemYap(new Cek(h3.HesapNo, 200));
            h3.IslemYap(new Cek(h3.HesapNo, 200));
            MessageBox.Show(h3.HesapIslemleri.Count.ToString());
            if(A1.GizliSoruKontrol(txtGizli.Text) == true)
            {
                int i = 0;
                foreach (Hesap h2 in A1.Hesaplar)
                {
                    Yatir y1 = new Yatir("123", Convert.ToDecimal(1000));
                    h2.IslemYap(y1);
                    MessageBox.Show(y1.Rapor().ToString());
                    i++;
                    MessageBox.Show(h2.Bakiye.ToString()+" "+i.ToString());
                }

            }
            CMG.MusteriEkle(A1);
            CMG.MusteriEkle(A2);
            txtGizli.Text = (CMG.MusteriListele().ElementAt(0).MusteriNo);
          //  MessageBox.Show(h1.Bakiye.ToString());
             MessageBox.Show(h3.Bakiye.ToString());
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {

            h3.IslemYap(new Cek(h3.HesapNo, 300));           
            if (A1.GirisYap(txtNo.Text, txtPass.Text))
            {
                MessageBox.Show(A1.Hesaplar.ElementAt(1).Bakiye.ToString());
            }

        }
        
    }
}
