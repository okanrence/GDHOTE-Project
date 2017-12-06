﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GDHOTE.Hub.Core.BusinessLogic;
using GDHOTE.Hub.Core.Services;

namespace GDHOTE.Hub.Mvc.ApiControllers
{
    [RoutePrefix(ConstantManager.ApiDefaultNamespace + "role")]
    public class RoleController : ApiController
    {
        [Route("getroles")]
        public IHttpActionResult GetCountries()
        {
            var roles = RoleService.GetRoles().ToList();
            if (roles.Count == 0) return NotFound();
            return Ok(roles);
        }
        [Route("getrole")]
        public IHttpActionResult GetRole(string id)
        {
            var role = RoleService.GetRole(id);
            if (role == null) return NotFound();
            return Ok(role);
        }

        [HttpDelete]
        [Route("deleterole")]
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