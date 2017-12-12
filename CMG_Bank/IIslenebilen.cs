using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMG_Bank
{
    public interface IIslenebilen
    {

        void NumaraAl(string gelenNumara);
        bool HesapKapama();
        List<Islem> HesapOzeti();
        bool IslemYap(Islem yapilanIslem);
    }
}
