using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMG_Bank
{

    /// <summary>
    /// Bireysel Class
    /// </summary>
    public class Bireysel : Musteri
    {
        /// <summary>
        /// Ctor Bireysel Class
        /// 
        /// </summary> 
        /// <param name="Ad">Sample param 1</param>
        /// <param name="Soyad"></param>
        /// <param name="TCKNO"></param>
        /// <param name="TelNo"></param>
        /// <param name="Adres"></param>
        /// <param name="AnneKizlikSoyadi"></param>
        /// <param name="GizliSoru"></param>
        /// <param name="gizliSoruCevabi"></param>
        /// <param name="sifre"></param>
        public Bireysel(string Ad, string Soyad, long TCKNO, long TelNo, string Adres, string AnneKizlikSoyadi, string GizliSoru, string gizliSoruCevabi, string sifre)
            : base(Ad, Soyad, TCKNO, TelNo, Adres, AnneKizlikSoyadi, GizliSoru, gizliSoruCevabi, sifre)
        {
          
        }

    }
}
