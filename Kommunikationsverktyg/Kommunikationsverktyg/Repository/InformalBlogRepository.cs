﻿using Kommunikationsverktyg.Models;
using Kommunikationsverktyg.Models.ViewModels;
using System;
using System.Collections.Generic;
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
                        FilePath = item.FilePath,
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
                var model = new InformalBlogModel
                {
                    FilePath = FormalBlogRepo.SaveFile(list.File),
                    Message = list.Message,
                    Title = list.Title,
                    Timestamp = DateTime.Now,
                    Id = list.SenderId

                };
                _db.InformalBlogPosts.Add(model);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}