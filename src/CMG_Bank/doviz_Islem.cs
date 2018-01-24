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
    public partial class doviz_Islem : UserControl
    {
        Banka CMG = Banka.BankaBilgisiGetir();
        public doviz_Islem()
        {
            InitializeComponent();
            mlvKur.Items.Clear();
            foreach (Kur _Kur in CMG.KurListesi())
            {
                ListViewItem eleman = new ListViewItem(_Kur.BirimAdi);
                eleman.SubItems.Add(_Kur.BirimKodu);
                eleman.SubItems.Add(_Kur.Sembol.ToString());
                eleman.SubItems.Add(_Kur.Oran.ToString());
                mlvKur.Items.Add(eleman);
            }
        }
        int indeks = 0;
        private void mlvKur_SelectedIndexChanged(object sender, EventArgs e)
        {
            indeks = Convert.ToInt32(mlvKur.FocusedItem.Index);
            txtGuncelleIsim.Text = CMG.KurListesi().ElementAt(indeks).BirimAdi;
            txtGuncelleKod.Text = CMG.KurListesi().ElementAt(indeks).BirimKodu;
            txtGuncelleSembol.Text = CMG.KurListesi().ElementAt(indeks).Sembol.ToString();
            txtGuncelleOran.Text = CMG.KurListesi().ElementAt(indeks).Oran.ToString();           
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            Kur _Kur = CMG.KurListesi().ElementAt(indeks);
            if(txtGuncelleIsim.Text == "" || txtGuncelleKod.Text == "" || txtGuncelleSembol.Text == "" || txtGuncelleOran.Text == "" )
            {
                lblOlumluSonuc.Visible = false;
                lblOlumsuzSonuc.Visible = true;
            }
            else
            {
                _Kur.Guncelle(txtGuncelleIsim.Text, txtGuncelleKod.Text, txtGuncelleSembol.Text, Convert.ToDecimal(txtGuncelleOran.Text));
                lblOlumsuzSonuc.Visible = false;           
                lblOlumluSonuc.Visible = true;
            }
        }

        private void btnKurEkle_Click(object sender, EventArgs e)
        {
            if (txtKurAdi.Text == "" || txtKurKodu.Text == "" || txtKurSembol.Text == "" || txtKurOrani.Text == "")
            {
                lblOlumluSonuc.Visible = false;
                lblOlumsuzSonuc.Visible = true;
            }
            else
            {
                Kur yeniKur = new Kur(txtKurAdi.Text, txtKurKodu.Text, txtKurSembol.Text, Convert.ToDecimal(txtKurOrani.Text));
                CMG.KurEkle(yeniKur);
                lblOlumsuzSonuc.Visible = false;
                lblOlumluSonuc.Visible = true;
            }
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            mlvKur.Items.Clear();
            foreach (Kur _Kur in CMG.KurListesi())
            {
                ListViewItem eleman = new ListViewItem(_Kur.BirimAdi);
                eleman.SubItems.Add(_Kur.BirimKodu);
                eleman.SubItems.Add(_Kur.Sembol.ToString());
                eleman.SubItems.Add(_Kur.Oran.ToString());
                mlvKur.Items.Add(eleman);
            }
            lblOlumsuzSonuc.Visible = false;
            lblOlumluSonuc.Visible = false;
        }



    }
}
