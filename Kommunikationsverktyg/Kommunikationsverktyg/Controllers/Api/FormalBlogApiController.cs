﻿using Kommunikationsverktyg.Repository;
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
    }
}
