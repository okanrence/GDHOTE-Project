using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GDHOTE.Hub.Core.Models;
using GDHOTE.Hub.Core.Services;

namespace GDHOTE.Hub.Mvc.ApiControllers
{
    //[RoutePrefix("api/state")]
    public class StateController : ApiController
    {
        public IHttpActionResult GetStates()
        {
            var states = new List<State>();
            states = StateService.GetStates().ToList();
            if (states.Count == 0) return NotFound();
            return Ok(states);
        }
        public IHttpActionResult GetState(int id)
        {
            var state = StateService.GetState(id);
            if (state == null) return NotFound();
            return Ok(state);
        }

        [HttpDelete]
        public IHttpActionResult DeleteState(int id)
        {
            var stateInDb = StateService.GetState(id);
            if (stateInDb == null) return NotFound();
            var result = StateService.Delete(id);
            return Ok(result);
        }
    }
}
