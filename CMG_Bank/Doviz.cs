using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMG_Bank
{
    public class Doviz : Hesap
    {
        public decimal Kur { get; set; }
        public Doviz(string ParaBirimi) : base()
        {
            this.ParaBirimi = ParaBirimi;
        }
        public decimal KurHesapla()
        {
            if(this.ParaBirimi == "USD")
            {
                return 3.8668M;
            }
            if(this.ParaBirimi == "EUR")
            {
                return 4.5459M;
            }
            if(this.ParaBirimi == "GBP")
            {
                return 4.5459M;
            }
            return 0;
        }
    }
}
