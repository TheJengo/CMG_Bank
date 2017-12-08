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

        public Hesap()
        {
            this.Bakiye = 0;
            this.Status = "Aktif";
            this.OlusturmaTarihi = DateTime.Now;
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
        public void IslemYap(){

        }

    }
}
