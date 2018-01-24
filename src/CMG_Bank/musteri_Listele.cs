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
    public partial class musteri_Listele : UserControl
    {
        Banka CMG = Banka.BankaBilgisiGetir();
        public void BilgiGetir()
        {
            mlvMusteriListesi.Items.Clear();
            int i = 0;
            foreach (Musteri _Musteri in CMG.MusteriListele())
            {
                i++;
                ListViewItem eleman = new ListViewItem(i.ToString());
                eleman.SubItems.Add(_Musteri.MusteriNo);
                eleman.SubItems.Add(_Musteri.Ad + " " + _Musteri.Soyad.ToUpper());
                eleman.SubItems.Add(_Musteri.Hesaplarim().Count.ToString());
                char[] AnneKizlikSoyadi = _Musteri.AnneKizlikSoyadi.ToCharArray();
                string sifreliAnneKizlikSoyadi = "";
                for (int j = 0; j < AnneKizlikSoyadi.Length; j++)
                {
                    if (j == AnneKizlikSoyadi.Length - 1 || j == 1)
                    {
                        sifreliAnneKizlikSoyadi += AnneKizlikSoyadi[j];
                    }
                    else
                    {
                        sifreliAnneKizlikSoyadi += "_";
                    }

                }
                eleman.SubItems.Add(sifreliAnneKizlikSoyadi);
                if(_Musteri is Bireysel)
                {
                    eleman.SubItems.Add("Bireysel");
                }
                else
                {

                    eleman.SubItems.Add("Ticari");
                }
                eleman.SubItems.Add(_Musteri.TelNo.ToString());
                mlvMusteriListesi.Items.Add(eleman);
            }
        }
        public musteri_Listele()
        {
            InitializeComponent();
            BilgiGetir();           
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            BilgiGetir();
        }
    }
}
