﻿namespace CMG_Bank
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
            this.SuspendLayout();
            // 
            // txtGizli
            // 
            this.txtGizli.Location = new System.Drawing.Point(94, 66);
            this.txtGizli.Name = "txtGizli";
            this.txtGizli.Size = new System.Drawing.Size(100, 20);
            this.txtGizli.TabIndex = 0;
            // 
            // mtrlIlerle
            // 
            this.mtrlIlerle.AutoSize = true;
            this.mtrlIlerle.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mtrlIlerle.Depth = 0;
            this.mtrlIlerle.Location = new System.Drawing.Point(86, 95);
            this.mtrlIlerle.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.mtrlIlerle.MouseState = MaterialSkin.MouseState.HOVER;
            this.mtrlIlerle.Name = "mtrlIlerle";
            this.mtrlIlerle.Primary = false;
            this.mtrlIlerle.Size = new System.Drawing.Size(116, 36);
            this.mtrlIlerle.TabIndex = 1;
            this.mtrlIlerle.Text = "2. Adıma İlerle";
            this.mtrlIlerle.UseVisualStyleBackColor = true;
            this.mtrlIlerle.Click += new System.EventHandler(this.mtrlIlerle_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.mtrlIlerle);
            this.Controls.Add(this.txtGizli);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtGizli;
        private MaterialSkin.Controls.MaterialFlatButton mtrlIlerle;
    }
}

