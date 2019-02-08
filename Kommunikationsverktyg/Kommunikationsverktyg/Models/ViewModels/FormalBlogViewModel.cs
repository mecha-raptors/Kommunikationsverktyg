using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kommunikationsverktyg.Models.ViewModels
{
    public class FormalBlogViewModel
    {

        public virtual DateTime Timestamp { get; set; }
        public virtual string Message { get; set; }
        public virtual string Title { get; set; }
        public virtual string FilePath { get; set; }
        public int PostId { get; set; }

        public string UserId { get; set; }
        public string Category { get; set; }
        public string Fullname { get; set; }
        public int Likes { get; set; }
        public List<LikeModel> Likers { get; set; }

        //public LikeModel Like { get; set; }
    }
}