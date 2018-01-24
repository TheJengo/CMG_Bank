using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMG_Bank
{
    public class Yatir : Islem
    {
        public Yatir(string HesapNo,decimal Miktar) : base(HesapNo,Miktar)
        {

        }
        public override string Rapor()
        {
 	        if(this.islemSonucu == true)
            {
                return "Hesabınıza yapmış olduğunuz, "+Environment.NewLine + Miktar+" yatırma işlemi başarılı!"+Environment.NewLine+"İyi günler Dileriz :)";
            }
            return "Havale işlemi başarısız!";
        }
    }
}
