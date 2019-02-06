using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kommunikationsverktyg.Models
{
    public class RequestedEventModel
    {
        [Key]
        [Required]
        public virtual int EventId { get; set; }
        [Required]
        public virtual string Title { get; set; }
        [Required]
        public virtual string Description { get; set; }
        [Required]
        public virtual List<DateModel> TimeSuggestions { get; set; }
        [Required]
        public virtual List<string> Invitees { get; set; }
    }
}