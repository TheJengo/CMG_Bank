using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMG_Bank
{
    public abstract class Musteri : Kimlik
    {
        private List<Hesap> Hesaplar;
        private int aktifHesapIndeksi;
        private string gizliSoruCevabi;
        private string sifre;
        public long TelNo { get; private set; }
        public string Adres { get; private set; }
        public string AnneKizlikSoyadi { get; set; }
        public string GizliSoru { get; private set; }
        public DateTime OlusturulmaTarihi { get; private set; }
        public string MusteriNo { get; private set; }
    
        public Musteri(string Ad, string Soyad, long TCKNO, long TelNo, string Adres, string AnneKizlikSoyadi, string GizliSoru, string gizliSoruCevabi, string sifre)
            : base(Ad, Soyad, TCKNO)
        {
            this.Adres = Adres;
            this.AnneKizlikSoyadi = AnneKizlikSoyadi;
            this.TelNo = TelNo;
            this.GizliSoru = GizliSoru;
            this.gizliSoruCevabi = gizliSoruCevabi;
            this.sifre = sifre;
            this.OlusturulmaTarihi = DateTime.Now;
            this.Hesaplar = new List<Hesap>();
        }
        public virtual bool GizliSoruKontrol(string GizliSoruCevabi)
        {
            if(this.gizliSoruCevabi == GizliSoruCevabi)
            {
                return true;
            }
            return false;
        }
        public virtual bool GirisYap(string MusteriNo, string gelenSifre)
        {
            if (this.MusteriNo == MusteriNo && sifre == gelenSifre )
            {
                return true;
            }
            return false;
        }
        public virtual void SifreDegistir(string yeniSifre)
        {
            this.sifre = yeniSifre;
        }
        public void HesapEkle(Hesap _Hesap)
        {
            this.Hesaplarim().Add(_Hesap);
        }
        public void NumaraAl(string MusteriNo)
        {
            this.MusteriNo = MusteriNo;
        }
        public List<Hesap> Hesaplarim()
        {
            return this.Hesaplar;
        }
        public void HesapIndeksi(string HesapNo)
        {
            int hesapIndeksi = 0;
            foreach (Hesap _Hesap in Hesaplar)
            {
                if (_Hesap.HesapNo == HesapNo)
                {
                    this.aktifHesapIndeksi = hesapIndeksi;
                }

                hesapIndeksi++;
            }
        }
        public Hesap SeciliHesap()
        {
            if (this.Hesaplar.Count > aktifHesapIndeksi && aktifHesapIndeksi >= 0)
                return Hesaplar.ElementAt(aktifHesapIndeksi);
            else
                return null;
        }
    }
}
