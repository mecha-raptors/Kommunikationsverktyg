using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kommunikationsverktyg.Models
{
    public class VoteModel
    {
        public int Id { get; set; }
        public bool VoteFor { get; set; }
        public ApplicationUser Voter { get; set; }
        public DateModel Date { get; set; }
    }
}