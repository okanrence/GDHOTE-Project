using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GDHOTE.Hub.Core.BusinessLogic;
using GDHOTE.Hub.Core.Services;

namespace GDHOTE.Hub.Mvc.ApiControllers
{
    [RoutePrefix(ConstantManager.ApiDefaultNamespace + "memberdetails")]
    public class MemberDetailsController : ApiController
    {
        [Route("getmembersdetails")]
        public IHttpActionResult GetMembers()
        {
            var membersDetails = MemberDetailsService.GetMembersDetails().ToList();
            return Ok(membersDetails);
        }
        [Route("getmemberdetails")]
        public IHttpActionResult GetMember(int id)
        {
            var memberDetails = MemberDetailsService.GetMemberDetails(id);
            if (memberDetails == null) return NotFound();
            return Ok(memberDetails);
        }

        [HttpDelete]
        [Route("deletememberdetails")]
        public IHttpActionResult DeleteMember(int id)
        {
            var memberDetailsInDb = MemberDetailsService.GetMemberDetails(id);
            if (memberDetailsInDb == null) return NotFound();
            var result = MemberDetailsService.Delete(id);
            return Ok(result);
        }
    }
}
