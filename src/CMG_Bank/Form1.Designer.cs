namespace CMG_Bank
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnGiris = new System.Windows.Forms.Button();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.txtNo = new System.Windows.Forms.TextBox();
            this.mtrlIlerle = new MaterialSkin.Controls.MaterialFlatButton();
            this.txtGizli = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnGiris);
            this.groupBox1.Controls.Add(this.txtPass);
            this.groupBox1.Controls.Add(this.txtNo);
            this.groupBox1.Controls.Add(this.mtrlIlerle);
            this.groupBox1.Controls.Add(this.txtGizli);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(297, 263);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // btnGiris
            // 
            this.btnGiris.Location = new System.Drawing.Point(94, 189);
            this.btnGiris.Name = "btnGiris";
            this.btnGiris.Size = new System.Drawing.Size(100, 23);
            this.btnGiris.TabIndex = 19;
            this.btnGiris.Text = "Giriş";
            this.btnGiris.UseVisualStyleBackColor = true;
            this.btnGiris.Click += new System.EventHandler(this.btnGiris_Click);
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(94, 162);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(100, 20);
            this.txtPass.TabIndex = 18;
            // 
            // txtNo
            // 
            this.txtNo.Location = new System.Drawing.Point(94, 136);
            this.txtNo.Name = "txtNo";
            this.txtNo.Size = new System.Drawing.Size(100, 20);
            this.txtNo.TabIndex = 17;
            // 
            // mtrlIlerle
            // 
            this.mtrlIlerle.AutoSize = true;
            this.mtrlIlerle.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mtrlIlerle.Depth = 0;
            this.mtrlIlerle.Icon = null;
            this.mtrlIlerle.Location = new System.Drawing.Point(86, 80);
            this.mtrlIlerle.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.mtrlIlerle.MouseState = MaterialSkin.MouseState.HOVER;
            this.mtrlIlerle.Name = "mtrlIlerle";
            this.mtrlIlerle.Primary = false;
            this.mtrlIlerle.Size = new System.Drawing.Size(125, 36);
            this.mtrlIlerle.TabIndex = 16;
            this.mtrlIlerle.Text = "2. Adıma İlerle";
            this.mtrlIlerle.UseVisualStyleBackColor = true;
            this.mtrlIlerle.Click += new System.EventHandler(this.mtrlIlerle_Click);
            // 
            // txtGizli
            // 
            this.txtGizli.Location = new System.Drawing.Point(94, 51);
            this.txtGizli.Name = "txtGizli";
            this.txtGizli.Size = new System.Drawing.Size(100, 20);
            this.txtGizli.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Data For Test";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnGiris;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.TextBox txtNo;
        private MaterialSkin.Controls.MaterialFlatButton mtrlIlerle;
        private System.Windows.Forms.TextBox txtGizli;



    }
}

