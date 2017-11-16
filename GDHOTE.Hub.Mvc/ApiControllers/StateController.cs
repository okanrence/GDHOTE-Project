using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GDHOTE.Hub.Core.BusinessLogic;
using GDHOTE.Hub.Core.Models;
using GDHOTE.Hub.Core.Services;

namespace GDHOTE.Hub.Mvc.ApiControllers
{
    [RoutePrefix(ConstantManager.ApiDefaultNamespace + "state")]
    public class StateController : ApiController
    {
        [Route("getstates")]
        public IHttpActionResult GetStates()
        {
            var states = StateService.GetStates().ToList();
            if (states.Count == 0) return NotFound();
            return Ok(states);
        }
        [Route("getstate")]
        public IHttpActionResult GetState(int id)
        {
            var state = StateService.GetState(id);
            if (state == null) return NotFound();
            return Ok(state);
        }

        [HttpDelete]
        [Route("deletestate")]
        public IHttpActionResult DeleteState(int id)
        {
            var stateInDb = StateService.GetState(id);
            if (stateInDb == null) return NotFound();
            var result = StateService.Delete(id);
            return Ok(result);
        }
    }
}
