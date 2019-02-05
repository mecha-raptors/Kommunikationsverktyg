using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Kommunikationsverktyg.Models
{
    public class FormalBlogModel
    {
        [Key]
        public virtual int FormalBlogModelId { get; set; }

        [Column(TypeName = "datetime2")]
        public virtual DateTime Timestamp { get; set; }

        public virtual string Message { get; set; }
        public virtual string Title { get; set; }
        public virtual string FilePath { get; set; }

        public virtual string Id  { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual int CategoryModelId { get; set; }
        public virtual CategoryModel Category { get; set; }
        public virtual int Likes { get; set; }
        
    }
}