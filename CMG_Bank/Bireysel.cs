using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMG_Bank
{
    public class Bireysel : Musteri
    {
        public Bireysel(string Ad, string Soyad, long TCKNO, long TelNo, string Adres, string AnneKizlikSoyadi, string GizliSoru, string gizliSoruCevabi, string sifre)
            : base(Ad, Soyad, TCKNO, TelNo, Adres, AnneKizlikSoyadi, GizliSoru, gizliSoruCevabi, sifre)
        {
          
        }
    }
}
