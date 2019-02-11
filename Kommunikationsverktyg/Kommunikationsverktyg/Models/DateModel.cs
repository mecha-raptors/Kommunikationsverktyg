using System;
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
        public virtual int Votes {
            get {
                if(Voters != null)
                {
                    return Voters.Count();
                }
                else
                {
                    return 0;
                }
            }
        }
        public virtual ICollection<ApplicationUser> Voters { get; set; }
        public virtual ICollection<ApplicationUser> VotersAgainst { get; set; }

    }
}