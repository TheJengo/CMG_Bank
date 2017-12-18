using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bunifu.Framework;
using BunifuAnimatorNS;
using MaterialSkin.Animations;
using MaterialSkin.Controls;
using MaterialSkin;

namespace CMG_Bank
{
    public partial class Personel_Ekrani : MaterialSkin.Controls.MaterialForm
    {
        public Personel_Ekrani()
        {
            InitializeComponent(); 
            MaterialSkin.MaterialSkinManager skinManager = MaterialSkin.MaterialSkinManager.Instance;
            skinManager.AddFormToManage(this);
            skinManager.Theme = MaterialSkin.MaterialSkinManager.Themes.DARK;
            skinManager.ColorScheme = new MaterialSkin.ColorScheme(MaterialSkin.Primary.Grey600, MaterialSkin.Primary.BlueGrey900, MaterialSkin.Primary.Blue500, MaterialSkin.Accent.Orange700, MaterialSkin.TextShade.WHITE);   
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
