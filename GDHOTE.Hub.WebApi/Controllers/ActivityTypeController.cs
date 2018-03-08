using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GDHOTE.Hub.BusinessCore.BusinessLogic;
using GDHOTE.Hub.BusinessCore.Services;

namespace GDHOTE.Hub.WebApi.Controllers
{
    [RoutePrefix(ConstantManager.ApiDefaultNamespace + "activitytype")]
    public class ActivityTypeController : ApiController
    {
        [Route("get-activity-types")]
        public IHttpActionResult GetActivityTypes()
        {
            var activityTypes = ActivityTypeService.GetActivityTypes().ToList();
            if (activityTypes.Count == 0) return NotFound();
            return Ok(activityTypes);
        }
        [Route("get-activity-type")]
        public IHttpActionResult GetActivityType(int id)
        {
            var activityType = ActivityTypeService.GetActivityType(id);
            if (activityType == null) return NotFound();
            return Ok(activityType);
        }
        [Route("delete-activity-type")]
        public IHttpActionResult DeleteActivityType(int id)
        {
            var activityTypeInDb = ActivityTypeService.GetActivityType(id);
            if (activityTypeInDb == null) return NotFound();
            var result = ActivityTypeService.Delete(id);
            return Ok(result);
        }
    }
}
