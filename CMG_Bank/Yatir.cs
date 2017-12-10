using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMG_Bank
{
    public class Yatir : Islem
    {
        public Yatir(long HesapNo,decimal Miktar) : base(HesapNo,Miktar)
        {

        }
        public override string Rapor(bool islemSonucu)
        {
 	        if(islemSonucu == true)
            {
                return "Hesabınıza yapmış olduğunuz, "+Miktar+" TL yatırma işlemi başarıyla tamamlandı!";
            }
            return "Havale işlemi başarısız!";
        }
    }
}
