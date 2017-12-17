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
            this.ArtiHesap = new EkHesap(VadeTarihi, Limit);
        }
    }
}
