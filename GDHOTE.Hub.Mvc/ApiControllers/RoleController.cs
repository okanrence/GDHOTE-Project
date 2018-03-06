using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GDHOTE.Hub.BusinessCore.BusinessLogic;
using GDHOTE.Hub.BusinessCore.Services;

namespace GDHOTE.Hub.Mvc.ApiControllers
{
    [RoutePrefix(ConstantManager.ApiDefaultNamespace + "role")]
    public class RoleController : ApiController
    {
        [Route("get-roles")]
        public IHttpActionResult GetRoles()
        {
            var roles = RoleService.GetRoles().ToList();
            if (roles.Count == 0) return NotFound();
            return Ok(roles);
        }
        [Route("get-role")]
        public IHttpActionResult GetRole(string id)
        {
            var role = RoleService.GetRole(id);
            if (role == null) return NotFound();
            return Ok(role);
        }

        [HttpDelete]
        [Route("delete-role")]
        public IHttpActionResult DeleteRole(string id)
        {
            var roleInDb = RoleService.GetRole(id);
            if (roleInDb == null) return NotFound();
            //var result = RoleService.Delete(id);
            var result = RoleService.Delete(roleInDb);
            return Ok(result);
        }
    }
}
