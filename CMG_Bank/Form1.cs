using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CMG_Bank
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Bireysel A1 = new Bireysel("Cengiz", "Cebeci", 30151295680, 5071278994, "Turgutlu/Manisa", "Çalışkan", "En Sevdiğin Renk", "Fuşya","1234");

        private void mtrlIlerle_Click(object sender, EventArgs e)
        {
            if(A1.GizliSoruKontrol(txtGizli.Text) == true)
            {
                MessageBox.Show("2. Adım");
            }
        }
        
    }
}
