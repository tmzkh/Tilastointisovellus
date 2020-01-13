using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Harjoitustyo1_pre_alpha
{
    public partial class frmJoukkueenValinta : Form
    {
        public frmJoukkueenValinta(List<Team> teams, bool uusiPeli)
        {
            joukkueet = teams;

            InitializeComponent();

            try
            {
                

                if (uusiPeli == true)
                {
                    PaivitaLista_uusiPeli();
                    label1.Text = "Valitse kotijoukkue";
                }
                else
                {
                    PaivitaLista_PelaajanSiirto(teams);
                }
            }
            catch (Exception)
            {
                if (joukkueet == null || joukkueet.Count < 2)
                {
                    MessageBox.Show("Peliä ei voida aloittaa, mikäli joukkueita ei ole vähintään kaksi.", "Uutta peliä ei voida aloittaa");
                }
                
                EventArgs e = new EventArgs();

                lbPeru_Click(lbPeru, e);
            }
        }

        // indeksi, joka osoittaa joukkueen, johon pelaaja halutaan siirtää
        public int valittujoukkue;

        public void PaivitaLista_PelaajanSiirto(List<Team> joukkueet)
        {
            label1.Text = "Valitse joukkue johon pelaaja siirretään";

            // luodaan dynaamisesti napit joukkueita varten
            foreach (Team t in joukkueet)
            {
                Label lb = new Label();
                lb.Size = new Size(fLPJoukkueet.Size.Width, 28);
                lb.Margin = new Padding(0, 3, 0, 3);
                lb.TextAlign = ContentAlignment.MiddleCenter;
                lb.BackColor = Color.FromArgb(31, 34, 41);
                lb.ForeColor = Color.White;
                lb.Text = t.nimi.ToString();
                lb.MouseEnter += new EventHandler(fLPJoukkueet_MouseEnter);
                lb.Click += new EventHandler(fLPLatkaRosteritJoukkueet_Click);
                fLPJoukkueet.Controls.Add(lb);
            }
        }

        List<Team> joukkueet = new List<Team>();

        public Team kotijoukkue = new Team();
        public Team vierasjoukkue = new Team();

        private void PaivitaLista_uusiPeli()
        {
            fLPJoukkueet.Controls.Clear();

                // luodaan dynaamisesti napit joukkueita varten
                foreach (Team t in joukkueet)
                {
                        Label lb = new Label();
                        lb.Size = new Size(fLPJoukkueet.Size.Width, 28);
                        lb.Margin = new Padding(0, 3, 0, 3);
                        lb.TextAlign = ContentAlignment.MiddleCenter;
                        lb.BackColor = Color.FromArgb(31, 34, 41);
                        lb.ForeColor = Color.White;
                        lb.Text = t.nimi.ToString();
                        lb.MouseEnter += new EventHandler(fLPJoukkueet_MouseEnter);
                        lb.Click += new EventHandler(fLPJoukkueet_Click);
                        fLPJoukkueet.Controls.Add(lb);

                }
        }

        private void fLPJoukkueet_Click(object sender, EventArgs e)
        {
            Control ctrl = (Control)sender;

            if (label1.Text == "Valitse kotijoukkue")
            {
                // valitaan kotijoukkue
                kotijoukkue = joukkueet[fLPJoukkueet.Controls.GetChildIndex(ctrl)];

                label1.Text = "Valitse vierasjoukkue";
                
            }
            else
            {
                // ei anneta valita samaa joukkuetta kahdesti
                if (joukkueet[fLPJoukkueet.Controls.GetChildIndex(ctrl)] == kotijoukkue)
                {
                    MessageBox.Show("Et voi valita samaa joukkuetta kahdesti");
                    return;
                }

                // valitaan vierasjoukkue
                vierasjoukkue = joukkueet[fLPJoukkueet.Controls.GetChildIndex(ctrl)];
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

             
        private void fLPJoukkueet_MouseEnter(object sender, EventArgs e)
        {
            // muutetaan nappien ulkoasuja

            foreach (Control c in fLPJoukkueet.Controls)
            {
                c.ForeColor = Color.White;
            }

            lbPeru.ForeColor = Color.White;

            Control ctrl = (Control)sender;

            ctrl.ForeColor = Color.FromArgb(252, 134, 0);
        }

        private void MouseEnters(object sender, EventArgs e)
        {
            // muutetaan nappien ulkoasuja

            foreach (Control c in fLPJoukkueet.Controls)
            {
                c.ForeColor = Color.White;
            }

            lbPeru.ForeColor = Color.White;
        }

        private void lbPeru_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void fLPLatkaRosteritJoukkueet_Click(object sender, EventArgs e)
        {
            Control ctrl = (Control)sender;

            // muutetaan indeksin arvo, niin sitä voidaan käyttää pelaajan siirtämiseen
            valittujoukkue = fLPJoukkueet.Controls.GetChildIndex(ctrl);

            MessageBoxButtons btns = MessageBoxButtons.OKCancel;
            DialogResult dgr = MessageBox.Show($"Haluatko varmasti siirtää pelaajan joukkueeseen {joukkueet[valittujoukkue].nimi}", "", btns);
            if (dgr == DialogResult.OK)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }
    }
}
