using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Kommunikationsverktyg.Models
{
    public class PlacardModel
    {
        [Key]
        public virtual int PlacardId { get; set; }

        [Column(TypeName = "datetime2")]
        public virtual DateTime Timestamp { get; set; }
        public string Fullname { get; set; }
        public virtual string Message { get; set; }
        public virtual string Title { get; set; }
        public string TypeName { get; set; }

        //public virtual string Id  { get; set; }
        public virtual ApplicationUser User { get; set; }

        //public virtual int CategoryModelId { get; set; }
        public virtual PlacardTypeModel Type { get; set; }
    }
}