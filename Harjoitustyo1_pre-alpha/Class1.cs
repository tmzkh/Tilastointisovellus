using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harjoitustyo1
{
    class Class1
    {
        private void Tilastoi()
        {
            switch (laji)
            {
                case "latka":

                    // avataan tapahtumaformi
                    frmTapahtuma frm = new frmTapahtuma(eventti, uusiLatkaPeli.kotijoukkue, uusiLatkaPeli.vierasjoukkue, true, laji);
                    frm.ShowDialog();

                    break;

                case "futis":

                    // avataan tapahtumaformi
                    frm = new frmTapahtuma(eventti, uusiFutisPeli.kotijoukkue, uusiFutisPeli.vierasjoukkue, true, laji);
                    frm.ShowDialog();

                    break;

                case "saba":

                    // avataan tapahtumaformi
                    frm = new frmTapahtuma(eventti, uusiSabaPeli.kotijoukkue, uusiSabaPeli.vierasjoukkue, true, laji);
                    frm.ShowDialog();

                    break;
            }

            switch (eventti.happeninki)
            {
                case "":
                    return;

                case "Maali":

                    switch (laji)
                    {
                        case "latka":

                            // pysäytetään kello ja siirretään se takaisin maalintekohetkeen
                            lbUPPlay.Text = "Start (väli)";
                            timer1.Enabled = false;
                            paused = true;

                            minutes = eventti.minuutit;
                            seconds = eventti.sekuntit;

                            lbUPAjastinMin.Text = minutes.ToString("D2");
                            lbUPAjastinSec.Text = seconds.ToString("D2");

                            // Tilastoidaan maali
                            PeliTapahtuma.TilastoiMaali(eventti, uusiLatkaPeli);

                            // päivitetään tulostaulu
                            lbUPJoukkueetJaTulos.Text = $"{uusiLatkaPeli.kotijoukkue.lyh} {uusiLatkaPeli.kotijoukkueenMaalit} - {uusiLatkaPeli.vierasjoukkueenMaalit} {uusiLatkaPeli.vierasjoukkue.lyh}";
                            break;

                        case "futis":

                            // Tilastoidaan maali
                            PeliTapahtuma.TilastoiMaali(eventti, uusiFutisPeli);

                            // päivitetään tulostaulu
                            lbUPJoukkueetJaTulos.Text = $"{uusiFutisPeli.kotijoukkue.lyh} {uusiFutisPeli.kotijoukkueenMaalit} - {uusiFutisPeli.vierasjoukkueenMaalit} {uusiFutisPeli.vierasjoukkue.lyh}";
                            break;

                        case "saba":

                            // pysäytetään kello ja siirretään se takaisin maalintekohetkeen
                            lbUPPlay.Text = "Start (väli)";
                            timer1.Enabled = false;
                            paused = true;

                            minutes = eventti.minuutit;
                            seconds = eventti.sekuntit;

                            lbUPAjastinMin.Text = minutes.ToString("D2");
                            lbUPAjastinSec.Text = seconds.ToString("D2");

                            // Tilastoidaan maali
                            PeliTapahtuma.TilastoiMaali(eventti, uusiSabaPeli);

                            // päivitetään tulostaulu
                            lbUPJoukkueetJaTulos.Text = $"{uusiSabaPeli.kotijoukkue.lyh} {uusiSabaPeli.kotijoukkueenMaalit} - {uusiSabaPeli.vierasjoukkueenMaalit} {uusiSabaPeli.vierasjoukkue.lyh}";
                            break;
                    }

                    break;

                case "Laukaus":

                    switch (laji)
                    {
                        case "latka":

                            // tilastoidaan laukaus
                            PeliTapahtuma.TilastoiLaukaus(eventti, uusiLatkaPeli);
                            break;

                        case "futis":

                            // tilastoidaan laukaus
                            PeliTapahtuma.TilastoiLaukaus(eventti, uusiFutisPeli);
                            break;

                        case "saba":

                            // tilastoidaan laukaus
                            PeliTapahtuma.TilastoiLaukaus(eventti, uusiSabaPeli);
                            break;
                    }

                    break;
            }
        }

    }
}
