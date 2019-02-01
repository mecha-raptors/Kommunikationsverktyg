using Kommunikationsverktyg.Models;
using Kommunikationsverktyg.Models.ViewModels;
using System;
using System.Collections.Generic;
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
                        PostId = item.FormalBlogModelId
                    };
                    list.Add(m);
                }
                model.Posts = list;
                return model;
            }
            catch(Exception exc) {
                throw new Exception();
            }
        }
        public void SavePost(ListFormalBlogViewModel list) {
            try
            {
                var model = new FormalBlogModel
                {
                    FilePath = SaveFile(list.File),
                    Message = list.Message,
                    Title = list.Title,
                    Timestamp = DateTime.Now,
                    Id = list.SenderId

                };
                _db.FormalBlogPosts.Add(model);
                _db.SaveChanges();
            }
            catch (Exception ex) {
                throw new Exception();
            }


        }
        public string SaveFile(HttpPostedFileBase file)
        {
            if (file == null) {
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
    }
}