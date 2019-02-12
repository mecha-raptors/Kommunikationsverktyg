﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kommunikationsverktyg.Models
{
    public class DateModel
    {
        [Key]
        public virtual int DateId { get; set; }
        [Display(Name = "Börjar")]
        public virtual DateTime StartTime { get; set; }
        [Display(Name = "Slutar")]
        public virtual DateTime EndTime { get; set; }
        public virtual ICollection<VoteModel> Votes { get; set; }

    }
}