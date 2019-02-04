using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kommunikationsverktyg.Models
{
    public class EventViewModel
    {
        [Required]
        [Display(Name = "Titel")]
        public virtual string Title { get; set; }
        [Required]
        [Display(Name = "Beskrivning")]
        public virtual string Description { get; set; }
        [Required]
        [Display(Name = "Börjar")]
        public virtual DateTime Start { get; set; }
        [Required]
        [Display(Name = "Slutar")]
        public virtual DateTime End { get; set; }
        
        [Display(Name = "Deltagare")]
        public virtual List<ApplicationUser> Invitees { get; set; }
    }
}