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

            string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/XmlWordList/"));
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;

            XmlReader xmlIn = XmlReader.Create(path, settings);

            if (xmlIn.ReadToDescendant("WordList"))
            {
                do
                {
                    BadWords.Add(xmlIn["word"]);
                }
                while (xmlIn.ReadToNextSibling("word"));

            }

            xmlIn.Close();
            return BadWords;
        }

    }
} 