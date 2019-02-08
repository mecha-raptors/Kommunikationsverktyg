using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Kommunikationsverktyg.Models
{
    public class InformalBlogModel
    {
        [Key]
        public virtual int InformalBlogModelId { get; set; }

        [Column(TypeName = "datetime2")]
        public virtual DateTime Timestamp { get; set; }

        public virtual string Message { get; set; }
        public virtual string Title { get; set; }

        
        public virtual string Id { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual List<ImageModel> Images { get; set; }

        public InformalBlogModel() {
            Images = new List<ImageModel>();
        }
    }
}