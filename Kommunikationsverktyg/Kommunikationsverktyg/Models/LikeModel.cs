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
        public int Id { get; set; }

        public FormalBlogModel FormalPosts { get; set; }
        public ApplicationUser Users { get; set; }
    }
}