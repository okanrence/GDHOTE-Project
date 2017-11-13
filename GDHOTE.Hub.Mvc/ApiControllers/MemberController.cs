using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GDHOTE.Hub.Core.BusinessLogic;
using GDHOTE.Hub.Core.Dtos;
using GDHOTE.Hub.Core.Services;

namespace GDHOTE.Hub.Mvc.ApiControllers
{
    [RoutePrefix(ConstantManager.ApiDefaultNamespace + "member")]
    public class MemberController : ApiController
    {
        [Route("getmembers")]
        public IHttpActionResult GetMembers()
        {
            var members = MemberService.GetMembers().ToList();
            return Ok(members);
        }
        [Route("getmember")]
        public IHttpActionResult GetMember(int id)
        {
            var member = MemberService.GetMember(id);
            if (member == null) return NotFound();
            return Ok(member);
        }

        [HttpDelete]
        [Route("deletemember")]
        public IHttpActionResult DeleteMember(int id)
        {
            var memberInDb = MemberService.GetMember(id);
            if (memberInDb == null) return NotFound();
            var result = MemberService.Delete(id);
            return Ok(result);
        }
        [HttpPost]
        [Route("approvemember")]
        public IHttpActionResult ApproveMember(MemberDto memberDto)
        {
            int id = 19;
            var member = MemberService.GetMember(id);
            if (member == null) return NotFound();
            return Ok(member);
        }
    }
}
