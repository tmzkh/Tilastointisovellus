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
    public partial class frmMVValinta : Form
    {
        LatkaPeli latkapeli;
        FutisPeli futispeli;
        SabaPeli sabapeli;

        public frmMVValinta(LatkaPeli peli)
        {
            latkapeli = peli;

            InitializeComponent();

            LatkaKotijoukkueenMV();
        }

        public frmMVValinta(FutisPeli peli)
        {
            futispeli = peli;

            InitializeComponent();

            FutisKotijoukkueenMV();
        }

        public frmMVValinta(SabaPeli peli)
        {

            sabapeli = peli;

            InitializeComponent();

            SabaKotijoukkueenMV();
        }

        private void LatkaKotijoukkueenMV()
        {
            // tuodaan kotijoukkueen mv:t valittavaksi
            foreach (Pelaaja p in latkapeli.kotijoukkue.plrs.Where(o => o.pelipaikka == "MV"))
            {
                customNappi btn = new customNappi(p);
                btn.Text = $"#{p.numero} {p.snimi}";
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatStyle = FlatStyle.Flat;
                btn.ForeColor = Color.White;
                btn.Size = new Size(100, 28);
                btn.Click += new EventHandler(LatkaKotiMVValinta_click);
                fLPPelaajanapit.Controls.Add(btn);
            }          
        }

        private void FutisKotijoukkueenMV()
        {
            // tuodaan kotijoukkueen mv:t valittavaksi
            foreach (Pelaaja p in futispeli.kotijoukkue.plrs.Where(o => o.pelipaikka == "MV"))
            {
                customNappi btn = new customNappi(p);
                btn.Text = $"#{p.numero} {p.snimi}";
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatStyle = FlatStyle.Flat;
                btn.ForeColor = Color.White;
                btn.Size = new Size(100, 28);
                btn.Click += new EventHandler(FutisKotiMVValinta_click);
                fLPPelaajanapit.Controls.Add(btn);
            }
        }

        private void SabaKotijoukkueenMV()
        {
            // tuodaan kotijoukkueen mv:t valittavaksi
            foreach (Pelaaja p in sabapeli.kotijoukkue.plrs.Where(o => o.pelipaikka == "MV"))
            {
                customNappi btn = new customNappi(p);
                btn.Text = $"#{p.numero} {p.snimi}";
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatStyle = FlatStyle.Flat;
                btn.ForeColor = Color.White;
                btn.Size = new Size(100, 28);
                btn.Click += new EventHandler(SabaKotiMVValinta_click);
                fLPPelaajanapit.Controls.Add(btn);
            }
        }

        private void LatkaVierasjoukkueenMV()
        {
            fLPPelaajanapit.Controls.Clear();

            // tuodaan vierasjoukkueen mv:t valittavaksi
            foreach (Pelaaja p in latkapeli.vierasjoukkue.plrs.Where(o => o.pelipaikka == "MV"))
            {
                customNappi btn = new customNappi(p);
                btn.Text = $"#{p.numero} {p.snimi}";
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatStyle = FlatStyle.Flat; 
                btn.ForeColor = Color.White;
                btn.Size = new Size(100, 28);
                btn.AutoSize = true;
                btn.Click += new EventHandler(LatkaVierasMVValinta_click);
                fLPPelaajanapit.Controls.Add(btn);
            }
        }

        private void FutisVierasjoukkueenMV()
        {
            fLPPelaajanapit.Controls.Clear();

            // tuodaan vierasjoukkueen mv:t valittavaksi
            foreach (Pelaaja p in futispeli.vierasjoukkue.plrs.Where(o => o.pelipaikka == "MV"))
            {
                customNappi btn = new customNappi(p);
                btn.Text = $"#{p.numero} {p.snimi}";
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatStyle = FlatStyle.Flat;
                btn.ForeColor = Color.White;
                btn.Size = new Size(100, 28);
                btn.AutoSize = true;
                btn.Click += new EventHandler(FutisVierasMVValinta_click);
                fLPPelaajanapit.Controls.Add(btn);
            }
        }

        private void SabaVierasjoukkueenMV()
        {
            fLPPelaajanapit.Controls.Clear();

            // tuodaan vierasjoukkueen mv:t valittavaksi
            foreach (Pelaaja p in sabapeli.vierasjoukkue.plrs.Where(o => o.pelipaikka == "MV"))
            {
                customNappi btn = new customNappi(p);
                btn.Text = $"#{p.numero} {p.snimi}";
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatStyle = FlatStyle.Flat;
                btn.ForeColor = Color.White;
                btn.Size = new Size(100, 28);
                btn.AutoSize = true;
                btn.Click += new EventHandler(SabaVierasMVValinta_click);
                fLPPelaajanapit.Controls.Add(btn);
            }
        }

        private void LatkaKotiMVValinta_click(object sender, EventArgs e)
        {
            customNappi btn = (customNappi)sender;

            latkapeli.kotiMV = btn.HaePelaaja();

            // vaihdetaan teksti
            label1.Text = "Valitse vierasjoukkueen maalivahti";

            // päivitetään listaan vierasjoukkueen maalivahdit
            LatkaVierasjoukkueenMV();
        }

        private void FutisKotiMVValinta_click(object sender, EventArgs e)
        {
            customNappi btn = (customNappi)sender;

            futispeli.kotijoukkueenMV = btn.Text;

            // vaihdetaan teksti
            label1.Text = "Valitse vierasjoukkueen maalivahti";

            // päivitetään listaan vierasjoukkueen maalivahdit
            FutisVierasjoukkueenMV();
        }

        private void SabaKotiMVValinta_click(object sender, EventArgs e)
        {
            customNappi btn = (customNappi)sender;

            sabapeli.kotijoukkueenMV = btn.Text;

            // vaihdetaan teksti
            label1.Text = "Valitse vierasjoukkueen maalivahti";

            // päivitetään listaan vierasjoukkueen maalivahdit
            SabaVierasjoukkueenMV();
        }

        private void LatkaVierasMVValinta_click(object sender, EventArgs e)
        {
            customNappi btn = (customNappi)sender;

            latkapeli.vierasMV = btn.HaePelaaja();

            Close();
        }

        private void FutisVierasMVValinta_click(object sender, EventArgs e)
        {
            customNappi btn = (customNappi)sender;

            futispeli.vierasjoukkueenMV = btn.Text;

            Close();
        }

        private void SabaVierasMVValinta_click(object sender, EventArgs e)
        {
            customNappi btn = (customNappi)sender;

            sabapeli.vierasjoukkueenMV = btn.Text;

            Close();
        }
    }
}
