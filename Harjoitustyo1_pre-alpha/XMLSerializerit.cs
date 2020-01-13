using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Harjoitustyo1_pre_alpha
{
    static class XMLSerializerit
    {
        // tuo lätkäjoukkueet xml-tiedostosta
        public static List<Team> DeserializeXMLLatkajoukkueet()
        {
            if (File.Exists("Latkajoukkueet.xml"))
            {
                StreamReader sr = new StreamReader("Latkajoukkueet.xml");
                XmlSerializer ser = new XmlSerializer(typeof(List<Team>));
                object obj = ser.Deserialize(sr);
                sr.Close();
                return (List<Team>)obj;               
            }
            else
            {
                return null;
            }
        }

        // tuo futisjoukkueet xml-tiedostosta
        public static List<Team> DeserializeXMLFutisjoukkueet()
        {
            if (File.Exists("Futisjoukkueet.xml"))
            {
                StreamReader sr = new StreamReader("Futisjoukkueet.xml");
                XmlSerializer ser = new XmlSerializer(typeof(List<Team>));
                object obj = ser.Deserialize(sr);
                sr.Close();
                return (List<Team>)obj;
            }
            else
            {
                return null;
            }
        }

        // tuo säbäjoukkueet xml-tiedostosta
        public static List<Team> DeserializeXMLSabajoukkueet()
        {
            if (File.Exists("Sabajoukkueet.xml"))
            {
                StreamReader sr = new StreamReader("Sabajoukkueet.xml");
                XmlSerializer ser = new XmlSerializer(typeof(List<Team>));
                object obj = ser.Deserialize(sr);
                sr.Close();
                return (List<Team>)obj;
            }
            else
            {
                return null;
            }
        }

        // tuo lätkäpelit xml-tiedostosta
        public static List<LatkaPeli> DeserializeXMLLatkapelit()
        {
            if (File.Exists("Latkapelit.xml"))
            {
                StreamReader sr = new StreamReader("Latkapelit.xml");
                XmlSerializer ser = new XmlSerializer(typeof(List<LatkaPeli>));
                object obj = ser.Deserialize(sr);
                sr.Close();

                return (List<LatkaPeli>)obj;
            }
            else
            {
                return null;
            }
        }

        // tuo futispelit xml-tiedostosta
        public static List<FutisPeli> DeserializeXMLFutispelit()
        {
            if (File.Exists("Futispelit.xml"))
            {
                StreamReader sr = new StreamReader("Futispelit.xml");
                XmlSerializer ser = new XmlSerializer(typeof(List<FutisPeli>));
                object obj = ser.Deserialize(sr);
                sr.Close();

                return (List<FutisPeli>)obj;
            }
            else
            {
                return null;
            }
        }

        // tuo säbäpelit xml-tiedostosta
        public static List<SabaPeli> DeserializeXMLSabapelit()
        {
            if (File.Exists("Sabapelit.xml"))
            {
                StreamReader sr = new StreamReader("Sabapelit.xml");
                XmlSerializer ser = new XmlSerializer(typeof(List<SabaPeli>));
                object obj = ser.Deserialize(sr);
                sr.Close();

                return (List<SabaPeli>)obj;
            }
            else
            {
                return null;
            }
        }

        // vie lätkäjoukkueen tiedot xml-tiedostoon
        public static void SerializeXMLLatkajoukkueet(List<Team> input)
        {
            XmlSerializer serlizer = new XmlSerializer(input.GetType());
            StreamWriter sw = new StreamWriter("Latkajoukkueet.xml");
            serlizer.Serialize(sw, input);
            sw.Close();
        }

        // vie futisjoukkueen tiedot xml-tiedostoon
        public static void SerializeXMLFutisjoukkueet(List<Team> input)
        {
            XmlSerializer serlizer = new XmlSerializer(input.GetType());
            StreamWriter sw = new StreamWriter("Futisjoukkueet.xml");
            serlizer.Serialize(sw, input);
            sw.Close();
        }

        // vie säbäjoukkueen tiedot xml-tiedostoon
        public static void SerializeXMLSabajoukkueet(List<Team> input)
        {
            XmlSerializer serlizer = new XmlSerializer(input.GetType());
            StreamWriter sw = new StreamWriter("Sabajoukkueet.xml");
            serlizer.Serialize(sw, input);
            sw.Close();
        }

        // vie lätkäpelit xml-tiedostoon
        public static void SerializeXMLLatkapelit(List<LatkaPeli> input)
        {
            XmlSerializer serlizer = new XmlSerializer(input.GetType());
            StreamWriter sw = new StreamWriter("Latkapelit.xml");
            serlizer.Serialize(sw, input);
            sw.Close();
        }

        // vie Futispelit xml-tiedostoon
        public static void SerializeXMLFutispelit(List<FutisPeli> input)
        {
            XmlSerializer serlizer = new XmlSerializer(input.GetType());
            StreamWriter sw = new StreamWriter("Futispelit.xml");
            serlizer.Serialize(sw, input);
            sw.Close();
        }

        // vie säbäpelit xml-tiedostoon
        public static void SerializeXMLSabapelit(List<SabaPeli> input)
        {
            XmlSerializer serlizer = new XmlSerializer(input.GetType());
            StreamWriter sw = new StreamWriter("Sabapelit.xml");
            serlizer.Serialize(sw, input);
            sw.Close();
        }

    }
}
