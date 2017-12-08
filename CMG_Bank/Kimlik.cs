using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMG_Bank
{
    public abstract class Kimlik
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public long TCKNO { get; set; }

        public Kimlik(string Ad, string Soyad, long TCKNO)
        {
            this.Ad = Ad;
            this.Soyad = Soyad;
            this.TCKNO = TCKNO;
        }
    }
}
