using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMG_Bank
{
    public class Kur
    {
        public string BirimAdi { get; private set; }
        public string BirimKodu { get; private set; }
        public string Sembol { get; private set; }
        public decimal Oran { get; private set; }
        public Kur(string BirimAdi, string BirimKodu, string Sembol, decimal Oran)
        {
            this.BirimAdi = BirimAdi;
            this.BirimKodu = BirimKodu;
            this.Sembol = Sembol;
            this.Oran = Oran;
        }
        public void Guncelle(string BirimAdi, string BirimKodu, string Sembol, decimal Oran)
        {
            this.BirimAdi = BirimAdi;
            this.BirimKodu = BirimKodu;
            this.Sembol = Sembol;
            this.Oran = Oran;
        }
    }
}
