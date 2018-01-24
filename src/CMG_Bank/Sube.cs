using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMG_Bank
{
    public class Sube
    {

        private int aktifHesapIndeksi;
        public string SubeKodu { get; set; }
        public string Ad { get; private set; }
        public string Adres { get; private set; }
        public List<Hesap> Hesaplar { get; private set; }
        public List<Personel> Personeller { get; private set; }
        public DateTime OlusturulmaTarihi { get; set; }

        public Sube(string Ad, string Adres)
        {
            this.Ad = Ad;
            this.Adres = Adres;
            this.OlusturulmaTarihi = DateTime.Now;
            Hesaplar = new List<Hesap>();
            Personeller = new List<Personel>();
        }
        public void Guncelle(string Ad, string Adres)
        {
            this.Ad = Ad;
            this.Adres = Adres;
        }
        public void KodAl(string SubeKodu)
        {
            this.SubeKodu = SubeKodu;
        }
        public void HesapEkle(Hesap H)
        {
            this.Hesaplar.Add(H);
        }
        public List<Personel> PersonelListesi()
        {
            return this.Personeller;
        }
        public void PersonelEkle(Personel P)
        {
            int personelSayac = 0;
            string geciciNumara = "";
            do
            {
                geciciNumara = this.SubeKodu + "" + Banka.BankaBilgisiGetir().SayiUret(3, 1);
                foreach (Personel _Personel in Personeller)
                {
                    if (_Personel.PersonelNo == geciciNumara)
                    {
                        break;
                    }
                    personelSayac++;
                }
            } while (Personeller.Count != personelSayac);
            P.PersonelNoAl(geciciNumara);
            Personeller.Add(P);
        }
        public void PersonelCikar(Personel P)
        {
            Personeller.Remove(P);
        }
        public Personel PersonelGetir(string personelNo)
        {
            foreach (Personel _Personel in this.PersonelListesi())
            {
                if (_Personel.PersonelNo == personelNo)
                {
                    return _Personel;
                }
            }
            return null;
        }
        public List<Islem> Rapor(Hesap H)
        {
            foreach (Hesap _Hesap in Hesaplar)
            {
                if(_Hesap == H)
                {
                    return _Hesap.HesapIslemleri;
                }
            }
            return null;
        }
        public void HesapIndeksi(string HesapNo)
        {
            int hesapIndeksi = 0;
            foreach (Hesap _Hesap in Hesaplar)
            {
                if(_Hesap.HesapNo == HesapNo)
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
