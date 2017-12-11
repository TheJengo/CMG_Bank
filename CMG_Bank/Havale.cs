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

        public Havale(string HesapNo, decimal Miktar, Hesap aliciHesap ) : base(HesapNo,Miktar)
        {
            this.aliciHesap = aliciHesap;
        }
        public override string Rapor()
        {
 	        if(this.islemSonucu == true)
            {
                return "Havale işlemi yaptığınız "+aliciHesap.HesapNo + "'lu kişiye "+Miktar+" TL Havale yapılmıştır.";
            }
            return "Havale işlemi başarısız.";
        }
    }
}
