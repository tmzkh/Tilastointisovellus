using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace Harjoitustyo1_pre_alpha
{

    public class Pelaaja
    {
        // pelaajan numero
        public string numero;

        // pelaajan etunimi
        public string enimi;

        // pelaajan sukunimi
        public string snimi;

        // pelaajan joukkue
        public string team;

        // pelipaikka
        public string pelipaikka;

        // laukausten määrä
        public int vedot;

        // maalit
        public int goals;

        // syötöt
        public int syotot;

        // maalit + syötöt
        public int pisteet;

        // maalivahdin torjunnat
        public int torjunnat;

        // maalivahdin päästämät maalit
        public int paastetyt;

        // pelattujen otteluiden määrä
        public int pelatutPelit;

        // maalivahdin torjumat voitot
        public int voitot;

        // maalivahdin hävityt pelit
        public int havityt;

        public static void TilastoiMaali(Pelaaja p)
        {
            p.vedot++;
            p.goals++;
            p.pisteet = p.goals + p.syotot;
        }

        public static void TilastoiSyotto(Pelaaja p)
        {
            p.syotot++;
            p.pisteet = p.goals + p.syotot;
        }

        public static void TilastoiLaukaus(Pelaaja p)
        {
            p.vedot++;
        }

        public static void TilastoiTorjunta(Pelaaja p)
        {
            p.torjunnat++;
        }

        public static void TilastoiPaastettyMaali(Pelaaja p)
        {
            p.paastetyt++;
        }

        public static void TilastoiPelattuPeli(Pelaaja p)
        {
            p.pelatutPelit++;
        }

        public static void PoistaiMaali(Pelaaja p)
        {
            p.vedot--;
            p.goals--;
            p.pisteet = p.goals + p.syotot;
        }

        public static void PoistaSyotto(Pelaaja p)
        {
            p.syotot--;
            p.pisteet = p.goals + p.syotot;
        }

        public static void PoistaLaukaus(Pelaaja p)
        {
            p.vedot--;
        }

        public static void PoistaTorjunta(Pelaaja p)
        {
            p.torjunnat--;
        }

        public static void PoistaPaastettyMaali(Pelaaja p)
        {
            p.paastetyt--;
        }
    }

    public class Team
    {
        // lista joukkueen pelaajista
        public List<Pelaaja> plrs;

        // joukkueen nimi
        public string nimi;

        public string Nimi
        {
            get { return nimi; }
        }

        // joukkueen lyhenne
        public string lyh;

        // joukkueen pelattujen pelien määrä
        public int ottelut;

        public int Ottelut
        {
            get { return ottelut; }
        }

        // joukkueen voittojen määrä
        public int voitot;

        public int Voitot
        {
            get { return voitot; }
        }

        // joukkueen tappioiden määrä
        public int haviot;

        public int Tappiot
        {
            get { return haviot; }
        }

        // joukkueen tasapelien määrä
        public int tasapelit;

        public int Tasapelit
        {
            get { return tasapelit; }
        }

        // joukkueen pistemäärä
        public int pisteet;

        public Team Uusijoukkue()
        {
            return new Team
            {
                plrs = this.plrs,
                nimi = this.nimi,
                lyh = this.lyh,
                ottelut = this.ottelut,
                voitot = this.voitot,
                haviot = this.haviot,
                tasapelit = this.tasapelit,
                pisteet = this.pisteet
            };
        }

        public int Pisteet
        {
            get { return pisteet; }
        }

    }

}


