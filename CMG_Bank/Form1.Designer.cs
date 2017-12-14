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
            this.txtGizli = new System.Windows.Forms.TextBox();
            this.mtrlIlerle = new MaterialSkin.Controls.MaterialFlatButton();
            this.txtNo = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.btnGiris = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtGizli
            // 
            this.txtGizli.Location = new System.Drawing.Point(125, 81);
            this.txtGizli.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtGizli.Name = "txtGizli";
            this.txtGizli.Size = new System.Drawing.Size(132, 22);
            this.txtGizli.TabIndex = 0;
            // 
            // mtrlIlerle
            // 
            this.mtrlIlerle.AutoSize = true;
            this.mtrlIlerle.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mtrlIlerle.Depth = 0;
            this.mtrlIlerle.Location = new System.Drawing.Point(115, 117);
            this.mtrlIlerle.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.mtrlIlerle.MouseState = MaterialSkin.MouseState.HOVER;
            this.mtrlIlerle.Name = "mtrlIlerle";
            this.mtrlIlerle.Primary = false;
            this.mtrlIlerle.Size = new System.Drawing.Size(144, 36);
            this.mtrlIlerle.TabIndex = 1;
            this.mtrlIlerle.Text = "2. Adıma İlerle";
            this.mtrlIlerle.UseVisualStyleBackColor = true;
            this.mtrlIlerle.Click += new System.EventHandler(this.mtrlIlerle_Click);
            // 
            // txtNo
            // 
            this.txtNo.Location = new System.Drawing.Point(125, 186);
            this.txtNo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtNo.Name = "txtNo";
            this.txtNo.Size = new System.Drawing.Size(132, 22);
            this.txtNo.TabIndex = 2;
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(125, 218);
            this.txtPass.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(132, 22);
            this.txtPass.TabIndex = 3;
            // 
            // btnGiris
            // 
            this.btnGiris.Location = new System.Drawing.Point(125, 251);
            this.btnGiris.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGiris.Name = "btnGiris";
            this.btnGiris.Size = new System.Drawing.Size(133, 28);
            this.btnGiris.TabIndex = 4;
            this.btnGiris.Text = "Giriş";
            this.btnGiris.UseVisualStyleBackColor = true;
            this.btnGiris.Click += new System.EventHandler(this.btnGiris_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 396);
            this.Controls.Add(this.btnGiris);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.txtNo);
            this.Controls.Add(this.mtrlIlerle);
            this.Controls.Add(this.txtGizli);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtGizli;
        private MaterialSkin.Controls.MaterialFlatButton mtrlIlerle;
        private System.Windows.Forms.TextBox txtNo;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Button btnGiris;
    }
}

