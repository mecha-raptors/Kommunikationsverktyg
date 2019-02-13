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
    [RoutePrefix("api/blog")]
    public class FormalBlogApiController : ApiController
    {
        [Route("delete")]
        [HttpPost]
        public IHttpActionResult RemovePost(List<string> blogid)
        {
            try
            {
                var helper = new FormalBlogRepository();
                helper.DeletePost(int.Parse(blogid[0]));
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("filter")]
        [HttpPost]
        public IHttpActionResult GetPostsById(List<string> id)
        {
            try
            {
                var helper = new FormalBlogRepository();
                var model = helper.GetPostById(int.Parse(id[0]));
                return Ok(model);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("like")]
        [HttpPost]
        public IHttpActionResult LikePost(List<string> postId)
        {
            try
            {
                var helper = new FormalBlogRepository();
                helper.LikePost(int.Parse(postId[0]), postId[1]);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("comment")]
        [HttpPost]
        public IHttpActionResult Comment (List<string> funThings)
        {
            try
            {
                var helper = new FormalBlogRepository();
                helper.SaveComment(funThings[0], funThings[1], funThings[2]);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("dislike")]
        [HttpPost]
        public IHttpActionResult DislikePost([FromBody]string id)
        {
            try
            {
                var helper = new FormalBlogRepository();
                helper.DisLikePost(int.Parse(id), User.Identity.GetUserId());
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
