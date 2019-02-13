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
    }
}