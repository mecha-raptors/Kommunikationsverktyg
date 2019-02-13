using Kommunikationsverktyg.Repository;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Kommunikationsverktyg.Controllers.Api
{
    [RoutePrefix("api/category")]
    public class CategoryApiController : ApiController
    {
        [Route("add")]
        [HttpPost]
        public IHttpActionResult AddCategory(List<string> content)
        {
            try
            {
                var helper = new CategoryRepository();

                helper.AddCategory(content[0]); 
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [Route("delete")]
        [HttpPost]
        public IHttpActionResult RemoveCategory(List<string> id)
        {
            try
            {
                var helper = new CategoryRepository();

                helper.DeleteCategory(int.Parse(id[0]));
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("follow")]
        [HttpPost]
        public IHttpActionResult FollowCategory([FromBody]string id)
        {
            try
            {
                var helper = new CategoryRepository();

                helper.FollowCategory(User.Identity.GetUserId(), int.Parse(id));
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("unfollow")]
        [HttpPost]
        public IHttpActionResult UnfollowCategory([FromBody]string id)
        {
            try
            {
                var helper = new CategoryRepository();

                helper.UnfollowCategory(User.Identity.GetUserId(), int.Parse(id));
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
