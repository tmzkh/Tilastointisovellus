namespace Harjoitustyo1_pre_alpha
{
    partial class frmJoukkueenValinta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmJoukkueenValinta));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.fLPJoukkueet = new System.Windows.Forms.FlowLayoutPanel();
            this.lbPeru = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(493, 154);
            this.panel1.TabIndex = 22;
            this.panel1.MouseEnter += new System.EventHandler(this.MouseEnters);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(493, 154);
            this.label1.TabIndex = 20;
            this.label1.Text = "Valitse kotijoukkue";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.MouseEnter += new System.EventHandler(this.MouseEnters);
            // 
            // fLPJoukkueet
            // 
            this.fLPJoukkueet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fLPJoukkueet.AutoScroll = true;
            this.fLPJoukkueet.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLPJoukkueet.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fLPJoukkueet.ForeColor = System.Drawing.Color.White;
            this.fLPJoukkueet.Location = new System.Drawing.Point(9, 158);
            this.fLPJoukkueet.Margin = new System.Windows.Forms.Padding(0);
            this.fLPJoukkueet.Name = "fLPJoukkueet";
            this.fLPJoukkueet.Size = new System.Drawing.Size(474, 510);
            this.fLPJoukkueet.TabIndex = 23;
            this.fLPJoukkueet.MouseEnter += new System.EventHandler(this.fLPJoukkueet_MouseEnter);
            // 
            // lbPeru
            // 
            this.lbPeru.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPeru.Location = new System.Drawing.Point(337, 682);
            this.lbPeru.Name = "lbPeru";
            this.lbPeru.Size = new System.Drawing.Size(144, 58);
            this.lbPeru.TabIndex = 24;
            this.lbPeru.Text = "Peruuta";
            this.lbPeru.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbPeru.Click += new System.EventHandler(this.lbPeru_Click);
            this.lbPeru.MouseEnter += new System.EventHandler(this.fLPJoukkueet_MouseEnter);
            // 
            // frmJoukkueenValinta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(493, 749);
            this.Controls.Add(this.lbPeru);
            this.Controls.Add(this.fLPJoukkueet);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximumSize = new System.Drawing.Size(509, 788);
            this.MinimumSize = new System.Drawing.Size(509, 788);
            this.Name = "frmJoukkueenValinta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Joukkueen valinta";
            this.MouseHover += new System.EventHandler(this.MouseEnters);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel fLPJoukkueet;
        private System.Windows.Forms.Label lbPeru;
    }
}