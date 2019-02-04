using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public int PostId { get; set; }
        public DateTime Timestamp { get; set; }

        [Required(ErrorMessage = "Du måste välja en kategori!")]
        public int CategoryModelId { get; set; }

        public List<CategoryModel> Categories { get; set; }

        public IEnumerable<SelectListItem> CategoryList
        {
            get { return new SelectList(Categories, "CategoryModelId", "Type"); }

        }

    }
}