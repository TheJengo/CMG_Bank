using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMG_Bank
{
    public class EkHesap
    {
        public const decimal GunlukFaizOrani = 0.0184M;
        public const decimal VergiOrani = 0.5M;
        public decimal VergiTutari { get; private set; }
        public decimal FaizTutari { get; private set; }
        public string HesapNo { get; private set; }
        public decimal Limit { get; private set; }

        private decimal odenecekTutar;
        public DateTime OlusturmaTarihi { get; private set; }
        public DateTime VadeTarihi { get; set; }
        private List<Islem> HesapIslemleri;
        public EkHesap(DateTime VadeTarihi, decimal Limit)
        {
            this.Limit = Limit;
            this.OlusturmaTarihi = DateTime.Now;
            this.VadeTarihi = VadeTarihi;
            this.HesapNo = Banka.BankaBilgisiGetir().SayiUret(4, 1);
            this.HesapIslemleri = new List<Islem>();
            this.FaizTutari = FaizTutariHesapla();
            this.VergiTutari = VergiTutariHesapla();
            this.odenecekTutar = Limit + FaizTutari;
            Banka.BankaBilgisiGetir().Giderler(new Cek("123",Limit));
        }
        public bool HesapKapama()
        {
            if(odenecekTutar == 0)
            {
                return true;
            }
            return false;
        }
        public decimal FaizTutariHesapla()
        {
            return (VadeTarihi - OlusturmaTarihi).Days * GunlukFaizOrani;
        }
        public decimal VergiTutariHesapla()
        {
            return FaizTutari * VergiOrani;
        }    
        public List<Islem> HesapOzeti()
        {
            Banka.BankaBilgisiGetir().Gelirler(new Yatir("123",2.30M));
            return this.HesapIslemleri;
        }
        public bool IslemYap(Islem yapilanIslem)
        {
            yapilanIslem.islemSonucu = true;
            this.HesapIslemleri.Add(yapilanIslem);
            /* Para Yatırma İşlemi */
            if (yapilanIslem is Yatir)
            {
                this.odenecekTutar -= yapilanIslem.Miktar;
                return true;
            }
            /* Para Çekme İşlemi */
            if (yapilanIslem is Cek)
            {
                if (this.odenecekTutar > yapilanIslem.Miktar && yapilanIslem.Miktar > 0)
                {
                    Banka.BankaBilgisiGetir().Gelirler(new Yatir("123", 2.30M));
                    this.odenecekTutar += yapilanIslem.Miktar;
                    return true;
                }
            }
            yapilanIslem.islemSonucu = false;
            return false;
        }
    }
}
