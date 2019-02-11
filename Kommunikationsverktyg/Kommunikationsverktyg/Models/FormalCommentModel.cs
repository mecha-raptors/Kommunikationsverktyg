using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Kommunikationsverktyg.Models
{
    public class FormalCommentModel
    {
        [Key]
        public int FormalBlogCommentId { get; set; }

        public virtual int BlogID { get; set; }
        public virtual FormalBlogModel BlogModel { get; set; }

        public virtual string userId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual string Message { get; set; }



        [Column(TypeName = "datetime2")]
        public virtual DateTime Timestamp { get; set; }

        //public virtual int Likes { get; set; }

        //public virtual ICollection<LikeModel> Likers { get; set; }
    }
}