using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMG_Bank
{
    public abstract class Musteri : Kimlik
    {
        public List<Hesap> Hesaplar { get; private set; }
        public long TelNo { get; private set; }
        public string Adres { get; private set; }
        public string AnneKizlikSoyadi { get; set; }
        public string GizliSoru { get; private set; }

        private string gizliSoruCevabi;
        public int MusteriNo { get; private set; }

        private string sifre;
        public Musteri(string Ad, string Soyad, long TCKNO, long TelNo, string Adres, string AnneKizlikSoyadi, string GizliSoru, string gizliSoruCevabi, string sifre)
            : base(Ad, Soyad, TCKNO)
        {
            this.Adres = Adres;
            this.AnneKizlikSoyadi = AnneKizlikSoyadi;
            this.TelNo = TelNo;
            this.GizliSoru = GizliSoru;
            this.gizliSoruCevabi = gizliSoruCevabi;
            this.MusteriNo = 123;
            this.sifre = sifre;
            Hesaplar = new List<Hesap>();
        }
        public bool GizliSoruKontrol(string GizliSoruCevabi)
        {
            if(this.gizliSoruCevabi == GizliSoruCevabi)
            {
                return true;
            }
            return false;
        }
        public bool GirisYap(long MusteriNo, string gelenSifre)
        {
            if (this.MusteriNo == MusteriNo && gelenSifre == sifre)
            {
                return true;
            }
            return false;
        }
        public void SifreDegistir(string yeniSifre)
        {
            this.sifre = yeniSifre;
        }
        public void HesapEkle(Hesap H)
        {
            Hesaplar.Add(H);
        }

        //public void HesapKapama(Hesap kapatilanHesap)
        //{
        //    foreach (Hesap H in Hesaplar)
        //    {
        //        if(kapatilanHesap == H && H.HesapKapama() == true)
        //        {
        //            H.Status = "Pasif";
        //        }
        //    }
        //}
    }
}
