using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kommunikationsverktyg.Models
{
    public class CategoryModel
    {
        [Key]
        public virtual int CategoryModelId { get; set; }
        public virtual string Type { get; set; }

        public virtual ICollection<FormalBlogModel> Posts { get; set; }
    }
}