using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMG_Bank
{
    public class Hesap
    {
        public decimal Bakiye { get; private set; }
        public string Status { get; private set; }
        public DateTime OlusturmaTarihi { get; private set; }
        public long HesapNo { get; private set; }

        public List<Islem> HesapIslemleri { get; set; }

        public Hesap()
        {
            this.Bakiye = 0;
            this.Status = "Aktif";
            this.OlusturmaTarihi = DateTime.Now;
            this.HesapIslemleri = new List<Islem>();
        }
        public bool HesapKapama()
        {
            if(Bakiye == 0)
            {
                this.Status = "Pasif";
                return true;
            }
            return false;
        }
        public void Ozet()
        {

        }
        public bool IslemYap(Islem yapilanIslem)
        {
            /* Para Yatırma İşlemi */
            if (yapilanIslem is Yatir)
            {
                this.Bakiye += yapilanIslem.Miktar;
                return true;
            }
            /* Para Çekme İşlemi */
            if(yapilanIslem is Cek)
            {
                if (yapilanIslem.Miktar < 750 && yapilanIslem.Miktar > 0)
                {
                    this.Bakiye -= yapilanIslem.Miktar;
                    return true;
                }
            }
            if(yapilanIslem is Havale)
            {
                Havale yapilanHavale =(Havale) yapilanIslem;
                if (this.HesapNo == yapilanIslem.HesapNo)
                {
                    if (this.Bakiye >= yapilanHavale.Miktar)
                    {
                        this.Bakiye -= yapilanHavale.Miktar;
                        yapilanHavale.aliciHesap.Bakiye += yapilanHavale.Miktar;
                        yapilanHavale.aliciHesap.HesapIslemleri.Add(yapilanHavale);
                        return true;
                    }
                }
            }
            return false;
        }

    }
}
