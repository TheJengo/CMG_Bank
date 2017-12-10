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
        Bireysel A1 = new Bireysel("Cengiz", "Cebeci", 30151295680, 5071278994, "Turgutlu/Manisa", "Çalışkan", "En Sevdiğin Renk", "Fuşya","1234");
        Hesap h1 = new Hesap();
        Hesap h3 = new Hesap();
        
        private void mtrlIlerle_Click(object sender, EventArgs e)
        {
            A1.HesapEkle(h1);
            h1.IslemYap(new Yatir(h1.HesapNo, 1000)); MessageBox.Show(h1.Bakiye.ToString());
            A1.HesapEkle(h3);
            Havale havale1 = new Havale(h1.HesapNo,1000,h3);
            
            MessageBox.Show(havale1.Rapor(h1.IslemYap(havale1)));
            if(A1.GizliSoruKontrol(txtGizli.Text) == true)
            {
                int i = 0;
                foreach (Hesap h2 in A1.Hesaplar)
                {
                    Yatir y1 = new Yatir(123, Convert.ToDecimal(1000));
                    h2.IslemYap(y1);
                    MessageBox.Show(y1.Rapor(true).ToString());
                    i++;
                    MessageBox.Show(h2.Bakiye.ToString()+" "+i.ToString());
                }

            }
            MessageBox.Show(h1.Bakiye.ToString());
            MessageBox.Show(h3.Bakiye.ToString());
        }
        
    }
}
