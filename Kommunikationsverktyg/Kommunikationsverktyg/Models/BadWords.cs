using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kommunikationsverktyg.Models
{
    public class BadWords
    {
        [Key]
        public int WordId { get; set; }
        public string Word { get; set; }
    }
}