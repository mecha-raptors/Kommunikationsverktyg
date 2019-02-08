using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kommunikationsverktyg.Models
{
    public class LikeModel
    {
        [Key]
        public int LikeModelId { get; set; }

        public virtual int FormalBlogModelId { get; set; }
        public virtual FormalBlogModel FormalPosts { get; set; }

        public virtual string Id { get; set; }
        public virtual ApplicationUser User { get; set; }
        
    }
}