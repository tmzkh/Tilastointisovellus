using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Harjoitustyo1;

namespace Harjoitustyo1_pre_alpha
{
    public partial class frmMainPage : Form
    {
        public frmMainPage()
        {
            InitializeComponent();

            // tuodaan joukkueet ja pelit tiedostoista

            latkajoukkueet = XMLSerializerit.DeserializeXMLLatkajoukkueet();
            futisjoukkueet = XMLSerializerit.DeserializeXMLFutisjoukkueet();
            sabajoukkueet = XMLSerializerit.DeserializeXMLSabajoukkueet();

            latkapelit = XMLSerializerit.DeserializeXMLLatkapelit();
            futispelit = XMLSerializerit.DeserializeXMLFutispelit();
            sabapelit = XMLSerializerit.DeserializeXMLSabapelit();

            panelUusiPeli.Visible = false;
            panelRosterManagement.Visible = false;
            panelTilastot.Visible = false;

        }

        #region Lopetusnappi
        private void btnExit_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Haluatko varmasti lopettaa?\rTallentamattomat muutokset häviävät", "", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
        }


        private void btnExit_MouseEnter(object sender, EventArgs e)
        {

            foreach (Control l in tableLayoutPanelValintanapit.Controls)
            {
                l.ForeColor = Color.White;
                l.BackColor = Color.FromArgb(41, 44, 51);
            }

            panelUusiPeliLajinValinta.Visible = false;
            panelTilastotLajinValinta.Visible = false;
            panelRosteritLajinValinta.Visible = false;


            lblPoistu.BackColor = Color.FromArgb(31, 34, 41);
            lblPoistu.ForeColor = Color.FromArgb(252, 134, 0);
        }

        #endregion

        #region ValintanapitUlkoasu
        private void BtnsValinnat_MouseEnter(object sender, EventArgs e)
        {

            Label lb = (Label)sender;

            foreach (Control l in tableLayoutPanelValintanapit.Controls)
            {
                l.ForeColor = Color.White;
                l.BackColor = Color.FromArgb(41, 44, 51);
            }

            foreach (Control ctrl in tableLayoutPanelUusiPeli.Controls)
            {
                ctrl.ForeColor = Color.White;
            }

            foreach (Control ctrl in tableLayoutPanelTilastot.Controls)
            {
                ctrl.ForeColor = Color.White;
            }

            foreach (Control ctrl in tableLayoutPanelRosteritLajinValinta.Controls)
            {
                ctrl.ForeColor = Color.White;
            }

            // vaihdetaan näkyvä paneeli
            if (lb == lbUusiPeli)
            {
                panelUusiPeliLajinValinta.Visible = true;
                panelTilastotLajinValinta.Visible = false;
                panelRosteritLajinValinta.Visible = false;
            }
            else if (lb == lbTilastot)
            {
                panelUusiPeliLajinValinta.Visible = false;
                panelTilastotLajinValinta.Visible = true;
                panelRosteritLajinValinta.Visible = false;
            }
            else if (lb == lbRosterManagement)
            {
                panelUusiPeliLajinValinta.Visible = false;
                panelTilastotLajinValinta.Visible = false;
                panelRosteritLajinValinta.Visible = true;
            }

            // muutetaan valittuna olevan valikon väriä
            lb.BackColor = Color.FromArgb(31, 34, 41);
            lb.ForeColor = Color.FromArgb(252, 134, 0);
        }

        private void PanelBtn_MouseEnter(object sender, EventArgs e)
        {
            Control ctr = (Control)sender;

            // nollataan tekstien värit
            foreach (Control ctrl in tableLayoutPanelUusiPeli.Controls)
            {
                ctrl.ForeColor = Color.White;
            }

            foreach (Control ctrl in tableLayoutPanelTilastot.Controls)
            {
                ctrl.ForeColor = Color.White;
            }

            foreach (Control ctrl in tableLayoutPanelRosteritLajinValinta.Controls)
            {
                ctrl.ForeColor = Color.White;
            }

            // muutetaan valittavana olevan napin teksti oranssiksi
            ctr.ForeColor = Color.FromArgb(252, 134, 0);

        }

        #endregion

        #region UusiPeli

        // ajastimen muuttujat
        public int seconds; // Seconds.
        public int minutes; // Minutes.
        public bool paused = true; // State of the timer [PAUSED/WORKING].
        public int era;
        public int puoliaika;

        // muuttuja, jonka perusteella käyttäjän valitseman lajin mukaiset tiedot tulevat näkyviin ja pelin aikaiset tapahtumat tapahtuvat
        public string laji;

        Laji l;

        private void timer1_Tick(object sender, EventArgs e)
        {

            switch (laji)
            {
                case "latka":

                    if ((minutes == 0) && (seconds == 0))
                    {
                        // jos ajastin on lopussa

                        lbUPAjastinMin.Text = "00";
                        lbUPAjastinSec.Text = "00";

                        lbUPPlay.Text = "Start (väli)";
                        paused = true;
                        timer1.Enabled = false;

                        if (era == 3)
                        {
                            LatkapeliKaynnissa = false;
                        }

                    }
                    else
                    {
                        // muuten jatketaan

                        if (seconds < 1)
                        {
                            seconds = 59;
                            if (minutes == 0)
                            {

                            }
                            else
                            {
                                minutes -= 1;
                            }
                        }
                        else
                        {
                            seconds -= 1;
                        }

                        // muutetaan ajastimen tekstit
                        lbUPAjastinMin.Text = minutes.ToString("D2");
                        lbUPAjastinSec.Text = seconds.ToString("D2");
                    }

                    break;

                case "futis":
                    if ((minutes == 45 && seconds == 0 && puoliaika == 1) || (minutes == 90 && seconds == 0))
                    {
                        // jos puoliaika on lopussa

                        lbUPPlay.Text = "Start (väli)";
                        paused = true;
                        timer1.Enabled = false;

                        if (minutes == 90 && seconds == 0)
                        {
                            futispeliKaynnissa = false;
                        }

                    }
                    else
                    {
                        // muuten jatketaan
                        if (seconds > 59)
                        {
                            seconds = 0;
                            if (minutes == 45 && puoliaika == 1)
                            {

                            }
                            else if (minutes == 90)
                            {

                            }
                            else
                            {
                                minutes += 1;
                            }
                        }
                        else
                        {
                            seconds += 1;
                        }

                        // muutetaan ajastimen tekstit
                        lbUPAjastinMin.Text = minutes.ToString("D2");
                        lbUPAjastinSec.Text = seconds.ToString("D2");
                    }
                    break;

                case "saba":

                    if (minutes == 15 && seconds == 0)
                    {
                        // jos erä on lopussa

                        lbUPPlay.Text = "Start (väli)";
                        paused = true;
                        timer1.Enabled = false;

                        if (era == 3)
                        {
                            sabapeliKaynnissa = false;
                        }

                    }
                    else
                    {
                        // muuten jatketaan
                        if (seconds > 59)
                        {
                            seconds = 0;
                            if (minutes == 15)
                            {

                            }
                            else
                            {
                                minutes += 1;
                            }
                        }
                        else
                        {
                            seconds += 1;
                        }

                        // muutetaan ajastimen tekstit
                        lbUPAjastinMin.Text = minutes.ToString("D2");
                        lbUPAjastinSec.Text = seconds.ToString("D2");
                    }
                    break;

            }


        }

        private void lbUPPlay_Click(object sender, EventArgs e)
        {

            // jos on pausetettu ja peli käynnissä
            if (paused == true && LatkapeliKaynnissa == true)
            {
                timer1.Enabled = true;
                lbUPPlay.Text = "Pause (väli)";
                paused = false;

                // jos erä loppu ja alle 3 erää pelattu, asetetaan timeriin uudestaan 20min ja kasvatetaan erää
                if ((minutes == 0) && (seconds == 0) && era < 3)
                {
                    seconds = 00;
                    minutes = 20;

                    era += 1;

                    // päivitetään tapahtumadatagridi ja laukaisukartta
                    switch (era)
                    {
                        case 2:
                            dGVTapahtumaLista.DataSource = uusiLatkaPeli.TokaEra;
                            PiirraLaukaisukartta(uusiLatkaPeli.TokaEra);
                            break;
                        case 3:
                            dGVTapahtumaLista.DataSource = uusiLatkaPeli.KolmasEra;
                            PiirraLaukaisukartta(uusiLatkaPeli.KolmasEra);
                            break;
                    }


                    // päivitetään teksti
                    lbUPEra.Text = $"{era}. Erä";

                }
            }
            else if (paused == false && LatkapeliKaynnissa == true)
            {
                timer1.Enabled = false;
                lbUPPlay.Text = "Start (väli)";
                paused = true;
            }

            // jos on pausetettu ja peli käynnissä
            if (paused == true && futispeliKaynnissa == true)
            {
                timer1.Enabled = true;
                lbUPPlay.Text = "Pause (väli)";
                paused = false;

                // jos ensimmäinen puoliaika ohi, siirrytään seuraavaan
                if ((minutes == 45) && (seconds == 0) && puoliaika < 2)
                {

                    puoliaika += 1;

                    // päivitetään tapahtumadatagridi ja laukaisukartta
                    //dGVTapahtumaLista.DataSource = uusiFutisPeli.TokaPuoliaika;


                    // päivitetään teksti
                    lbUPEra.Text = $"{puoliaika}. puoliaika";

                }
            }
            else if (paused == false && futispeliKaynnissa == true)
            {
                timer1.Enabled = false;
                lbUPPlay.Text = "Start (väli)";
                paused = true;
            }

            if (paused == true && sabapeliKaynnissa == true)
            {
                timer1.Enabled = true;
                lbUPPlay.Text = "Pause (väli)";
                paused = false;

                // jos erä loppu ja alle 3 erää pelattu, niin nollataan timerin arvot ja aloitetaan uusi erä
                if ((minutes == 15) && (seconds == 0) && era < 3)
                {
                    seconds = 00;
                    minutes = 00;

                    era += 1;

                    // päivitetään tapahtumadatagridi ja laukaisukartta
                    switch (era)
                    {
                        case 2:
                            //dGVTapahtumaLista.DataSource = uusiSabaPeli.TokaEra;
                            //PiirraLaukaisukartta(uusiSabaPeli.TokaEra);
                            break;
                        case 3:
                            //dGVTapahtumaLista.DataSource = uusiSabaPeli.KolmasEra;
                            //PiirraLaukaisukartta(uusiSabaPeli.KolmasEra);
                            break;
                    }


                    // päivitetään teksti
                    lbUPEra.Text = $"{era}. Erä";

                }
            }
            else if (paused == false && sabapeliKaynnissa == true)
            {
                timer1.Enabled = false;
                lbUPPlay.Text = "Start (väli)";
                paused = true;
            }

        }

        private void Keypressed(object o, KeyPressEventArgs e)
        {

            // jos painetaan spacea niin kello käynnistyy
            if (e.KeyChar == (char)Keys.Space)
            {
                e.Handled = true;
                lbUPPlay_Click(lbUPPlay, e);
            }
        }

        private void lbAjanNopeutus_Click(object sender, EventArgs e)
        {
            if (timer1.Interval == 1000)
            {
                timer1.Interval = 10;
                lbAjanNopeutus.Text = "Normaali nopeus";

            }
            else
            {
                timer1.Interval = 1000;
                lbAjanNopeutus.Text = "Peliajan nopeutus";
            }
        }

        private void pBoxKentta_Click(object sender, EventArgs e)
        {
            // uusi tapahtuma
            PeliTapahtuma eventti = new PeliTapahtuma();

            // otetaan ylös tapahtuman ajankohta
            eventti.minuutit = minutes;
            eventti.sekuntit = seconds;
            eventti.aika = minutes.ToString("D2") + ":" + seconds.ToString("D2");

            // tapahtuman paikka
            Point p = pBoxKentta.PointToClient(Cursor.Position);
            eventti.koodrinaatit = p;

            // merkitään syöttäjät alksi tyhjiksi
            eventti.syottaja1 = string.Empty;
            eventti.syottaja2 = string.Empty;
            eventti.assist1 = null;
            eventti.assist2 = null;

            switch (l)
            {
                case Laji.latka:

                    frmTapahtuma frm = new frmTapahtuma(eventti, uusiLatkaPeli, true, laji);
                    frm.ShowDialog();

                    break;

                case Laji.futis:

                    // avataan tapahtumaformi
                    frm = new frmTapahtuma(eventti, uusiFutisPeli.kotijoukkue, uusiFutisPeli.vierasjoukkue, true, laji);
                    frm.ShowDialog();

                    break;

                case Laji.saba:

                    // avataan tapahtumaformi
                    frm = new frmTapahtuma(eventti, uusiSabaPeli.kotijoukkue, uusiSabaPeli.vierasjoukkue, true, laji);
                    frm.ShowDialog();

                    break;
            }

            if (eventti.happeninki == "")
            {
                return;
            }
                
            if (l == Laji.latka)
            {
                // lisätään tapahtuma tapahtumaluetteloon, viedään erä datagridiin ja piirretään tapahtumalista kenttään
                switch (era)
                {
                    case 1:
                        uusiLatkaPeli.EkaEra.Add(eventti);
                        dGVTapahtumaLista.DataSource = uusiLatkaPeli.EkaEra;
                        PiirraLaukaisukartta(uusiLatkaPeli.EkaEra);
                        break;
                    case 2:
                        uusiLatkaPeli.TokaEra.Add(eventti);
                        dGVTapahtumaLista.DataSource = uusiLatkaPeli.TokaEra;
                        PiirraLaukaisukartta(uusiLatkaPeli.TokaEra);
                        break;
                    case 3:
                        uusiLatkaPeli.KolmasEra.Add(eventti);
                        dGVTapahtumaLista.DataSource = uusiLatkaPeli.KolmasEra;
                        PiirraLaukaisukartta(uusiLatkaPeli.KolmasEra);
                        break;
                }

                lbUPJoukkueetJaTulos.Text = $"{uusiLatkaPeli.kotijoukkue.lyh} {uusiLatkaPeli.kotijoukkueenMaalit} - {uusiLatkaPeli.vierasjoukkueenMaalit} {uusiLatkaPeli.vierasjoukkue.lyh}";
            }
            else if (laji == "futis")
            {

                switch (puoliaika)
                {
                    case 1:
                        uusiFutisPeli.EkaPuoliaika.Add(eventti);
                        dGVTapahtumaLista.DataSource = uusiFutisPeli.EkaPuoliaika;
                        PiirraLaukaisukartta(uusiFutisPeli.EkaPuoliaika);
                        break;
                    case 2:
                        uusiFutisPeli.TokaPuoliaika.Add(eventti);
                        dGVTapahtumaLista.DataSource = uusiFutisPeli.TokaPuoliaika;
                        PiirraLaukaisukartta(uusiFutisPeli.TokaPuoliaika);
                        break;
                }

            }
            else if (laji == "saba")
            {
                switch (era)
                {
                    case 1:
                        uusiSabaPeli.EkaEra.Add(eventti);
                        dGVTapahtumaLista.DataSource = uusiSabaPeli.EkaEra;
                        PiirraLaukaisukartta(uusiSabaPeli.EkaEra);
                        break;
                    case 2:
                        uusiLatkaPeli.TokaEra.Add(eventti);
                        dGVTapahtumaLista.DataSource = uusiLatkaPeli.TokaEra;
                        PiirraLaukaisukartta(uusiSabaPeli.TokaEra);
                        break;
                    case 3:
                        uusiLatkaPeli.KolmasEra.Add(eventti);
                        dGVTapahtumaLista.DataSource = uusiLatkaPeli.KolmasEra;
                        PiirraLaukaisukartta(uusiSabaPeli.KolmasEra);
                        break;
                }
            }



        }

        private void PiirraLaukaisukartta(BindingList<PeliTapahtuma> era)
        {

            switch (laji)
            {

                case "latka":
                    // tyhjennetään kenttä ja aloitetaan tyhjästä kentästä
                    pBoxKentta.Image = null;
                    pBoxKentta.Image = Bitmap.FromFile("hockeyrink.png");

                    Bitmap laukaus = (Bitmap)Bitmap.FromFile("laukaus.png");

                    // piirretään joka tapahtuma kenttään
                    foreach (PeliTapahtuma tapahtuma in era)
                    {
                        Bitmap kentta = (Bitmap)pBoxKentta.Image;
                        Graphics g = Graphics.FromImage(pBoxKentta.Image);
                        g.DrawImage(laukaus, tapahtuma.koodrinaatit);
                        pBoxKentta.Image = kentta;
                        g.Dispose();
                    }

                    break;

                case "futis":

                    // tyhjennetään kenttä ja aloitetaan tyhjästä kentästä
                    pBoxKentta.Image = null;
                    pBoxKentta.Image = Bitmap.FromFile("soccerfield.png");

                    Bitmap veto = (Bitmap)Bitmap.FromFile("laukaus.png");

                    // piirretään joka tapahtuma kenttään
                    foreach (PeliTapahtuma tapahtuma in era)
                    {
                        Bitmap kentta = (Bitmap)pBoxKentta.Image;
                        Graphics g = Graphics.FromImage(pBoxKentta.Image);
                        g.DrawImage(veto, tapahtuma.koodrinaatit);
                        pBoxKentta.Image = kentta;
                        g.Dispose();
                    }

                    break;

                case "saba":
                    // tyhjennetään kenttä ja aloitetaan tyhjästä kentästä
                    pBoxKentta.Image = null;
                    pBoxKentta.Image = Bitmap.FromFile("fbrink.png");

                    Bitmap kuti = (Bitmap)Bitmap.FromFile("laukaus.png");

                    // piirretään joka tapahtuma kenttään
                    foreach (PeliTapahtuma tapahtuma in era)
                    {
                        Bitmap kentta = (Bitmap)pBoxKentta.Image;
                        Graphics g = Graphics.FromImage(pBoxKentta.Image);
                        g.DrawImage(kuti, tapahtuma.koodrinaatit);
                        pBoxKentta.Image = kentta;
                        g.Dispose();
                    }
                    break;
            }
        }

        private void dataGridViewTapahtumaLista_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // muokataan tapahtumaa

            int indeksi;

            // jos klikkaus tapahtuu headerista, ei tapahdu mitään, muuten otetaan indeksi jonka perusteella tapahtuma valitaan
            if (e.RowIndex != -1)
            {
                indeksi = e.RowIndex;
            }
            else
            {
                return;
            }

            // väliaikainen lista tapahtumista muokkauksia vareten
            BindingList<PeliTapahtuma> lista = new BindingList<PeliTapahtuma>();

            if (l == Laji.latka)
            {
                // otetaan haluttu erä väliaikaiseen listaan
                switch (era)
                {
                    case 1:
                        lista = uusiLatkaPeli.EkaEra;
                        break;
                    case 2:
                        lista = uusiLatkaPeli.TokaEra;
                        break;
                    case 3:
                        lista = uusiLatkaPeli.KolmasEra;
                        break;
                }

                try
                {
                    if (indeksi > -1 && indeksi < lista.Count)
                    {
                        // otetaan valittu tapahtuma listasta
                        PeliTapahtuma valiaikainen = lista[indeksi].tapahtumaKopio();

                        // avataan tapahtumaformi, josta pääsee muokkaamaan pelaajia ja tapahtumatyyppiä tai poistamaan
                        frmTapahtuma frm = new frmTapahtuma(valiaikainen, uusiLatkaPeli, false, laji);
                        frm.ShowDialog();

                        if (frm.poistetaanko == true)
                        {
                            PeliTapahtuma.PoistaTapahtuma(lista, indeksi, uusiLatkaPeli);
                        }
                        else if (valiaikainen != lista[indeksi])
                        {
                            // jos tapahtumaa on muokattu, niin poistetaan alkuperäinen tapahtuma ja korvataan se muokatulla tapahtumalla
                            PeliTapahtuma.PoistaTapahtuma(lista, indeksi, uusiLatkaPeli);

                            switch (valiaikainen.happeninki)
                            {
                                case "Maali":
                                    PeliTapahtuma.TilastoiMaali(valiaikainen, uusiLatkaPeli);
                                    // päivitetään tulostaulu
                                    lbUPJoukkueetJaTulos.Text = $"{uusiLatkaPeli.kotijoukkue.lyh} {uusiLatkaPeli.kotijoukkueenMaalit} - {uusiLatkaPeli.vierasjoukkueenMaalit} {uusiLatkaPeli.vierasjoukkue.lyh}";
                                    break;

                                case "Laukaus":
                                    PeliTapahtuma.TilastoiLaukaus(valiaikainen, uusiLatkaPeli);
                                    break;
                            }

                            lista.Add(valiaikainen);
                        }


                        // järjestellään lista aikajärjestykseen
                        List<PeliTapahtuma> jarjestykseen = lista.OrderByDescending(o => o.aika).ToList();
                        lista = new BindingList<PeliTapahtuma>(jarjestykseen);

                        // korvataan erä muokatulla listalla
                        switch (era)
                        {
                            case 1:
                                uusiLatkaPeli.EkaEra = lista;
                                dGVTapahtumaLista.DataSource = uusiLatkaPeli.EkaEra;
                                PiirraLaukaisukartta(uusiLatkaPeli.EkaEra);
                                break;
                            case 2:
                                uusiLatkaPeli.TokaEra = lista;
                                dGVTapahtumaLista.DataSource = uusiLatkaPeli.TokaEra;
                                PiirraLaukaisukartta(uusiLatkaPeli.TokaEra);
                                break;
                            case 3:
                                uusiLatkaPeli.KolmasEra = lista;
                                dGVTapahtumaLista.DataSource = uusiLatkaPeli.KolmasEra;
                                PiirraLaukaisukartta(uusiLatkaPeli.KolmasEra);
                                break;
                        }

                    }

                }
                catch (Exception)
                {
                    return;
                }

            }
            else if (laji == "futis")
            {
                // otetaan haluttu erä väliaikaiseen listaan
                switch (puoliaika)
                {
                    case 1:
                        lista = uusiFutisPeli.EkaPuoliaika;
                        break;
                    case 2:
                        lista = uusiFutisPeli.TokaPuoliaika;
                        break;
                }

                try
                {
                    if (indeksi > -1 && indeksi < lista.Count)
                    {
                        // otetaan valittu tapahtuma listasta
                        PeliTapahtuma valiaikainen = lista[indeksi];

                        // avataan tapahtumaformi, josta pääsee muokkaamaan pelaajia ja tapahtumatyyppiä tai poistamaan
                        frmTapahtuma frm = new frmTapahtuma(valiaikainen, uusiFutisPeli.kotijoukkue, uusiFutisPeli.vierasjoukkue, false, laji);
                        frm.ShowDialog();

                        if (frm.poistetaanko == true)
                        {
                            PeliTapahtuma.PoistaTapahtuma(lista, indeksi, uusiFutisPeli);
                        }
                        else if (valiaikainen != lista[indeksi])
                        {
                            // jos tapahtumaa on muokattu, niin poistetaan alkuperäinen tapahtuma ja korvataan se muokatulla tapahtumalla
                            PeliTapahtuma.PoistaTapahtuma(lista, indeksi, uusiFutisPeli);

                            switch (valiaikainen.happeninki)
                            {
                                case "Maali":
                                    PeliTapahtuma.TilastoiMaali(valiaikainen, uusiFutisPeli);
                                    // päivitetään tulostaulu
                                    lbUPJoukkueetJaTulos.Text = $"{uusiFutisPeli.kotijoukkue.lyh} {uusiFutisPeli.kotijoukkueenMaalit} - {uusiFutisPeli.vierasjoukkueenMaalit} {uusiFutisPeli.vierasjoukkue.lyh}";
                                    break;

                                case "Laukaus":
                                    PeliTapahtuma.TilastoiLaukaus(valiaikainen, uusiFutisPeli);
                                    break;
                            }

                            lista.Add(valiaikainen);
                        }


                        // järjestellään lista aikajärjestykseen
                        List<PeliTapahtuma> jarjestykseen = lista.OrderByDescending(o => o.aika).ToList();
                        lista = new BindingList<PeliTapahtuma>(jarjestykseen);

                        // korvataan erä muokatulla listalla
                        switch (puoliaika)
                        {
                            case 1:
                                uusiFutisPeli.EkaPuoliaika = lista;
                                dGVTapahtumaLista.DataSource = uusiFutisPeli.EkaPuoliaika;
                                PiirraLaukaisukartta(uusiFutisPeli.EkaPuoliaika);
                                break;
                            case 2:
                                uusiFutisPeli.TokaPuoliaika = lista;
                                dGVTapahtumaLista.DataSource = uusiFutisPeli.TokaPuoliaika;
                                PiirraLaukaisukartta(uusiFutisPeli.TokaPuoliaika);
                                break;
                        }

                    }

                }
                catch (Exception)
                {
                    return;
                }

            }
            else if (laji == "saba")
            {

                // otetaan haluttu erä väliaikaiseen listaan
                switch (era)
                {
                    case 1:
                        lista = uusiSabaPeli.EkaEra;
                        break;
                    case 2:
                        lista = uusiSabaPeli.TokaEra;
                        break;
                    case 3:
                        lista = uusiSabaPeli.KolmasEra;
                        break;
                }

                try
                {
                    if (indeksi > -1 && indeksi < lista.Count)
                    {
                        // otetaan valittu tapahtuma listasta
                        PeliTapahtuma valiaikainen = lista[indeksi];

                        // avataan tapahtumaformi, josta pääsee muokkaamaan pelaajia ja tapahtumatyyppiä tai poistamaan
                        frmTapahtuma frm = new frmTapahtuma(valiaikainen, uusiSabaPeli.kotijoukkue, uusiSabaPeli.vierasjoukkue, false, laji);
                        frm.ShowDialog();

                        if (frm.poistetaanko == true)
                        {
                            PeliTapahtuma.PoistaTapahtuma(lista, indeksi, uusiSabaPeli);
                        }
                        else if (valiaikainen != lista[indeksi])
                        {
                            // jos tapahtumaa on muokattu, niin poistetaan alkuperäinen tapahtuma ja korvataan se muokatulla tapahtumalla
                            PeliTapahtuma.PoistaTapahtuma(lista, indeksi, uusiSabaPeli);

                            switch (valiaikainen.happeninki)
                            {
                                case "Maali":
                                    PeliTapahtuma.TilastoiMaali(valiaikainen, uusiSabaPeli);
                                    // päivitetään tulostaulu
                                    lbUPJoukkueetJaTulos.Text = $"{uusiSabaPeli.kotijoukkue.lyh} {uusiSabaPeli.kotijoukkueenMaalit} - {uusiSabaPeli.vierasjoukkueenMaalit} {uusiSabaPeli.vierasjoukkue.lyh}";
                                    break;

                                case "Laukaus":
                                    PeliTapahtuma.TilastoiLaukaus(valiaikainen, uusiSabaPeli);
                                    break;
                            }

                            lista.Add(valiaikainen);
                        }


                        // järjestellään lista aikajärjestykseen
                        List<PeliTapahtuma> jarjestykseen = lista.OrderByDescending(o => o.aika).ToList();
                        lista = new BindingList<PeliTapahtuma>(jarjestykseen);

                        // korvataan erä muokatulla listalla
                        switch (era)
                        {
                            case 1:
                                uusiSabaPeli.EkaEra = lista;
                                dGVTapahtumaLista.DataSource = uusiSabaPeli.EkaEra;
                                PiirraLaukaisukartta(uusiSabaPeli.EkaEra);
                                break;
                            case 2:
                                uusiSabaPeli.TokaEra = lista;
                                dGVTapahtumaLista.DataSource = uusiSabaPeli.TokaEra;
                                PiirraLaukaisukartta(uusiSabaPeli.TokaEra);
                                break;
                            case 3:
                                uusiSabaPeli.KolmasEra = lista;
                                dGVTapahtumaLista.DataSource = uusiSabaPeli.KolmasEra;
                                PiirraLaukaisukartta(uusiSabaPeli.KolmasEra);
                                break;
                        }

                    }

                }
                catch (Exception)
                {
                    return;
                }
            }

        }

        private void lbUPLatkaLopetaJaTallennaPeli_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Haluatko varmasti lopettaa ja tallentaa pelin?", "", MessageBoxButtons.YesNo);

            if (dg == DialogResult.No)
            {
                return;
            }

            switch (laji)
            {

                case "latka":

                    try
                    {
                        // jos lista on tyhjä (xml-puuttuu), niin alustetaan lista
                        if (latkapelit == null)
                        {
                            latkapelit = new List<LatkaPeli>();
                        }

                        // merkataan pelin lopetusaika
                        uusiLatkaPeli.lopaik = DateTime.Now.ToShortTimeString();

                        // lisätään joukkueiden pelattujen pelien määrää
                        uusiLatkaPeli.kotijoukkue.ottelut++;
                        uusiLatkaPeli.vierasjoukkue.ottelut++;

                        // tilastoidaan pelaajille pelattu ottelu (pelaaville maalivahdielle erikseen ja kenttäpelaajille erikseen)
                        foreach (Pelaaja mv in uusiLatkaPeli.kotijoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                        {
                            if ($"#{mv.numero} {mv.snimi}" == uusiLatkaPeli.kotijoukkueenMV)
                            {
                                mv.pelatutPelit++;
                            }
                        }
                        foreach (Pelaaja pl in uusiLatkaPeli.kotijoukkue.plrs.Where(o => o.pelipaikka == "H" || o.pelipaikka == "P"))
                        {
                            pl.pelatutPelit++;
                        }
                        foreach (Pelaaja mv in uusiLatkaPeli.vierasjoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                        {
                            if ($"#{mv.numero} {mv.snimi}" == uusiLatkaPeli.vierasjoukkueenMV)
                            {
                                mv.pelatutPelit++;
                            }
                        }
                        foreach (Pelaaja pl in uusiLatkaPeli.vierasjoukkue.plrs.Where(o => o.pelipaikka == "H" || o.pelipaikka == "P"))
                        {
                            pl.pelatutPelit++;
                        }

                        // merkitään peliin mahdollinen voittaja ja tilastoidaan peli joukkueiden tietoihin
                        if (uusiLatkaPeli.kotijoukkueenMaalit > uusiLatkaPeli.vierasjoukkueenMaalit)
                        {
                            uusiLatkaPeli.voittaja = uusiLatkaPeli.kotijoukkue.nimi;
                            uusiLatkaPeli.kotijoukkue.voitot++;
                            uusiLatkaPeli.vierasjoukkue.haviot++;
                            uusiLatkaPeli.kotijoukkue.pisteet = (3 * uusiLatkaPeli.kotijoukkue.voitot) + uusiLatkaPeli.kotijoukkue.tasapelit;
                            uusiLatkaPeli.vierasjoukkue.pisteet = (3 * uusiLatkaPeli.vierasjoukkue.voitot) + uusiLatkaPeli.vierasjoukkue.tasapelit;

                            // tilastoidaan maalivahdeille voitto ja tappio
                            foreach (Pelaaja mv in uusiLatkaPeli.kotijoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                            {
                                if ($"#{mv.numero} {mv.snimi}" == uusiLatkaPeli.kotijoukkueenMV)
                                {
                                    mv.voitot++;
                                }
                            }
                            foreach (Pelaaja mv in uusiLatkaPeli.vierasjoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                            {
                                if ($"#{mv.numero} {mv.snimi}" == uusiLatkaPeli.vierasjoukkueenMV)
                                {
                                    mv.havityt++;
                                }
                            }
                        }
                        else if (uusiLatkaPeli.kotijoukkueenMaalit < uusiLatkaPeli.vierasjoukkueenMaalit)
                        {
                            uusiLatkaPeli.voittaja = uusiLatkaPeli.vierasjoukkue.nimi;
                            uusiLatkaPeli.vierasjoukkue.voitot++;
                            uusiLatkaPeli.kotijoukkue.haviot++;
                            uusiLatkaPeli.kotijoukkue.pisteet = (3 * uusiLatkaPeli.kotijoukkue.voitot) + uusiLatkaPeli.kotijoukkue.tasapelit;
                            uusiLatkaPeli.vierasjoukkue.pisteet = (3 * uusiLatkaPeli.vierasjoukkue.voitot) + uusiLatkaPeli.vierasjoukkue.tasapelit;

                            // tilastoidaan maalivahdeille voitto ja tappio
                            foreach (Pelaaja mv in uusiLatkaPeli.kotijoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                            {
                                if ($"#{mv.numero} {mv.snimi}" == uusiLatkaPeli.kotijoukkueenMV)
                                {
                                    mv.havityt++;
                                }
                            }
                            foreach (Pelaaja mv in uusiLatkaPeli.vierasjoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                            {
                                if ($"#{mv.numero} {mv.snimi}" == uusiLatkaPeli.vierasjoukkueenMV)
                                {
                                    mv.voitot++;
                                }
                            }
                        }
                        else
                        {
                            uusiLatkaPeli.kotijoukkue.tasapelit++;
                            uusiLatkaPeli.vierasjoukkue.tasapelit++;
                            uusiLatkaPeli.kotijoukkue.pisteet = (3 * uusiLatkaPeli.kotijoukkue.voitot) + uusiLatkaPeli.kotijoukkue.tasapelit;
                            uusiLatkaPeli.vierasjoukkue.pisteet = (3 * uusiLatkaPeli.vierasjoukkue.voitot) + uusiLatkaPeli.vierasjoukkue.tasapelit;

                        }

                        // lisätään lätkäpelin tiedot listaan peleistä
                        latkapelit.Add(uusiLatkaPeli);


                        int i = 0;

                        // korvataan lätkäjoukkuelistassa oleva joukkue pelin aikaisella joukkueella, joka sisältää päivitetyt tilastot
                        foreach (Team t in latkajoukkueet)
                        {
                            if (uusiLatkaPeli.kotijoukkue.nimi == t.nimi)
                            {
                                latkajoukkueet[i].plrs = uusiLatkaPeli.kotijoukkue.plrs;
                            }
                            if (uusiLatkaPeli.vierasjoukkue.nimi == t.nimi)
                            {
                                latkajoukkueet[i].plrs = uusiLatkaPeli.vierasjoukkue.plrs;
                            }

                            i++;
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Tietojen viemisessä tapahtui virhe.");
                        return;
                    }

                    try
                    {
                        // tallennetaan listat peleistä ja joukkueista xml-tiedostoihin
                        XMLSerializerit.SerializeXMLLatkapelit(latkapelit);
                        XMLSerializerit.SerializeXMLLatkajoukkueet(latkajoukkueet);

                        // tyhjennetään lätkäpelin tiedot
                        uusiLatkaPeli = null;

                        // lopetetaan peli
                        LatkapeliKaynnissa = false;

                        // poistetaan keypressevent
                        this.KeyPress -= new KeyPressEventHandler(Keypressed);
                        this.KeyPreview = false;

                        panelUusiPeli.Visible = false;

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Tallentamisessa tapahtui virhe.");
                        return;
                    }

                    MessageBox.Show("Pelin tiedot tallennettu onnistuneesti.");

                    break;

                case "futis":

                    try
                    {
                        // jos lista on tyhjä (xml-puuttuu), niin alustetaan lista
                        if (futispelit == null)
                        {
                            futispelit = new List<FutisPeli>();
                        }

                        // merkataan pelin lopetusaika
                        uusiFutisPeli.lopaik = DateTime.Now.ToShortTimeString();

                        // lisätään joukkueiden pelattujen pelien määrää
                        uusiFutisPeli.kotijoukkue.ottelut++;
                        uusiFutisPeli.vierasjoukkue.ottelut++;

                        // tilastoidaan pelaajille pelattu ottelu (pelaaville maalivahdielle erikseen ja kenttäpelaajille erikseen)
                        foreach (Pelaaja mv in uusiFutisPeli.kotijoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                        {
                            if ($"#{mv.numero} {mv.snimi}" == uusiFutisPeli.kotijoukkueenMV)
                            {
                                mv.pelatutPelit++;
                            }
                        }
                        foreach (Pelaaja pl in uusiFutisPeli.kotijoukkue.plrs.Where(o => o.pelipaikka == "H" || o.pelipaikka == "P"))
                        {
                            pl.pelatutPelit++;
                        }
                        foreach (Pelaaja mv in uusiFutisPeli.vierasjoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                        {
                            if ($"#{mv.numero} {mv.snimi}" == uusiFutisPeli.vierasjoukkueenMV)
                            {
                                mv.pelatutPelit++;
                            }
                        }
                        foreach (Pelaaja pl in uusiFutisPeli.vierasjoukkue.plrs.Where(o => o.pelipaikka == "H" || o.pelipaikka == "P"))
                        {
                            pl.pelatutPelit++;
                        }

                        // merkitään peliin mahdollinen voittaja ja tilastoidaan peli joukkueiden tietoihin
                        if (uusiFutisPeli.kotijoukkueenMaalit > uusiFutisPeli.vierasjoukkueenMaalit)
                        {
                            uusiFutisPeli.voittaja = uusiFutisPeli.kotijoukkue.nimi;
                            uusiFutisPeli.kotijoukkue.voitot++;
                            uusiFutisPeli.vierasjoukkue.haviot++;
                            uusiFutisPeli.kotijoukkue.pisteet = (3 * uusiFutisPeli.kotijoukkue.voitot) + uusiFutisPeli.kotijoukkue.tasapelit;
                            uusiFutisPeli.vierasjoukkue.pisteet = (3 * uusiFutisPeli.vierasjoukkue.voitot) + uusiFutisPeli.vierasjoukkue.tasapelit;

                            // tilastoidaan maalivahdeille voitto ja tappio
                            foreach (Pelaaja mv in uusiFutisPeli.kotijoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                            {
                                if ($"#{mv.numero} {mv.snimi}" == uusiFutisPeli.kotijoukkueenMV)
                                {
                                    mv.voitot++;
                                }
                            }
                            foreach (Pelaaja mv in uusiFutisPeli.vierasjoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                            {
                                if ($"#{mv.numero} {mv.snimi}" == uusiFutisPeli.vierasjoukkueenMV)
                                {
                                    mv.havityt++;
                                }
                            }
                        }
                        else if (uusiFutisPeli.kotijoukkueenMaalit < uusiFutisPeli.vierasjoukkueenMaalit)
                        {
                            uusiFutisPeli.voittaja = uusiFutisPeli.vierasjoukkue.nimi;
                            uusiFutisPeli.vierasjoukkue.voitot++;
                            uusiFutisPeli.kotijoukkue.haviot++;
                            uusiFutisPeli.kotijoukkue.pisteet = (3 * uusiFutisPeli.kotijoukkue.voitot) + uusiFutisPeli.kotijoukkue.tasapelit;
                            uusiFutisPeli.vierasjoukkue.pisteet = (3 * uusiFutisPeli.vierasjoukkue.voitot) + uusiFutisPeli.vierasjoukkue.tasapelit;

                            // tilastoidaan maalivahdeille voitto ja tappio
                            foreach (Pelaaja mv in uusiFutisPeli.kotijoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                            {
                                if ($"#{mv.numero} {mv.snimi}" == uusiFutisPeli.kotijoukkueenMV)
                                {
                                    mv.havityt++;
                                }
                            }
                            foreach (Pelaaja mv in uusiFutisPeli.vierasjoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                            {
                                if ($"#{mv.numero} {mv.snimi}" == uusiFutisPeli.vierasjoukkueenMV)
                                {
                                    mv.voitot++;
                                }
                            }
                        }
                        else
                        {
                            uusiFutisPeli.kotijoukkue.tasapelit++;
                            uusiFutisPeli.vierasjoukkue.tasapelit++;
                            uusiFutisPeli.kotijoukkue.pisteet = (3 * uusiFutisPeli.kotijoukkue.voitot) + uusiFutisPeli.kotijoukkue.tasapelit;
                            uusiFutisPeli.vierasjoukkue.pisteet = (3 * uusiFutisPeli.vierasjoukkue.voitot) + uusiFutisPeli.vierasjoukkue.tasapelit;
                        }

                        // lisätään lätkäpelin tiedot listaan peleistä
                        futispelit.Add(uusiFutisPeli);


                        int i = 0;

                        // korvataan lätkäjoukkuelistassa oleva joukkue pelin aikaisella joukkueella, joka sisältää päivitetyt tilastot
                        foreach (Team t in futisjoukkueet)
                        {
                            if (uusiFutisPeli.kotijoukkue.nimi == t.nimi)
                            {
                                futisjoukkueet[i].plrs = uusiFutisPeli.kotijoukkue.plrs;
                            }
                            if (uusiFutisPeli.vierasjoukkue.nimi == t.nimi)
                            {
                                futisjoukkueet[i].plrs = uusiFutisPeli.vierasjoukkue.plrs;
                            }

                            i++;
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Tietojen viemisessä tapahtui virhe.");
                        return;
                    }

                    try
                    {
                        // tallennetaan listat peleistä ja joukkueista xml-tiedostoihin
                        XMLSerializerit.SerializeXMLFutispelit(futispelit);
                        XMLSerializerit.SerializeXMLFutisjoukkueet(futisjoukkueet);

                        // tyhjennetään lätkäpelin tiedot
                        uusiFutisPeli = null;

                        // lopetetaan peli
                        futispeliKaynnissa = false;

                        // poistetaan keypressevent
                        this.KeyPress -= new KeyPressEventHandler(Keypressed);
                        this.KeyPreview = false;

                        panelUusiPeli.Visible = false;

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Tallentamisessa tapahtui virhe.");
                        return;
                    }

                    MessageBox.Show("Pelin tiedot tallennettu onnistuneesti.");

                    break;

                case "saba":

                    try
                    {
                        // jos lista on tyhjä (xml-puuttuu), niin alustetaan lista
                        if (sabapelit == null)
                        {
                            sabapelit = new List<SabaPeli>();
                        }

                        // merkataan pelin lopetusaika
                        uusiSabaPeli.lopaik = DateTime.Now.ToShortTimeString();

                        // lisätään joukkueiden pelattujen pelien määrää
                        uusiSabaPeli.kotijoukkue.ottelut++;
                        uusiSabaPeli.vierasjoukkue.ottelut++;

                        // tilastoidaan pelaajille pelattu ottelu (pelaaville maalivahdielle erikseen ja kenttäpelaajille erikseen)
                        foreach (Pelaaja mv in uusiSabaPeli.kotijoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                        {
                            if ($"#{mv.numero} {mv.snimi}" == uusiSabaPeli.kotijoukkueenMV)
                            {
                                mv.pelatutPelit++;
                            }
                        }
                        foreach (Pelaaja pl in uusiSabaPeli.kotijoukkue.plrs.Where(o => o.pelipaikka == "H" || o.pelipaikka == "P"))
                        {
                            pl.pelatutPelit++;
                        }
                        foreach (Pelaaja mv in uusiSabaPeli.vierasjoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                        {
                            if ($"#{mv.numero} {mv.snimi}" == uusiSabaPeli.vierasjoukkueenMV)
                            {
                                mv.pelatutPelit++;
                            }
                        }
                        foreach (Pelaaja pl in uusiSabaPeli.vierasjoukkue.plrs.Where(o => o.pelipaikka == "H" || o.pelipaikka == "P"))
                        {
                            pl.pelatutPelit++;
                        }

                        // merkitään peliin mahdollinen voittaja ja tilastoidaan peli joukkueiden tietoihin
                        if (uusiSabaPeli.kotijoukkueenMaalit > uusiSabaPeli.vierasjoukkueenMaalit)
                        {
                            uusiSabaPeli.voittaja = uusiSabaPeli.kotijoukkue.nimi;
                            uusiSabaPeli.kotijoukkue.voitot++;
                            uusiSabaPeli.vierasjoukkue.haviot++;
                            uusiSabaPeli.kotijoukkue.pisteet = (3 * uusiSabaPeli.kotijoukkue.voitot) + uusiSabaPeli.kotijoukkue.tasapelit;
                            uusiSabaPeli.vierasjoukkue.pisteet = (3 * uusiSabaPeli.vierasjoukkue.voitot) + uusiSabaPeli.vierasjoukkue.tasapelit;

                            // tilastoidaan maalivahdeille voitto ja tappio
                            foreach (Pelaaja mv in uusiSabaPeli.kotijoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                            {
                                if ($"#{mv.numero} {mv.snimi}" == uusiSabaPeli.kotijoukkueenMV)
                                {
                                    mv.voitot++;
                                }
                            }
                            foreach (Pelaaja mv in uusiSabaPeli.vierasjoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                            {
                                if ($"#{mv.numero} {mv.snimi}" == uusiSabaPeli.vierasjoukkueenMV)
                                {
                                    mv.havityt++;
                                }
                            }
                        }
                        else if (uusiSabaPeli.kotijoukkueenMaalit < uusiSabaPeli.vierasjoukkueenMaalit)
                        {
                            uusiSabaPeli.voittaja = uusiSabaPeli.vierasjoukkue.nimi;
                            uusiSabaPeli.vierasjoukkue.voitot++;
                            uusiSabaPeli.kotijoukkue.haviot++;
                            uusiSabaPeli.kotijoukkue.pisteet = (3 * uusiSabaPeli.kotijoukkue.voitot) + uusiSabaPeli.kotijoukkue.tasapelit;
                            uusiSabaPeli.vierasjoukkue.pisteet = (3 * uusiSabaPeli.vierasjoukkue.voitot) + uusiSabaPeli.vierasjoukkue.tasapelit;

                            // tilastoidaan maalivahdeille voitto ja tappio
                            foreach (Pelaaja mv in uusiSabaPeli.kotijoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                            {
                                if ($"#{mv.numero} {mv.snimi}" == uusiSabaPeli.kotijoukkueenMV)
                                {
                                    mv.havityt++;
                                }
                            }
                            foreach (Pelaaja mv in uusiSabaPeli.vierasjoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                            {
                                if ($"#{mv.numero} {mv.snimi}" == uusiSabaPeli.vierasjoukkueenMV)
                                {
                                    mv.voitot++;
                                }
                            }
                        }
                        else
                        {
                            uusiSabaPeli.kotijoukkue.tasapelit++;
                            uusiSabaPeli.vierasjoukkue.tasapelit++;
                            uusiSabaPeli.kotijoukkue.pisteet = (3 * uusiSabaPeli.kotijoukkue.voitot) + uusiSabaPeli.kotijoukkue.tasapelit;
                            uusiSabaPeli.vierasjoukkue.pisteet = (3 * uusiSabaPeli.vierasjoukkue.voitot) + uusiSabaPeli.vierasjoukkue.tasapelit;
                        }

                        // lisätään lätkäpelin tiedot listaan peleistä
                        sabapelit.Add(uusiSabaPeli);


                        int i = 0;

                        // korvataan lätkäjoukkuelistassa oleva joukkue pelin aikaisella joukkueella, joka sisältää päivitetyt tilastot
                        foreach (Team t in sabajoukkueet)
                        {
                            if (uusiSabaPeli.kotijoukkue.nimi == t.nimi)
                            {
                                sabajoukkueet[i].plrs = uusiSabaPeli.kotijoukkue.plrs;
                            }
                            if (uusiSabaPeli.vierasjoukkue.nimi == t.nimi)
                            {
                                sabajoukkueet[i].plrs = uusiSabaPeli.vierasjoukkue.plrs;
                            }

                            i++;
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Tietojen viemisessä tapahtui virhe.");
                        return;
                    }

                    try
                    {
                        // tallennetaan listat peleistä ja joukkueista xml-tiedostoihin
                        XMLSerializerit.SerializeXMLSabapelit(sabapelit);
                        XMLSerializerit.SerializeXMLSabajoukkueet(sabajoukkueet);

                        // tyhjennetään lätkäpelin tiedot
                        uusiSabaPeli = null;

                        // lopetetaan peli
                        sabapeliKaynnissa = false;

                        // poistetaan keypressevent
                        this.KeyPress -= new KeyPressEventHandler(Keypressed);
                        this.KeyPreview = false;

                        panelUusiPeli.Visible = false;

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Tallentamisessa tapahtui virhe.");
                        return;
                    }

                    MessageBox.Show("Pelin tiedot tallennettu onnistuneesti.");

                    break;

            }

        }

        private void lbLopetaPeliTallentamatta_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Haluatko varmasti lopettaa pelin?", "", MessageBoxButtons.YesNo);

            if (dg == DialogResult.No)
            {
                return;
            }

            switch (l)
            {
                case Laji.latka:

                    // poistetaan kaikki tapahtumat
                    while (uusiLatkaPeli.EkaEra.Count > 0)
                    {
                        PeliTapahtuma.PoistaTapahtuma(uusiLatkaPeli.EkaEra, 0, uusiLatkaPeli);
                    }

                    while (uusiLatkaPeli.TokaEra.Count > 0)
                    {
                        PeliTapahtuma.PoistaTapahtuma(uusiLatkaPeli.TokaEra, 0, uusiLatkaPeli);
                    }

                    while (uusiLatkaPeli.KolmasEra.Count > 0)
                    {
                        PeliTapahtuma.PoistaTapahtuma(uusiLatkaPeli.KolmasEra, 0, uusiLatkaPeli);
                    }

                    // tyhjennetään peli
                    uusiLatkaPeli = null;

                    // lopetetaan peli
                    LatkapeliKaynnissa = false;

                    break;

                case Laji.futis:

                    // poistetaan kaikki tapahtumat
                    while (uusiFutisPeli.EkaPuoliaika.Count > 0)
                    {
                        PeliTapahtuma.PoistaTapahtuma(uusiFutisPeli.EkaPuoliaika, 0, uusiFutisPeli);
                    }

                    while (uusiFutisPeli.TokaPuoliaika.Count > 0)
                    {
                        PeliTapahtuma.PoistaTapahtuma(uusiFutisPeli.TokaPuoliaika, 0, uusiFutisPeli);
                    }

                    // tyhjennetään peli
                    uusiFutisPeli = null;

                    // lopetetaan peli
                    futispeliKaynnissa = false;

                    break;

                case Laji.saba:

                    // poistetaan kaikki tapahtumat
                    while (uusiSabaPeli.EkaEra.Count > 0)
                    {
                        PeliTapahtuma.PoistaTapahtuma(uusiSabaPeli.EkaEra, 0, uusiSabaPeli);
                    }

                    while (uusiSabaPeli.TokaEra.Count > 0)
                    {
                        PeliTapahtuma.PoistaTapahtuma(uusiSabaPeli.TokaEra, 0, uusiSabaPeli);
                    }

                    while (uusiSabaPeli.KolmasEra.Count > 0)
                    {
                        PeliTapahtuma.PoistaTapahtuma(uusiSabaPeli.KolmasEra, 0, uusiSabaPeli);
                    }

                    // tyhjennetään peli
                    uusiSabaPeli = null;

                    // lopetetaan peli
                    sabapeliKaynnissa = false;

                    break;
            }

            // poistetaan keypressevent
            this.KeyPress -= new KeyPressEventHandler(Keypressed);
            this.KeyPreview = false;

            // poistetaan pelin paneeli 
            panelUusiPeli.Visible = false;
        }

        #endregion

        #region Lätkä

        // lista lätkäjoukkueista
        public List<Team> latkajoukkueet = new List<Team>();

        // lista lätkäpeleistä
        public List<LatkaPeli> latkapelit = new List<LatkaPeli>();

        // lätkäpelin aikainen objekti
        LatkaPeli uusiLatkaPeli;

        public bool LatkapeliKaynnissa = false;

        // aloittaa uuden pelin lätkää
        private void btnUusiPeliLatka_Click(object sender, EventArgs e)
        {

            if (muokattu == true || LatkapeliKaynnissa == true || futispeliKaynnissa == true || sabapeliKaynnissa == true)
            {
                DialogResult d = MessageBox.Show("Haluatko varmasti poistua?\rTallentamattomat tiedot häviävät.", "", MessageBoxButtons.OKCancel);
                if (d == DialogResult.Cancel)
                {
                    return;
                }

                // jos peli käynnissä ja silti tahdotaan jatkaa, lopeteaan edellinen peli
                if (LatkapeliKaynnissa == true)
                {
                    lbLopetaPeliTallentamatta_Click(lbLopetaPeliTallentamatta, e);
                    LatkapeliKaynnissa = false;
                }
                else if (futispeliKaynnissa == true)
                {
                    lbLopetaPeliTallentamatta_Click(lbLopetaPeliTallentamatta, e);
                    futispeliKaynnissa = false;
                }
                else if (futispeliKaynnissa == true)
                {
                    lbLopetaPeliTallentamatta_Click(lbLopetaPeliTallentamatta, e);
                    sabapeliKaynnissa = false;
                }

            }

            laji = "latka";

            l = Laji.latka;

            //luodaan uusi peli
            uusiLatkaPeli = new LatkaPeli
            {

                EkaEra = new BindingList<PeliTapahtuma>(),
                TokaEra = new BindingList<PeliTapahtuma>(),
                KolmasEra = new BindingList<PeliTapahtuma>(),

                // merkitään pelin alkuaika
                paiv = DateTime.Now.ToShortDateString(),
                alkaik = DateTime.Now.ToShortTimeString(),

                kotijoukkueenMV = string.Empty,
                vierasjoukkueenMV = string.Empty
            };

            // kysytään käyttäjältä mitkä joukkueet pelaavat vastakkain
            using (var f = new frmJoukkueenValinta(latkajoukkueet, true))
            {

                try
                {
                    if (latkajoukkueet.Count < 2)
                    {
                        throw new Exception("Peliä ei voida aloittaa, mikäli joukkueita ei ole vähintään kaksi.");
                    }

                    var result = f.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        uusiLatkaPeli.kotijoukkue = f.kotijoukkue.Uusijoukkue();
                        uusiLatkaPeli.vierasjoukkue = f.vierasjoukkue.Uusijoukkue();
                    }
                    else
                    {
                        LatkapeliKaynnissa = false;
                        return;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Uutta peliä ei voitu aloittaa.\r");

                    LatkapeliKaynnissa = false;
                    return;
                }
            }

            // kysytään käyttäjältä pelaavat maalivahdit
            frmMVValinta frm = new frmMVValinta(uusiLatkaPeli);
            frm.ShowDialog();

            if (uusiLatkaPeli.kotiMV == null || uusiLatkaPeli.vierasMV == null)
            {
                return;
            }

            LatkapeliKaynnissa = true;

            uusiLatkaPeli.kotijoukkueenMaalit = 0;
            uusiLatkaPeli.vierasjoukkueenMaalit = 0;

            // listään keypresseventti välilyönnille
            this.KeyPress += new KeyPressEventHandler(Keypressed);
            this.KeyPreview = true;

            // tuodaan uuden pelin paneli näkyviin
            panelUusiPeli.Visible = true;
            panelRosterManagement.Visible = false;
            panelTilastot.Visible = false;

            // piirretään kenttä
            pBoxKentta.Image = Bitmap.FromFile("hockeyrink.png");

            dGVTapahtumaLista.DataSource = uusiLatkaPeli.EkaEra;

            // "nollataan" peliaika
            seconds = 00;
            minutes = 20;
            era = 1;

            // muutetaan ajastimen tekstit
            lbUPAjastinMin.Text = minutes.ToString("D2");
            lbUPAjastinSec.Text = seconds.ToString("D2");

            lbUPEra.Text = $"{era}. Erä";

            // muutetaan tekstejä

            lbUPJoukkueetJaTulos.Text = $"{uusiLatkaPeli.kotijoukkue.lyh} {uusiLatkaPeli.kotijoukkueenMaalit} - {uusiLatkaPeli.vierasjoukkueenMaalit} {uusiLatkaPeli.vierasjoukkue.lyh}";
            lbUPEra.Text = $"{era}. Erä";

            kaikkipelaajat = new List<Pelaaja>();

            foreach (Team t in latkajoukkueet)
            {
                kaikkipelaajat.AddRange(t.plrs);
            }
        }

        #endregion

        #region Futis

        // lista futisjoukkueista
        public List<Team> futisjoukkueet = new List<Team>();

        // lista futispeleistä
        public List<FutisPeli> futispelit = new List<FutisPeli>();

        // futispeliobjekti
        public FutisPeli uusiFutisPeli;

        bool futispeliKaynnissa = false;

        // avaa uuden pelin futista
        private void lbFutisPeli_Click(object sender, EventArgs e)
        {

            if (muokattu == true || LatkapeliKaynnissa == true || futispeliKaynnissa == true || sabapeliKaynnissa == true)
            {
                DialogResult d = MessageBox.Show("Haluatko varmasti poistua?\rTallentamattomat tiedot häviävät.", "", MessageBoxButtons.OKCancel);
                if (d == DialogResult.Cancel)
                {
                    return;
                }

                // jos peli käynnissä ja silti tahdotaan jatkaa, lopeteaan edellinen peli
                if (LatkapeliKaynnissa == true)
                {
                    lbLopetaPeliTallentamatta_Click(lbLopetaPeliTallentamatta, e);
                    LatkapeliKaynnissa = false;
                }
                else if (futispeliKaynnissa == true)
                {
                    lbLopetaPeliTallentamatta_Click(lbLopetaPeliTallentamatta, e);
                    futispeliKaynnissa = false;
                }
                else if (futispeliKaynnissa == true)
                {
                    lbLopetaPeliTallentamatta_Click(lbLopetaPeliTallentamatta, e);
                    sabapeliKaynnissa = false;
                }

            }

            laji = "futis";

            //luodaan uusi peli
            uusiFutisPeli = new FutisPeli
            {
                EkaPuoliaika = new BindingList<PeliTapahtuma>(),
                TokaPuoliaika = new BindingList<PeliTapahtuma>(),
                kotijoukkue = new Team(),
                vierasjoukkue = new Team(),

                // merkitään pelin alkuaika
                paiv = DateTime.Now.ToShortDateString(),
                alkaik = DateTime.Now.ToShortTimeString(),

                kotijoukkueenMV = string.Empty,
                vierasjoukkueenMV = string.Empty
            };

            // kysytään käyttäjältä mitkä joukkueet pelaavat vastakkain
            using (var frm = new frmJoukkueenValinta(futisjoukkueet, true))
            {

                try
                {
                    if (futisjoukkueet.Count < 2)
                    {
                        throw new Exception("Peliä ei voida aloittaa, mikäli joukkueita ei ole vähintään kaksi.");
                    }

                    var result = frm.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        uusiFutisPeli.kotijoukkue = frm.kotijoukkue;
                        uusiFutisPeli.vierasjoukkue = frm.vierasjoukkue;
                    }
                    else
                    {
                        futispeliKaynnissa = false;
                        return;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Uutta peliä ei voitu aloittaa.\r");

                    futispeliKaynnissa = false;
                    return;
                }
            }

            // kysytään käyttäjältä pelaavat maalivahdit
            frmMVValinta f = new frmMVValinta(uusiFutisPeli);
            f.ShowDialog();

            if (uusiFutisPeli.kotijoukkueenMV == string.Empty || uusiFutisPeli.vierasjoukkueenMV == string.Empty)
            {
                return;
            }

            futispeliKaynnissa = true;

            uusiFutisPeli.kotijoukkueenMaalit = 0;
            uusiFutisPeli.vierasjoukkueenMaalit = 0;

            // avataan uuden pelin paneli
            panelUusiPeli.Visible = true;
            panelRosterManagement.Visible = false;
            panelTilastot.Visible = false;

            // listään keypresseventti välilyönnille
            this.KeyPress += new KeyPressEventHandler(Keypressed);
            this.KeyPreview = true;

            // piirretään kenttä
            pBoxKentta.Image = Bitmap.FromFile("soccerfield.png");

            // nollataan peliaika          
            minutes = 0;
            seconds = 0;
            puoliaika = 1;

            // muutetaan ajastimen tekstit
            lbUPAjastinMin.Text = minutes.ToString("D2");
            lbUPAjastinSec.Text = seconds.ToString("D2");

            // muutetaan tekstejä
            lbUPEra.Text = $"{puoliaika}. puoliaika";
            lbUPJoukkueetJaTulos.Text = $"{uusiFutisPeli.kotijoukkue.lyh} {uusiFutisPeli.kotijoukkueenMaalit} - {uusiFutisPeli.vierasjoukkueenMaalit} {uusiFutisPeli.vierasjoukkue.lyh}";


            dGVTapahtumaLista.DataSource = uusiFutisPeli.EkaPuoliaika;
        }

        #endregion

        #region Säbä

        // lista säbäjoukkueista
        public List<Team> sabajoukkueet;

        // lista säbäpeleistä
        public List<SabaPeli> sabapelit;

        // säbäpeliobjekti
        public SabaPeli uusiSabaPeli;

        bool sabapeliKaynnissa = false;

        // avaa uuden pelin säbää
        private void lbSabaPeli_Click(object sender, EventArgs e)
        {
            if (muokattu == true || LatkapeliKaynnissa == true || futispeliKaynnissa == true || sabapeliKaynnissa == true)
            {
                DialogResult d = MessageBox.Show("Haluatko varmasti poistua?\rTallentamattomat tiedot häviävät.", "", MessageBoxButtons.OKCancel);
                if (d == DialogResult.Cancel)
                {
                    return;
                }

                // jos peli käynnissä ja silti tahdotaan jatkaa, lopeteaan edellinen peli
                if (LatkapeliKaynnissa == true)
                {
                    lbLopetaPeliTallentamatta_Click(lbLopetaPeliTallentamatta, e);
                    LatkapeliKaynnissa = false;
                }
                else if (futispeliKaynnissa == true)
                {
                    lbLopetaPeliTallentamatta_Click(lbLopetaPeliTallentamatta, e);
                    futispeliKaynnissa = false;
                }
                else if (futispeliKaynnissa == true)
                {
                    lbLopetaPeliTallentamatta_Click(lbLopetaPeliTallentamatta, e);
                    sabapeliKaynnissa = false;
                }

            }

            laji = "saba";

            //luodaan uusi peli
            uusiSabaPeli = new SabaPeli
            {
                EkaEra = new BindingList<PeliTapahtuma>(),
                TokaEra = new BindingList<PeliTapahtuma>(),
                KolmasEra = new BindingList<PeliTapahtuma>(),
                kotijoukkue = new Team(),
                vierasjoukkue = new Team(),

                // merkitään pelin alkuaika
                paiv = DateTime.Now.ToShortDateString(),
                alkaik = DateTime.Now.ToShortTimeString(),

                kotijoukkueenMV = string.Empty,
                vierasjoukkueenMV = string.Empty
            };

            // kysytään käyttäjältä mitkä joukkueet pelaavat vastakkain
            using (var frm = new frmJoukkueenValinta(sabajoukkueet, true))
            {

                try
                {
                    if (sabajoukkueet.Count < 2)
                    {
                        throw new Exception("Peliä ei voida aloittaa, mikäli joukkueita ei ole vähintään kaksi.");
                    }

                    var result = frm.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        uusiSabaPeli.kotijoukkue = frm.kotijoukkue;
                        uusiSabaPeli.vierasjoukkue = frm.vierasjoukkue;
                    }
                    else
                    {
                        sabapeliKaynnissa = false;
                        return;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Uutta peliä ei voitu aloittaa.\r");

                    sabapeliKaynnissa = false;
                    return;
                }
            }

            // kysytään käyttäjältä pelaavat maalivahdit
            frmMVValinta f = new frmMVValinta(uusiSabaPeli);
            f.ShowDialog();

            if (uusiLatkaPeli.kotijoukkueenMV == string.Empty || uusiLatkaPeli.vierasjoukkueenMV == string.Empty)
            {
                return;
            }

            sabapeliKaynnissa = true;

            uusiSabaPeli.kotijoukkueenMaalit = 0;
            uusiSabaPeli.vierasjoukkueenMaalit = 0;

            // avataan uuden pelin paneli
            panelUusiPeli.Visible = true;
            panelRosterManagement.Visible = false;
            panelTilastot.Visible = false;

            // listään keypresseventti välilyönnille
            this.KeyPress += new KeyPressEventHandler(Keypressed);
            this.KeyPreview = true;

            // piirretään kenttä
            pBoxKentta.Image = Bitmap.FromFile("fbrink.png");

            // nollataan peliaika          
            minutes = 0;
            seconds = 0;
            era = 1;

            // muutetaan ajastimen tekstit
            lbUPAjastinMin.Text = minutes.ToString("D2");
            lbUPAjastinSec.Text = seconds.ToString("D2");

            // muutetaan tekstejä
            lbUPEra.Text = $"{puoliaika}. puoliaika";
            lbUPJoukkueetJaTulos.Text = $"{uusiSabaPeli.kotijoukkue.lyh} {uusiSabaPeli.kotijoukkueenMaalit} - {uusiSabaPeli.vierasjoukkueenMaalit} {uusiSabaPeli.vierasjoukkue.lyh}";


            dGVTapahtumaLista.DataSource = uusiSabaPeli.EkaEra;
        }

        #endregion

        #region Rosterit

        // indeksi, jonka perusteella oikean joukkueen tiedot tuodaan eri näkymiin
        int valitunJoukkueenIndeksi;

        // indeksi, jolla valittu pelaaja on joukkueen pelaajalistassa
        int valitunPelaajanIndeksi;

        // onko rosterimuokkauksia tehty
        public bool muokattu = false;

        // avaa joukkueiden hallinnan lätkän osalta
        private void btnLatkaRosterit_Click(object sender, EventArgs e)
        {
            if (LatkapeliKaynnissa == true || futispeliKaynnissa == true || sabapeliKaynnissa == true || muokattu == true)
            {
                DialogResult dg = MessageBox.Show("Haluatko varmasti poistua?\rTallentamattomat tiedot eivät tallennu.", "", MessageBoxButtons.YesNo);
                if (dg == DialogResult.No)
                {
                    return;
                }

                // jos peli käynnissä ja silti tahdotaan jatkaa, lopeteaan edellinen peli
                if (LatkapeliKaynnissa == true)
                {
                    lbLopetaPeliTallentamatta_Click(lbLopetaPeliTallentamatta, e);
                    LatkapeliKaynnissa = false;
                }
                else if (futispeliKaynnissa == true)
                {
                    lbLopetaPeliTallentamatta_Click(lbLopetaPeliTallentamatta, e);
                    futispeliKaynnissa = false;
                }
                else if (futispeliKaynnissa == true)
                {
                    lbLopetaPeliTallentamatta_Click(lbLopetaPeliTallentamatta, e);
                    sabapeliKaynnissa = false;
                }

            }

            laji = "latka";

            panelUusiPeli.Visible = false;
            panelRosterManagement.Visible = true;
            panelTilastot.Visible = false;

            // jos puuttuva xml-tiedosto
            if (latkajoukkueet == null || latkajoukkueet.Count < 1)
            {
                latkajoukkueet = new List<Team>();
            }
            else
            {

                // avaa joukkuelistan
                lbRMJoukkue_MouseEnter(lbRMJoukkue, e);
                // painaa listaan ensimmäistä nappia
                fLPRMJoukkueControls_Click(fLPRMJoukkue.Controls[0], e);

                valitunJoukkueenIndeksi = 0;

                muokattu = false;
            }

        }

        // avaa joukkueiden hallinnan futiksen osalta
        private void lbFutisRosterit_Click(object sender, EventArgs e)
        {

            if (LatkapeliKaynnissa == true || futispeliKaynnissa == true || sabapeliKaynnissa == true || muokattu == true)
            {
                DialogResult dg = MessageBox.Show("Haluatko varmasti poistua?\rTallentamattomat tiedot eivät tallennu.", "", MessageBoxButtons.YesNo);
                if (dg == DialogResult.No)
                {
                    return;
                }

                // jos peli käynnissä ja silti tahdotaan jatkaa, lopeteaan edellinen peli
                if (LatkapeliKaynnissa == true)
                {
                    lbLopetaPeliTallentamatta_Click(lbLopetaPeliTallentamatta, e);
                    LatkapeliKaynnissa = false;
                }
                else if (futispeliKaynnissa == true)
                {
                    lbLopetaPeliTallentamatta_Click(lbLopetaPeliTallentamatta, e);
                    futispeliKaynnissa = false;
                }
                else if (futispeliKaynnissa == true)
                {
                    lbLopetaPeliTallentamatta_Click(lbLopetaPeliTallentamatta, e);
                    sabapeliKaynnissa = false;
                }
            }

            laji = "futis";

            panelUusiPeli.Visible = false;
            panelRosterManagement.Visible = true;
            panelTilastot.Visible = false;

            // jos puuttuva xml-tiedosto
            if (futisjoukkueet == null || futisjoukkueet.Count < 1)
            {
                futisjoukkueet = new List<Team>();
            }
            else
            {

                // avaa joukkuelistan
                lbRMJoukkue_MouseEnter(lbRMJoukkue, e);
                // painaa listaan ensimmäistä nappia
                fLPRMJoukkueControls_Click(fLPRMJoukkue.Controls[0], e);

                valitunJoukkueenIndeksi = 0;

                muokattu = false;
            }
        }

        // avaa joukkueiden hallinnan säbän osalta
        private void lbSabaRosterit_Click(object sender, EventArgs e)
        {
            if (LatkapeliKaynnissa == true || futispeliKaynnissa == true || sabapeliKaynnissa == true || muokattu == true)
            {
                DialogResult dg = MessageBox.Show("Haluatko varmasti poistua?\rTallentamattomat tiedot eivät tallennu.", "", MessageBoxButtons.YesNo);
                if (dg == DialogResult.No)
                {
                    return;
                }

                // jos peli käynnissä ja silti tahdotaan jatkaa, lopeteaan edellinen peli
                if (LatkapeliKaynnissa == true)
                {
                    lbLopetaPeliTallentamatta_Click(lbLopetaPeliTallentamatta, e);
                    LatkapeliKaynnissa = false;
                }
                else if (futispeliKaynnissa == true)
                {
                    lbLopetaPeliTallentamatta_Click(lbLopetaPeliTallentamatta, e);
                    futispeliKaynnissa = false;
                }
                else if (futispeliKaynnissa == true)
                {
                    lbLopetaPeliTallentamatta_Click(lbLopetaPeliTallentamatta, e);
                    sabapeliKaynnissa = false;
                }
            }

            laji = "saba";

            panelUusiPeli.Visible = false;
            panelRosterManagement.Visible = true;
            panelTilastot.Visible = false;

            // jos puuttuva xml-tiedosto
            if (sabajoukkueet == null || sabajoukkueet.Count < 1)
            {
                sabajoukkueet = new List<Team>();
            }
            else
            {

                // avaa joukkuelistan
                lbRMJoukkue_MouseEnter(lbRMJoukkue, e);
                // painaa listaan ensimmäistä nappia
                fLPRMJoukkueControls_Click(fLPRMJoukkue.Controls[0], e);

                valitunJoukkueenIndeksi = 0;

                muokattu = false;
            }
        }

        // avaa valitun pelaajan tiedot
        private void fLPRosteritPelaajatControls_Click(object sender, EventArgs e)
        {

            // jos jotain on muokattu, varmistetaan halutaanko jatkaa tallentamatta
            if (muokattu == true)
            {
                MessageBoxButtons btns = MessageBoxButtons.OKCancel;
                DialogResult dialogResult = MessageBox.Show("Haluatko varmasti poistua?\rTallentamattomat muutokset häviävät.", "", btns);
                if (dialogResult == DialogResult.Cancel)
                {
                    return;
                }

            }

            Label lbl = (Label)sender;

            fLPRMPelaajat.Visible = false;
            panelRMJoukkueenTiedot.Visible = false;
            panelRMPelaajanTiedot.Visible = true;

            switch (laji)
            {
                case "latka":

                    int i = 0;

                    try
                    {

                        foreach (Pelaaja p in latkajoukkueet[valitunJoukkueenIndeksi].plrs)
                        {
                            // haetaan pelaajan indeksi joukkueen sisältämästä listasta pelaajia
                            if ($"#{latkajoukkueet[valitunJoukkueenIndeksi].plrs[i].numero} {latkajoukkueet[valitunJoukkueenIndeksi].plrs[i].enimi} {latkajoukkueet[valitunJoukkueenIndeksi].plrs[i].snimi}" == lbl.Text)
                            {
                                valitunPelaajanIndeksi = i;
                            }
                            i++;
                        }

                        // syötetään textboxeihin pelaajan tiedot
                        tbpanelRMPelaajanENimi.Text = latkajoukkueet[valitunJoukkueenIndeksi].plrs[valitunPelaajanIndeksi].enimi;
                        tbpanelRMPelaajanSNimi.Text = latkajoukkueet[valitunJoukkueenIndeksi].plrs[valitunPelaajanIndeksi].snimi;
                        tbRMPelaajanNumero.Text = latkajoukkueet[valitunJoukkueenIndeksi].plrs[valitunPelaajanIndeksi].numero;
                        cmBxRosteritPeliPaikka.Text = latkajoukkueet[valitunJoukkueenIndeksi].plrs[valitunPelaajanIndeksi].pelipaikka;
                        muokattu = false;
                    }
                    catch (Exception)
                    {
                        return;
                    }

                    break;

                case "futis":

                    i = 0;

                    try
                    {
                        foreach (Pelaaja p in futisjoukkueet[valitunJoukkueenIndeksi].plrs)
                        {
                            // haetaan pelaajan indeksi joukkueen sisältämästä listasta pelaajia
                            if ($"#{futisjoukkueet[valitunJoukkueenIndeksi].plrs[i].numero} {futisjoukkueet[valitunJoukkueenIndeksi].plrs[i].enimi} {futisjoukkueet[valitunJoukkueenIndeksi].plrs[i].snimi}" == lbl.Text)
                            {
                                valitunPelaajanIndeksi = i;
                            }
                            i++;
                        }

                        // syötetään textboxeihin pelaajan tiedot
                        tbpanelRMPelaajanENimi.Text = futisjoukkueet[valitunJoukkueenIndeksi].plrs[valitunPelaajanIndeksi].enimi;
                        tbpanelRMPelaajanSNimi.Text = futisjoukkueet[valitunJoukkueenIndeksi].plrs[valitunPelaajanIndeksi].snimi;
                        tbRMPelaajanNumero.Text = futisjoukkueet[valitunJoukkueenIndeksi].plrs[valitunPelaajanIndeksi].numero;
                        cmBxRosteritPeliPaikka.Text = futisjoukkueet[valitunJoukkueenIndeksi].plrs[valitunPelaajanIndeksi].pelipaikka;
                        muokattu = false;
                    }
                    catch (Exception)
                    {
                        return;
                    }

                    break;

                case "saba":

                    i = 0;

                    try
                    {
                        foreach (Pelaaja p in sabajoukkueet[valitunJoukkueenIndeksi].plrs)
                        {
                            // haetaan pelaajan indeksi joukkueen sisältämästä listasta pelaajia
                            if ($"#{sabajoukkueet[valitunJoukkueenIndeksi].plrs[i].numero} {sabajoukkueet[valitunJoukkueenIndeksi].plrs[i].enimi} {latkajoukkueet[valitunJoukkueenIndeksi].plrs[i].snimi}" == lbl.Text)
                            {
                                valitunPelaajanIndeksi = i;
                            }
                            i++;
                        }

                        // syötetään textboxeihin pelaajan tiedot
                        tbpanelRMPelaajanENimi.Text = sabajoukkueet[valitunJoukkueenIndeksi].plrs[valitunPelaajanIndeksi].enimi;
                        tbpanelRMPelaajanSNimi.Text = sabajoukkueet[valitunJoukkueenIndeksi].plrs[valitunPelaajanIndeksi].snimi;
                        tbRMPelaajanNumero.Text = sabajoukkueet[valitunJoukkueenIndeksi].plrs[valitunPelaajanIndeksi].numero;
                        cmBxRosteritPeliPaikka.Text = sabajoukkueet[valitunJoukkueenIndeksi].plrs[valitunPelaajanIndeksi].pelipaikka;
                        muokattu = false;
                    }
                    catch (Exception)
                    {
                        return;
                    }

                    break;
            }
        }

        // avaa valitun joukkueen tiedot
        private void lbRMJoukkueenTiedot_Click(object sender, EventArgs e)
        {
            // jos jotain on muokattu, varmistetaan halutaanko jatkaa tallentamatta
            if (muokattu == true)
            {
                MessageBoxButtons btns = MessageBoxButtons.OKCancel;
                DialogResult dialogResult = MessageBox.Show("Haluatko varmasti poistua?\rTallentamattomat muutokset häviävät.", "", btns);
                if (dialogResult == DialogResult.Cancel)
                {
                    return;
                }
            }

            lbRMMitkaTiedot.Text = "Joukkueen tiedot";

            fLPRMPelaajat.Visible = false;
            panelRMJoukkueenTiedot.Visible = true;
            panelRMPelaajanTiedot.Visible = false;
        }

        // muuttaa booleania jos textboxeja muokataan
        private void tbRosteriMuokkaus_TextChanged(object sender, EventArgs e)
        {
            // muutetaan boolean, että näkymästä poistuttaessa tehdään varmistus
            muokattu = true;
        }

        // luo uuden joukkeen
        private void lbRMUusiJoukkue_Click(object sender, EventArgs e)
        {

            // jos jotain on muokattu, varmistetaan halutaanko poistua näkymästä
            if (muokattu == true)
            {
                MessageBoxButtons btns = MessageBoxButtons.OKCancel;
                DialogResult dialogResult = MessageBox.Show("Haluatko varmasti poistua?\rTallentamattomat muutokset häviävät.", "", btns);
                if (dialogResult == DialogResult.Cancel)
                {
                    return;
                }
            }

            switch (laji)
            {
                case "latka":

                    Team latkajoukkue = new Team
                    {
                        nimi = $"HCTeam{latkajoukkueet.Count + 1}",
                        lyh = $"HC{latkajoukkueet.Count + 1}",
                        voitot = 0,
                        tasapelit = 0,
                        haviot = 0,
                        pisteet = 0
                    };

                    latkajoukkue.plrs = new List<Pelaaja>();

                    // luodaan maalivahdit
                    for (int i = 0; i < 2; i++)
                    {
                        Pelaaja player = new Pelaaja
                        {
                            enimi = $"Etu{i + 1}",
                            snimi = $"Suku{i + 1}",
                            pelipaikka = "MV",
                            numero = $"{i + 1}",
                            team = latkajoukkue.nimi,
                            goals = 0,
                            vedot = 0,
                            syotot = 0
                        };

                        latkajoukkue.plrs.Add(player);
                    }
                    // luodaan puolustajat
                    for (int i = 2; i < 8; i++)
                    {
                        Pelaaja player = new Pelaaja
                        {
                            enimi = $"Etu{i + 1}",
                            snimi = $"Suku{i + 1}",
                            pelipaikka = "P",
                            numero = $"{i + 1}",
                            team = latkajoukkue.nimi,
                            goals = 0,
                            vedot = 0,
                            syotot = 0
                        };

                        latkajoukkue.plrs.Add(player);
                    }
                    // luodaan hyökkääjät
                    for (int i = 8; i < 20; i++)
                    {
                        Pelaaja player = new Pelaaja
                        {
                            enimi = $"Etu{i + 1}",
                            snimi = $"Suku{i + 1}",
                            pelipaikka = "H",
                            numero = $"{i + 1}",
                            team = latkajoukkue.nimi,
                            goals = 0,
                            vedot = 0,
                            syotot = 0
                        };

                        latkajoukkue.plrs.Add(player);
                    }

                    try
                    {
                        // lisätään joukkue listaan ja päivitetään xml
                        latkajoukkueet.Add(latkajoukkue);

                        XMLSerializerit.SerializeXMLLatkajoukkueet(latkajoukkueet);

                        // päivitetään napit
                        lbRMJoukkue_MouseEnter(lbRMJoukkue, e);

                        // painetaan viimeistä nappia, joka aukaisee luodun joukkueen tiedot
                        fLPRMJoukkueControls_Click(fLPRMJoukkue.Controls[latkajoukkueet.Count - 1], e);
                    }
                    catch (Exception)
                    {
                        return;
                    }

                    break;

                case "futis":

                    Team futisjoukkue = new Team
                    {
                        nimi = $"FCTeam{futisjoukkueet.Count + 1}",
                        lyh = $"FC{futisjoukkueet.Count + 1}",
                        voitot = 0,
                        tasapelit = 0,
                        haviot = 0,
                        pisteet = 0
                    };

                    futisjoukkue.plrs = new List<Pelaaja>();

                    // luodaan maalivahdit
                    for (int i = 0; i < 2; i++)
                    {
                        Pelaaja player = new Pelaaja
                        {
                            enimi = $"Etu{i + 1}",
                            snimi = $"Suku{i + 1}",
                            pelipaikka = "MV",
                            numero = $"{i + 1}",
                            team = futisjoukkue.nimi,
                            goals = 0,
                            vedot = 0,
                            syotot = 0
                        };

                        futisjoukkue.plrs.Add(player);
                    }
                    // luodaan puolustajat
                    for (int i = 2; i < 8; i++)
                    {
                        Pelaaja player = new Pelaaja
                        {
                            enimi = $"Etu{i + 1}",
                            snimi = $"Suku{i + 1}",
                            pelipaikka = "P",
                            numero = $"{i + 1}",
                            team = futisjoukkue.nimi,
                            goals = 0,
                            vedot = 0,
                            syotot = 0
                        };

                        futisjoukkue.plrs.Add(player);
                    }
                    // luodaan hyökkääjät
                    for (int i = 8; i < 16; i++)
                    {
                        Pelaaja player = new Pelaaja
                        {
                            enimi = $"Etu{i + 1}",
                            snimi = $"Suku{i + 1}",
                            pelipaikka = "H",
                            numero = $"{i + 1}",
                            team = futisjoukkue.nimi,
                            goals = 0,
                            vedot = 0,
                            syotot = 0
                        };

                        futisjoukkue.plrs.Add(player);
                    }

                    try
                    {
                        // lisätään joukkue listaan ja päivitetään xml
                        futisjoukkueet.Add(futisjoukkue);

                        XMLSerializerit.SerializeXMLFutisjoukkueet(futisjoukkueet);

                        // päivitetään napit
                        lbRMJoukkue_MouseEnter(lbRMJoukkue, e);

                        // painetaan viimeistä nappia, joka aukaisee luodun joukkueen tiedot
                        fLPRMJoukkueControls_Click(fLPRMJoukkue.Controls[futisjoukkueet.Count - 1], e);
                    }
                    catch (Exception)
                    {
                        return;
                    }

                    break;

                case "saba":

                    Team sabajoukkue = new Team
                    {
                        nimi = $"FBCTeam{sabajoukkueet.Count + 1}",
                        lyh = $"FBC{sabajoukkueet.Count + 1}",
                        voitot = 0,
                        tasapelit = 0,
                        haviot = 0,
                        pisteet = 0
                    };

                    sabajoukkue.plrs = new List<Pelaaja>();

                    // luodaan maalivahdit
                    for (int i = 0; i < 2; i++)
                    {
                        Pelaaja player = new Pelaaja
                        {
                            enimi = $"Etu{i + 1}",
                            snimi = $"Suku{i + 1}",
                            pelipaikka = "MV",
                            numero = $"{i + 1}",
                            team = sabajoukkue.nimi,
                            goals = 0,
                            vedot = 0,
                            syotot = 0
                        };

                        sabajoukkue.plrs.Add(player);
                    }
                    // luodaan puolustajat
                    for (int i = 2; i < 8; i++)
                    {
                        Pelaaja player = new Pelaaja
                        {
                            enimi = $"Etu{i + 1}",
                            snimi = $"Suku{i + 1}",
                            pelipaikka = "P",
                            numero = $"{i + 1}",
                            team = sabajoukkue.nimi,
                            goals = 0,
                            vedot = 0,
                            syotot = 0
                        };

                        sabajoukkue.plrs.Add(player);
                    }
                    // luodaan hyökkääjät
                    for (int i = 8; i < 20; i++)
                    {
                        Pelaaja player = new Pelaaja
                        {
                            enimi = $"Etu{i + 1}",
                            snimi = $"Suku{i + 1}",
                            pelipaikka = "H",
                            numero = $"{i + 1}",
                            team = sabajoukkue.nimi,
                            goals = 0,
                            vedot = 0,
                            syotot = 0
                        };

                        sabajoukkue.plrs.Add(player);
                    }

                    try
                    {
                        // lisätään joukkue listaan ja päivitetään xml
                        sabajoukkueet.Add(sabajoukkue);

                        XMLSerializerit.SerializeXMLSabajoukkueet(sabajoukkueet);

                        // päivitetään napit
                        lbRMJoukkue_MouseEnter(lbRMJoukkue, e);

                        // painetaan viimeistä nappia, joka aukaisee luodun joukkueen tiedot
                        fLPRMJoukkueControls_Click(fLPRMJoukkue.Controls[sabajoukkueet.Count - 1], e);
                    }
                    catch (Exception)
                    {
                        return;
                    }

                    break;
            }
        }

        // tallentaa joukkueiden ja pelaajien tiedot
        private void lbRMTallenna_Click(object sender, EventArgs e)
        {
            switch (laji)
            {
                case "latka":

                    // jos joukkeita ei ole, ei tallenneta mitään
                    if (latkajoukkueet == null || latkajoukkueet.Count < 1)
                    {
                        return;
                    }

                    // jos ollaan joukkueen näkymissä, tallennetaan joukkueen tiedot
                    if (panelRMJoukkueenTiedot.Visible == true || fLPRMPelaajat.Visible == true)
                    {

                        // tarkastetaan, onko käyttäjä jättämässä jonkun kentän tyhjäksi, eikä anneta tallentaa tyhjiä tietoja

                        foreach (Control tb in panelRMJoukkueenTiedot.Controls.OfType<TextBoxBase>())
                        {
                            if (string.IsNullOrEmpty(tb.Text.ToString().Replace(" ", "")))
                            {
                                errorProvider1.SetError(tb, "Et voi jättää kenttää tyhjäksi");
                                return;
                            }

                            errorProvider1.SetError(tb, (string)null);
                        }

                        try
                        {
                            foreach (Pelaaja p in latkajoukkueet[valitunJoukkueenIndeksi].plrs)
                            {
                                // päivitetään pelaajan joukkue
                                p.team = tbRMJoukkueenNimi.Text.ToString();
                            }

                            // päivitetään joukkueen tiedot
                            latkajoukkueet[valitunJoukkueenIndeksi].nimi = tbRMJoukkueenNimi.Text.ToString();
                            latkajoukkueet[valitunJoukkueenIndeksi].lyh = tbRMJoukkueenLyhenne.Text.ToString();
                            lbRMJoukkue.Text = tbRMJoukkueenNimi.Text.ToString();

                            //päivitetään xml
                            XMLSerializerit.SerializeXMLLatkajoukkueet(latkajoukkueet);

                            muokattu = false;
                        }
                        catch (Exception)
                        {
                            return;
                        }

                        


                    }
                    else if (panelRMPelaajanTiedot.Visible == true)
                    {

                        // tarkastetaan, onko käyttäjä jättämässä jonkun kentän tyhjäksi, eikä anneta tallentaa tyhjiä tietoja

                        foreach (Control tb in panelRMPelaajanTiedot.Controls.OfType<TextBoxBase>())
                        {
                            if (string.IsNullOrEmpty(tb.Text.ToString().Replace(" ", "")))
                            {
                                errorProvider1.SetError(tb, "Et voi jättää kenttää tyhjäksi");
                                return;
                            }

                            errorProvider1.SetError(tb, (string)null);
                        }

                        try
                        {
                            // päivitetään pelaajan tiedot latkajoukkueet-listaan
                            latkajoukkueet[valitunJoukkueenIndeksi].plrs[valitunPelaajanIndeksi].enimi = tbpanelRMPelaajanENimi.Text.ToString();
                            latkajoukkueet[valitunJoukkueenIndeksi].plrs[valitunPelaajanIndeksi].snimi = tbpanelRMPelaajanSNimi.Text.ToString();
                            latkajoukkueet[valitunJoukkueenIndeksi].plrs[valitunPelaajanIndeksi].numero = tbRMPelaajanNumero.Text.ToString();
                            latkajoukkueet[valitunJoukkueenIndeksi].plrs[valitunPelaajanIndeksi].pelipaikka = cmBxRosteritPeliPaikka.Text.ToString();

                            //päivitetään xml
                            XMLSerializerit.SerializeXMLLatkajoukkueet(latkajoukkueet);
                            muokattu = false;
                        }
                        catch (Exception)
                        {
                            return;
                        }
                    }

                    MessageBox.Show("Tietojen tallennus onnistui.");

                    break;

                case "futis":

                    // jos joukkeita ei ole, ei tallenneta mitään
                    if (futisjoukkueet == null || futisjoukkueet.Count < 1)
                    {
                        return;
                    }

                    // jos ollaan joukkueen näkymissä, tallennetaan joukkueen tiedot
                    if (panelRMJoukkueenTiedot.Visible == true || fLPRMPelaajat.Visible == true)
                    {

                        // tarkastetaan, onko käyttäjä jättämässä jonkun kentän tyhjäksi, eikä anneta tallentaa tyhjiä tietoja

                        foreach (Control tb in panelRMPelaajanTiedot.Controls.OfType<TextBoxBase>())
                        {
                            if (string.IsNullOrEmpty(tb.Text.ToString().Replace(" ", "")))
                            {
                                errorProvider1.SetError(tb, "Et voi jättää kenttää tyhjäksi");
                                return;
                            }

                            errorProvider1.SetError(tb, (string)null);
                        }

                        try
                        {
                            foreach (Pelaaja p in futisjoukkueet[valitunJoukkueenIndeksi].plrs)
                            {
                                // päivitetään pelaajan joukkue
                                p.team = tbRMJoukkueenNimi.Text.ToString();
                            }

                            // päivitetään joukkueen tiedot
                            futisjoukkueet[valitunJoukkueenIndeksi].nimi = tbRMJoukkueenNimi.Text.ToString();
                            futisjoukkueet[valitunJoukkueenIndeksi].lyh = tbRMJoukkueenLyhenne.Text.ToString();
                            lbRMJoukkue.Text = tbRMJoukkueenNimi.Text.ToString();

                            //päivitetään xml
                            XMLSerializerit.SerializeXMLFutisjoukkueet(futisjoukkueet);

                            muokattu = false;
                        }
                        catch (Exception)
                        {
                            return;
                        }


                    }
                    else if (panelRMPelaajanTiedot.Visible == true)
                    {
                        // tarkastetaan, onko käyttäjä jättämässä jonkun kentän tyhjäksi, eikä anneta tallentaa tyhjiä tietoja

                        foreach(Control tb in panelRMPelaajanTiedot.Controls.OfType<TextBoxBase>())
                        {
                            if (string.IsNullOrEmpty(tb.Text.ToString().Replace(" ", "")))
                            {
                                errorProvider1.SetError(tb, "Et voi jättää kenttää tyhjäksi");
                                return;
                            }

                            errorProvider1.SetError(tb, (string)null);
                        }

                        try
                        {
                            
                            // päivitetään pelaajan tiedot latkajoukkueet-listaan
                            futisjoukkueet[valitunJoukkueenIndeksi].plrs[valitunPelaajanIndeksi].enimi = tbpanelRMPelaajanENimi.Text.ToString();
                            futisjoukkueet[valitunJoukkueenIndeksi].plrs[valitunPelaajanIndeksi].snimi = tbpanelRMPelaajanSNimi.Text.ToString();
                            futisjoukkueet[valitunJoukkueenIndeksi].plrs[valitunPelaajanIndeksi].numero = tbRMPelaajanNumero.Text.ToString();
                            futisjoukkueet[valitunJoukkueenIndeksi].plrs[valitunPelaajanIndeksi].pelipaikka = cmBxRosteritPeliPaikka.Text.ToString();

                            //päivitetään xml
                            XMLSerializerit.SerializeXMLFutisjoukkueet(futisjoukkueet);
                            muokattu = false;
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("");
                            return;
                        }
                    }

                    MessageBox.Show("Tietojen tallennus onnistui.");

                    break;

                case "saba":

                    // jos joukkeita ei ole, ei tallenneta mitään
                    if (sabajoukkueet == null || sabajoukkueet.Count < 1)
                    {
                        return;
                    }

                    // jos ollaan joukkueen näkymissä, tallennetaan joukkueen tiedot
                    if (panelRMJoukkueenTiedot.Visible == true || fLPRMPelaajat.Visible == true)
                    {

                        // tarkastetaan, onko käyttäjä jättämässä jonkun kentän tyhjäksi, eikä anneta tallentaa tyhjiä tietoja

                        foreach (Control tb in panelRMPelaajanTiedot.Controls.OfType<TextBoxBase>())
                        {
                            if (string.IsNullOrEmpty(tb.Text.ToString().Replace(" ", "")))
                            {
                                errorProvider1.SetError(tb, "Et voi jättää kenttää tyhjäksi");
                                return;
                            }

                            errorProvider1.SetError(tb, (string)null);
                        }

                        try
                        {
                            foreach (Pelaaja p in sabajoukkueet[valitunJoukkueenIndeksi].plrs)
                            {
                                // päivitetään pelaajan joukkue
                                p.team = tbRMJoukkueenNimi.Text.ToString();
                            }

                            // päivitetään joukkueen tiedot
                            sabajoukkueet[valitunJoukkueenIndeksi].nimi = tbRMJoukkueenNimi.Text.ToString();
                            sabajoukkueet[valitunJoukkueenIndeksi].lyh = tbRMJoukkueenLyhenne.Text.ToString();
                            lbRMJoukkue.Text = tbRMJoukkueenNimi.Text.ToString();

                            //päivitetään xml
                            XMLSerializerit.SerializeXMLSabajoukkueet(sabajoukkueet);

                            muokattu = false;
                        }
                        catch (Exception)
                        {
                            return;
                        }


                    }
                    else if (panelRMPelaajanTiedot.Visible == true)
                    {
                        // tarkastetaan, onko käyttäjä jättämässä jonkun kentän tyhjäksi, eikä anneta tallentaa tyhjiä tietoja

                        foreach (Control tb in panelRMPelaajanTiedot.Controls.OfType<TextBoxBase>())
                        {
                            if (string.IsNullOrEmpty(tb.Text.ToString().Replace(" ", "")))
                            {
                                errorProvider1.SetError(tb, "Et voi jättää kenttää tyhjäksi");
                                return;
                            }

                            errorProvider1.SetError(tb, (string)null);
                        }

                        try
                        {
                            // päivitetään pelaajan tiedot latkajoukkueet-listaan
                            sabajoukkueet[valitunJoukkueenIndeksi].plrs[valitunPelaajanIndeksi].enimi = tbpanelRMPelaajanENimi.Text.ToString();
                            sabajoukkueet[valitunJoukkueenIndeksi].plrs[valitunPelaajanIndeksi].snimi = tbpanelRMPelaajanSNimi.Text.ToString();
                            sabajoukkueet[valitunJoukkueenIndeksi].plrs[valitunPelaajanIndeksi].numero = tbRMPelaajanNumero.Text.ToString();
                            sabajoukkueet[valitunJoukkueenIndeksi].plrs[valitunPelaajanIndeksi].pelipaikka = cmBxRosteritPeliPaikka.Text.ToString();

                            //päivitetään xml
                            XMLSerializerit.SerializeXMLSabajoukkueet(sabajoukkueet);
                            muokattu = false;
                        }
                        catch (Exception)
                        {
                            return;
                        }

                        MessageBox.Show("Tietojen tallennus onnistui.");
                    }
                    break;
            }
        }

        // poistaa joukkueen, jonka tiedoissa ollaan
        private void lbRMPoistaJoukkue_Click(object sender, EventArgs e)
        {
            switch (laji)
            {
                case "latka":

                    if (valitunJoukkueenIndeksi > -1 && latkajoukkueet.Count > 1)
                    {

                        DialogResult dr = MessageBox.Show("Haluatko varmasti poistaa valitun joukkueen?", "", MessageBoxButtons.OKCancel);

                        if (dr == DialogResult.OK)
                        {
                            try
                            {
                                // poistetaan joukkue listasta
                                latkajoukkueet.RemoveAt(valitunJoukkueenIndeksi);

                                // avaa ja päivittää joukkuelistan valikossa
                                lbRMJoukkue_MouseEnter(lbRMJoukkue, e);
                                // painaa listaan ensimmäistä nappia
                                fLPRMJoukkueControls_Click(fLPRMJoukkue.Controls[0], e);
                                //päivitetään xml
                                XMLSerializerit.SerializeXMLLatkajoukkueet(latkajoukkueet);
                            }
                            catch (Exception)
                            {
                                return;
                            }
                        }
                    }
                    else if (valitunJoukkueenIndeksi > -1 && latkajoukkueet.Count == 1)
                    {
                        MessageBox.Show("Et voi poistaa ainoaa joukkuetta.");
                        return;
                    }
                    break;

                case "futis":

                    if (valitunJoukkueenIndeksi > -1 && futisjoukkueet.Count > 1)
                    {

                        DialogResult dr = MessageBox.Show("Haluatko varmasti poistaa valitun joukkueen?", "", MessageBoxButtons.OKCancel);

                        if (dr == DialogResult.OK)
                        {
                            try
                            {
                                futisjoukkueet.RemoveAt(valitunJoukkueenIndeksi);

                                // avaa ja päivittää joukkuelistan valikossa
                                lbRMJoukkue_MouseEnter(lbRMJoukkue, e);
                                // painaa listaan ensimmäistä nappia
                                fLPRMJoukkueControls_Click(fLPRMJoukkue.Controls[0], e);
                                //päivitetään xml
                                XMLSerializerit.SerializeXMLFutisjoukkueet(futisjoukkueet);
                            }
                            catch (Exception)
                            {
                                return;
                            }
                        }
                    }
                    else if (valitunJoukkueenIndeksi > -1 && futisjoukkueet.Count == 1)
                    {
                        MessageBox.Show("Et voi poistaa ainoaa joukkuetta.");
                        return;
                    }
                    break;

                case "saba":

                    if (valitunJoukkueenIndeksi > -1 && sabajoukkueet.Count > 1)
                    {

                        DialogResult dr = MessageBox.Show("Haluatko varmasti poistaa valitun joukkueen?", "", MessageBoxButtons.OKCancel);

                        if (dr == DialogResult.OK)
                        {

                            try
                            {
                                // poistetaan joukkue listasta
                                sabajoukkueet.RemoveAt(valitunJoukkueenIndeksi);

                                // avaa ja päivittää joukkuelistan valikossa
                                lbRMJoukkue_MouseEnter(lbRMJoukkue, e);
                                // painaa listaan ensimmäistä nappia
                                fLPRMJoukkueControls_Click(fLPRMJoukkue.Controls[0], e);
                                //päivitetään xml
                                XMLSerializerit.SerializeXMLSabajoukkueet(sabajoukkueet);
                            }
                            catch (Exception)
                            {
                                return;
                            }
                            
                        }
                    }
                    else if (valitunJoukkueenIndeksi > -1 && sabajoukkueet.Count == 1)
                    {
                        MessageBox.Show("Et voi poistaa ainoaa joukkuetta.");
                        return;
                    }
                    break;
            }

            MessageBox.Show("Joukkue on nyt poistettu");
        }

        // muuttaa valitun joukkueen indeksin ja avaa valitun joukkueen tiedot
        private void fLPRMJoukkueControls_Click(object sender, EventArgs e)
        {

            if (muokattu == true)
            {
                MessageBoxButtons btns = MessageBoxButtons.OKCancel;
                DialogResult dialogResult = MessageBox.Show("Haluatko varmasti poistua?\rTallentamattomat muutokset häviävät.", "", btns);
                if (dialogResult == DialogResult.Cancel)
                {
                    return;
                }
            }

            Control ctrl = (Control)sender;
            // valitun joukkueen indeksi otetaan flowlayoutpanelin controllin indeksistä, monesko nappi panelissa onS
            valitunJoukkueenIndeksi = fLPRMJoukkue.Controls.GetChildIndex(ctrl);

            switch (laji)
            {
                case "latka":

                    try
                    {
                        // tuodaan joukkueen tiedot näkyviin
                        lbRMJoukkue.Text = latkajoukkueet[valitunJoukkueenIndeksi].nimi;
                        tbRMJoukkueenNimi.Text = latkajoukkueet[valitunJoukkueenIndeksi].nimi;
                        tbRMJoukkueenLyhenne.Text = latkajoukkueet[valitunJoukkueenIndeksi].lyh;
                    }
                    catch(Exception)
                    {
                        return;
                    }
                    

                    break;

                case "futis":

                    try
                    {
                        // tuodaan joukkueen tiedot näkyviin
                        lbRMJoukkue.Text = futisjoukkueet[valitunJoukkueenIndeksi].nimi;
                        tbRMJoukkueenNimi.Text = futisjoukkueet[valitunJoukkueenIndeksi].nimi;
                        tbRMJoukkueenLyhenne.Text = futisjoukkueet[valitunJoukkueenIndeksi].lyh;
                    }
                    catch (Exception)
                    {
                        return;
                    }

                    break;

                case "saba":

                    try
                    {
                        // tuodaan joukkueen tiedot näkyviin
                        lbRMJoukkue.Text = sabajoukkueet[valitunJoukkueenIndeksi].nimi;
                        tbRMJoukkueenNimi.Text = sabajoukkueet[valitunJoukkueenIndeksi].nimi;
                        tbRMJoukkueenLyhenne.Text = sabajoukkueet[valitunJoukkueenIndeksi].lyh;
                    }
                    catch (Exception)
                    {
                        return;
                    }

                    break;
            }

            muokattu = false;

            // piilotetaan valikot ja avataan joukkueen tiedot -näkymä
            fLPRMJoukkue.Visible = false;
            fLPRMPelaajat.Visible = false;
            panelRMJoukkueenTiedot.Visible = true;
            panelRMPelaajanTiedot.Visible = false;
            lbRMMitkaTiedot.Text = "Joukkueen tiedot";
        }

        // siirtää pelaajan käyttäjän valitsemaan joukkueeseen
        private void lbRMSiirraPelaaja_Click(object sender, EventArgs e)
        {
            try
            {
                switch (laji)
                {
                    case "latka":
                        using (var f = new frmJoukkueenValinta(latkajoukkueet, false))
                        {
                            var result = f.ShowDialog();
                            if (result == DialogResult.OK)
                            {
                                int indeksi = f.valittujoukkue;

                                // muokataan pelaajan tietoihin uusi joukkue
                                latkajoukkueet[valitunJoukkueenIndeksi].plrs[valitunPelaajanIndeksi].team = latkajoukkueet[indeksi].nimi;

                                // lisätään pelaaja uuteen joukkueeseen
                                latkajoukkueet[indeksi].plrs.Add(latkajoukkueet[valitunJoukkueenIndeksi].plrs[valitunPelaajanIndeksi]);

                                // poistetaan pelaaja vanhasta
                                latkajoukkueet[valitunJoukkueenIndeksi].plrs.RemoveAt(valitunPelaajanIndeksi);

                                //päivitetään xml
                                XMLSerializerit.SerializeXMLLatkajoukkueet(latkajoukkueet);

                                // mennään valitun joukkueen tietoihin
                                fLPRMJoukkueControls_Click(fLPRMJoukkue.Controls[f.valittujoukkue], e);
                            }
                        }
                        break;

                    case "futis":
                        using (var f = new frmJoukkueenValinta(futisjoukkueet, false))
                        {
                            var result = f.ShowDialog();
                            if (result == DialogResult.OK)
                            {
                                int indeksi = f.valittujoukkue;

                                // muokataan pelaajan tietoihin uusi joukkue
                                futisjoukkueet[valitunJoukkueenIndeksi].plrs[valitunPelaajanIndeksi].team = futisjoukkueet[indeksi].nimi;

                                // lisätään pelaaja uuteen joukkueeseen
                                futisjoukkueet[indeksi].plrs.Add(futisjoukkueet[valitunJoukkueenIndeksi].plrs[valitunPelaajanIndeksi]);

                                // poistetaan pelaaja vanhasta
                                futisjoukkueet[valitunJoukkueenIndeksi].plrs.RemoveAt(valitunPelaajanIndeksi);

                                // mennään valitun joukkueen tietoihin
                                fLPRMJoukkueControls_Click(fLPRMJoukkue.Controls[f.valittujoukkue], e);

                                //päivitetään xml
                                XMLSerializerit.SerializeXMLFutisjoukkueet(futisjoukkueet);
                            }
                        }
                        break;

                    case "saba":
                        using (var f = new frmJoukkueenValinta(sabajoukkueet, false))
                        {
                            var result = f.ShowDialog();
                            if (result == DialogResult.OK)
                            {
                                int indeksi = f.valittujoukkue;

                                // muokataan pelaajan tietoihin uusi joukkue
                                sabajoukkueet[valitunJoukkueenIndeksi].plrs[valitunPelaajanIndeksi].team = latkajoukkueet[indeksi].nimi;

                                // lisätään pelaaja uuteen joukkueeseen
                                sabajoukkueet[indeksi].plrs.Add(sabajoukkueet[valitunJoukkueenIndeksi].plrs[valitunPelaajanIndeksi]);

                                // poistetaan pelaaja vanhasta
                                sabajoukkueet[valitunJoukkueenIndeksi].plrs.RemoveAt(valitunPelaajanIndeksi);

                                //päivitetään xml
                                XMLSerializerit.SerializeXMLFutisjoukkueet(futisjoukkueet);

                                // mennään valitun joukkueen tietoihin
                                fLPRMJoukkueControls_Click(fLPRMJoukkue.Controls[f.valittujoukkue], e);
                            }
                        }
                        break;
                }
            }
            catch (Exception)
            {
                return;
            }

            MessageBox.Show("Pelaajan on nyt siirretty uuteen joukkueeseen.");
        }

        // poistaa valitun pelaajan
        private void lbRMPoistaPelaaja_Click(object sender, EventArgs e)
        {

            DialogResult dr = MessageBox.Show("Haluatko varmasti poistaa pelaajan?\rPoistetun pelaajan tilastoja ei voida palauttaa.", "", MessageBoxButtons.OKCancel);

            if (dr == DialogResult.Cancel)
            {
                return;
            }

            try
            {

                switch (laji)
                {
                    case "latka":

                        if (valitunPelaajanIndeksi > -1 && latkajoukkueet[valitunJoukkueenIndeksi].plrs.Count > 1)
                        {
                            // poistetaan pelaaja listasta
                            latkajoukkueet[valitunJoukkueenIndeksi].plrs.RemoveAt(valitunPelaajanIndeksi);

                            // palaa takaisin joukkueen tietoihin
                            lbRMJoukkueenTiedot_Click(lbRMJoukkueenTiedot, e);

                            //päivitetään xml
                            XMLSerializerit.SerializeXMLLatkajoukkueet(latkajoukkueet);
                        }
                        else if (valitunPelaajanIndeksi > -1 && latkajoukkueet[valitunJoukkueenIndeksi].plrs.Count == 1)
                        {
                            MessageBox.Show("Et voi poistaa joukkueen ainoaa pelaajaa.");
                            return;
                        }

                        break;

                    case "futis":

                        if (valitunPelaajanIndeksi > -1 && futisjoukkueet[valitunJoukkueenIndeksi].plrs.Count > 1)
                        {
                            // poistetaan pelaaja listasta
                            futisjoukkueet[valitunJoukkueenIndeksi].plrs.RemoveAt(valitunPelaajanIndeksi);

                            // palaa takaisin joukkueen tietoihin
                            lbRMJoukkueenTiedot_Click(lbRMJoukkueenTiedot, e);

                            //päivitetään xml
                            XMLSerializerit.SerializeXMLFutisjoukkueet(futisjoukkueet);
                        }
                        else if (valitunPelaajanIndeksi > -1 && futisjoukkueet[valitunJoukkueenIndeksi].plrs.Count == 1)
                        {
                            MessageBox.Show("Et voi poistaa joukkueen ainoaa pelaajaa.");
                            return;
                        }
                        break;

                    case "saba":

                        if (valitunPelaajanIndeksi > -1 && sabajoukkueet[valitunJoukkueenIndeksi].plrs.Count > 1)
                        {
                            // poistetaan pelaaja listasta
                            sabajoukkueet[valitunJoukkueenIndeksi].plrs.RemoveAt(valitunPelaajanIndeksi);

                            // palaa takaisin joukkueen tietoihin
                            lbRMJoukkueenTiedot_Click(lbRMJoukkueenTiedot, e);

                            //päivitetään xml
                            XMLSerializerit.SerializeXMLSabajoukkueet(sabajoukkueet);
                        }
                        else if (valitunPelaajanIndeksi > -1 && sabajoukkueet[valitunJoukkueenIndeksi].plrs.Count == 1)
                        {
                            MessageBox.Show("Et voi poistaa joukkueen ainoaa pelaajaa.");
                            return;
                        }
                        break;
                }
            }
            catch (Exception)
            {
                return;
            }

            MessageBox.Show("Pelaaja on nyt poistettu.");
        }

        // luo uuden pelaajan joukkueeseen, joka on valittuna
        private void lbRMUusiPelaaja_Click(object sender, EventArgs e)
        {
            // jos jotain on muokattu, varmistetaan halutaanko poistua näkymästä
            if (muokattu == true)
            {
                MessageBoxButtons btns = MessageBoxButtons.OKCancel;
                DialogResult dialogResult = MessageBox.Show("Haluatko varmasti poistua?\rTallentamattomat muutokset häviävät.", "", btns);
                if (dialogResult == DialogResult.Cancel)
                {
                    return;
                }
            }

            try
            {
                // luodaan uusi pelaaja
                Pelaaja player = new Pelaaja
                {
                    enimi = "Etu",
                    snimi = "Suku",
                    pelipaikka = "H",
                    numero = "0",
                    goals = 0,
                    vedot = 0,
                    syotot = 0,
                    pisteet = 0,
                    pelatutPelit = 0,
                    voitot = 0,
                    havityt = 0,
                    torjunnat = 0,
                    paastetyt = 0,
                };

                switch (laji)
                {
                    case "latka":

                        player.team = latkajoukkueet[valitunJoukkueenIndeksi].nimi;

                        // lisätään pelaaja joukkueeseen
                        latkajoukkueet[valitunJoukkueenIndeksi].plrs.Add(player);

                        // muutetaan indeksi
                        valitunPelaajanIndeksi = latkajoukkueet[valitunJoukkueenIndeksi].plrs.Count - 1;

                        //päivitetään xml
                        XMLSerializerit.SerializeXMLLatkajoukkueet(latkajoukkueet);

                        break;

                    case "futis":

                        player.team = futisjoukkueet[valitunJoukkueenIndeksi].nimi;

                        // lisätään pelaaja joukkueeseen
                        futisjoukkueet[valitunJoukkueenIndeksi].plrs.Add(player);

                        // muutetaan indeksi
                        valitunPelaajanIndeksi = futisjoukkueet[valitunJoukkueenIndeksi].plrs.Count - 1;

                        //päivitetään xml
                        XMLSerializerit.SerializeXMLFutisjoukkueet(futisjoukkueet);

                        break;

                    case "saba":

                        player.team = sabajoukkueet[valitunJoukkueenIndeksi].nimi;

                        // lisätään pelaaja joukkueeseen
                        sabajoukkueet[valitunJoukkueenIndeksi].plrs.Add(player);

                        // muutetaan indeksi
                        valitunPelaajanIndeksi = sabajoukkueet[valitunJoukkueenIndeksi].plrs.Count - 1;

                        //päivitetään xml
                        XMLSerializerit.SerializeXMLSabajoukkueet(sabajoukkueet);

                        break;
                }

                // laitetaan luodun pelaajan tiedot näkyviin
                tbpanelRMPelaajanENimi.Text = player.enimi;
                tbpanelRMPelaajanSNimi.Text = player.snimi;
                tbRMPelaajanNumero.Text = player.numero;
                cmBxRosteritPeliPaikka.Text = player.pelipaikka;

                fLPRMPelaajat.Visible = false;
                panelRMJoukkueenTiedot.Visible = false;
                panelRMPelaajanTiedot.Visible = true;
            }
            catch (Exception)
            {
                return;
            }


        }

        #endregion

        #region mouseEntereitäRM
        private void lbRMJoukkue_MouseEnter(object sender, EventArgs e)
        {
            fLPRMJoukkue.Visible = true;

            fLPRMJoukkue.Controls.Clear();

            fLPRMJoukkue.Size = new Size(160, 0);

            switch (laji)
            {
                case "latka":

                    // tuodaan joukkueet listaan
                    foreach (Team joukkue in latkajoukkueet)
                    {
                        Size vanha = fLPRMJoukkue.Size;
                        vanha.Height += 58;
                        fLPRMJoukkue.Size = vanha;

                        Label lbLatkaTeam = new Label
                        {
                            Size = new Size(150, 58),
                            TextAlign = ContentAlignment.MiddleCenter,
                            Text = $"{joukkue.nimi}"
                        };
                        lbLatkaTeam.MouseEnter += new EventHandler(fLPRMJoukkueControls_MouseEnter);
                        lbLatkaTeam.Click += new EventHandler(fLPRMJoukkueControls_Click);
                        fLPRMJoukkue.Controls.Add(lbLatkaTeam);

                    }

                    break;

                case "futis":

                    // tuodaan joukkueet listaan
                    foreach (Team joukkue in futisjoukkueet)
                    {
                        Size vanha = fLPRMJoukkue.Size;
                        vanha.Height += 58;
                        fLPRMJoukkue.Size = vanha;

                        Label lbFutisTeam = new Label
                        {
                            Size = new Size(150, 58),
                            TextAlign = ContentAlignment.MiddleCenter,
                            Text = $"{joukkue.nimi}"
                        };
                        lbFutisTeam.MouseEnter += new EventHandler(fLPRMJoukkueControls_MouseEnter);
                        lbFutisTeam.Click += new EventHandler(fLPRMJoukkueControls_Click);
                        fLPRMJoukkue.Controls.Add(lbFutisTeam);
                    }

                    break;

                case "saba":

                    // tuodaan joukkueet listaan
                    foreach (Team joukkue in sabajoukkueet)
                    {
                        Size vanha = fLPRMJoukkue.Size;
                        vanha.Height += 58;
                        fLPRMJoukkue.Size = vanha;

                        Label lbSabaTeam = new Label
                        {
                            Size = new Size(150, 58),
                            TextAlign = ContentAlignment.MiddleCenter,
                            Text = $"{joukkue.nimi}"
                        };
                        lbSabaTeam.MouseEnter += new EventHandler(fLPRMJoukkueControls_MouseEnter);
                        lbSabaTeam.Click += new EventHandler(fLPRMJoukkueControls_Click);
                        fLPRMJoukkue.Controls.Add(lbSabaTeam);
                    }

                    break;
            }

            foreach (Control c in fLPRMJoukkue.Controls)
            {
                c.ForeColor = Color.White;
            }
            foreach (Control c in tPLRMPelaajanSiirtoJaPoisto.Controls)
            {
                c.ForeColor = Color.White;
            }

            lbRMJoukkue.ForeColor = Color.FromArgb(252, 134, 0);
            lbRMMitkaTiedot.ForeColor = Color.White;
        }

        private void panelRosterManagement_MouseEnter(object sender, EventArgs e)
        {
            // piilotetaan valikot
            fLPRMJoukkue.Visible = false;
            fLPRMPelaaja.Visible = false;
            panelUusiPeliLajinValinta.Visible = false;
            panelTilastotLajinValinta.Visible = false;
            panelRosteritLajinValinta.Visible = false;

            // muutetaan controllien ulkoasuja
            foreach (Control c in panelRosterManagement.Controls)
            {
                c.ForeColor = Color.White;
            }
            foreach (Control c in tableLayoutPanelValintanapit.Controls)
            {
                c.ForeColor = Color.White;
                c.BackColor = Color.FromArgb(41, 44, 51);
            }
            foreach (Control c in fLPRMPelaajat.Controls)
            {
                c.ForeColor = Color.White;
            }
            foreach (Control c in tLPRMMuokkausNapit.Controls)
            {
                c.ForeColor = Color.White;
            }
            foreach (Control c in tPLRMPelaajanSiirtoJaPoisto.Controls)
            {
                c.ForeColor = Color.White;
            }

        }

        private void fLPRMJoukkueControls_MouseEnter(object sender, EventArgs e)
        {
            Label lb = (Label)sender;

            foreach (Control c in fLPRMJoukkue.Controls)
            {
                c.ForeColor = Color.White;
            }
            foreach (Control c in tPLRMPelaajanSiirtoJaPoisto.Controls)
            {
                c.ForeColor = Color.White;
            }
            lbRMJoukkue.ForeColor = Color.White;
            lbRMMitkaTiedot.ForeColor = Color.White;

            lb.ForeColor = Color.FromArgb(252, 134, 0);
        }

        private void lbRMMitkaTiedot_MouseEnter(object sender, EventArgs e)
        {
            // muutetaan controllien ulkoasuja
            fLPRMPelaaja.Visible = true;
            lbRMMitkaTiedot.ForeColor = Color.FromArgb(252, 134, 0);
            lbRMJoukkue.ForeColor = Color.White;
            foreach (Control c in fLPRMPelaaja.Controls)
            {
                c.ForeColor = Color.White;
            }
            foreach (Control c in tLPRMMuokkausNapit.Controls)
            {
                c.ForeColor = Color.White;
            }
            foreach (Control c in tPLRMPelaajanSiirtoJaPoisto.Controls)
            {
                c.ForeColor = Color.White;
            }
        }

        private void lbRMJoukkueenTiedot_MouseEnter(object sender, EventArgs e)
        {
            // muutetaan controllien ulkoasuja
            Label lb = (Label)sender;
            foreach (Control c in fLPRMPelaaja.Controls)
            {
                c.ForeColor = Color.White;
            }
            foreach (Control c in tLPRMMuokkausNapit.Controls)
            {
                c.ForeColor = Color.White;
            }
            foreach (Control c in tPLRMPelaajanSiirtoJaPoisto.Controls)
            {
                c.ForeColor = Color.White;
            }

            lbRMJoukkue.ForeColor = Color.White;
            lbRMMitkaTiedot.ForeColor = Color.White;

            lb.ForeColor = Color.FromArgb(252, 134, 0);
        }

        private void tLPRMMuokkausNapit_MouseEnter(object sender, EventArgs e)
        {
            // muutetaan controllien ulkoasuja
            Control cont = (Control)sender;
            foreach (Control c in tLPRMMuokkausNapit.Controls)
            {
                c.ForeColor = Color.White;
            }
            foreach (Control c in tPLRMPelaajanSiirtoJaPoisto.Controls)
            {
                c.ForeColor = Color.White;
            }

            cont.ForeColor = Color.FromArgb(252, 134, 0);
        }

        private void fLPRMPelaajatControls_MouseEnter(object sender, EventArgs e)
        {
            fLPRMPelaaja.Visible = false;

            Control ctrl = (Control)sender;

            foreach (Control c in fLPRMPelaajat.Controls)
            {
                c.ForeColor = Color.White;
            }
            foreach (Control c in tPLRMPelaajanSiirtoJaPoisto.Controls)
            {
                c.ForeColor = Color.White;
            }

            ctrl.ForeColor = Color.FromArgb(252, 134, 0);
        }

        private void fLPRMPelaajat_MouseEnter(object sender, EventArgs e)
        {
            fLPRMPelaaja.Visible = false;
            fLPRMJoukkue.Visible = false;

            foreach (Control c in fLPRMPelaajat.Controls)
            {
                c.ForeColor = Color.White;
            }
            foreach (Control c in tPLRMPelaajanSiirtoJaPoisto.Controls)
            {
                c.ForeColor = Color.White;
            }
        }

        private void panelRMPelaajanTiedot_MouseEnter(object sender, EventArgs e)
        {
            foreach (Control c in tPLRMPelaajanSiirtoJaPoisto.Controls)
            {
                c.ForeColor = Color.White;
            }
        }

        #endregion

        #region RMHyökPuolMVvalinta
        private void lbRMHyokkaajat_Click(object sender, EventArgs e)
        {

            // jos jotain on muokattu, varmistetaan halutaanko jatkaa tallentamatta
            if (muokattu == true)
            {
                MessageBoxButtons btns = MessageBoxButtons.OKCancel;
                DialogResult dialogResult = MessageBox.Show("Haluatko varmasti poistua?\rTallentamattomat muutokset häviävät.", "", btns);
                if (dialogResult == DialogResult.Cancel)
                {
                    return;
                }
            }

            try
            {

                muokattu = false;

                fLPRMPelaajat.Visible = true;
                panelRMJoukkueenTiedot.Visible = false;
                panelRMPelaajanTiedot.Visible = false;

                lbRMMitkaTiedot.Text = "Hyökkääjät";

                fLPRMPelaajat.Controls.Clear();

                switch (laji)
                {
                    case "latka":

                        if (latkajoukkueet == null || latkajoukkueet.Count < 1)
                        {
                            throw new Exception();
                        }

                        // luodaan dynaamisesti napit pelaajille
                        foreach (Pelaaja p in latkajoukkueet[valitunJoukkueenIndeksi].plrs.Where(o => o.pelipaikka == "H"))
                        {
                            Label lb = new Label
                            {
                                Size = new Size(fLPRMPelaajat.Size.Width, 28),
                                Margin = new Padding(0, 3, 0, 3),
                                TextAlign = ContentAlignment.MiddleCenter,
                                BackColor = Color.FromArgb(21, 24, 31),
                                Text = $"#{p.numero} {p.enimi} {p.snimi}"
                            };

                            lb.MouseEnter += new EventHandler(fLPRMPelaajatControls_MouseEnter);
                            lb.Click += new EventHandler(fLPRosteritPelaajatControls_Click);
                            fLPRMPelaajat.Controls.Add(lb);

                        }

                        break;

                    case "futis":

                        if (futisjoukkueet == null || futisjoukkueet.Count < 1)
                        {
                            throw new Exception();
                        }

                        // luodaan dynaamisesti napit pelaajille
                        foreach (Pelaaja p in futisjoukkueet[valitunJoukkueenIndeksi].plrs.Where(o => o.pelipaikka == "H"))
                        {
                            Label lb = new Label
                            {
                                Size = new Size(fLPRMPelaajat.Size.Width, 28),
                                Margin = new Padding(0, 3, 0, 3),
                                TextAlign = ContentAlignment.MiddleCenter,
                                BackColor = Color.FromArgb(21, 24, 31),
                                Text = $"#{p.numero} {p.enimi} {p.snimi}"
                            };

                            lb.MouseEnter += new EventHandler(fLPRMPelaajatControls_MouseEnter);
                            lb.Click += new EventHandler(fLPRosteritPelaajatControls_Click);
                            fLPRMPelaajat.Controls.Add(lb);

                        }

                        break;

                    case "saba":

                        if (sabajoukkueet == null || sabajoukkueet.Count < 1)
                        {
                            throw new Exception();
                        }

                        // luodaan dynaamisesti napit pelaajille
                        foreach (Pelaaja p in sabajoukkueet[valitunJoukkueenIndeksi].plrs.Where(o => o.pelipaikka == "H"))
                        {
                            Label lb = new Label
                            {
                                Size = new Size(fLPRMPelaajat.Size.Width, 28),
                                Margin = new Padding(0, 3, 0, 3),
                                TextAlign = ContentAlignment.MiddleCenter,
                                BackColor = Color.FromArgb(21, 24, 31),
                                Text = $"#{p.numero} {p.enimi} {p.snimi}"
                            };

                            lb.MouseEnter += new EventHandler(fLPRMPelaajatControls_MouseEnter);
                            lb.Click += new EventHandler(fLPRosteritPelaajatControls_Click);
                            fLPRMPelaajat.Controls.Add(lb);

                        }

                        break;
                }

            }
            catch (Exception)
            {
                return;
            }

        }

        private void lbRMPuolustajat_Click(object sender, EventArgs e)
        {
            // jos jotain on muokattu, varmistetaan halutaanko jatkaa tallentamatta
            if (muokattu == true)
            {
                MessageBoxButtons btns = MessageBoxButtons.OKCancel;
                DialogResult dialogResult = MessageBox.Show("Haluatko varmasti poistua?\rTallentamattomat muutokset häviävät.", "", btns);
                if (dialogResult == DialogResult.Cancel)
                {
                    return;
                }
            }

            try
            {
                if (latkajoukkueet == null || latkajoukkueet.Count < 1)
                {
                    throw new IndexOutOfRangeException("Sinun tulee luoda ensin joukkue, että voit tarkastella sen pelaajia");
                }

                muokattu = false;

                fLPRMPelaajat.Visible = true;
                panelRMJoukkueenTiedot.Visible = false;
                panelRMPelaajanTiedot.Visible = false;

                lbRMMitkaTiedot.Text = "Puolustajat";

                fLPRMPelaajat.Controls.Clear();

                switch (laji)
                {
                    case "latka":

                        if (latkajoukkueet == null || latkajoukkueet.Count < 1)
                        {
                            throw new Exception();
                        }

                        // luodaan dynaamisesti napit pelaajille
                        foreach (Pelaaja p in latkajoukkueet[valitunJoukkueenIndeksi].plrs.Where(o => o.pelipaikka == "P"))
                        {
                            Label lb = new Label
                            {
                                Size = new Size(fLPRMPelaajat.Size.Width, 28),
                                Margin = new Padding(0, 3, 0, 3),
                                TextAlign = ContentAlignment.MiddleCenter,
                                BackColor = Color.FromArgb(21, 24, 31),
                                Text = $"#{p.numero} {p.enimi} {p.snimi}"
                            };

                            lb.MouseEnter += new EventHandler(fLPRMPelaajatControls_MouseEnter);
                            lb.Click += new EventHandler(fLPRosteritPelaajatControls_Click);
                            fLPRMPelaajat.Controls.Add(lb);
                        }

                        break;

                    case "futis":

                        if (futisjoukkueet == null || futisjoukkueet.Count < 1)
                        {
                            throw new Exception();
                        }

                        // luodaan dynaamisesti napit pelaajille
                        foreach (Pelaaja p in futisjoukkueet[valitunJoukkueenIndeksi].plrs.Where(o => o.pelipaikka == "P"))
                        {
                            Label lb = new Label
                            {
                                Size = new Size(fLPRMPelaajat.Size.Width, 28),
                                Margin = new Padding(0, 3, 0, 3),
                                TextAlign = ContentAlignment.MiddleCenter,
                                BackColor = Color.FromArgb(21, 24, 31),
                                Text = $"#{p.numero} {p.enimi} {p.snimi}"
                            };

                            lb.MouseEnter += new EventHandler(fLPRMPelaajatControls_MouseEnter);
                            lb.Click += new EventHandler(fLPRosteritPelaajatControls_Click);
                            fLPRMPelaajat.Controls.Add(lb);

                        }

                        break;

                    case "saba":

                        if (sabajoukkueet == null || sabajoukkueet.Count < 1)
                        {
                            throw new Exception();
                        }

                        // luodaan dynaamisesti napit pelaajille
                        foreach (Pelaaja p in sabajoukkueet[valitunJoukkueenIndeksi].plrs.Where(o => o.pelipaikka == "P"))
                        {
                            Label lb = new Label
                            {
                                Size = new Size(fLPRMPelaajat.Size.Width, 28),
                                Margin = new Padding(0, 3, 0, 3),
                                TextAlign = ContentAlignment.MiddleCenter,
                                BackColor = Color.FromArgb(21, 24, 31),
                                Text = $"#{p.numero} {p.enimi} {p.snimi}"
                            };

                            lb.MouseEnter += new EventHandler(fLPRMPelaajatControls_MouseEnter);
                            lb.Click += new EventHandler(fLPRosteritPelaajatControls_Click);
                            fLPRMPelaajat.Controls.Add(lb);

                        }

                        break;
                }
            }
            catch (Exception)
            {
                if (latkajoukkueet == null || latkajoukkueet.Count < 1)
                {
                    MessageBox.Show("Sinun tulee luoda ensin joukkue, että voit tarkastella sen pelaajia");
                }

                return;
            }

        }

        private void lbRMMaalivahdit_Click(object sender, EventArgs e)
        {
            // jos jotain on muokattu, varmistetaan halutaanko jatkaa tallentamatta
            if (muokattu == true)
            {
                MessageBoxButtons btns = MessageBoxButtons.OKCancel;
                DialogResult dialogResult = MessageBox.Show("Haluatko varmasti poistua?\rTallentamattomat muutokset häviävät.", "", btns);
                if (dialogResult == DialogResult.Cancel)
                {
                    return;
                }
            }

            try
            {
                if (latkajoukkueet == null || latkajoukkueet.Count < 1)
                {
                    throw new IndexOutOfRangeException("Sinun tulee luoda ensin joukkue, että voit tarkastella sen pelaajia");
                }

                muokattu = false;

                fLPRMPelaajat.Visible = true;
                panelRMJoukkueenTiedot.Visible = false;
                panelRMPelaajanTiedot.Visible = false;

                lbRMMitkaTiedot.Text = "Maalivahdit";

                fLPRMPelaajat.Controls.Clear();

                switch (laji)
                {
                    case "latka":

                        if (latkajoukkueet == null || latkajoukkueet.Count < 1)
                        {
                            throw new Exception();
                        }

                        // luodaan dynaamisesti napit pelaajille
                        foreach (Pelaaja p in latkajoukkueet[valitunJoukkueenIndeksi].plrs.Where(o => o.pelipaikka == "MV"))
                        {
                            Label lb = new Label
                            {
                                Size = new Size(fLPRMPelaajat.Size.Width, 28),
                                Margin = new Padding(0, 3, 0, 3),
                                TextAlign = ContentAlignment.MiddleCenter,
                                BackColor = Color.FromArgb(21, 24, 31),
                                Text = $"#{p.numero} {p.enimi} {p.snimi}"
                            };

                            lb.MouseEnter += new EventHandler(fLPRMPelaajatControls_MouseEnter);
                            lb.Click += new EventHandler(fLPRosteritPelaajatControls_Click);
                            fLPRMPelaajat.Controls.Add(lb);
                        }

                        break;

                    case "futis":

                        if (futisjoukkueet == null || futisjoukkueet.Count < 1)
                        {
                            throw new Exception();
                        }

                        // luodaan dynaamisesti napit pelaajille
                        foreach (Pelaaja p in futisjoukkueet[valitunJoukkueenIndeksi].plrs.Where(o => o.pelipaikka == "MV"))
                        {
                            Label lb = new Label
                            {
                                Size = new Size(fLPRMPelaajat.Size.Width, 28),
                                Margin = new Padding(0, 3, 0, 3),
                                TextAlign = ContentAlignment.MiddleCenter,
                                BackColor = Color.FromArgb(21, 24, 31),
                                Text = $"#{p.numero} {p.enimi} {p.snimi}"
                            };

                            lb.MouseEnter += new EventHandler(fLPRMPelaajatControls_MouseEnter);
                            lb.Click += new EventHandler(fLPRosteritPelaajatControls_Click);
                            fLPRMPelaajat.Controls.Add(lb);

                        }

                        break;

                    case "saba":

                        if (sabajoukkueet == null || sabajoukkueet.Count < 1)
                        {
                            throw new Exception();
                        }

                        // luodaan dynaamisesti napit pelaajille
                        foreach (Pelaaja p in sabajoukkueet[valitunJoukkueenIndeksi].plrs.Where(o => o.pelipaikka == "MV"))
                        {
                            Label lb = new Label
                            {
                                Size = new Size(fLPRMPelaajat.Size.Width, 28),
                                Margin = new Padding(0, 3, 0, 3),
                                TextAlign = ContentAlignment.MiddleCenter,
                                BackColor = Color.FromArgb(21, 24, 31),
                                Text = $"#{p.numero} {p.enimi} {p.snimi}"
                            };

                            lb.MouseEnter += new EventHandler(fLPRMPelaajatControls_MouseEnter);
                            lb.Click += new EventHandler(fLPRosteritPelaajatControls_Click);
                            fLPRMPelaajat.Controls.Add(lb);

                        }

                        break;
                }

            }
            catch (Exception)
            {
                if (latkajoukkueet == null || latkajoukkueet.Count < 1)
                {
                    MessageBox.Show("Sinun tulee luoda ensin joukkue, että voit tarkastella sen pelaajia");
                }

                return;
            }



        }

        #endregion

        #region Tilastot

        // avaa lätkätilastot
        private void lbLatkaTilastot_Click(object sender, EventArgs e)
        {

            if (LatkapeliKaynnissa == true || futispeliKaynnissa == true || sabapeliKaynnissa == true || muokattu == true)
            {
                DialogResult dg = MessageBox.Show("Haluatko varmasti poistua?\rTallentamattomat tiedot eivät tallennu.", "", MessageBoxButtons.YesNo);
                if (dg == DialogResult.No)
                {
                    return;
                }

                // jos peli käynnissä ja silti tahdotaan jatkaa, lopeteaan edellinen peli
                if (LatkapeliKaynnissa == true)
                {
                    lbLopetaPeliTallentamatta_Click(lbLopetaPeliTallentamatta, e);
                    LatkapeliKaynnissa = false;
                }
                else if (futispeliKaynnissa == true)
                {
                    lbLopetaPeliTallentamatta_Click(lbLopetaPeliTallentamatta, e);
                    futispeliKaynnissa = false;
                }
                else if (futispeliKaynnissa == true)
                {
                    lbLopetaPeliTallentamatta_Click(lbLopetaPeliTallentamatta, e);
                    sabapeliKaynnissa = false;
                }

            }

            panelUusiPeli.Visible = false;
            panelRosterManagement.Visible = false;
            panelTilastot.Visible = true;

            laji = "latka";

            // mennään sarjataulukkoon
            lbTilastotSarjataulukko_Click(lbTilastotSarjataulukko, e);
        }

        // avaa futistilastot
        private void lbFutisTilastot_Click(object sender, EventArgs e)
        {
            if (LatkapeliKaynnissa == true || futispeliKaynnissa == true || sabapeliKaynnissa == true || muokattu == true)
            {
                DialogResult dg = MessageBox.Show("Haluatko varmasti poistua?\rTallentamattomat tiedot eivät tallennu.", "", MessageBoxButtons.YesNo);
                if (dg == DialogResult.No)
                {
                    return;
                }

                // jos peli käynnissä ja silti tahdotaan jatkaa, lopeteaan edellinen peli
                if (LatkapeliKaynnissa == true)
                {
                    lbLopetaPeliTallentamatta_Click(lbLopetaPeliTallentamatta, e);
                    LatkapeliKaynnissa = false;
                }
                else if (futispeliKaynnissa == true)
                {
                    lbLopetaPeliTallentamatta_Click(lbLopetaPeliTallentamatta, e);
                    futispeliKaynnissa = false;
                }
                else if (futispeliKaynnissa == true)
                {
                    lbLopetaPeliTallentamatta_Click(lbLopetaPeliTallentamatta, e);
                    sabapeliKaynnissa = false;
                }

            }

            panelUusiPeli.Visible = false;
            panelRosterManagement.Visible = false;
            panelTilastot.Visible = true;

            laji = "futis";

            // mennään sarjataulukkoon
            lbTilastotSarjataulukko_Click(lbTilastotSarjataulukko, e);
        }

        // avaa säbätilastot
        private void lbSabaTilastot_Click(object sender, EventArgs e)
        {
            if (LatkapeliKaynnissa == true || futispeliKaynnissa == true || sabapeliKaynnissa == true || muokattu == true)
            {
                DialogResult dg = MessageBox.Show("Haluatko varmasti poistua?\rTallentamattomat tiedot eivät tallennu.", "", MessageBoxButtons.YesNo);
                if (dg == DialogResult.No)
                {
                    return;
                }

                // jos peli käynnissä ja silti tahdotaan jatkaa, lopeteaan edellinen peli
                if (LatkapeliKaynnissa == true)
                {
                    lbLopetaPeliTallentamatta_Click(lbLopetaPeliTallentamatta, e);
                    LatkapeliKaynnissa = false;
                }
                else if (futispeliKaynnissa == true)
                {
                    lbLopetaPeliTallentamatta_Click(lbLopetaPeliTallentamatta, e);
                    futispeliKaynnissa = false;
                }
                else if (futispeliKaynnissa == true)
                {
                    lbLopetaPeliTallentamatta_Click(lbLopetaPeliTallentamatta, e);
                    sabapeliKaynnissa = false;
                }

            }

            panelUusiPeli.Visible = false;
            panelRosterManagement.Visible = false;
            panelTilastot.Visible = true;

            laji = "saba";

            // mennään sarjataulukkoon
            lbTilastotSarjataulukko_Click(lbTilastotSarjataulukko, e);
        }

        // avaa datagridiin sarjataulukon
        private void lbTilastotSarjataulukko_Click(object sender, EventArgs e)
        {
            switch (laji)
            {
                case "latka":

                    try
                    {

                        if (latkajoukkueet == null || latkajoukkueet.Count < 1)
                        {
                            throw new Exception();
                        }

                        // järjestellään joukkueet pistejärjestykseen
                        BindingList<Team> joukkueetdatagridiin = new BindingList<Team>(latkajoukkueet.OrderByDescending(o => o.pisteet).ToList());

                        dGVTilastot.DataSource = joukkueetdatagridiin;
                        lbTilastotMitkaTiedot.Text = "Sarjataulukko";
                        lbTilastotPelaajienValinta.Visible = false;

                    }
                    catch (Exception)
                    {
                        return;
                    }

                    break;

                case "futis":

                    try
                    {

                        if (futisjoukkueet == null || futisjoukkueet.Count < 1)
                        {
                            throw new Exception();
                        }

                        // järjestellään joukkueet pistejärjestykseen
                        BindingList<Team> joukkueetdatagridiin = new BindingList<Team>(futisjoukkueet.OrderByDescending(o => o.pisteet).ToList());

                        dGVTilastot.DataSource = joukkueetdatagridiin;
                        lbTilastotMitkaTiedot.Text = "Sarjataulukko";
                        lbTilastotPelaajienValinta.Visible = false;

                    }
                    catch (Exception)
                    {
                        return;
                    }

                    break;

                case "saba":

                    try
                    {

                        if (sabajoukkueet == null || sabajoukkueet.Count < 1)
                        {
                            throw new Exception();
                        }

                        // järjestellään joukkueet pistejärjestykseen
                        BindingList<Team> joukkueetdatagridiin = new BindingList<Team>(sabajoukkueet.OrderByDescending(o => o.pisteet).ToList());

                        dGVTilastot.DataSource = joukkueetdatagridiin;
                        lbTilastotMitkaTiedot.Text = "Sarjataulukko";
                        lbTilastotPelaajienValinta.Visible = false;

                    }
                    catch (Exception)
                    {
                        return;
                    }

                    break;
            }




        }

        // lista kaikista pelaajista tilastoja varten
        List<Pelaaja> kaikkipelaajat;

        // avaa datagridiin pistepörssin
        private void lbTilastotPisteporssi_Click(object sender, EventArgs e)
        {

            try
            {
                if (latkajoukkueet == null || latkajoukkueet.Count < 1)
                {
                    throw new Exception();
                }

                lbTilastotMitkaTiedot.Text = "Pistepörssi";
                lbTilastotPelaajienValinta.Visible = false;

                kaikkipelaajat = new List<Pelaaja>();

                switch (laji)
                {
                    case "latka":

                        foreach (Team t in latkajoukkueet)
                        {
                            kaikkipelaajat.AddRange(t.plrs);
                        }

                        break;

                    case "futis":

                        foreach (Team t in futisjoukkueet)
                        {
                            kaikkipelaajat.AddRange(t.plrs);
                        }

                        break;

                    case "saba":

                        foreach (Team t in sabajoukkueet)
                        {
                            kaikkipelaajat.AddRange(t.plrs);
                        }

                        break;
                }



                // järjestellään pelaajalista
                List<Pelaaja> pelaajat = new List<Pelaaja>(kaikkipelaajat.OrderByDescending(o => o.pisteet).ToList());

                // filteröidään datagridiin vain kenttäpelaajat
                List<Pelaaja> n = new List<Pelaaja>(pelaajat.Where(o => o.pelipaikka.Contains("H") || o.pelipaikka.Contains("P")).ToList());

                // luodaan datagridin näkymä, jossa näkyy vain kenttäpelaajien tiedot
                dGVTilastot.DataSource = n.Select(o => new
                {
                    Pelaaja = $"#{o.numero} { o.enimi} { o.snimi}",
                    Joukkue = o.team,
                    Pelipaikka = o.pelipaikka,
                    Pelatut = o.pelatutPelit,
                    Maalit = o.goals,
                    Syötöt = o.syotot,
                    Pisteet = o.pisteet,
                    Laukaukset = o.vedot,
                    Laukaisuprosentti = Math.Round((o.vedot != 0) ? (double)o.goals * 100 / (o.vedot) : 0, 2),
                }).ToList();

                kaikkipelaajat = null;
            }
            catch (Exception)
            {
                return;
            }
        }

        // avaa datagridiin vaalivahtien tilastot voittojen määrällä järjesteltynä
        private void lbTilastotTOPMV_Click(object sender, EventArgs e)
        {

            try
            {

                // otetaan kaikki pelaajat listaan
                kaikkipelaajat = new List<Pelaaja>();

                switch (laji)
                {
                    case "latka":

                        if (latkajoukkueet == null || latkajoukkueet.Count < 1)
                        {
                            throw new Exception();
                        }

                        foreach (Team t in latkajoukkueet)
                        {
                            kaikkipelaajat.AddRange(t.plrs);
                        }

                        break;

                    case "futis":

                        if (futisjoukkueet == null || futisjoukkueet.Count < 1)
                        {
                            throw new Exception();
                        }

                        foreach (Team t in futisjoukkueet)
                        {
                            kaikkipelaajat.AddRange(t.plrs);
                        }

                        break;

                    case "saba":

                        if (sabajoukkueet == null || sabajoukkueet.Count < 1)
                        {
                            throw new Exception();
                        }

                        foreach (Team t in sabajoukkueet)
                        {
                            kaikkipelaajat.AddRange(t.plrs);
                        }

                        break;
                }
            }
            catch (Exception)
            {
                return;
            }

            try
            {
                // filteröidään datagridiin vain maalivahdit ja järjestellään voittojen mukaan
                List<Pelaaja> n = new List<Pelaaja>(kaikkipelaajat.Where(o => o.pelipaikka == "MV").OrderByDescending(o => o.voitot).ToList());

                // luodaan datagridin näkymä, jossa näkyy vain maalivahtien tilastot
                dGVTilastot.DataSource = n.Select(o => new
                {
                    Pelaaja = $"#{o.numero} { o.enimi} { o.snimi}",
                    Joukkue = o.team,
                    Pelatut = o.pelatutPelit,
                    Voitot = o.voitot,
                    Tappiot = o.havityt,
                    Torjunnat = o.torjunnat,
                    PM = o.paastetyt,
                    PMK = Math.Round((o.pelatutPelit != 0) ? ((double)o.paastetyt / o.pelatutPelit) : 0, 2),
                    TP = Math.Round((o.torjunnat != 0) ? (double)o.torjunnat * 100 / (o.paastetyt + o.torjunnat) : 0, 2),

                }).ToList();

                lbTilastotMitkaTiedot.Text = "Maalivahdit";
                lbTilastotPelaajienValinta.Visible = false;
            }
            catch (Exception)
            {
                return;
            }

            kaikkipelaajat = null;

        }

        // joukkueen valinta
        private void lbTilastotJoukkueenValinta_Click(object sender, EventArgs e)
        {
            Control ctrl = (Control)sender;

            switch (laji)
            {
                case "latka":

                    try
                    {
                        if (latkajoukkueet == null || latkajoukkueet.Count < 1)
                        {
                            throw new Exception();
                        }

                        // otetaan valitun joukkueen indeksi (vähennetään alussa olevien nappien lukumäärä)
                        valitunJoukkueenIndeksi = fLPTilastot1.Controls.GetChildIndex(ctrl) - 3;

                        lbTilastotMitkaTiedot.Text = latkajoukkueet[valitunJoukkueenIndeksi].nimi;

                        lbTilastotOttelut_Click(lbTilastotOttelut, e);

                        lbTilastotPelaajienValinta.Visible = true;

                    }
                    catch (Exception)
                    {
                        return;
                    }

                    break;

                case "futis":

                    try
                    {
                        if (futisjoukkueet == null || futisjoukkueet.Count < 1)
                        {
                            throw new Exception();
                        }

                        // otetaan valitun joukkueen indeksi (vähennetään alussa olevien nappien lukumäärä)
                        valitunJoukkueenIndeksi = fLPTilastot1.Controls.GetChildIndex(ctrl) - 3;

                        lbTilastotMitkaTiedot.Text = futisjoukkueet[valitunJoukkueenIndeksi].nimi;

                        lbTilastotOttelut_Click(lbTilastotOttelut, e);

                        lbTilastotPelaajienValinta.Visible = true;

                    }
                    catch (Exception)
                    {
                        return;
                    }

                    break;

                case "saba":

                    try
                    {
                        if (sabajoukkueet == null || sabajoukkueet.Count < 1)
                        {
                            throw new Exception();
                        }

                        // otetaan valitun joukkueen indeksi (vähennetään alussa olevien nappien lukumäärä)
                        valitunJoukkueenIndeksi = fLPTilastot1.Controls.GetChildIndex(ctrl) - 3;

                        lbTilastotMitkaTiedot.Text = sabajoukkueet[valitunJoukkueenIndeksi].nimi;

                        lbTilastotOttelut_Click(lbTilastotOttelut, e);

                        lbTilastotPelaajienValinta.Visible = true;

                    }
                    catch (Exception)
                    {
                        return;
                    }

                    break;
            }






        }

        // avaa datagridiin valitun joukkueen pelaamat pelit
        private void lbTilastotOttelut_Click(object sender, EventArgs e)
        {

            dGVTilastot.DataSource = null;

            switch (laji)
            {
                case "latka":

                    try
                    {
                        if (latkapelit == null || latkapelit.Count < 1)
                        {
                            throw new Exception();
                        }

                        if (valitunJoukkueenIndeksi > -1)
                        {
                            // valitaan pelit, joissa valittu joukkue on ollut mukana
                            List<LatkaPeli> pelit = new List<LatkaPeli>(latkapelit.Where(n => n.kotijoukkue.nimi == latkajoukkueet[valitunJoukkueenIndeksi].nimi || n.vierasjoukkue.nimi == latkajoukkueet[valitunJoukkueenIndeksi].nimi).ToList());

                            dGVTilastot.DataSource = pelit.Select(o => new
                            {
                                Päiväys = o.paiv,
                                Alkamisaika = o.alkaik.ToString(),
                                Lopetusaika = o.lopaik.ToString(),
                                Kotijoukkue = o.kotijoukkue.nimi,
                                Vierasjoukkue = o.vierasjoukkue.nimi,
                                Lopputulos = $"{o.kotijoukkueenMaalit} - {o.vierasjoukkueenMaalit}"

                            }).ToList();

                        }
                    }
                    catch (Exception)
                    {
                        return;
                    }

                    break;

                case "futis":

                    try
                    {
                        if (futispelit == null || futispelit.Count < 1)
                        {
                            throw new Exception();
                        }

                        if (valitunJoukkueenIndeksi > -1)
                        {
                            // valitaan pelit, joissa valittu joukkue on ollut mukana
                            List<FutisPeli> pelit = new List<FutisPeli>(futispelit.Where(n => n.kotijoukkue.nimi == futisjoukkueet[valitunJoukkueenIndeksi].nimi || n.vierasjoukkue.nimi == futisjoukkueet[valitunJoukkueenIndeksi].nimi).ToList());

                            dGVTilastot.DataSource = pelit.Select(o => new
                            {
                                Päiväys = o.paiv,
                                Alkamisaika = o.alkaik.ToString(),
                                Lopetusaika = o.lopaik.ToString(),
                                Kotijoukkue = o.kotijoukkue.nimi,
                                Vierasjoukkue = o.vierasjoukkue.nimi,
                                Lopputulos = $"{o.kotijoukkueenMaalit} - {o.vierasjoukkueenMaalit}"

                            }).ToList();
                        }
                    }
                    catch (Exception)
                    {
                        return;
                    }

                    break;

                case "saba":

                    try
                    {
                        if (sabapelit == null || sabapelit.Count < 1)
                        {
                            throw new Exception();
                        }

                        if (valitunJoukkueenIndeksi > -1)
                        {
                            // valitaan pelit, joissa valittu joukkue on ollut mukana
                            List<SabaPeli> pelit = new List<SabaPeli>(sabapelit.Where(n => n.kotijoukkue.nimi == sabajoukkueet[valitunJoukkueenIndeksi].nimi || n.vierasjoukkue.nimi == sabajoukkueet[valitunJoukkueenIndeksi].nimi).ToList());

                            dGVTilastot.DataSource = pelit.Select(o => new
                            {
                                Päiväys = o.paiv,
                                Alkamisaika = o.alkaik.ToString(),
                                Lopetusaika = o.lopaik.ToString(),
                                Kotijoukkue = o.kotijoukkue.nimi,
                                Vierasjoukkue = o.vierasjoukkue.nimi,
                                Lopputulos = $"{o.kotijoukkueenMaalit} - {o.vierasjoukkueenMaalit}"

                            }).ToList();
                        }
                    }
                    catch (Exception)
                    {
                        return;
                    }

                    break;
            }

            lbTilastotPelaajienValinta.Text = "Pelit";



        }

        private void lbTilastotHyokkaajat_Click(object sender, EventArgs e)
        {
            List<Pelaaja> pelaajat = new List<Pelaaja>();

            switch (laji)
            {
                case "latka":

                    try
                    {
                        // otetaan valitun joukkueen hyökkääjät ja järjestellään ne pisteiden mukaan
                        pelaajat = new List<Pelaaja>(latkajoukkueet[valitunJoukkueenIndeksi].plrs.Where(p => p.pelipaikka == "H").OrderByDescending(o => o.pisteet).ToList());

                    }
                    catch (Exception)
                    {
                        return;
                    }

                    break;

                case "futis":

                    try
                    {
                        // otetaan valitun joukkueen hyökkääjät ja järjestellään ne pisteiden mukaan
                        pelaajat = new List<Pelaaja>(futisjoukkueet[valitunJoukkueenIndeksi].plrs.Where(p => p.pelipaikka == "H").OrderByDescending(o => o.pisteet).ToList());
                    }
                    catch (Exception)
                    {
                        return;
                    }

                    break;

                case "saba":

                    try
                    {
                        // otetaan valitun joukkueen hyökkääjät ja järjestellään ne pisteiden mukaan
                        pelaajat = new List<Pelaaja>(sabajoukkueet[valitunJoukkueenIndeksi].plrs.Where(p => p.pelipaikka == "H").OrderByDescending(o => o.pisteet).ToList());
                    }
                    catch (Exception)
                    {
                        return;
                    }

                    break;
            }

            try
            {
                // luodaan datagridin näkymä, jossa näkyy vain kenttäpelaajien tiedot
                dGVTilastot.DataSource = pelaajat.Select(o => new
                {
                    Pelaaja = $"#{o.numero} { o.enimi} { o.snimi}",
                    Joukkue = o.team,
                    Pelipaikka = o.pelipaikka,
                    Pelatut = o.pelatutPelit,
                    Maalit = o.goals,
                    Syötöt = o.syotot,
                    Pisteet = o.pisteet,
                    Laukaukset = o.vedot,
                    Laukaisuprosentti = Math.Round((o.vedot != 0) ? (double)o.goals * 100 / (o.vedot) : 0, 2),
                }).ToList();

                lbTilastotPelaajienValinta.Text = "Hyökkääjät";
            }
            catch (Exception)
            {
                return;
            }


        }

        private void lbTilastotPuolustajat_Click(object sender, EventArgs e)
        {
            List<Pelaaja> pelaajat = new List<Pelaaja>();

            switch (laji)
            {
                case "latka":

                    try
                    {
                        // otetaan valitun joukkueen puolustajat ja järjestellään ne pisteiden mukaan
                        pelaajat = new List<Pelaaja>(latkajoukkueet[valitunJoukkueenIndeksi].plrs.Where(p => p.pelipaikka == "P").OrderByDescending(o => o.pisteet).ToList());

                    }
                    catch (Exception)
                    {
                        return;
                    }

                    break;

                case "futis":

                    try
                    {
                        // otetaan valitun joukkueen puolustajat ja järjestellään ne pisteiden mukaan
                        pelaajat = new List<Pelaaja>(futisjoukkueet[valitunJoukkueenIndeksi].plrs.Where(p => p.pelipaikka == "P").OrderByDescending(o => o.pisteet).ToList());
                    }
                    catch (Exception)
                    {
                        return;
                    }

                    break;

                case "saba":

                    try
                    {
                        // otetaan valitun joukkueen puolustajat ja järjestellään ne pisteiden mukaan
                        pelaajat = new List<Pelaaja>(sabajoukkueet[valitunJoukkueenIndeksi].plrs.Where(p => p.pelipaikka == "P").OrderByDescending(o => o.pisteet).ToList());
                    }
                    catch (Exception)
                    {
                        return;
                    }

                    break;
            }

            try
            {
                dGVTilastot.AutoGenerateColumns = false;

                // luodaan datagridin näkymä, jossa näkyy vain kenttäpelaajien tiedot
                dGVTilastot.DataSource = pelaajat.Select(o => new
                {
                    Pelaaja = $"#{o.numero} { o.enimi} { o.snimi}",
                    Joukkue = o.team,
                    Pelipaikka = o.pelipaikka,
                    Pelatut = o.pelatutPelit,
                    Maalit = o.goals,
                    Syötöt = o.syotot,
                    Pisteet = o.pisteet,
                    Laukaukset = o.vedot,
                    Laukaisuprosentti = Math.Round((o.vedot != 0) ? (double)o.goals * 100 / (o.vedot) : 0, 2),
                }).ToList();

                lbTilastotPelaajienValinta.Text = "Hyökkääjät";
            }
            catch (Exception)
            {
                return;
            }
        }

        private void lbTilastotMaalivahdit_Click(object sender, EventArgs e)
        {
            List<Pelaaja> pelaajat = new List<Pelaaja>();

            switch (laji)
            {
                case "latka":

                    try
                    {
                        // otetaan valitun joukkueen maalivahdit ja järjestellään ne pisteiden mukaan
                        pelaajat = new List<Pelaaja>(latkajoukkueet[valitunJoukkueenIndeksi].plrs.Where(p => p.pelipaikka == "MV").OrderByDescending(o => o.voitot).ToList());

                    }
                    catch (Exception)
                    {
                        return;
                    }

                    break;

                case "futis":

                    try
                    {
                        // otetaan valitun joukkueen maalivahdit ja järjestellään ne pisteiden mukaan
                        pelaajat = new List<Pelaaja>(futisjoukkueet[valitunJoukkueenIndeksi].plrs.Where(p => p.pelipaikka == "MV").OrderByDescending(o => o.voitot).ToList());
                    }
                    catch (Exception)
                    {
                        return;
                    }

                    break;

                case "saba":

                    try
                    {
                        // otetaan valitun joukkueen maalivahdit ja järjestellään ne pisteiden mukaan
                        pelaajat = new List<Pelaaja>(sabajoukkueet[valitunJoukkueenIndeksi].plrs.Where(p => p.pelipaikka == "MV").OrderByDescending(o => o.voitot).ToList());
                    }
                    catch (Exception)
                    {
                        return;
                    }

                    break;

            }

            try
            {
                // luodaan datagridin näkymä, jossa näkyy vain maalivahtien tilastot
                dGVTilastot.DataSource = pelaajat.Select(o => new
                {
                    Pelaaja = $"#{o.numero} { o.enimi} { o.snimi}",
                    Joukkue = o.team,
                    Pelatut = o.pelatutPelit,
                    Voitot = o.voitot,
                    Tappiot = o.havityt,
                    Torjunnat = o.torjunnat,
                    PM = o.paastetyt,
                    PMK = Math.Round((o.pelatutPelit != 0) ? ((double)o.paastetyt / o.pelatutPelit) : 0, 2),
                    TP = Math.Round((o.torjunnat != 0) ? (double)o.torjunnat * 100 / (o.paastetyt + o.torjunnat) : 0, 2),

                }).ToList();
            }
            catch (Exception)
            {
                return;
            }

        }


        #endregion

        #region MouseEntereitäTilastot
        private void TilastotControlsMouseEnters(object sender, EventArgs e)
        {
            Control ctrl = (Control)sender;


            lbTilastotMitkaTiedot.ForeColor = Color.White;
            lbTilastotPelaajienValinta.ForeColor = Color.White;

            foreach (Control c in fLPTilastot1.Controls)
            {
                c.ForeColor = Color.White;
            }

            foreach (Control c in fLPTilastot2.Controls)
            {
                c.ForeColor = Color.White;
            }

            // piilotetaan sivupalkin valikot
            panelUusiPeliLajinValinta.Visible = false;
            panelTilastotLajinValinta.Visible = false;
            panelRosteritLajinValinta.Visible = false;

            if (fLPTilastot1.Controls.Contains(ctrl))
            {
                fLPTilastot1.Visible = true;
                fLPTilastot2.Visible = false;
            }
            else if (sender == lbTilastotPelaajienValinta || fLPTilastot2.Controls.Contains(ctrl))
            {
                fLPTilastot1.Visible = false;
                fLPTilastot2.Visible = true;
            }

            ctrl.ForeColor = Color.FromArgb(252, 134, 0);
        }

        private void panelTilastot_Enter(object sender, EventArgs e)
        {

            lbTilastotMitkaTiedot.ForeColor = Color.White;
            lbTilastotPelaajienValinta.ForeColor = Color.White;

            foreach (Control c in fLPTilastot1.Controls)
            {
                c.ForeColor = Color.White;
            }

            foreach (Control c in fLPTilastot2.Controls)
            {
                c.ForeColor = Color.White;
            }

            // piilotetaan valikot
            fLPTilastot1.Visible = false;
            fLPTilastot2.Visible = false;

            panelUusiPeliLajinValinta.Visible = false;
            panelTilastotLajinValinta.Visible = false;
            panelRosteritLajinValinta.Visible = false;
        }

        private void lbTilastotMitkaTiedot_MouseEnter(object sender, EventArgs e)
        {
            fLPTilastot1.Visible = true;
            fLPTilastot2.Visible = false;

            // piilotetaan sivupalkin valikot
            panelUusiPeliLajinValinta.Visible = false;
            panelTilastotLajinValinta.Visible = false;
            panelRosteritLajinValinta.Visible = false;

            // tyhjennetään vanhat napit
            fLPTilastot1.Controls.Clear();

            fLPTilastot1.Size = new Size(150, 181);

            // lisätään sarjataulukko- ja pörssinapit
            fLPTilastot1.Controls.Add(lbTilastotSarjataulukko);
            fLPTilastot1.Controls.Add(lbTilastotPisteporssi);
            fLPTilastot1.Controls.Add(lbTilastotTOPMV);

            switch (laji)
            {
                case "latka":

                    try
                    {
                        if (latkajoukkueet != null && latkajoukkueet.Count > 0)
                        {
                            // lisätään napit joukkueille
                            foreach (Team joukkue in latkajoukkueet)
                            {
                                Size vanha = fLPTilastot1.Size;
                                vanha.Height += 58;
                                fLPTilastot1.Size = vanha;

                                Label lbLatkaTeam = new Label
                                {
                                    Size = new Size(150, 58),
                                    TextAlign = ContentAlignment.MiddleCenter,
                                    Text = $"{joukkue.nimi}"
                                };
                                lbLatkaTeam.MouseEnter += new EventHandler(TilastotControlsMouseEnters);
                                lbLatkaTeam.Click += new EventHandler(lbTilastotJoukkueenValinta_Click);
                                fLPTilastot1.Controls.Add(lbLatkaTeam);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        return;
                    }

                    break;

                case "futis":

                    try
                    {
                        if (futisjoukkueet != null && futisjoukkueet.Count > 0)
                        {
                            // lisätään napit joukkueille
                            foreach (Team joukkue in futisjoukkueet)
                            {
                                Size vanha = fLPTilastot1.Size;
                                vanha.Height += 58;
                                fLPTilastot1.Size = vanha;

                                Label lbFutisTeam = new Label
                                {
                                    Size = new Size(150, 58),
                                    TextAlign = ContentAlignment.MiddleCenter,
                                    Text = $"{joukkue.nimi}"
                                };
                                lbFutisTeam.MouseEnter += new EventHandler(TilastotControlsMouseEnters);
                                lbFutisTeam.Click += new EventHandler(lbTilastotJoukkueenValinta_Click);
                                fLPTilastot1.Controls.Add(lbFutisTeam);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        return;
                    }

                    break;

                case "saba":

                    try
                    {
                        if (sabajoukkueet != null && sabajoukkueet.Count > 0)
                        {
                            // lisätään napit joukkueille
                            foreach (Team joukkue in sabajoukkueet)
                            {
                                Size vanha = fLPTilastot1.Size;
                                vanha.Height += 58;
                                fLPTilastot1.Size = vanha;

                                Label lbSabaTeam = new Label
                                {
                                    Size = new Size(150, 58),
                                    TextAlign = ContentAlignment.MiddleCenter,
                                    Text = $"{joukkue.nimi}"
                                };
                                lbSabaTeam.MouseEnter += new EventHandler(TilastotControlsMouseEnters);
                                lbSabaTeam.Click += new EventHandler(lbTilastotJoukkueenValinta_Click);
                                fLPTilastot1.Controls.Add(lbSabaTeam);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        return;
                    }

                    break;
            }

        }

        #endregion


    }
}

