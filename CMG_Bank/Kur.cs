using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMG_Bank
{
    public class Kur
    {
        public string ParaBirimi { get; private set; }
        public string BirimKodu { get; private set; }
        public char Sembol { get; private set; }
        public decimal Oran { get; private set; }
        public Kur(string ParaBirimi, string BirimKodu,char Sembol,decimal Oran)
        {
            this.ParaBirimi = ParaBirimi;
            this.BirimKodu = BirimKodu;
            this.Sembol = Sembol;
            this.Oran = Oran;
        }
        public void Guncelle(string ParaBirimi, string BirimKodu, char Sembol, decimal Oran)
        {
            this.ParaBirimi = ParaBirimi;
            this.BirimKodu = BirimKodu;
            this.Sembol = Sembol;
            this.Oran = Oran;
        }
    }
}
