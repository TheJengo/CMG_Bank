using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMG_Bank
{
    public abstract class Personel : Kimlik
    {
        public DateTime IseAlimTarihi { get; private set; }
        public string PersonelNo { get; private set; }
        public decimal Maas { get; set; }

        private string sifre;
        public Personel(string Ad, string Soyad, long TCKNO, decimal Maas, string sifre) : base(Ad,Soyad,TCKNO)
        {
            this.Maas = Maas;
            this.sifre = sifre;
            IseAlimTarihi = DateTime.Now;
            //Banka.BankaBilgisiGetir().SeciliSube().SeciliHesap().IslemYap(new Cek(Banka.BankaBilgisiGetir().SeciliSube().Hesaplar.ElementAt(0).HesapNo, this.Maas));
        }
        public bool GirisYap(string PersonelNo, string gelenSifre)
        {
            if (this.PersonelNo == PersonelNo && gelenSifre == sifre)
            {
                return true;
            }
            return false;
        }
        public void BilgileriGuncelle(string Ad, string Soyad, long TCKNO, decimal Maas)
        {
            this.Ad = Ad;
            this.Soyad = Soyad;
            this.TCKNO = TCKNO;
            this.Maas = Maas;
        }
        public void SifreDegistir(string yeniSifre)
        {
            this.sifre = yeniSifre;
        }
        public void PersonelNoAl(string gelenNo)
        {
            this.PersonelNo = gelenNo;
        }
        
    }
}
