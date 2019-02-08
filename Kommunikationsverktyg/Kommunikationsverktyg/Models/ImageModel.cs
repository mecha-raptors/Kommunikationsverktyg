using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kommunikationsverktyg.Models
{
    public class ImageModel
    {
        [Key]
        public virtual int ImageModelId { get; set; }
        public virtual string Path { get; set; }

        public virtual int InformalBlogModelId { get; set; }
        public virtual InformalBlogModel InformalPost{ get; set; }
    }
}