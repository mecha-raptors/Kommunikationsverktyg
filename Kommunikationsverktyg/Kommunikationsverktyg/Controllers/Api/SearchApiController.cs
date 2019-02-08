using Kommunikationsverktyg.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Kommunikationsverktyg.Controllers.Api
{
    [System.Web.Http.RoutePrefix("api/search")]
    public class SearchApiController : ApiController
    {
        //Hämtar alla användare.
        [System.Web.Http.Route("users")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                var currUser = User.Identity.GetUserId();
                ApplicationDbContext db = new ApplicationDbContext();
                var users = db.Users.Where(u => u.Id != currUser).ToList();
                return Ok(users);
            }
            catch
            {
                return BadRequest();
            }

        }


    }
}