using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kommunikationsverktyg.Models.ViewModels
{
    public class ListFormalBlogViewModel
    {
        public List<FormalBlogViewModel> Posts { get; set; }
        public HttpPostedFileBase File { get; set; }

        [StringLength(int.MaxValue, ErrorMessage = "{0} måste vara minst {2} tecken långt.", MinimumLength = 6)]
        [Display(Name = "Meddelande")]
        [Required]
        public string Message { get; set; }

        [StringLength(100, ErrorMessage = "{0} måste vara minst {2} tecken långt.", MinimumLength = 6)]
        [Display(Name = "Titel")]
        [Required]
        public string Title { get; set; }

        public string SenderId { get; set; }
    }
}