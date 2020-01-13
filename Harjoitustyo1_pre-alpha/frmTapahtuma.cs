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
    public partial class frmTapahtuma : Form
    {
        PeliTapahtuma eventti;

        Team kotijoukkue;
        Team vierasjoukkue;

        LatkaPeli LatkaPeli;

        string laji;


        // ilmoittaa että poistetaanko tapahtuma
        public bool poistetaanko;

        public frmTapahtuma(PeliTapahtuma tapahtuma, Team team1, Team team2, bool onkoUusi, string valittuLaji)
        {
            InitializeComponent();

            eventti = tapahtuma;

            kotijoukkue = team1;
            vierasjoukkue = team2;
            laji = valittuLaji;

            // jos uusi tapahtuma, poistetaan ajanmäärittämismahdollisuus ja poistomahdollisuus
            if (onkoUusi == true)
            {
                Controls.Remove(cmBxMin);
                Controls.Remove(label1);
                Controls.Remove(cmBxSec);
                Controls.Remove(btnPoista);
                Controls.Remove(btnTallenna);

                eventti.player = string.Empty;
                eventti.happeninki = string.Empty;
            }

            ViePelaajanapitFLPn();

            if (onkoUusi == false)
            {
                cmBxMin.Text = tapahtuma.minuutit.ToString("D2");
                cmBxSec.Text = tapahtuma.sekuntit.ToString("D2");

                

                foreach (customNappi b in flowLayoutPanelPelaajaNapitJoukkue1.Controls)
                {
                    if (b.p == eventti.pelaaja)
                    {
                        b.ForeColor = Color.FromArgb(252, 134, 0);
                    }
                }
                foreach (customNappi b in flowLayoutPanelPelaajaNapitJoukkue2.Controls)
                {
                    if (b.p == eventti.pelaaja)
                    {
                        b.ForeColor = Color.FromArgb(252, 134, 0);
                    }
                }

            }

            poistetaanko = false;

        }

        public frmTapahtuma(PeliTapahtuma tapahtuma, LatkaPeli latkaPeli, bool onkoUusi, string valittuLaji)
        {
            InitializeComponent();

            eventti = tapahtuma;

            LatkaPeli = latkaPeli;
            kotijoukkue = latkaPeli.kotijoukkue;
            vierasjoukkue = latkaPeli.vierasjoukkue;
            laji = valittuLaji;

            // jos uusi tapahtuma, poistetaan ajanmäärittämismahdollisuus ja poistomahdollisuus
            if (onkoUusi == true)
            {
                Controls.Remove(cmBxMin);
                Controls.Remove(label1);
                Controls.Remove(cmBxSec);
                Controls.Remove(btnPoista);
                Controls.Remove(btnTallenna);

                eventti.player = string.Empty;
                eventti.happeninki = string.Empty;
            }

            ViePelaajanapitFLPn();

            if (onkoUusi == false)
            {
                cmBxMin.Text = tapahtuma.minuutit.ToString("D2");
                cmBxSec.Text = tapahtuma.sekuntit.ToString("D2");



                foreach (customNappi b in flowLayoutPanelPelaajaNapitJoukkue1.Controls)
                {
                    if (b.p == eventti.pelaaja)
                    {
                        b.ForeColor = Color.FromArgb(252, 134, 0);
                    }
                }
                foreach (customNappi b in flowLayoutPanelPelaajaNapitJoukkue2.Controls)
                {
                    if (b.p == eventti.pelaaja)
                    {
                        b.ForeColor = Color.FromArgb(252, 134, 0);
                    }
                }

            }

            poistetaanko = false;

        }

        private void ViePelaajanapitFLPn()
        {
            foreach (Pelaaja p in kotijoukkue.plrs.Where(o=>o.pelipaikka == "H" || o.pelipaikka == "P"))
            {
                customNappi btn = new customNappi(p);
                btn.Text = $"#{p.numero} {p.snimi}";
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatStyle = FlatStyle.Flat;
                btn.ForeColor = Color.White;
                btn.Size = new Size(100, 28);
                btn.AutoSize = true;
                btn.Click += new EventHandler(PelaajanValinta_click);
                flowLayoutPanelPelaajaNapitJoukkue1.Controls.Add(btn);
            }

            foreach (Pelaaja p in vierasjoukkue.plrs.Where(o => o.pelipaikka == "H" || o.pelipaikka == "P"))
            {
                customNappi btn = new customNappi(p);
                btn.Text = $"#{p.numero} {p.snimi}";
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatStyle = FlatStyle.Flat;
                btn.ForeColor = Color.White;
                btn.Size = new Size(100, 28);
                btn.AutoSize = true;
                btn.Click += new EventHandler(PelaajanValinta_click);
                flowLayoutPanelPelaajaNapitJoukkue2.Controls.Add(btn);
            }
        }


        private void PelaajanValinta_click(object sender, EventArgs e)
        {
            #region Muotoilua

            foreach (Button b in flowLayoutPanelPelaajaNapitJoukkue1.Controls)
            {
                b.ForeColor = Color.White;
            }
            foreach (Button b in flowLayoutPanelPelaajaNapitJoukkue2.Controls)
            {
                b.ForeColor = Color.White;
            }
           
            customNappi btn = (customNappi)sender;
            btn.ForeColor = Color.FromArgb(252, 134, 0);

            #endregion

            eventti.player = null;
            eventti.pelaaja = null;
            eventti.puoli = null;

            eventti.player = btn.Text;
            eventti.pelaaja = btn.HaePelaaja();
            eventti.puoli = eventti.pelaaja.team;


            // lisätään pikanäppäineventti
            if (this.KeyPreview == false)
            {
                this.KeyDown += new KeyEventHandler(KeyPresses);
                this.KeyPreview = true;
            }
            
            // enabloidaan napit
            if (btnLaukaus.Enabled == false)
            {
                btnLaukaus.Enabled = true;
            } 
            if (btnMaali.Enabled == false)
            {
                btnMaali.Enabled = true;
            }
                
        }

        private void KeyPresses(object sender, KeyEventArgs e)
        {
            // pikanapit tapahtumatyypille
            if (e.KeyValue == (char)Keys.M)
            {
                e.Handled = true;
                btnMaaliTaiLaukaus_Click(btnMaali, e);
            }
            if (e.KeyValue == (char)Keys.L)
            {
                e.Handled = true;
                btnMaaliTaiLaukaus_Click(btnLaukaus, e);
            }
            
        }

        private void btnMaaliTaiLaukaus_Click(object sender, EventArgs e)
        {
            this.KeyDown -= new KeyEventHandler(KeyPresses);
            this.KeyPreview = false;

            Button btn = (Button)sender;

            if (sender == btnMaali)
            {
                eventti.happeninki = "Maali";

                eventti.syottaja1 = string.Empty;
                eventti.syottaja2 = string.Empty;

                // avataan syöttäjien valinta
                if (eventti.puoli == kotijoukkue.nimi)
                {
                    frmSyottajienValinta frmSyottajienValinta = new frmSyottajienValinta(kotijoukkue, eventti, laji);
                    frmSyottajienValinta.ShowDialog();
                }
                else if (eventti.puoli == vierasjoukkue.nimi)
                {
                    frmSyottajienValinta frmSyottajienValinta = new frmSyottajienValinta(vierasjoukkue, eventti, laji);
                    frmSyottajienValinta.ShowDialog();
                }

                Pelaaja.TilastoiMaali(eventti.pelaaja);
                
                if (eventti.puoli == kotijoukkue.nimi)
                {
                    LatkaPeli.kotijoukkueenMaalit++;
                    Pelaaja.TilastoiPaastettyMaali(LatkaPeli.vierasMV);
                }
                else
                {
                    LatkaPeli.vierasjoukkueenMaalit++;
                    Pelaaja.TilastoiPaastettyMaali(LatkaPeli.kotiMV);
                }
                

            }
            else if (sender == btnLaukaus)
            {
                eventti.happeninki = "Laukaus";
                Pelaaja.TilastoiLaukaus(eventti.pelaaja);

                if (eventti.puoli == kotijoukkue.nimi)
                {
                    Pelaaja.TilastoiTorjunta(LatkaPeli.vierasMV);
                }
                else
                {
                    Pelaaja.TilastoiTorjunta(LatkaPeli.kotiMV);
                }
            }

            Close();
        }

        private void btnPoista_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Haluatko varmasti poistaa tapahtuman?", "", MessageBoxButtons.OKCancel);

            if (dr == DialogResult.OK)
            {
                poistetaanko = true;

                Close();
            }
        }

        private void btnTallenna_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Haluatko varmasti tallentaa tapahtuman?", "", MessageBoxButtons.OKCancel);

            if (dr == DialogResult.OK)
            {
                eventti.minuutit = int.Parse(cmBxMin.Text);
                eventti.sekuntit = int.Parse(cmBxSec.Text);
                eventti.aika = cmBxMin.Text + ":" + cmBxSec.Text;

                Close();
            }
        }
    }

    // nappi joka sisältää pelaajan, jonka mukaan se on luotu
    // tämän avulla tapahtumaan saadaan merkittyä pelaaja
    public class customNappi : Button
    {
        public Pelaaja p;
        public customNappi(Pelaaja player)
        {
            p = player;
        }

        public Pelaaja HaePelaaja()
        {
            return p;
        }
    }
}
