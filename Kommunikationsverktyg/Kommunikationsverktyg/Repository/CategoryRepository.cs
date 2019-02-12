using Kommunikationsverktyg.Models;
using Kommunikationsverktyg.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kommunikationsverktyg.Repository
{
    public class CategoryRepository
    {
        public void AddCategory(string type)
        {
            if(type.Length < 3 || type.Length > 20 || IsUnique(type))
            {
                throw new Exception();
            }
            var db = new ApplicationDbContext();
            var category = new CategoryModel
            {
               Type = type.ToCapitalize() 
            };
            db.Categories.Add(category);
            db.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            var db = new ApplicationDbContext();
            var posts = db.FormalBlogPosts.Where(i => i.Category.CategoryModelId == id).ToList();
            var defaultCategory = db.Categories.Single(i => i.Type == "Övrigt");
            posts.ForEach(i => i.Category = defaultCategory);

            
            var category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
        }
        public ListCategoryViewModel FillModel()
        {
            var model = new ListCategoryViewModel();
            try {
                var db = new ApplicationDbContext();
                model.Categories = GetCategories();
                return model;
            }
            catch
            {
                model.Categories = new List<CategoryModel>();
                return model;
            }
            
        }
        public List<CategoryModel> GetCategories()
        {
            var db = new ApplicationDbContext();
            return db.Categories.OrderBy(i => i.Type).ToList();
        }
        private bool IsUnique(string str)
        { 
            var db = new ApplicationDbContext();
            var type = str.ToCapitalize();
            return db.Categories.Any(i => i.Type == type);
        }
        public void FollowCategory(string user, int categoryId)
        {
            var follower = new FollowersModel
            {
                CategoryModelId = categoryId,
                Id = user
            };
            var db = new ApplicationDbContext();
            db.Followers.Add(follower);
            db.SaveChanges();

        }
        public void UnfollowCategory(string user, int categoryId)
        {
            var db = new ApplicationDbContext();
            var follower = db.Followers.Single(f => f.Id == user && f.CategoryModelId == categoryId);

            db.Followers.Remove(follower);
            db.SaveChanges();
        }

    }
}
public static class StringExtensions
{
    public static string ToCapitalize(this string str)
    {
        return str.Substring(0, 1).ToUpper() + str.Substring(1).ToLower();
    }

}