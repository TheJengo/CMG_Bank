using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMG_Bank
{
    public class Personel : Kimlik
    {
        public DateTime IseAlimTarihi { get; private set; }
        public long PersonelNo { get; private set; }
        public decimal Maas { get; set; }

        private string sifre;
        public Personel(string Ad, string Soyad, long TCKNO, decimal Maas, string sifre) : base(Ad,Soyad,TCKNO)
        {
            this.Maas = Maas;
            this.sifre = sifre;
            IseAlimTarihi = DateTime.Now;
        }
        public bool GirisYap(long PersonelNo, string gelenSifre)
        {
            if (this.PersonelNo == PersonelNo && gelenSifre == sifre)
            {
                return true;
            }
            return false;
        }
        public void SifreDegistir(string yeniSifre)
        {
            this.sifre = yeniSifre;
        }
        
    }
}
