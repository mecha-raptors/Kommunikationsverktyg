using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kommunikationsverktyg.Models
{
    public class JsonEventRequestModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<DateModel> TimeSuggestions { get; set; }
    }
}