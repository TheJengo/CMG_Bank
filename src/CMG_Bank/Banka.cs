using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMG_Bank
{
    /// <summary>
    /// Banka Sınıfı:
    ///     İçinde bankamızın temel bileşenleri olan; şube, müşteri, kur ve işlem bilgilerini tutan,
    /// Singleton Design Pattern'i kullanılmış sınıftır.
    /// </summary>
    public class Banka
    {
        private Random getRandom = new Random();

        private int aktifSubeIndeksi;
        public const decimal IslemTutari = 2.30M;
        public decimal KaynakPara { get; private set; }
        public string Adi { get; private set; }
        public string BankaKodu { get; private set; }
        public decimal Gelir { get; private set; }
        public decimal Gider { get; private set; }

        private static Banka BankaNesnesi;

        private List<Sube> Subeler;
        private List<Musteri> Musteriler;
        private List<Kur> Kurlar;
        private List<Islem> Islemler;

        private Banka()
        {
            Musteriler = new List<Musteri>();
            Subeler = new List<Sube>();
            Kurlar = new List<Kur>();
            Islemler = new List<Islem>();
        }
        public static Banka BankaBilgisiGetir()
        {
            if(BankaNesnesi == null)
            {
                BankaNesnesi = new Banka();
            }
            return BankaNesnesi;
        }
        /// <summary>
        /// Banka üzerinde işlem yapılmak üzere Şube Listesinden seçili olan şubeyi 
        /// döndüren metod.
        /// </summary>
        /// <returns></returns>
        public Sube SeciliSube()
        {
            if (this.Subeler.Count > aktifSubeIndeksi && aktifSubeIndeksi >= 0)
                return Subeler.ElementAt(aktifSubeIndeksi);
            else
                return null;
        }
        /// <summary>
        /// Seçili şubenin liste üzerindeki indeksini bulan metod.
        /// </summary>
        /// <param name="SubeKodu">5 haneli şube kodu</param>
        public void SubeIndeksi(string SubeKodu)
        {
            int subeIndeksi = 0;
            foreach (Sube _Sube in Subeler)
            {               
                if (_Sube.SubeKodu == SubeKodu)
                {
                    this.aktifSubeIndeksi = subeIndeksi;
                    break;
                }
                subeIndeksi++;
            }
        }
        /// <summary>
        /// Şube Ekle Metodu Bankaya şube ekleme işlemi yapar ve SayiUret metodundan aldığı
        /// rastgele sayı ile oluşturur, oluşturulan unique numarayı eklenen Şubeye atama işlemi yapar.
        /// </summary>
        /// <param name="S">Şube Nesnesi</param>
        public void SubeEkle(Sube S)
        {
            int subeSayac = 0;
            string geciciSubeKodu = "";
            do
            {
                geciciSubeKodu = SayiUret(5, 1);
                foreach (Sube _Sube in Subeler)
                {
                    if (_Sube.SubeKodu == geciciSubeKodu)
                    {
                        break;
                    }
                    subeSayac++;
                }
            } while (Subeler.Count != subeSayac);
            S.KodAl(geciciSubeKodu);
            Subeler.Add(S);
        } 
        /// <summary>
        /// Bankaya eklenmiş tüm şubelerin listesini döndürür.
        /// </summary>
        /// <returns></returns>
        public List<Sube> SubeListesi()
        {
            return this.Subeler;
        }     

        /// <summary>
        /// Kur Ekleme işlemi daha önce aynı Birim Kodu (Örn: "USD", "TRY" vs.) ile 
        /// bir Kur Eklenmemiş ise ekleme işlemini gerçekleştiren metottur.
        /// </summary>
        /// <param name="K">K</param>
        /// <returns>true</returns>
        public bool KurEkle(Kur K)
        {
            int kurIndeksi = 0;
            foreach (Kur _Kur in Kurlar)
	        {
		         if(_Kur.BirimKodu ==  K.BirimKodu )
                 {
                     break;
                 }
                 kurIndeksi++;
            }
            if (kurIndeksi != Kurlar.Count)
            {
                return false;
            }
            Kurlar.Add(K);
            return true;        
        }
        /// <summary>
        /// Girilen Kur koduna ait Kur Oranini getiren metot.
        /// </summary>
        /// <param name="ParaBirimi">"USD"</param>
        /// <returns>3.8558</returns>
        public decimal KurGetir(string ParaBirimi)
        {
            foreach (Kur _Kur in Kurlar)
            {
                if (_Kur.BirimKodu == ParaBirimi)
                {
                    return _Kur.Oran;
                }
            }
            return 1;
        }
        /// <summary>
        /// Bankaya eklenmiş tüm Kur bilgilerini getirir.
        /// </summary>
        /// <returns></returns>
        public List<Kur> KurListesi()
        {
            return this.Kurlar;
        }
        /// <summary>
        /// Müşteri Ekle Metodu Bankaya müşteri ekleme işlemi yapar ve SayiUret metodundan aldığı
        /// rastgele sayı ile oluşturur, oluşturulan unique numarayı eklenen müşteriye atama işlemi yapar.
        /// </summary>
        /// <param name="M"></param>
        public void MusteriEkle(Musteri M)
        {
            int musteriSayac = 0;
            string geciciMusteriNo = "";
            do{
               geciciMusteriNo = SayiUret(4, 2);
               foreach (Musteri _Musteri in Musteriler)
               {
                   if (_Musteri.MusteriNo == geciciMusteriNo)
                    {
                        break;
                    }
                   musteriSayac++;
               }
            } while (Musteriler.Count != musteriSayac);
            M.NumaraAl(geciciMusteriNo);
            Musteriler.Add(M);
        }
        /// <summary>
        /// Bankaya eklenmiş tüm Müşteri bilgilerini getirir.
        /// </summary>
        /// <returns></returns>
        public List<Musteri> MusteriListele()
        {
            return this.Musteriler;
        }
        /// <summary>
        /// Hesaplar için benzersiz hesap numarası oluşturan metot.
        /// Bu metodun Banka sınıfında olmasının sebebi ise eklenen tüm hesaplara ulaşmak
        /// gerekli sorguları yapmak ve bu sorguları yaparken tüm müşterilerin hesaplarına ait 
        /// numaraları karşılaştırma işlemi yapmasıdır.
        /// </summary>
        /// <param name="H"></param>
        public void HesapNumarasiOlustur(Hesap H)
        {
            int musteriSayac = 0, hesapSayac = 0;
            string geciciNumara = "";
            do
            {
                hesapSayac = 0;
                geciciNumara = SayiUret(3, 4); //Hesap No
                foreach (Musteri _Musteri in Musteriler)
                {   
                   foreach(Hesap _Hesap in _Musteri.Hesaplarim())
                   {
                        if (_Hesap.HesapNo == geciciNumara)
                        {
                            break;
                        }
                        hesapSayac++;
                   }
                   if (_Musteri.Hesaplarim().Count != hesapSayac)
                   {
                       break;
                   }
                   musteriSayac++;
                } 
            } while (Musteriler.Count != musteriSayac);
            H.NumaraAl(geciciNumara);
        }
        /// <summary>
        /// Bankaya ait bilgilerin düzenlendiği metot.
        /// </summary>
        /// <param name="Adi">CMG</param>
        /// <param name="BankaKodu">00056</param>
        /// <param name="KaynakPara">100000</param>
        public void BilgileriDuzenle(string Adi, string BankaKodu, decimal KaynakPara)
        {
            this.KaynakPara = KaynakPara;
            this.Adi = Adi;
            this.BankaKodu = BankaKodu;
        }
        /// <summary>
        /// Girilen basamak sayısına göre istenilen miktarda rastgele sayı oluşturan ve bu sayıları uç uca ekleyerek
        /// geri döndüren metottur.
        /// </summary>
        /// <param name="basamakSayisi">4</param>
        /// <param name="Miktar">2</param>
        /// <returns>12349874</returns>
        public string SayiUret(int basamakSayisi, int Miktar)
        {
            string temp = "";
            for (int i = 0; i < Miktar; i++)
            {
                temp += getRandom.Next(0, Convert.ToInt32((Math.Pow(10,basamakSayisi))-1)).ToString("D"+basamakSayisi);
            }
            return temp;
        }
        /// <summary>
        /// Banka gelirlerini hesaplar.
        /// </summary>
        /// <param name="Miktar">1000</param>
        public void Gelirler(decimal Miktar)
        {
            this.Gelir += Miktar;
        }
        /// <summary>
        /// Banka giderlerini hesaplar.
        /// </summary>
        /// <param name="Miktar">1000</param>
        public void Giderler(decimal Miktar)
        {
            this.Gider += Miktar;
        }
        /// <summary>
        /// Banka üzerinde yapılan tüm işlemleri rapor halinde döndürür.
        /// </summary>
        /// <returns></returns>
        public List<Islem> Rapor()
        {
            Gelir = 0;
            Gider = 0;
            foreach (Sube _Sube in SubeListesi())
            {
                foreach (Hesap _Hesap in _Sube.Hesaplar)
                {
                    foreach (Islem _Islem in _Hesap.HesapIslemleri)
                    {
                        if (_Islem is Yatir)
                        {
                            this.Islemler.Add(_Islem);
                        }
                        if (_Islem is Cek)
                        {
                            this.Islemler.Add(_Islem);
                        }
                        if (_Islem is Havale)
                        {
                            this.Islemler.Add(_Islem);
                        }
                    }
                }
            }
            return this.Islemler;
        }
    }
}
