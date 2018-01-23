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
    [RoutePrefix(ConstantManager.ApiDefaultNamespace + "yeargroup")]

    public class YearGroupController : ApiController
    {
        [Route("get-year-groups")]
        public IHttpActionResult GetYearGroups()
        {
            var yearGroups = YearGroupService.GetActiveYearGroups().ToList();
            if (yearGroups.Count == 0) return NotFound();
            return Ok(yearGroups);
        }
        [Route("get-year-group")]
        public IHttpActionResult GetYearGroup(int id)
        {
            var yearGroup = YearGroupService.GetYearGroup(id);
            if (yearGroup == null) return NotFound();
            return Ok(yearGroup);
        }

        [HttpDelete]
        [Route("delete-year-group")]
        public IHttpActionResult DeleteYearGroup(int id)
        {
            var yearGroupInDb = YearGroupService.GetYearGroup(id);
            if (yearGroupInDb == null) return NotFound();
            var result = YearGroupService.Delete(id);
            return Ok(result);
        }
    }
}
