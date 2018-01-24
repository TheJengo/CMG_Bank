using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMG_Bank
{
    public class Havale : Islem
    {
        public Hesap aliciHesap { get; private set; }
        public EkHesap aliciEkHesap { get; private set; }
        public string gonderenAdi { get; private set; }
        public string aliciAdi { get; private set; }
        public Havale(string HesapNo, decimal Miktar, Hesap aliciHesap ) : base(HesapNo,Miktar)
        {
            this.aliciHesap = aliciHesap;
            MusteriIsimGetir(HesapNo,aliciHesap.HesapNo);
        }
        public Havale(string HesapNo, decimal Miktar, EkHesap aliciEkHesap)
            : base(HesapNo, Miktar)
        {
            this.aliciEkHesap = aliciEkHesap;
        }
        public void MusteriIsimGetir(string gonderenHesapNo,string aliciHesapNo)
        {
            foreach (Musteri _Musteri in Banka.BankaBilgisiGetir().MusteriListele())
            {
                foreach (Hesap _Hesap in _Musteri.Hesaplarim())
                {
                    if (_Hesap.HesapNo == gonderenHesapNo)
                    {
                        this.gonderenAdi = _Musteri.Ad + " " + _Musteri.Soyad.ToUpper();
                    }
                    if(_Hesap.HesapNo == aliciHesapNo )
                    {
                        this.aliciAdi = _Musteri.Ad + " " + _Musteri.Soyad.ToUpper();
                    }
                }
            }
        }
        public override string Rapor()
        {
            if(aliciHesap != null && this.islemSonucu == true)
            {
                    return "Havale işlemi yaptığınız "+Environment.NewLine+aliciHesap.HesapNo + "'lu kişiye "+Environment.NewLine + Miktar + " TL Havale yapılmıştır.";
            }
            if (aliciEkHesap != null && this.islemSonucu == true)
            {
                return "Havale işlemi yaptığınız " + Environment.NewLine + aliciEkHesap.HesapNo + "'lu kişiye " + Environment.NewLine + Miktar + " TL Havale yapılmıştır.";
            }	        
            return "Havale işlemi başarısız.";
        }
    }
}
