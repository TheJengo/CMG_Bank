using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMG_Bank
{
    public interface IIslenebilen
    {
        List<Islem> HesapOzeti();
        bool IslemYap(Islem yapilanIslem);
    }
}
