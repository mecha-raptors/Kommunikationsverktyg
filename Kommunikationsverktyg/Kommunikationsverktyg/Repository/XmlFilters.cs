using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace Kommunikationsverktyg.Repository
{
    public class XmlFilters
    { 
        
            [Remote("BadWords", "Validation")]
            public string Content { get; set; }
        }

        public class ValidationController
        {
            public JsonResult BadWords(string content)
            {
                var badWords = new[] { "fan", "helvete", "jävlar" };
                if (CheckText(content, badWords))
                {
                    return Json("Sorry, you must not curse!", JsonRequestBehavior.AllowGet);
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }

        private JsonResult Json(bool v, JsonRequestBehavior allowGet)
        {
            throw new NotImplementedException();
        }

        private JsonResult Json(string v, JsonRequestBehavior allowGet)
        {
            throw new NotImplementedException();
        }

        private bool CheckText(string content, string[] badWords)
            {
                foreach (var badWord in badWords)
                {
                    var regex = new Regex("(^|[\\?\\.,\\s])" + badWord + "([\\?\\.,\\s]|$)");
                    if (regex.IsMatch(content)) return true;
                }
                return false;
            }
        }

    }