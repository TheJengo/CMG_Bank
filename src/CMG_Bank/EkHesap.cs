using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMG_Bank
{
    public class EkHesap : IIslenebilen
    {
        public const decimal GunlukFaizOrani = 0.0613M;
        public const decimal VergiOrani = 0.0499M;
        public decimal VergiTutari { get; private set; }
        public decimal FaizTutari { get; private set; }
        public string HesapNo { get; private set; }
        public decimal Limit { get; private set; }

        private decimal odenecekTutar;

        public decimal OdenecekTutar
        {
            get { return odenecekTutar; }
        }
        
        public decimal Bakiye { get; private set; }
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
            this.FaizTutari = Math.Round(FaizTutariHesapla(),2);
            this.VergiTutari = Math.Round(VergiTutariHesapla(),2);
            this.odenecekTutar = this.Limit + FaizTutari + VergiTutari;
            this.Bakiye = Limit;
        }
        public bool HesapKapama()
        {
            if(this.odenecekTutar == 0 && this.Bakiye == 0)
            {
                return true;
            }
            return false;
        }
        public decimal FaizTutariHesapla()
        {
            return Convert.ToDecimal((VadeTarihi - OlusturmaTarihi).TotalDays)  * GunlukFaizOrani * (Limit / 100);
        }
        public decimal VergiTutariHesapla()
        {
            return FaizTutari * VergiOrani;
        }
        public List<Islem> HesapOzeti()
        {
            return this.HesapIslemleri;
        }
        public bool IslemYap(Islem yapilanIslem)
        {
            yapilanIslem.islemSonucu = true;
            this.HesapIslemleri.Add(yapilanIslem);
            /* Para Yatırma İşlemi */
            if (yapilanIslem is Yatir)
            {
                if(odenecekTutar != 0)
                {
                    Banka.BankaBilgisiGetir().SeciliSube().SeciliHesap().IslemYap((new Yatir(Banka.BankaBilgisiGetir().SeciliSube().Hesaplar.ElementAt(0).HesapNo, yapilanIslem.Miktar)));
                    this.odenecekTutar -= yapilanIslem.Miktar;
                    return true;
                }
                return false;
            }
            /* Para Çekme İşlemi */
            if (yapilanIslem is Cek)
            {
                if (this.Bakiye >= yapilanIslem.Miktar && yapilanIslem.Miktar > 0)
                {
                    this.Bakiye -= yapilanIslem.Miktar;
                    if(yapilanIslem.Miktar == Banka.IslemTutari)
                    {
                         this.odenecekTutar += yapilanIslem.Miktar;
                    }
                    return true;
                }
            }
            yapilanIslem.islemSonucu = false;
            return false;
        }
    }
}
