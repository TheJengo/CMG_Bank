using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMG_Bank
{
    public class TRY : Hesap
    {
        public TRY() : base()
        {
            this.ParaBirimi = "TRY";
        }
        public override void EkHesapAc(DateTime VadeTarihi, decimal Limit)
        {
            foreach (Sube _Sube in Banka.BankaBilgisiGetir().SubeListesi())
            {
                foreach (Hesap _Hesap in _Sube.Hesaplar)
                {
                    if (_Hesap is TRY)
                    {
                        if (_Hesap.Bakiye >= Limit)
                        {
                            this.ArtiHesap = new EkHesap(VadeTarihi, Limit);
                            _Hesap.IslemYap(new Havale(_Hesap.HesapNo, Limit, this));
                            this.IslemYap(new Havale(this.HesapNo, Limit, this.ArtiHesap));
                        }
                    }
                }
            }
        }
    }
}
