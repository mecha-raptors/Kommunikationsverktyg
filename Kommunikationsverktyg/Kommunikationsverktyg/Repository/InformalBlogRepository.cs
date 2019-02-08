using Kommunikationsverktyg.Models;
using Kommunikationsverktyg.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;


namespace Kommunikationsverktyg.Repository
{
    public class InformalBlogRepository
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        public UserRepository UserRepo = new UserRepository();
        public FormalBlogRepository FormalBlogRepo = new FormalBlogRepository();

        public ListInformalBlogViewModel GetInformalPosts()
        {
            try
            {
                var model = new ListInformalBlogViewModel();
                var localDb = new ApplicationDbContext();

                var posts = localDb.InformalBlogPosts.OrderByDescending(i => i.Timestamp).ToList();
                var list = new List<InformalBlogViewModel>();
                foreach (var item in posts)
                {
                    var m = new InformalBlogViewModel
                    {
                        ImagePaths = localDb.Images.Where(i => i.InformalBlogModelId == item.InformalBlogModelId).Select(i => i.Path).ToList(),
                        Message = item.Message,
                        Fullname = item.User.Firstname + " " + item.User.Lastname,
                        Timestamp = item.Timestamp,
                        Title = item.Title,
                        UserId = item.User.Id
                    };
                    list.Add(m);
                }
                model.Posts = list;
                return model;
            }
            catch (Exception exc)
            {
                throw new Exception();
            }
        }

        public void SavePost(ListInformalBlogViewModel list)
        {
            
            try
            {
                var helper = new UserRepository();
                var images = new List<ImageModel>();
                foreach(var item in list.files)
                {
                    var img = new ImageModel
                    {
                        Path = helper.SaveImage(item)
                    };
                    images.Add(img);
                }
                var model = new InformalBlogModel
                {
                    FilePath = FormalBlogRepo.SaveFile(list.File),
                    Message = list.Message,
                    Title = list.Title,
                    Timestamp = DateTime.Now,
                    Id = list.SenderId,
                    Images = images

                };
                foreach (var file in list.File)
                {
                    if (file != null)
                    {
                        var fileModel = new FileModel
                        {
                            FilePath = SaveFile(file)
                        };
                        model.Files.Add(fileModel);
                    }
                }
                _db.InformalBlogPosts.Add(model);
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

            string filePath = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);

            string finalPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Files/"), filePath);
            file.SaveAs(finalPath);

            var userPath = @"Files/" + filePath;

            return userPath;
        }
    }
}
