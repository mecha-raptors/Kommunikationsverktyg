using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kommunikationsverktyg.Models
{
    public class EventInvitedUsers
    {
        [Key]
        public virtual int InviteId { get; set; }

        public virtual RequestedEventModel Event { get; set; }

        public virtual List<ApplicationUser> User { get; set; }
    }
}