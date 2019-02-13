using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kommunikationsverktyg.Models.ViewModels
{
    public class InformalBlogViewModel
    {
        public virtual DateTime Timestamp { get; set; }
        public virtual string Message { get; set; }
        public virtual string Title { get; set; }
        public List<string> ImagePaths { get; set; }
        public virtual int PostID { get; set; }
        public virtual string UserId { get; set; }
        public string Fullname { get; set; }
        public List<InformalCommentModel> Comments { get; set; }
    }
}