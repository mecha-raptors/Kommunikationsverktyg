using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kommunikationsverktyg.Models
{
    public class FileModel
    {
        [Key]
        public int FileId { get; set; }
        public string FilePath { get; set; }
    }
}