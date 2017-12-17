using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMG_Bank
{
    public abstract class Hesap
    {
        public decimal Bakiye { get; private set; }
        public string Status { get; private set; }
        public DateTime OlusturmaTarihi { get; private set; }
        public string HesapNo { get; private set; }
        public EkHesap ArtiHesap { get; set; }
        public List<Islem> HesapIslemleri { get; private set; }
        public string ParaBirimi { get; set; }

        public Hesap()
        {
            this.Bakiye = 0;
            this.Status = "Aktif";
            this.OlusturmaTarihi = DateTime.Now;
            this.HesapIslemleri = new List<Islem>();
            Banka.BankaBilgisiGetir().HesapNumarasiOlustur(this);
            this.ParaBirimi = "TRY";
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
        public bool IslemYap(Islem yapilanIslem)
        {
            yapilanIslem.islemSonucu = true;
            this.HesapIslemleri.Add(yapilanIslem);
            /* Para Yatırma İşlemi */
            if (yapilanIslem is Yatir)
            {
                this.Bakiye += yapilanIslem.Miktar;
                return true;
            }
            /* Para Çekme İşlemi */
            if(yapilanIslem is Cek)
            {
                decimal gunlukCekilen = 0;
                foreach (Islem _Islem in HesapIslemleri)
                {
                    if(_Islem is Cek)
                    {
                        if ((DateTime.Today - _Islem.IslemTarihi).TotalDays < 1)
                        {
                            gunlukCekilen += _Islem.Miktar;
                        }
                    }
                }
                if(this.Bakiye > yapilanIslem.Miktar && gunlukCekilen <= 750 && yapilanIslem.Miktar > 0)
                {
                    this.Bakiye -= yapilanIslem.Miktar;
                    return true;
                }
            }
            /* Havale İşlemi */
            if(yapilanIslem is Havale)
            {
                Havale yapilanHavale =(Havale) yapilanIslem;
                if (this.HesapNo == yapilanIslem.HesapNo)
                {
                    if (this.Bakiye >= yapilanHavale.Miktar && this.Bakiye > 0)
                    {
                        this.Bakiye -= yapilanHavale.Miktar;
                        decimal dovizHavalesi = yapilanHavale.Miktar;
                        if (yapilanHavale.aliciHesap is Doviz)
                        {                            
                            Doviz alici =(Doviz) yapilanHavale.aliciHesap;
                            if(this is Doviz)
                            {
                                Doviz gonderen = (Doviz) this;
                                dovizHavalesi *= alici.Kur;
                                dovizHavalesi /= gonderen.Kur;
                     
                            }
                            else
                            {
                                dovizHavalesi *= alici.Kur;  
                            }
                        }
                        yapilanHavale.aliciHesap.Bakiye += dovizHavalesi;
                        yapilanHavale.aliciHesap.HesapIslemleri.Add(yapilanHavale);
                        return true;
                    }
                }
            }
            yapilanIslem.islemSonucu = false;
            return false;
        }

        public virtual void EkHesapAc(DateTime VadeTarihi, decimal Limit){}
    }
}
