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
                var likers = new List<ApplicationUser>();

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
                        Likes = item.Likes,
                        Likers = item.Likers
             
                    };

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
                    Message = FilterContent(list.Message),
                    Title = FilterContent(list.Title),
                    Timestamp = DateTime.Now,
                    User = user,
                    Category = category
                };
                _db.FormalBlogPosts.Add(model);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception();
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

        public string FilterContent(string content)
        {
            var words = content.Split(' ');
            var badWords = _db.BadWords.ToList();
            string filteredContent = "";
            foreach (var item in words)
            {
                var IsComitted = false;
                foreach (var badword in badWords)
                {
                    if (item.ToLower().Contains(badword.Word))
                    {
                        filteredContent += item.Mask() + " ";
                        IsComitted = true;
                        break;
                    }

                }
                if (!IsComitted)
                {
                    filteredContent += item + " ";
                }
            }
            return filteredContent;
        }


        public void DeletePost(int id)
        {

            try
            {
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

        public void LikePost(int postId, string user)
        {
            try
            {
               
                var db = new ApplicationDbContext();
                var model = new LikeModel();
                var post = db.FormalBlogPosts.Find(postId);
                var liker = db.Users.FirstOrDefault(u => u.Email == user);
                var result = from u in db.Likes
                             where u.FormalPosts == post && u.Users == liker
                             select u;
                
                if (result != null)
                {
                    //model.FormalPosts = post; 
                    //model.Users = liker; 
                    //db.Likes.Add(model);
                    post.Likes++;
                    post.Likers.Add(liker);
                    db.SaveChanges();
                }
            }
            catch(Exception e) {
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