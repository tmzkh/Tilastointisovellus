namespace Harjoitustyo1_pre_alpha
{
    partial class frmTapahtuma
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTapahtuma));
            this.flowLayoutPanelPelaajaNapitJoukkue1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnLaukaus = new System.Windows.Forms.Button();
            this.btnMaali = new System.Windows.Forms.Button();
            this.flowLayoutPanelPelaajaNapitJoukkue2 = new System.Windows.Forms.FlowLayoutPanel();
            this.cmBxMin = new System.Windows.Forms.ComboBox();
            this.cmBxSec = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPoista = new System.Windows.Forms.Button();
            this.btnTallenna = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // flowLayoutPanelPelaajaNapitJoukkue1
            // 
            this.flowLayoutPanelPelaajaNapitJoukkue1.Location = new System.Drawing.Point(12, 12);
            this.flowLayoutPanelPelaajaNapitJoukkue1.Name = "flowLayoutPanelPelaajaNapitJoukkue1";
            this.flowLayoutPanelPelaajaNapitJoukkue1.Size = new System.Drawing.Size(776, 116);
            this.flowLayoutPanelPelaajaNapitJoukkue1.TabIndex = 0;
            // 
            // btnLaukaus
            // 
            this.btnLaukaus.Enabled = false;
            this.btnLaukaus.FlatAppearance.BorderSize = 0;
            this.btnLaukaus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLaukaus.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLaukaus.ForeColor = System.Drawing.Color.White;
            this.btnLaukaus.Location = new System.Drawing.Point(13, 252);
            this.btnLaukaus.Name = "btnLaukaus";
            this.btnLaukaus.Size = new System.Drawing.Size(111, 73);
            this.btnLaukaus.TabIndex = 1;
            this.btnLaukaus.Text = "Laukaus (L)";
            this.btnLaukaus.UseVisualStyleBackColor = true;
            this.btnLaukaus.Click += new System.EventHandler(this.btnMaaliTaiLaukaus_Click);
            // 
            // btnMaali
            // 
            this.btnMaali.Enabled = false;
            this.btnMaali.FlatAppearance.BorderSize = 0;
            this.btnMaali.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaali.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMaali.ForeColor = System.Drawing.Color.White;
            this.btnMaali.Location = new System.Drawing.Point(134, 252);
            this.btnMaali.Name = "btnMaali";
            this.btnMaali.Size = new System.Drawing.Size(111, 73);
            this.btnMaali.TabIndex = 1;
            this.btnMaali.Text = "Maali (M)";
            this.btnMaali.UseVisualStyleBackColor = true;
            this.btnMaali.Click += new System.EventHandler(this.btnMaaliTaiLaukaus_Click);
            // 
            // flowLayoutPanelPelaajaNapitJoukkue2
            // 
            this.flowLayoutPanelPelaajaNapitJoukkue2.Location = new System.Drawing.Point(12, 131);
            this.flowLayoutPanelPelaajaNapitJoukkue2.Name = "flowLayoutPanelPelaajaNapitJoukkue2";
            this.flowLayoutPanelPelaajaNapitJoukkue2.Size = new System.Drawing.Size(776, 116);
            this.flowLayoutPanelPelaajaNapitJoukkue2.TabIndex = 1;
            // 
            // cmBxMin
            // 
            this.cmBxMin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.cmBxMin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmBxMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmBxMin.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmBxMin.ForeColor = System.Drawing.Color.White;
            this.cmBxMin.FormattingEnabled = true;
            this.cmBxMin.Items.AddRange(new object[] {
            "00",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.cmBxMin.Location = new System.Drawing.Point(375, 276);
            this.cmBxMin.Name = "cmBxMin";
            this.cmBxMin.Size = new System.Drawing.Size(67, 29);
            this.cmBxMin.TabIndex = 2;
            // 
            // cmBxSec
            // 
            this.cmBxSec.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.cmBxSec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmBxSec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmBxSec.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmBxSec.ForeColor = System.Drawing.Color.White;
            this.cmBxSec.FormattingEnabled = true;
            this.cmBxSec.Items.AddRange(new object[] {
            "00",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59"});
            this.cmBxSec.Location = new System.Drawing.Point(473, 276);
            this.cmBxSec.Name = "cmBxSec";
            this.cmBxSec.Size = new System.Drawing.Size(67, 29);
            this.cmBxSec.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(451, 279);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 21);
            this.label1.TabIndex = 4;
            this.label1.Text = ":";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPoista
            // 
            this.btnPoista.FlatAppearance.BorderSize = 0;
            this.btnPoista.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPoista.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPoista.ForeColor = System.Drawing.Color.White;
            this.btnPoista.Location = new System.Drawing.Point(677, 252);
            this.btnPoista.Name = "btnPoista";
            this.btnPoista.Size = new System.Drawing.Size(111, 73);
            this.btnPoista.TabIndex = 5;
            this.btnPoista.Text = "Poista";
            this.btnPoista.UseVisualStyleBackColor = true;
            this.btnPoista.Click += new System.EventHandler(this.btnPoista_Click);
            // 
            // btnTallenna
            // 
            this.btnTallenna.FlatAppearance.BorderSize = 0;
            this.btnTallenna.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTallenna.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTallenna.ForeColor = System.Drawing.Color.White;
            this.btnTallenna.Location = new System.Drawing.Point(561, 253);
            this.btnTallenna.Name = "btnTallenna";
            this.btnTallenna.Size = new System.Drawing.Size(111, 73);
            this.btnTallenna.TabIndex = 6;
            this.btnTallenna.Text = "Tallenna";
            this.btnTallenna.UseVisualStyleBackColor = true;
            this.btnTallenna.Click += new System.EventHandler(this.btnTallenna_Click);
            // 
            // frmTapahtuma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(800, 342);
            this.Controls.Add(this.btnTallenna);
            this.Controls.Add(this.btnPoista);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmBxMin);
            this.Controls.Add(this.cmBxSec);
            this.Controls.Add(this.flowLayoutPanelPelaajaNapitJoukkue2);
            this.Controls.Add(this.btnMaali);
            this.Controls.Add(this.btnLaukaus);
            this.Controls.Add(this.flowLayoutPanelPelaajaNapitJoukkue1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(816, 381);
            this.MinimumSize = new System.Drawing.Size(816, 381);
            this.Name = "frmTapahtuma";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tapahtuma";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPelaajaNapitJoukkue1;
        private System.Windows.Forms.Button btnLaukaus;
        private System.Windows.Forms.Button btnMaali;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPelaajaNapitJoukkue2;
        private System.Windows.Forms.ComboBox cmBxMin;
        private System.Windows.Forms.ComboBox cmBxSec;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPoista;
        private System.Windows.Forms.Button btnTallenna;
    }
}