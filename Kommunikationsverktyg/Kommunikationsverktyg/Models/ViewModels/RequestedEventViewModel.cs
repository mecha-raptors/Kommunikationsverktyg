using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kommunikationsverktyg.Models.ViewModels
{
    public class RequestedEventViewModel
    {
        [Required]
        [Display(Name = "Titel")]
        public virtual string Title { get; set; }
        [Required]
        [Display(Name = "Beskrivning")]
        public virtual string Description { get; set; }
        [Required]
        public virtual List<DateModel> TimeSuggestions { get; set; }
        [Required]
        public virtual List<string> Invitees { get; set; }
        public virtual Dictionary<string, string> InvitableUsers { get; set; }
        
    }
}