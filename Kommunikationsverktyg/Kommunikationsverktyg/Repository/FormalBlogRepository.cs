using Kommunikationsverktyg.Models;
using Kommunikationsverktyg.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;

namespace Kommunikationsverktyg.Repository
{
    public class FormalBlogRepository
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        public UserRepository UserRepo = new UserRepository();

        public ListFormalBlogViewModel GetFormalPosts()
        {
            try
            {
                var model = new ListFormalBlogViewModel();
                var localDb = new ApplicationDbContext();
              

                var posts = localDb.FormalBlogPosts.OrderByDescending(i => i.Timestamp).ToList();
                var list = new List<FormalBlogViewModel>();
                foreach (var item in posts)
                {
                    
                    var m = new FormalBlogViewModel
                    {
                        FilePath = item.FilePath,
                        Message = item.Message,
                        Fullname = item.User.Firstname + " " + item.User.Lastname,
                        Timestamp = item.Timestamp,
                        Title = item.Title,
                        UserId = item.User.Id,
                        PostId = item.FormalBlogModelId,
                        Category = item.Category.Type,
                        Likes = item.Likers.Count,
                        Comments = item.Comments.ToList(),
                        CategoryId = item.Category.CategoryModelId,
                        Followers = localDb.Followers.Where(f => f.CategoryModelId == item.Category.CategoryModelId).ToList()
                };
                    m.Likers = item.Likers.ToList();
                   if(m.Likers == null)
                    {
                        m.Likers = new List<LikeModel>();
                    }


                    list.Add(m);
                }
                var helper = new CategoryRepository();
                model.Categories = helper.GetCategories();
                model.Posts = list;
                return model;
            }
            catch (Exception exc)
            {
                throw new Exception();
            }
        }

        public void SavePost(ListFormalBlogViewModel list)
        {
            try
            {
                var user = _db.Users.FirstOrDefault(u => u.Id == list.SenderId);
                var category = _db.Categories.FirstOrDefault(c => c.CategoryModelId == list.CategoryModelId);
                var model = new FormalBlogModel
                {
                    FilePath = SaveFile(list.File),
                    Message = FilterContent(list.Message, user, DateTime.Now),
                    Title = FilterContent(list.Title, user, DateTime.Now),
                    Timestamp = DateTime.Now,
                    User = user,
                    Category = category
                };
                NotifyFollower(category);
                _db.FormalBlogPosts.Add(model);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }


        }

        private void NotifyFollower(CategoryModel category)
        {
            var helper = new EmailNotification();

            foreach(var item in category.Followers)
            {
                helper.SendEmail(item.User.Email, "Ett nytt inlägg i kategorin " + category.Type + " är tillgängligt", "Nytt inlägg");
            }

        }

        public string SaveFile(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return null;
            }
            if (UserRepo.IsImage(file.FileName))
            {
                throw new Exception();
            }
            string filePath = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);

            string finalPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Files/"), filePath);
            file.SaveAs(finalPath);

            var userPath = @"Files/" + filePath;

            return userPath;
        }

        public string FilterContent(string content, ApplicationUser user, DateTime timestamp)
        {
            var words = content.Split(' ');
            var badWords = _db.BadWords.ToList();
            string filteredContent = "";
            var IsValid = false;

            foreach (var item in words)
            {
                var IsComitted = false;
                foreach (var badword in badWords)
                {
                    if (item.ToLower().Contains(badword.Word))
                    {
                        filteredContent += item.Mask() + " ";
                        IsComitted = true;
                        IsValid = true;
                        break;
                    }

                }
                if (!IsComitted)
                {
                    filteredContent += item + " ";
                }
            }
            if (IsValid) { 
                SendViolationNotificationEmail(user, timestamp, content);
            }
            return filteredContent;
        }

        private void SendViolationNotificationEmail(ApplicationUser user, DateTime timestamp, string content)
        {
            var email = new EmailNotification();
            var roles = new RoleRepository();

            var admins = roles.GetUsersByRole("admin");
            admins.ForEach(i => email.SendEmail(i.Email, "Användaren: " + user.Firstname + " " + user.Lastname + " skrev: " + "\n" + content + ". \n" + "Datum: " + timestamp.ToString() , "Varning!"));

        }

        public void DeletePost(int id)
        {

            try
            {
                var comments = _db.FormalComments.Where(i => i.BlogModel.FormalBlogModelId == id).ToList();
                _db.FormalComments.RemoveRange(comments);
                _db.SaveChanges();
                var likes = _db.Likes.Where(i => i.FormalBlogModelId == id).ToList();
                _db.Likes.RemoveRange(likes);
                _db.SaveChanges();
                var db = new ApplicationDbContext();
                var post = db.FormalBlogPosts.Find(id);
                db.FormalBlogPosts.Remove(post);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public void LikePost(int postId, string email)
        {
            try
            {
               
                var db = new ApplicationDbContext();
                
                var post = db.FormalBlogPosts.Find(postId);
                var user = db.Users.Single(i => i.Email == email);

                var like = new LikeModel
                {
                    FormalBlogModelId = post.FormalBlogModelId,
                    Id = user.Id
                };
                db.Likes.Add(like);
                db.SaveChanges();
            }
            catch(Exception e) {
                throw new Exception();
            }
        }

        public void DisLikePost(int postId, string UserId)
        {
            try
            {

                var db = new ApplicationDbContext();

                var like = db.Likes.Single(i => i.FormalBlogModelId == postId && i.Id == UserId);
                db.Likes.Remove(like);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }
        public List<FormalBlogViewModel> GetPostById(int id)
        {
            {
                try
                {
                    var model = new ListFormalBlogViewModel();
                    var localDb = new ApplicationDbContext();
                    var category = _db.Categories.FirstOrDefault(c => c.CategoryModelId == id);

                    var posts = localDb.FormalBlogPosts.OrderByDescending(i => i.Timestamp).Where(i => i.Category.CategoryModelId == id).ToList();
                    var list = new List<FormalBlogViewModel>();
                    foreach (var item in posts)
                    {
                        var m = new FormalBlogViewModel
                        {
                            FilePath = item.FilePath,
                            Message = item.Message,
                            Fullname = item.User.Firstname + " " + item.User.Lastname,
                            Timestamp = item.Timestamp,
                            Title = item.Title,
                            UserId = item.User.Id,
                            PostId = item.FormalBlogModelId,
                            Category = item.Category.Type
                        };

                        list.Add(m);
                    }
                    return list;
                }
                catch (Exception exc)
                {
                    throw new Exception();
                }
            }
        }

        public void SaveComment(string postId, string userID, string content)
        {
            var blogID = int.Parse(postId); 
            try
            {
                var user = _db.Users.FirstOrDefault(u => u.Id == userID);
                var post = _db.FormalBlogPosts.Find(blogID);
                var answer = new FormalCommentModel {
                    BlogModel = post,
                    BlogID = post.FormalBlogModelId,
                    userId = user.Id,
                    Message = content,
                    Timestamp = DateTime.Now
            };
               
                _db.FormalComments.Add(answer);
                _db.SaveChanges();
            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }
            
        }

    }
    public static class StringExtensions
    {
        public static string Mask(this string str)
        {
            var characters = str.ToCharArray();
            var maskedString = "";
            for (var i = 1; i < characters.Count() - 1; i++)
            {
                maskedString += "*";
            }
            return str.Substring(0, 1) + maskedString + str[str.Length - 1];

        }
    }
}