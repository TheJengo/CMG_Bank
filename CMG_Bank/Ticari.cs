using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMG_Bank
{
    public class Ticari : Musteri
    {
        public long VergiNo { get; private set; }
        public Ticari(string Ad, string Soyad, long TCKNO, long TelNo, string Adres, string AnneKizlikSoyadi, string GizliSoru, string gizliSoruCevabi,long VergiNo, string sifre)
            : base(Ad, Soyad, TCKNO, TelNo, Adres, AnneKizlikSoyadi, GizliSoru, gizliSoruCevabi, sifre)
        {
            this.VergiNo = VergiNo;
        }
    }
}
