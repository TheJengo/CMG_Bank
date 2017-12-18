using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMG_Bank
{
    public class Banka
    {
        private Random getRandom = new Random();

        private int aktifSubeIndeksi;
        public decimal KaynakPara { get; private set; }
        public string Adi { get; private set; }
        public int BankaKodu { get; private set; }
        public decimal Gelir { get; private set; }
        public decimal Gider { get; private set; }

        private static Banka BankaNesnesi;

        private List<Sube> Subeler;
        private List<Musteri> Musteriler;
        private List<Kur> Kurlar;
        private List<Islem> Islemler;

        private Banka()
        {
            Musteriler = new List<Musteri>();
            Subeler = new List<Sube>();
            Kurlar = new List<Kur>();
            Islemler = new List<Islem>();
        }
        public static Banka BankaBilgisiGetir()
        {
            if(BankaNesnesi == null)
            {
                BankaNesnesi = new Banka();
            }
            return BankaNesnesi;
        }
        public Sube SeciliSube()
        {
            if (this.Subeler.Count > aktifSubeIndeksi && aktifSubeIndeksi >= 0)
                return Subeler.ElementAt(aktifSubeIndeksi);
            else
                return null;
        }
        public void SubeIndeksi(string SubeKodu)
        {
            int subeIndeksi = 0;
            foreach (Sube _Sube in Subeler)
            {               
                if (_Sube.SubeKodu == SubeKodu)
                {
                    this.aktifSubeIndeksi = subeIndeksi;
                }
                subeIndeksi++;
            }
        }
        public void SubeEkle(Sube S)
        {
            int subeSayac = 0;
            string geciciSubeKodu = "";
            do
            {
                geciciSubeKodu = SayiUret(5, 1);
                foreach (Sube _Sube in Subeler)
                {
                    if (_Sube.SubeKodu == geciciSubeKodu)
                    {
                        break;
                    }
                    subeSayac++;
                }
            } while (Subeler.Count != subeSayac);
            S.KodAl(geciciSubeKodu);
            Subeler.Add(S);
        } 
        public List<Sube> SubeListesi()
        {
            return this.Subeler;
        }     
        public bool KurEkle(Kur K)
        {
            int kurIndeksi = 0;
            foreach (Kur _Kur in Kurlar)
	        {
		         if(_Kur.BirimKodu ==  K.BirimKodu )
                 {
                     break;
                 }
                 kurIndeksi++;
            }
            if (kurIndeksi != Kurlar.Count)
            {
                return false;
            }
            Kurlar.Add(K);
            return true;        
        }
        public decimal KurGetir(string ParaBirimi)
        {
            foreach (Kur _Kur in Kurlar)
            {
                if (_Kur.ParaBirimi == ParaBirimi)
                {
                    return _Kur.Oran;
                }
            }
            return 1;
        }
        public List<Kur> KurListesi()
        {
            return this.Kurlar;
        }
        public void MusteriEkle(Musteri M)
        {
            int musteriSayac = 0;
            string geciciMusteriNo = "";
            do{
               geciciMusteriNo = SayiUret(4, 2);
               foreach (Musteri _Musteri in Musteriler)
               {
                   if (_Musteri.MusteriNo == geciciMusteriNo)
                    {
                        break;
                    }
                   musteriSayac++;
               }
            } while (Musteriler.Count != musteriSayac);
            M.NumaraAl(geciciMusteriNo);
            Musteriler.Add(M);
        }
        public List<Musteri> MusteriListele()
        {
            return this.Musteriler;
        }
        public void HesapNumarasiOlustur(Hesap H)
        {
            int musteriSayac = 0, hesapSayac = 0;
            string geciciNumara = "";
            do
            {
                geciciNumara = SayiUret(3, 4); //Hesap No
                foreach (Musteri _Musteri in Musteriler)
                {   
                   foreach(Hesap _Hesap in _Musteri.Hesaplarim())
                   {
                        if (_Hesap.HesapNo == geciciNumara)
                        {
                            break;
                        }
                        hesapSayac++;
                   }
                   if (_Musteri.Hesaplarim().Count != hesapSayac)
                   {
                       break;
                   }
                   musteriSayac++;
                } 
            } while (Musteriler.Count != musteriSayac);
            H.NumaraAl(geciciNumara);
        }
        public void BilgileriDuzenle(string Adi, int BankaKodu, decimal KaynakPara)
        {
            this.KaynakPara = KaynakPara;
            this.Adi = Adi;
            this.BankaKodu = BankaKodu;
        }
        public string SayiUret(int basamakSayisi, int Miktar)
        {
            string temp = "";
            for (int i = 0; i < Miktar; i++)
            {
                temp += getRandom.Next(0, Convert.ToInt32((Math.Pow(10,basamakSayisi))-1)).ToString("D"+basamakSayisi);
            }
            return temp;
        }
        public List<Islem> Rapor()
        {
            foreach (Sube _Sube in SubeListesi())
            {
                foreach (Hesap _Hesap in _Sube.Hesaplar)
                {
                    foreach (Islem _Islem in _Hesap.HesapIslemleri)
                    {
                        if (_Islem is Yatir)
                        {
                            this.Islemler.Add(_Islem);
                            this.Gelir += _Islem.Miktar;
                        }
                        if (_Islem is Cek)
                        {
                            this.Islemler.Add(_Islem);
                            this.Gider -= _Islem.Miktar;
                        }
                    }
                }
            }
            return this.Islemler;
        }

    }
}
