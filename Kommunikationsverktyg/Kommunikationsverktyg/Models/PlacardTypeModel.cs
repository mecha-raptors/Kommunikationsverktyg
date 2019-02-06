using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kommunikationsverktyg.Models
{
    public class PlacardTypeModel
    {
        [Key]
        public virtual int PlacardTypeModelId { get; set; }
        public virtual string Type { get; set; }

        public virtual ICollection<PlacardModel> Placards { get; set; }
    }
}