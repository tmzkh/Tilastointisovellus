using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harjoitustyo1_pre_alpha
{
    public class PeliTapahtuma
    {
        // tapahtuman tyyppi: maali tai laukaus
        public string happeninki;
        public string Tapahtuma
        {
            get { return happeninki; }
        }

        // joukkue, jonka pelaaja tapahtuman tuotti
        public string puoli;
        public string Joukkue
        {
            get { return puoli; }
        }

        // pelaaja, joka tapahtuman tuotti
        public string player;

        public string Player
        {
            get { return player; }
        }

        // pelaaja
        public Pelaaja pelaaja;

        // tapahtuman koordinaatit laukaisukarttaa varten
        public Point koodrinaatit;

        // tapahtuma-ajan minuutit
        public int minuutit;

        // tapahtuma-ajan sekuntit
        public int sekuntit;

        // tapahtuma-aika string-muodossa
        public string aika;

        public string Aika
        {
            get { return aika; }
        }

        // maalin mahdollinen ensimmäinen syöttäjä
        public string syottaja1;

        // maalin mahdollinen toinen syöttäjä (vain lätkässä)
        public string syottaja2;

        public Pelaaja assist1;

        public Pelaaja assist2;

        /// <summary>
        /// Tilastoi maalin joukkueelle ja pelaajalle ja syötöt mahdollisille syöttäjille ja päästetyn maalin maalivahdille.
        /// </summary>
        /// <param name="eventti"></param>
        /// <param name="uusiLatkaPeli"></param>
        public static void TilastoiMaali(PeliTapahtuma eventti, LatkaPeli uusiLatkaPeli)
        {

            // kasvatetaan maalimäärää ja tilastoidaan veto, maali ja syötöt pelaajille
            if (eventti.puoli == uusiLatkaPeli.kotijoukkue.nimi)
            {
                // lisätään joukkueen maalimäärää
                uusiLatkaPeli.kotijoukkueenMaalit += 1;

                // käydään joukkueen pelaajat läpi
                foreach (Pelaaja pl in uusiLatkaPeli.kotijoukkue.plrs)
                {
                    // jos pelaaja on sama kuin eventin pelaaja, lisätään maalien määrää ja vetojen määrää ja lasketaan pisteet
                    if (pl == eventti.pelaaja)
                    {
                        pl.vedot++;
                        pl.goals++;
                        pl.pisteet = pl.goals + pl.syotot;
                    }
                    // jos pelaaja on taas syöttäjä, lisätään syöttöjen määrää ja lasketaan pisteet
                    else if (eventti.syottaja1 == $"#{pl.numero} {pl.snimi}")
                    {
                        pl.syotot++;
                        pl.pisteet = pl.goals + pl.syotot;
                    }
                    else if (eventti.syottaja2 == $"#{pl.numero} {pl.snimi}")
                    {
                        pl.syotot++;
                        pl.pisteet = pl.goals + pl.syotot;
                    }
                }
                // tilastoidaan päästetty maali
                foreach (Pelaaja mv in uusiLatkaPeli.vierasjoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                {
                    if ($"#{mv.numero} {mv.snimi}" == uusiLatkaPeli.vierasjoukkueenMV)
                    {
                        mv.paastetyt++;
                    }
                }
            }
            // kasvatetaan maalimäärää ja tilastoidaan veto, maali ja syötöt pelaajille
            else if (eventti.puoli == uusiLatkaPeli.vierasjoukkue.nimi)
            {
                uusiLatkaPeli.vierasjoukkueenMaalit += 1;

                foreach (Pelaaja pl in uusiLatkaPeli.vierasjoukkue.plrs)
                {
                    // jos pelaaja on sama kuin eventin pelaaja, lisätään maalien määrää ja vetojen määrää ja lasketaan pisteet
                    if (pl == eventti.pelaaja)
                    {
                        pl.vedot++;
                        pl.goals++;
                        pl.pisteet = pl.goals + pl.syotot;
                    }
                    // jos pelaaja on taas syöttäjä, lisätään syöttöjen määrää ja lasketaan pisteet
                    else if (eventti.syottaja1 == $"#{pl.numero} {pl.snimi}")
                    {
                        pl.syotot++;
                        pl.pisteet = pl.goals + pl.syotot;
                    }
                    else if (eventti.syottaja2 == $"#{pl.numero} {pl.snimi}")
                    {
                        pl.syotot++;
                        pl.pisteet = pl.goals + pl.syotot;
                    }
                }
                // tilastoidaan päästetty maali
                foreach (Pelaaja mv in uusiLatkaPeli.kotijoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                {
                    // jos pelaaja on merkitty pelaavaksi maalivahdiksi, lisätään päästettyjä maaleja
                    if ($"#{mv.numero} {mv.snimi}" == uusiLatkaPeli.kotijoukkueenMV)
                    {
                        mv.paastetyt++;
                    }
                }
            }
        }

        /// <summary>
        /// Tilastoi maalin joukkueelle ja pelaajalle ja syötöt mahdollisille syöttäjille ja päästetyn maalin maalivahdille.
        /// </summary>
        /// <param name="eventti"></param>
        /// <param name="uusiFutisPeli"></param>
        public static void TilastoiMaali(PeliTapahtuma eventti, FutisPeli uusiFutisPeli)
        {

            // kasvatetaan maalimäärää ja tilastoidaan veto, maali ja syötöt pelaajille
            if (eventti.puoli == uusiFutisPeli.kotijoukkue.nimi)
            {
                // lisätään joukkueen maalimäärää
                uusiFutisPeli.kotijoukkueenMaalit += 1;

                // käydään joukkueen pelaajat läpi
                foreach (Pelaaja pl in uusiFutisPeli.kotijoukkue.plrs)
                {
                    // jos pelaaja on sama kuin eventin pelaaja, lisätään maalien määrää ja vetojen määrää ja lasketaan pisteet
                    if (pl == eventti.pelaaja)
                    {
                        pl.vedot++;
                        pl.goals++;
                        pl.pisteet = pl.goals + pl.syotot;
                    }
                    // jos pelaaja on taas syöttäjä, lisätään syöttöjen määrää ja lasketaan pisteet
                    else if (eventti.syottaja1 == $"#{pl.numero} {pl.snimi}")
                    {
                        pl.syotot++;
                        pl.pisteet = pl.goals + pl.syotot;
                    }
                }
                // tilastoidaan päästetty maali
                foreach (Pelaaja mv in uusiFutisPeli.vierasjoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                {
                    if ($"#{mv.numero} {mv.snimi}" == uusiFutisPeli.vierasjoukkueenMV)
                    {
                        mv.paastetyt++;
                    }
                }
            }
            // kasvatetaan maalimäärää ja tilastoidaan veto, maali ja syötöt pelaajille
            else if (eventti.puoli == uusiFutisPeli.vierasjoukkue.nimi)
            {
                uusiFutisPeli.vierasjoukkueenMaalit += 1;

                foreach (Pelaaja pl in uusiFutisPeli.vierasjoukkue.plrs)
                {
                    // jos pelaaja on sama kuin eventin pelaaja, lisätään maalien määrää ja vetojen määrää ja lasketaan pisteet
                    if (pl == eventti.pelaaja)
                    {
                        pl.vedot++;
                        pl.goals++;
                        pl.pisteet = pl.goals + pl.syotot;
                    }
                    // jos pelaaja on taas syöttäjä, lisätään syöttöjen määrää ja lasketaan pisteet
                    else if (eventti.syottaja1 == $"#{pl.numero} {pl.snimi}")
                    {
                        pl.syotot++;
                        pl.pisteet = pl.goals + pl.syotot;
                    }
                }
                // tilastoidaan päästetty maali
                foreach (Pelaaja mv in uusiFutisPeli.kotijoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                {
                    // jos pelaaja on merkitty pelaavaksi maalivahdiksi, lisätään päästettyjä maaleja
                    if ($"#{mv.numero} {mv.snimi}" == uusiFutisPeli.kotijoukkueenMV)
                    {
                        mv.paastetyt++;
                    }
                }
            }
        }

        /// <summary>
        /// Tilastoi maalin joukkueelle ja pelaajalle ja syötöt mahdollisille syöttäjille ja päästetyn maalin maalivahdille.
        /// </summary>
        /// <param name="eventti"></param>
        /// <param name="uusiSabaPeli"></param>
        public static void TilastoiMaali(PeliTapahtuma eventti, SabaPeli uusiSabaPeli)
        {

            // kasvatetaan maalimäärää ja tilastoidaan veto, maali ja syötöt pelaajille
            if (eventti.puoli == uusiSabaPeli.kotijoukkue.nimi)
            {
                // lisätään joukkueen maalimäärää
                uusiSabaPeli.kotijoukkueenMaalit += 1;

                // käydään joukkueen pelaajat läpi
                foreach (Pelaaja pl in uusiSabaPeli.kotijoukkue.plrs)
                {
                    // jos pelaaja on sama kuin eventin pelaaja, lisätään maalien määrää ja vetojen määrää ja lasketaan pisteet
                    if (pl == eventti.pelaaja)
                    {
                        pl.vedot++;
                        pl.goals++;
                        pl.pisteet = pl.goals + pl.syotot;
                    }
                    // jos pelaaja on taas syöttäjä, lisätään syöttöjen määrää ja lasketaan pisteet
                    else if (eventti.syottaja1 == $"#{pl.numero} {pl.snimi}")
                    {
                        pl.syotot++;
                        pl.pisteet = pl.goals + pl.syotot;
                    }
                }
                // tilastoidaan päästetty maali
                foreach (Pelaaja mv in uusiSabaPeli.vierasjoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                {
                    if ($"#{mv.numero} {mv.snimi}" == uusiSabaPeli.vierasjoukkueenMV)
                    {
                        mv.paastetyt++;
                    }
                }
            }
            // kasvatetaan maalimäärää ja tilastoidaan veto, maali ja syötöt pelaajille
            else if (eventti.puoli == uusiSabaPeli.vierasjoukkue.nimi)
            {
                uusiSabaPeli.vierasjoukkueenMaalit += 1;

                foreach (Pelaaja pl in uusiSabaPeli.vierasjoukkue.plrs)
                {
                    // jos pelaaja on sama kuin eventin pelaaja, lisätään maalien määrää ja vetojen määrää ja lasketaan pisteet
                    if (pl == eventti.pelaaja)
                    {
                        pl.vedot++;
                        pl.goals++;
                        pl.pisteet = pl.goals + pl.syotot;
                    }
                    // jos pelaaja on taas syöttäjä, lisätään syöttöjen määrää ja lasketaan pisteet
                    else if (eventti.syottaja1 == $"#{pl.numero} {pl.snimi}")
                    {
                        pl.syotot++;
                        pl.pisteet = pl.goals + pl.syotot;
                    }
                }
                // tilastoidaan päästetty maali
                foreach (Pelaaja mv in uusiSabaPeli.kotijoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                {
                    // jos pelaaja on merkitty pelaavaksi maalivahdiksi, lisätään päästettyjä maaleja
                    if ($"#{mv.numero} {mv.snimi}" == uusiSabaPeli.kotijoukkueenMV)
                    {
                        mv.paastetyt++;
                    }
                }
            }
        }

        /// <summary>
        /// Tilastoi laukauksen pelaajalle ja torjunnan maalivahdille.
        /// </summary>
        /// <param name="eventti"></param>
        /// <param name="uusiLatkaPeli"></param>
        public static void TilastoiLaukaus(PeliTapahtuma eventti, LatkaPeli uusiLatkaPeli)
        {
            if (eventti.puoli == uusiLatkaPeli.kotijoukkue.nimi)
            {
                // tilastoidaan veto
                foreach (Pelaaja pl in uusiLatkaPeli.kotijoukkue.plrs)
                {
                    // jos pelaaja on sama kuin eventin pelaaja, kasvatetaan vetojen määrää
                    if (pl == eventti.pelaaja)
                    {
                        pl.vedot++;
                    }
                }

                // tilastoidaan torjunta
                foreach (Pelaaja mv in uusiLatkaPeli.vierasjoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                {
                    // jos pelaaja on merkitty pelaavaksi maalivahdiksi, lisätään torjuntoja
                    if ($"#{mv.numero} {mv.snimi}" == uusiLatkaPeli.vierasjoukkueenMV)
                    {
                        mv.torjunnat++;
                    }
                }
            }
            else if (eventti.puoli == uusiLatkaPeli.vierasjoukkue.nimi)
            {
                // tilastoidaan veto
                foreach (Pelaaja pl in uusiLatkaPeli.vierasjoukkue.plrs)
                {
                    // jos pelaaja on sama kuin eventin pelaaja, kasvatetaan vetojen määrää
                    if (pl == eventti.pelaaja)
                    {
                        pl.vedot++;

                    }
                }
                // tilastoidaan torjunta
                foreach (Pelaaja mv in uusiLatkaPeli.kotijoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                {
                    // jos pelaaja on merkitty pelaavaksi maalivahdiksi, lisätään torjuntoja
                    if ($"#{mv.numero} {mv.snimi}" == uusiLatkaPeli.kotijoukkueenMV)
                    {
                        mv.torjunnat++;
                    }
                }
            }
        }

        /// <summary>
        /// Tilastoi laukauksen pelaajalle ja torjunnan maalivahdille.
        /// </summary>
        /// <param name="eventti"></param>
        /// <param name="uusiFutisPeli"></param>
        public static void TilastoiLaukaus(PeliTapahtuma eventti, FutisPeli uusiFutisPeli)
        {
            if (eventti.puoli == uusiFutisPeli.kotijoukkue.nimi)
            {
                // tilastoidaan veto
                foreach (Pelaaja pl in uusiFutisPeli.kotijoukkue.plrs)
                {
                    // jos pelaaja on sama kuin eventin pelaaja, kasvatetaan vetojen määrää
                    if (pl == eventti.pelaaja)
                    {
                        pl.vedot++;
                    }
                }

                // tilastoidaan torjunta
                foreach (Pelaaja mv in uusiFutisPeli.vierasjoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                {
                    // jos pelaaja on merkitty pelaavaksi maalivahdiksi, lisätään torjuntoja
                    if ($"#{mv.numero} {mv.snimi}" == uusiFutisPeli.vierasjoukkueenMV)
                    {
                        mv.torjunnat++;
                    }
                }
            }
            else if (eventti.puoli == uusiFutisPeli.vierasjoukkue.nimi)
            {
                // tilastoidaan veto
                foreach (Pelaaja pl in uusiFutisPeli.vierasjoukkue.plrs)
                {
                    // jos pelaaja on sama kuin eventin pelaaja, kasvatetaan vetojen määrää
                    if (pl == eventti.pelaaja)
                    {
                        pl.vedot++;

                    }
                }
                // tilastoidaan torjunta
                foreach (Pelaaja mv in uusiFutisPeli.kotijoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                {
                    // jos pelaaja on merkitty pelaavaksi maalivahdiksi, lisätään torjuntoja
                    if ($"#{mv.numero} {mv.snimi}" == uusiFutisPeli.kotijoukkueenMV)
                    {
                        mv.torjunnat++;
                    }
                }
            }
        }

        /// <summary>
        /// Tilastoi laukauksen pelaajalle ja torjunnan maalivahdille.
        /// </summary>
        /// <param name="eventti"></param>
        /// <param name="uusiSabaPeli"></param>
        public static void TilastoiLaukaus(PeliTapahtuma eventti, SabaPeli uusiSabaPeli)
        {
            if (eventti.puoli == uusiSabaPeli.kotijoukkue.nimi)
            {
                // tilastoidaan veto
                foreach (Pelaaja pl in uusiSabaPeli.kotijoukkue.plrs)
                {
                    // jos pelaaja on sama kuin eventin pelaaja, kasvatetaan vetojen määrää
                    if (pl == eventti.pelaaja)
                    {
                        pl.vedot++;
                    }
                }

                // tilastoidaan torjunta
                foreach (Pelaaja mv in uusiSabaPeli.vierasjoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                {
                    // jos pelaaja on merkitty pelaavaksi maalivahdiksi, lisätään torjuntoja
                    if ($"#{mv.numero} {mv.snimi}" == uusiSabaPeli.vierasjoukkueenMV)
                    {
                        mv.torjunnat++;
                    }
                }
            }
            else if (eventti.puoli == uusiSabaPeli.vierasjoukkue.nimi)
            {
                // tilastoidaan veto
                foreach (Pelaaja pl in uusiSabaPeli.vierasjoukkue.plrs)
                {
                    // jos pelaaja on sama kuin eventin pelaaja, kasvatetaan vetojen määrää
                    if (pl == eventti.pelaaja)
                    {
                        pl.vedot++;

                    }
                }
                // tilastoidaan torjunta
                foreach (Pelaaja mv in uusiSabaPeli.kotijoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                {
                    // jos pelaaja on merkitty pelaavaksi maalivahdiksi, lisätään torjuntoja
                    if ($"#{mv.numero} {mv.snimi}" == uusiSabaPeli.kotijoukkueenMV)
                    {
                        mv.torjunnat++;
                    }
                }
            }
        }

        /// <summary>
        /// Vähentää tilastoja ja poistaa tapahtuman listasta.
        /// </summary>
        /// <param name="lista tapahtumista"></param>
        /// <param name="tapahtuman indeksi listassa"></param>
        /// <param name="uusiLatkaPeli"></param>
        public static void PoistaTapahtuma(BindingList<PeliTapahtuma> lista, int indeksi, LatkaPeli uusiLatkaPeli)
        {
            // jos käyttäjä on valinnut tapahtuman poistamisen, niin vähennetään tilastoja ja poistetaan tapahtuma listasta
            if (lista[indeksi].puoli == uusiLatkaPeli.kotijoukkue.nimi)
            {
                uusiLatkaPeli.kotijoukkueenMaalit--;

                switch (lista[indeksi].happeninki)
                {
                    case "Maali":

                        Pelaaja.PoistaiMaali(lista[indeksi].pelaaja);

                        if (lista[indeksi].assist1 != null)
                        {
                            Pelaaja.PoistaSyotto(lista[indeksi].assist1);
                        }
                        if (lista[indeksi].assist2 != null)
                        {
                            Pelaaja.PoistaSyotto(lista[indeksi].assist2);
                        }

                        Pelaaja.PoistaPaastettyMaali(uusiLatkaPeli.vierasMV);

                        break;

                    case "Laukaus":

                        Pelaaja.PoistaLaukaus(lista[indeksi].pelaaja);
                        Pelaaja.PoistaTorjunta(uusiLatkaPeli.vierasMV);

                        break;
                }

            }
            else if (lista[indeksi].puoli == uusiLatkaPeli.vierasjoukkue.nimi)
            {

                uusiLatkaPeli.kotijoukkueenMaalit--;

                switch (lista[indeksi].happeninki)
                {
                    case "Maali":
                        Pelaaja.PoistaiMaali(lista[indeksi].pelaaja);

                        if (lista[indeksi].assist1 != null)
                        {
                            Pelaaja.PoistaSyotto(lista[indeksi].assist1);
                        }
                        if (lista[indeksi].assist2 != null)
                        {
                            Pelaaja.PoistaSyotto(lista[indeksi].assist2);
                        }

                        Pelaaja.PoistaPaastettyMaali(uusiLatkaPeli.kotiMV);

                        break;

                    case "Laukaus":

                        Pelaaja.PoistaLaukaus(lista[indeksi].pelaaja);
                        Pelaaja.PoistaTorjunta(uusiLatkaPeli.kotiMV);

                        break;
                }
            }

            lista.RemoveAt(indeksi);
        }

        /// <summary>
        /// Vähentää tilastoja ja poistaa tapahtuman listasta.
        /// </summary>
        /// <param name="lista tapahtumista"></param>
        /// <param name="tapahtuman indeksi listassa"></param>
        /// <param name="uusiFutisPeli"></param>
        public static void PoistaTapahtuma(BindingList<PeliTapahtuma> lista, int indeksi, FutisPeli uusiFutisPeli)
        {
            // jos käyttäjä on valinnut tapahtuman poistamisen, niin vähennetään tilastoja ja poistetaan tapahtuma listasta
            if (lista[indeksi].puoli == uusiFutisPeli.kotijoukkue.nimi)
            {

                switch (lista[indeksi].happeninki)
                {
                    case "Maali":

                        // vähennetään kotijoukkueen maaleja
                        uusiFutisPeli.kotijoukkueenMaalit--;

                        // vähennetään pelaajan vetoja ja maaleja sekä mahdollisten syöttäjien syöttöjä
                        foreach (Pelaaja pl in uusiFutisPeli.kotijoukkue.plrs)
                        {
                            if (pl == lista[indeksi].pelaaja)
                            {
                                pl.vedot--;
                                pl.goals--;
                                pl.pisteet = pl.goals + pl.syotot;
                            }
                            else if (lista[indeksi].syottaja1 == $"#{pl.numero} {pl.snimi}")
                            {
                                pl.syotot--;
                                pl.pisteet = pl.goals + pl.syotot;
                            }
                            else if (lista[indeksi].syottaja2 == $"#{pl.numero} {pl.snimi}")
                            {
                                pl.syotot--;
                                pl.pisteet = pl.goals + pl.syotot;
                            }
                        }

                        // vähennetään maalivahdin päästettyjä maaleja
                        foreach (Pelaaja mv in uusiFutisPeli.vierasjoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                        {
                            if ($"#{mv.numero} {mv.snimi}" == uusiFutisPeli.vierasjoukkueenMV)
                            {
                                mv.paastetyt--;
                            }
                        }
                        break;

                    case "Laukaus":

                        // vähennetään pelaajan vetoja
                        foreach (Pelaaja pl in uusiFutisPeli.kotijoukkue.plrs)
                        {
                            if (pl == lista[indeksi].pelaaja)
                            {
                                pl.vedot--;
                            }
                        }
                        // vähennetään maalivahdin torjuntoja
                        foreach (Pelaaja mv in uusiFutisPeli.vierasjoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                        {
                            if ($"#{mv.numero} {mv.snimi}" == uusiFutisPeli.vierasjoukkueenMV)
                            {
                                mv.torjunnat--;
                            }
                        }

                        break;
                }

                lista.RemoveAt(indeksi);
            }
            else if (lista[indeksi].puoli == uusiFutisPeli.vierasjoukkue.nimi)
            {

                switch (lista[indeksi].happeninki)
                {
                    case "Maali":

                        // vähennetään vierasjoukkueen maaleja
                        uusiFutisPeli.vierasjoukkueenMaalit--;

                        // vähennetään pelaajan vetoja ja maaleja sekä mahdollisten syöttäjien syöttöjä
                        foreach (Pelaaja pl in uusiFutisPeli.vierasjoukkue.plrs)
                        {
                            if (pl == lista[indeksi].pelaaja)
                            {
                                pl.vedot--;
                                pl.goals--;
                                pl.pisteet = pl.goals + pl.syotot;
                            }
                            else if (lista[indeksi].syottaja1 == $"#{pl.numero} {pl.snimi}")
                            {
                                pl.syotot--;
                                pl.pisteet = pl.goals + pl.syotot;
                            }
                            else if (lista[indeksi].syottaja2 == $"#{pl.numero} {pl.snimi}")
                            {
                                pl.syotot--;
                                pl.pisteet = pl.goals + pl.syotot;
                            }
                        }
                        // vähennetään kotijoukkueen maalivahdin päästettyjä maaleja
                        foreach (Pelaaja mv in uusiFutisPeli.kotijoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                        {
                            if ($"#{mv.numero} {mv.snimi}" == uusiFutisPeli.kotijoukkueenMV)
                            {
                                mv.paastetyt--;
                            }
                        }
                        break;

                    case "Laukaus":

                        // vähennetään pelaajan vetoja 
                        foreach (Pelaaja pl in uusiFutisPeli.vierasjoukkue.plrs)
                        {
                            if (pl == lista[indeksi].pelaaja)
                            {
                                pl.vedot--;
                            }
                        }
                        // vähennetään kotijoukkueen maalivahdin torjuntoja
                        foreach (Pelaaja mv in uusiFutisPeli.kotijoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                        {
                            if ($"#{mv.numero} {mv.snimi}" == uusiFutisPeli.kotijoukkueenMV)
                            {
                                mv.torjunnat--;
                            }
                        }

                        break;
                }

                lista.RemoveAt(indeksi);

            }
        }

        /// <summary>
        /// Vähentää tilastoja ja poistaa tapahtuman listasta.
        /// </summary>
        /// <param name="lista tapahtumista"></param>
        /// <param name="tapahtuman indeksi listassa"></param>
        /// <param name="uusiSabaPeli"></param>
        public static void PoistaTapahtuma(BindingList<PeliTapahtuma> lista, int indeksi, SabaPeli uusiSabaPeli)
        {
            // jos käyttäjä on valinnut tapahtuman poistamisen, niin vähennetään tilastoja ja poistetaan tapahtuma listasta
            if (lista[indeksi].puoli == uusiSabaPeli.kotijoukkue.nimi)
            {

                switch (lista[indeksi].happeninki)
                {
                    case "Maali":

                        // vähennetään kotijoukkueen maaleja
                        uusiSabaPeli.kotijoukkueenMaalit--;

                        // vähennetään pelaajan vetoja ja maaleja sekä mahdollisten syöttäjien syöttöjä
                        foreach (Pelaaja pl in uusiSabaPeli.kotijoukkue.plrs)
                        {
                            if (pl == lista[indeksi].pelaaja)
                            {
                                pl.vedot--;
                                pl.goals--;
                                pl.pisteet = pl.goals + pl.syotot;
                            }
                            else if (lista[indeksi].syottaja1 == $"#{pl.numero} {pl.snimi}")
                            {
                                pl.syotot--;
                                pl.pisteet = pl.goals + pl.syotot;
                            }
                            else if (lista[indeksi].syottaja2 == $"#{pl.numero} {pl.snimi}")
                            {
                                pl.syotot--;
                                pl.pisteet = pl.goals + pl.syotot;
                            }
                        }

                        // vähennetään maalivahdin päästettyjä maaleja
                        foreach (Pelaaja mv in uusiSabaPeli.vierasjoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                        {
                            if ($"#{mv.numero} {mv.snimi}" == uusiSabaPeli.vierasjoukkueenMV)
                            {
                                mv.paastetyt--;
                            }
                        }
                        break;

                    case "Laukaus":

                        // vähennetään pelaajan vetoja
                        foreach (Pelaaja pl in uusiSabaPeli.kotijoukkue.plrs)
                        {
                            if (pl == lista[indeksi].pelaaja)
                            {
                                pl.vedot--;
                            }
                        }
                        // vähennetään maalivahdin torjuntoja
                        foreach (Pelaaja mv in uusiSabaPeli.vierasjoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                        {
                            if ($"#{mv.numero} {mv.snimi}" == uusiSabaPeli.vierasjoukkueenMV)
                            {
                                mv.torjunnat--;
                            }
                        }

                        break;
                }

                lista.RemoveAt(indeksi);
            }
            else if (lista[indeksi].puoli == uusiSabaPeli.vierasjoukkue.nimi)
            {

                switch (lista[indeksi].happeninki)
                {
                    case "Maali":

                        // vähennetään vierasjoukkueen maaleja
                        uusiSabaPeli.vierasjoukkueenMaalit--;

                        // vähennetään pelaajan vetoja ja maaleja sekä mahdollisten syöttäjien syöttöjä
                        foreach (Pelaaja pl in uusiSabaPeli.vierasjoukkue.plrs)
                        {
                            if (pl == lista[indeksi].pelaaja)
                            {
                                pl.vedot--;
                                pl.goals--;
                                pl.pisteet = pl.goals + pl.syotot;
                            }
                            else if (lista[indeksi].syottaja1 == $"#{pl.numero} {pl.snimi}")
                            {
                                pl.syotot--;
                                pl.pisteet = pl.goals + pl.syotot;
                            }
                            else if (lista[indeksi].syottaja2 == $"#{pl.numero} {pl.snimi}")
                            {
                                pl.syotot--;
                                pl.pisteet = pl.goals + pl.syotot;
                            }
                        }
                        // vähennetään kotijoukkueen maalivahdin päästettyjä maaleja
                        foreach (Pelaaja mv in uusiSabaPeli.kotijoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                        {
                            if ($"#{mv.numero} {mv.snimi}" == uusiSabaPeli.kotijoukkueenMV)
                            {
                                mv.paastetyt--;
                            }
                        }
                        break;

                    case "Laukaus":

                        // vähennetään pelaajan vetoja 
                        foreach (Pelaaja pl in uusiSabaPeli.vierasjoukkue.plrs)
                        {
                            if (pl == lista[indeksi].pelaaja)
                            {
                                pl.vedot--;
                            }
                        }
                        // vähennetään kotijoukkueen maalivahdin torjuntoja
                        foreach (Pelaaja mv in uusiSabaPeli.kotijoukkue.plrs.Where(o => o.pelipaikka == "MV"))
                        {
                            if ($"#{mv.numero} {mv.snimi}" == uusiSabaPeli.kotijoukkueenMV)
                            {
                                mv.torjunnat--;
                            }
                        }

                        break;
                }

                lista.RemoveAt(indeksi);

            }
        }

        public PeliTapahtuma tapahtumaKopio() 
        {
            return new PeliTapahtuma
            {
                happeninki = this.happeninki,
                puoli = this.puoli,
                pelaaja = this.pelaaja,
                koodrinaatit = this.koodrinaatit,
                minuutit = this.minuutit,
                sekuntit = this.sekuntit,
                aika = this.aika,
                assist1 = this.assist1,
                assist2 = this.assist2

            };
        }

    }
    public class LatkaPeli
    {

        public Team kotijoukkue;

        public Team vierasjoukkue;

        // lista ekan erän tapahtumista
        public BindingList<PeliTapahtuma> EkaEra;

        // lista tokan erän tapahtumista
        public BindingList<PeliTapahtuma> TokaEra;

        // lista kolmannen erän tapahtumista
        public BindingList<PeliTapahtuma> KolmasEra;

        // pelin päiväys
        public string paiv;

        // pelin alkamisaika
        public string alkaik;

        // pelin lopettamisaika
        public string lopaik;

        // pelin voittaja
        public string voittaja;

        // kotijoukkueen maalimäärä
        public int kotijoukkueenMaalit;

        // vierasjoukkueen maalimäärä
        public int vierasjoukkueenMaalit;

        // kotijoukkueen pelaava maalivahti
        public string kotijoukkueenMV;

        // vierasjoukkueen pelaava maalivahti
        public string vierasjoukkueenMV;

        public Pelaaja kotiMV;

        public Pelaaja vierasMV;

    }

    public class FutisPeli
    {

        public Team kotijoukkue;

        public Team vierasjoukkue;

        // lista ekan puoliajan tapahtumista
        public BindingList<PeliTapahtuma> EkaPuoliaika;

        // lista tokan puoliajan tapahtumista
        public BindingList<PeliTapahtuma> TokaPuoliaika;

        // pelin päiväys
        public string paiv;

        // pelin alkamisaika
        public string alkaik;

        // pelin lopettamisaika
        public string lopaik;

        // pelin voittaja
        public string voittaja;

        // kotijoukkueen maalimäärä
        public int kotijoukkueenMaalit;

        // vierasjoukkueen maalimäärä
        public int vierasjoukkueenMaalit;

        // kotijoukkueen pelaava maalivahti
        public string kotijoukkueenMV;

        // vierasjoukkueen pelaava maalivahti
        public string vierasjoukkueenMV;

    }

    public class SabaPeli
    {

        public Team kotijoukkue;

        public Team vierasjoukkue;

        // lista ekan erän tapahtumista
        public BindingList<PeliTapahtuma> EkaEra;

        // lista tokan erän tapahtumista
        public BindingList<PeliTapahtuma> TokaEra;

        // lista kolmannen erän tapahtumista
        public BindingList<PeliTapahtuma> KolmasEra;

        // pelin päiväys
        public string paiv;

        // pelin alkamisaika
        public string alkaik;

        // pelin lopettamisaika
        public string lopaik;

        // pelin voittaja
        public string voittaja;

        // kotijoukkueen maalimäärä
        public int kotijoukkueenMaalit;

        // vierasjoukkueen maalimäärä
        public int vierasjoukkueenMaalit;

        // kotijoukkueen pelaava maalivahti
        public string kotijoukkueenMV;

        // vierasjoukkueen pelaava maalivahti
        public string vierasjoukkueenMV;

    }

}


