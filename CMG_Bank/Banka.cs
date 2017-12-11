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
        public decimal KaynakPara { get; private set; }
        public string Adi { get; private set; }
        public int BankaKodu { get; private set; }
        public decimal Gelir { get; private set; }
        public decimal Gider { get; private set; }

        private static Banka BankaNesnesi;

        private List<Hesap> Hesaplar;
        private List<Personel> Personeller;
        private List<Musteri> Musteriler;

        private Banka()
        {
            Hesaplar = new List<Hesap>();
            Personeller = new List<Personel>();
            Musteriler = new List<Musteri>();
        }
        public static Banka BankaBilgisiGetir()
        {
            if(BankaNesnesi == null)
            {
                BankaNesnesi = new Banka();
            }
            return BankaNesnesi;
        }
        public void MusteriEkle(Musteri M)
        {
            int musteriSayac = 0;
            string geciciMusteriNo = "";
            do{
               geciciMusteriNo = SayiUret(4, 2);
               foreach (Musteri musteri in Musteriler)
               {
                   if (musteri.MusteriNo == geciciMusteriNo)
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
        public  string[] HesapNumarasiOlustur()
        {
            int musteriSayac = 0, hesapSayac = 0;
            string[] geciciNumaralar = new string[2];
            do
            {
                geciciNumaralar[0] = SayiUret(3, 4); //Hesap No
                geciciNumaralar[1] = SayiUret(1, 4); // Ek Hesap No
                foreach (Musteri _Musteri in Musteriler)
                {   
                   foreach(Hesap _Hesap in _Musteri.Hesaplar)
                   {
                       if (_Hesap.HesapNo == geciciNumaralar[0] && _Hesap.ArtiHesap.No == geciciNumaralar[1])
                       {
                           break;
                       }
                       hesapSayac++;
                   }
                   if (_Musteri.Hesaplar.Count != hesapSayac)
                   {
                       break;
                   }
                   musteriSayac++;
                } 
            } while (Musteriler.Count != musteriSayac);
            return geciciNumaralar;

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

    }
}
