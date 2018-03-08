using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GDHOTE.Hub.BusinessCore.BusinessLogic;
using GDHOTE.Hub.BusinessCore.Services;
using GDHOTE.Hub.CoreObject.DataTransferObjects;

namespace GDHOTE.Hub.WebApi.Controllers
{
    [RoutePrefix(ConstantManager.ApiDefaultNamespace + "user")]
    public class UserController : ApiController
    {
        [HttpPost]
        [Route("authenticate-user")]
        public IHttpActionResult AuthenticationUser(LoginRequest model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var response = UserService.AuthenticateUser(model);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
