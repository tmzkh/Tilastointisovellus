namespace Harjoitustyo1_pre_alpha
{
    partial class frmSyottajienValinta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSyottajienValinta));
            this.fLPPelaajanapit = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPeruuta = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // fLPPelaajanapit
            // 
            this.fLPPelaajanapit.Location = new System.Drawing.Point(6, 75);
            this.fLPPelaajanapit.Name = "fLPPelaajanapit";
            this.fLPPelaajanapit.Size = new System.Drawing.Size(776, 116);
            this.fLPPelaajanapit.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(788, 72);
            this.label1.TabIndex = 2;
            this.label1.Text = "Valitse syöttäjä";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPeruuta
            // 
            this.btnPeruuta.FlatAppearance.BorderSize = 0;
            this.btnPeruuta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPeruuta.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPeruuta.ForeColor = System.Drawing.Color.White;
            this.btnPeruuta.Location = new System.Drawing.Point(671, 197);
            this.btnPeruuta.Name = "btnPeruuta";
            this.btnPeruuta.Size = new System.Drawing.Size(111, 73);
            this.btnPeruuta.TabIndex = 6;
            this.btnPeruuta.Text = "Peruuta";
            this.btnPeruuta.UseVisualStyleBackColor = true;
            this.btnPeruuta.Click += new System.EventHandler(this.btnPeruuta_Click);
            // 
            // frmSyottajienValinta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(788, 274);
            this.Controls.Add(this.btnPeruuta);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fLPPelaajanapit);
            this.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximumSize = new System.Drawing.Size(804, 313);
            this.MinimumSize = new System.Drawing.Size(804, 313);
            this.Name = "frmSyottajienValinta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Syöttäjien valinta";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel fLPPelaajanapit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPeruuta;
    }
}