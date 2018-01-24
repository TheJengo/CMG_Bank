using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMG_Bank
{
    public class Mudur : Personel
    {
        public Mudur(string Ad, string Soyad, long TCKNO, decimal Maas, string sifre) : base(Ad,Soyad,TCKNO,Maas,sifre)
        {
        }
    }
}
