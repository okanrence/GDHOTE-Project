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
    [RoutePrefix(ConstantManager.ApiDefaultNamespace + "membershipstatus")]
    public class MembershipStatusController : ApiController
    {
        [Route("get-membership-statuses")]
        public IHttpActionResult GetMembershipStatus()
        {
            var membershipStatuses = MembershipStatusService.GetCMembershipStatuses().ToList();
            if (membershipStatuses.Count == 0) return NotFound();
            return Ok(membershipStatuses);
        }
        [Route("get-membership-status")]
        public IHttpActionResult GetMembershipStatus(int id)
        {
            var membershipStatus = MembershipStatusService.GetMembershipStatus(id);
            if (membershipStatus == null) return NotFound();
            return Ok(membershipStatus);
        }

        [HttpDelete]
        [Route("delete-membership-status")]
        public IHttpActionResult DeleteMembershipStatus(int id)
        {
            var membershipStatusInDb = MembershipStatusService.GetMembershipStatus(id);
            if (membershipStatusInDb == null) return NotFound();
            var result = MembershipStatusService.Delete(id);
            return Ok(result);
        }
    }
}
