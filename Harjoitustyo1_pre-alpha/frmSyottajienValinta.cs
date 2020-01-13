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
    public partial class frmSyottajienValinta : Form
    {
        PeliTapahtuma eventti;

        Team joukkue;

        LatkaPeli LatkaPeli;

        string laji;

        public frmSyottajienValinta(Team team, PeliTapahtuma happeninki, string valittuLaji)
        {
            InitializeComponent();

            joukkue = team;
            eventti = happeninki;
            laji = valittuLaji;

            PaivitaLista();

        }

        public frmSyottajienValinta(Team team, PeliTapahtuma happeninki, string valittuLaji, LatkaPeli latkaPeli)
        {
            InitializeComponent();

            LatkaPeli = latkaPeli;
            joukkue = team;
            eventti = happeninki;
            laji = valittuLaji;

            PaivitaLista();

        }


        private void PaivitaLista()
        {
            fLPPelaajanapit.Controls.Clear();

            Button bttn = new Button();
            bttn.Text = "Ei syöttäjää";
            bttn.FlatAppearance.BorderSize = 0;
            bttn.FlatStyle = FlatStyle.Flat;
            bttn.ForeColor = Color.White;
            bttn.Size = new Size(100, 28);
            bttn.Click += new EventHandler(EiSyottajaa_Click);
            fLPPelaajanapit.Controls.Add(bttn);

            // Lisätään pelaajanapit flowLayoutPaneliin
            foreach (Pelaaja p in joukkue.plrs.Where(o=> o != eventti.pelaaja && $"#{o.numero} {o.snimi}" != eventti.syottaja1))
            {
                customNappi btn = new customNappi(p);
                btn.Text = $"#{p.numero} {p.snimi}";
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatStyle = FlatStyle.Flat;
                btn.ForeColor = Color.White;
                btn.Size = new Size(100, 28);
                btn.Click += new EventHandler(PelaajanValinta_click);
                fLPPelaajanapit.Controls.Add(btn);
            }

        }

        private void EiSyottajaa_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PelaajanValinta_click(object sender, EventArgs e)
        {
            #region Muotoilua

            foreach (Control b in fLPPelaajanapit.Controls)
            {
                b.ForeColor = Color.White;
            }

            customNappi btn = (customNappi)sender;

            btn.ForeColor = Color.FromArgb(252, 134, 0);

            #endregion

            // merkitään syöttäjäksi valittu pelaaja
            if (eventti.syottaja1 == string.Empty)
            {
                eventti.syottaja1 = btn.Text;
                eventti.assist1 = btn.HaePelaaja();
                Pelaaja.TilastoiSyotto(eventti.assist1);

                // jos laji on lätkä, kysytään 2. syöttäjää, muuten suljetaan
                if (laji == "latka")
                {
                    PaivitaLista();
                }
                else
                {
                    Close();
                }
            }
            else
            {
                eventti.syottaja2 = btn.Text;
                eventti.assist2 = btn.HaePelaaja();
                Pelaaja.TilastoiSyotto(eventti.assist2);
                Close();
            }

        }

        private void btnPeruuta_Click(object sender, EventArgs e)
        {
            Pelaaja.PoistaiMaali(eventti.pelaaja);

            if (eventti.assist1 != null)
            {
                Pelaaja.PoistaSyotto(eventti.assist1);
            }

            if (eventti.puoli == LatkaPeli.kotijoukkue.nimi)
            {
                Pelaaja.PoistaPaastettyMaali(LatkaPeli.vierasMV);
            }
            else
            {
                Pelaaja.PoistaPaastettyMaali(LatkaPeli.kotiMV);
            }

            eventti.happeninki = string.Empty;
            eventti.syottaja1 = string.Empty;
            eventti.syottaja2 = string.Empty;
            eventti.assist1 = null;
            eventti.assist2 = null;
            Close();
        }
    }
}
