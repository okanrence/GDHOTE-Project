﻿using System;
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
    [RoutePrefix(ConstantManager.ApiDefaultNamespace + "membershipstatus")]

    public class MembershipStatusController : ApiController
    {
        [Route("getmembershipstatuses")]
        public IHttpActionResult GetMembershipStatus()
        {
            var membershipStatuses = MembershipStatusService.GetCMembershipStatuses().ToList();
            if (membershipStatuses.Count == 0) return NotFound();
            return Ok(membershipStatuses);
        }
        [Route("getmembershipstatus")]
        public IHttpActionResult GetMembershipStatus(int id)
        {
            var membershipStatus = MembershipStatusService.GetMembershipStatus(id);
            if (membershipStatus == null) return NotFound();
            return Ok(membershipStatus);
        }

        [HttpDelete]
        [Route("deletemembershipstatus")]
        public IHttpActionResult DeleteMembershipStatus(int id)
        {
            var membershipStatusInDb = MembershipStatusService.GetMembershipStatus(id);
            if (membershipStatusInDb == null) return NotFound();
            var result = MembershipStatusService.Delete(id);
            return Ok(result);
        }
    }
}