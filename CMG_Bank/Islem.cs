using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMG_Bank
{
    public abstract class Islem
    {
        public DateTime IslemTarihi { get; private set; }
        public decimal Miktar { get; private set; }
        public long HesapNo { get; private set; }

        public Islem(long HesapNo, decimal Miktar)
        {
            this.IslemTarihi = DateTime.Now;
            this.Miktar = Miktar;
            this.HesapNo = HesapNo;
        }

        public abstract string Rapor(bool islemSonucu);

        }
}
