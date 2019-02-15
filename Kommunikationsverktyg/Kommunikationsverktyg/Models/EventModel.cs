using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Kommunikationsverktyg.Models
{
    public class EventModel
    {
        [Key]
        [Required]
        public virtual int EventId { get; set; }
        [Required]
        [Display(Name = "Titel")]
        public virtual string Title { get; set; }
        [Required]
        [Display(Name = "Beskrivning")]
        public virtual string Description { get; set; }
        public virtual string Location { get; set; }
        [Required]
        [Display(Name = "Börjar")]
        public virtual DateTime Start { get; set; }
        [Required]
        [Display(Name = "Slutar")]
        public virtual DateTime End { get; set; }
        public virtual ApplicationUser EventCreator { get; set; }
    }
}