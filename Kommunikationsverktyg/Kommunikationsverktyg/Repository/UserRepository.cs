using Kommunikationsverktyg.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Kommunikationsverktyg.Repository
{
    public class UserRepository
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public string SaveImage(HttpPostedFileBase img)
        {
            if (!IsImage(img.FileName))
            {
                throw new Exception();
            }
            string imgPath = Guid.NewGuid().ToString() + "_" + Path.GetFileName(img.FileName);

            string finalPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Images/"), imgPath);
            img.SaveAs(finalPath);

            var userPath = @"Images/" + imgPath;

            return userPath;
        }

        public bool IsImage(string img)
        {
            var ImageExtensions = new List<string>
            {
                ".JPG",
                ".JPE",
                ".BMP",
                ".GIF",
                ".PNG",
                ".MP4"
            };

            if (ImageExtensions.Contains(Path.GetExtension(img).ToUpperInvariant()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void DeleteImg(string path)
        {
            File.Delete(Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/"), path));
        }

        public ApplicationUser GetUser(string id)
        {
            ApplicationUser user = null;
            try
            {
                user = db.Users.FirstOrDefault(u => u.Id == id);

                
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            return user;


        }

        public void RemoveUser(string userId)
        {
            var blogRepo = new FormalBlogRepository();
            var user = GetUser(userId);

            try
            {
                if (db.FormalBlogPosts.Any(u => u.User.Id == user.Id))
                {
                    var posts = db.FormalBlogPosts.Where(u => u.User.Id == user.Id);
                    
                    foreach(var post in posts)
                    {
                        blogRepo.DeletePost(post.FormalBlogModelId);
                    }
                    db.FormalBlogPosts.RemoveRange(posts);
                    db.SaveChanges();
                }

                if (db.InformalBlogPosts.Any(u => u.User.Id == user.Id))
                {
                    var posts = db.InformalBlogPosts.Where(u => u.User.Id == user.Id);
                    db.InformalBlogPosts.RemoveRange(posts);
                    db.SaveChanges();
                }

                if (db.Likes.Any(u => u.User.Id == user.Id))
                {
                    var likes = db.Likes.Where(u => u.User.Id == user.Id);
                    db.Likes.RemoveRange(likes);
                    db.SaveChanges();
                }

                if (db.Placards.Any(u => u.User.Id == user.Id))
                {
                    var placards = db.Placards.Where(u => u.User.Id == user.Id);
                    db.Placards.RemoveRange(placards);
                    db.SaveChanges();
                }

                if (db.Followers.Any(u => u.User.Id == user.Id))
                {
                    var followers = db.Followers.Where(u => u.User.Id == user.Id);
                    db.Followers.RemoveRange(followers);
                    db.SaveChanges();
                }

                if (db.FormalComments.Any(u => u.User.Id == user.Id))
                {
                    var formalComments = db.FormalComments.Where(u => u.User.Id == user.Id);
                    db.FormalComments.RemoveRange(formalComments);
                    db.SaveChanges();
                }
                var _db = new ApplicationDbContext();
                var usern = _db.Users.Find(userId);
                _db.Users.Remove(usern);
                _db.SaveChanges();
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }
    }
}