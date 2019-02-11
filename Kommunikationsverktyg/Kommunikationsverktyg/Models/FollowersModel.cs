using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kommunikationsverktyg.Models
{
    public class FollowersModel
    {
        [Key]
        public virtual int FollowerModelId { get; set; }

        public virtual int CategoryModelId { get; set; }
        public virtual CategoryModel Category { get; set; }

        public virtual string Id { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}