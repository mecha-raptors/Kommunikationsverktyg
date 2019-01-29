using Kommunikationsverktyg.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Kommunikationsverktyg.Controllers.Api
{
    [RoutePrefix("api/roles")]
    public class AdminApiController : ApiController
    {
        private RoleRepository _rolemanager = new RoleRepository();

        [Route("add")]
        [HttpPost]
        public IHttpActionResult AddAndRemoveRoles(List<string> userid)
        {
            try
            {
                _rolemanager.AddUserToRole(userid[0], "user");
                _rolemanager.RemoveUserFromRole(userid[0], "pending");
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
