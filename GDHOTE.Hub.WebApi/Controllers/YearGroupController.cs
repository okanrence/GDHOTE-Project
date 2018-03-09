﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GDHOTE.Hub.BusinessCore.BusinessLogic;
using GDHOTE.Hub.BusinessCore.Services;

namespace GDHOTE.Hub.WebApi.Controllers
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