using Kommunikationsverktyg.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Kommunikationsverktyg.Controllers.Api
{
    [RoutePrefix("api/informalblog")]
    public class InformalBlogApiController : ApiController
    {
        [Route("comment")]
        [HttpPost]
        public IHttpActionResult Comment(List<string> funThings)
        {
            try
            {
                var helper = new InformalBlogRepository();
                helper.SaveComment(funThings[0], funThings[1], funThings[2]);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

    }
       
}