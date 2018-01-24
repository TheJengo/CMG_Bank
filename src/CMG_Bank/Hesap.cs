using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMG_Bank
{
    public abstract class Hesap : IIslenebilen
    {
        public decimal Bakiye { get; private set; }
        public string Status { get; private set; }
        public DateTime OlusturmaTarihi { get; private set; }
        public string HesapNo { get; private set; }
        public EkHesap ArtiHesap { get; set; }
        public List<Islem> HesapIslemleri { get; private set; }
        public string ParaBirimi { get; set; }
        public decimal GunlukLimit { get; set; }

        public Hesap()
        {
            this.Bakiye = 0;
            this.Status = "Aktif";
            this.OlusturmaTarihi = DateTime.Now;
            this.GunlukLimit = 750;
            this.HesapIslemleri = new List<Islem>();
            Banka.BankaBilgisiGetir().HesapNumarasiOlustur(this);
        }
        public void NumaraAl(string gelenNumara)
        {
            this.HesapNo = gelenNumara;
        }
        public bool HesapKapama()
        {
            if(this.Bakiye == 0)
            {
                if (this.ArtiHesap == null)
                {
                    this.Status = "Pasif";
                    return true;
                } 
            }
            return false;
        }
        public List<Islem> HesapOzeti()
        {
            return this.HesapIslemleri;
        }
        /* Para Çekme İşlemi */
        public bool IslemYap(Islem yapilanIslem)
        {
            yapilanIslem.islemSonucu = true;
            this.HesapIslemleri.Add(yapilanIslem);
            /* Para Yatırma İşlemi */
            if (yapilanIslem is Yatir)
            {
                if(this.Status == "Pasif")
                {
                    this.Status = "Aktif";
                }
                this.Bakiye += yapilanIslem.Miktar;
                return true;
            }
            if(yapilanIslem is Cek)
            {
                decimal gunlukCekilen = 0;
                foreach (Islem _Islem in HesapIslemleri)
                {
                    if(_Islem is Cek)
                    {
                        if(_Islem.islemSonucu)
                        {
                            if ((DateTime.Today - _Islem.IslemTarihi).TotalDays < 1)
                            {
                                gunlukCekilen += _Islem.Miktar;
                            }
                        }
                    }
                }
                if(this.Bakiye >= yapilanIslem.Miktar && gunlukCekilen <= 750 && yapilanIslem.Miktar > 0)
                {
                    this.GunlukLimit = 750 - gunlukCekilen;
                    this.Bakiye -= yapilanIslem.Miktar;
                    return true;
                }
            }
            /* Havale İşlemi */
            if (yapilanIslem is Havale)
            {
                Havale yapilanHavale = (Havale)yapilanIslem;
                if (yapilanHavale.aliciHesap != null)
                {
                    if (this.HesapNo == yapilanIslem.HesapNo)
                    {
                        if (this.Bakiye >= yapilanHavale.Miktar && this.Bakiye > 0 && yapilanHavale.Miktar > 0)
                        {
                            this.Bakiye -= yapilanHavale.Miktar;
                            decimal dovizHavalesi = yapilanHavale.Miktar;
                            if(this is Doviz)
                            {
                                Doviz gonderen = (Doviz)this;
                                if(yapilanHavale.aliciHesap is Doviz)
                                {
                                        Doviz alici = (Doviz)yapilanHavale.aliciHesap;     
                                        dovizHavalesi *= Banka.BankaBilgisiGetir().KurGetir(gonderen.ParaBirimi);
                                        dovizHavalesi /= Banka.BankaBilgisiGetir().KurGetir(alici.ParaBirimi);
                                }
                                if (yapilanHavale.aliciHesap is TRY)
                                {
                                    dovizHavalesi *= Banka.BankaBilgisiGetir().KurGetir(gonderen.ParaBirimi);
                                }
                            }
                            if(this is TRY)
                            {
                                if (yapilanHavale.aliciHesap is Doviz)
                                {
                                    Doviz alici = (Doviz)yapilanHavale.aliciHesap;
                                    TRY gonderen = (TRY)this;
                                    dovizHavalesi *= Banka.BankaBilgisiGetir().KurGetir(gonderen.ParaBirimi);
                                    dovizHavalesi /= Banka.BankaBilgisiGetir().KurGetir(alici.ParaBirimi);
                                }
                            }
                            yapilanHavale.aliciHesap.Bakiye += dovizHavalesi;
                            yapilanHavale.aliciHesap.HesapIslemleri.Add(yapilanHavale);
                            return true;
                        }
                    }
                }
                if (yapilanHavale.aliciEkHesap != null)
                {
                    if (this.HesapNo == yapilanIslem.HesapNo && this.ArtiHesap.HesapNo == yapilanHavale.aliciEkHesap.HesapNo)
                    {
                        if (this.Bakiye >= yapilanHavale.Miktar && this.Bakiye > 0 && yapilanHavale.Miktar > 0)
                        {
                            this.Bakiye -= yapilanHavale.Miktar;
                            return true;
                        }
                    }
                }
            }
                yapilanIslem.islemSonucu = false;
                return false;
        }

        public virtual void EkHesapAc(DateTime VadeTarihi, decimal Limit){}
    }
}
