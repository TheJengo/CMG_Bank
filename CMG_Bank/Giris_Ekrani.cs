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
using System.Drawing.Drawing2D;



namespace CMG_Bank
{
    public partial class Giris_Ekrani : MaterialSkin.Controls.MaterialForm
    {
        public Giris_Ekrani()
        {
            InitializeComponent();
            MaterialSkin.MaterialSkinManager skinMenager = MaterialSkin.MaterialSkinManager.Instance;
            skinMenager.AddFormToManage(this);
            skinMenager.Theme = MaterialSkin.MaterialSkinManager.Themes.DARK;
            skinMenager.ColorScheme = new MaterialSkin.ColorScheme(MaterialSkin.Primary.Grey600, MaterialSkin.Primary.BlueGrey900, MaterialSkin.Primary.Blue500, MaterialSkin.Accent.Orange700, MaterialSkin.TextShade.WHITE);
        }
        
        private void Giris_Ekrani_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuGradientPanel3_MouseEnter(object sender, EventArgs e)
        {

        }

        private void bunifuGradientPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        

        

        
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
