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
    [RoutePrefix(ConstantManager.ApiDefaultNamespace + "activitytype")]

    public class ActivityTypeController : ApiController
    {

        [Route("getactivitytypes")]
        public IHttpActionResult GetActivityTypes()
        {
            var activityTypes = ActivityTypeService.GetActivityTypes().ToList();
            if (activityTypes.Count == 0) return NotFound();
            return Ok(activityTypes);
        }
        [Route("getactivitytype")]
        public IHttpActionResult GetActivityType(int id)
        {
            var activityType = ActivityTypeService.GetActivityType(id);
            if (activityType == null) return NotFound();
            return Ok(activityType);
        }
        [Route("deleteactivitytype")]
        public IHttpActionResult DeleteActivityType(int id)
        {
            var activityTypeInDb = ActivityTypeService.GetActivityType(id);
            if (activityTypeInDb == null) return NotFound();
            var result = ActivityTypeService.Delete(id);
            return Ok(result);
        }
    }
}
