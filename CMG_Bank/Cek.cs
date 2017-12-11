﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMG_Bank
{
    public class Cek : Islem
    {
        public Cek(string HesapNo,decimal Miktar) : base(HesapNo,Miktar)
        {

        }
     
 	   public override string Rapor()
        {
 	        if(this.islemSonucu == true)
            {
                return Miktar+" TL Para Çekme işlemi başarısız.";
            }
            return "Para Çekme işlemi başarısız.";
        }
    }
}
