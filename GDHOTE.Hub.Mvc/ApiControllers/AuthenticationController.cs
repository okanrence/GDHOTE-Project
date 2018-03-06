using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using GDHOTE.Hub.BusinessCore.BusinessLogic;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.BusinessCore.Services;

namespace GDHOTE.Hub.Mvc.ApiControllers
{
    [RoutePrefix(ConstantManager.ApiDefaultNamespace + "user")]
    public class AuthenticationController : ApiController
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
