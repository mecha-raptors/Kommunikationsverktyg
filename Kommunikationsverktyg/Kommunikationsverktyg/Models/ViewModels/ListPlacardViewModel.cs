using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kommunikationsverktyg.Models.ViewModels
{
    public class ListPlacardViewModel
    {
        public List<PlacardModel> Placards { get; set; }
        public virtual DateTime Timestamp { get; set; }
        public string SenderId { get; set; }
        public int PlacardId { get; set; }
        public int PlacardTypeId { get; set; }

        [Required]
        [Display(Name="Välj kategori")]
        [Range(1, 2, ErrorMessage ="Du måste välja en kategori")]
        public virtual PlacardTypes PlacardType { get; set; }

        [StringLength(int.MaxValue, ErrorMessage = "{0} måste vara minst {2} tecken långt.", MinimumLength = 6)]
        [Display(Name = "Meddelande")]
        [Required]
        public string Message { get; set; }

        [StringLength(100, ErrorMessage = "{0} måste vara minst {2} tecken långt.", MinimumLength = 6)]
        [Display(Name = "Titel")]
        [Required]
        public string Title { get; set; }
        
        public enum PlacardTypes
        {
            Forskning = 1,
            Utbildning = 2
        };
    }
}