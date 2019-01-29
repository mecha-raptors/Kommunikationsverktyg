using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public virtual string Title { get; set; }
        [Required]
        public virtual string Description { get; set; }
        [Required]
        public virtual DateTime Start { get; set; }
        [Required]
        public virtual DateTime End { get; set; }

    }
}