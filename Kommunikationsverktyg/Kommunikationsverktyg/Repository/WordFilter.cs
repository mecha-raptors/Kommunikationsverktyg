using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace Kommunikationsverktyg.Repository
{
    public class WordFilter
    {
        public List<string> Words()
        {
            List<string> BadWords = new List<string>();

            string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/XmlWordList/"), "WordListFormell.xml");
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;

            XmlReader xmlIn = XmlReader.Create(path, settings);

            if (xmlIn.ReadToDescendant("WordList"))
            {
                xmlIn.ReadStartElement("WordList");
                do
                {

                    var value = xmlIn.ReadElementContentAsString();

                    BadWords.Add(value.ToLower());
                }
                while (xmlIn.Name == "word");

            }

            xmlIn.Close();
            return BadWords;
        }

    }
} 