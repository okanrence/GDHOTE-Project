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
    [RoutePrefix(ConstantManager.ApiDefaultNamespace + "member")]
    public class MemberDetailsController : ApiController
    {
        [Route("get-members-details")]
        public IHttpActionResult GetMembersDetails()
        {
            var memberDetails = MemberDetailsService.GetMembersDetails().ToList();
            return Ok(memberDetails);
        }
        [Route("get-member-details")]
        public IHttpActionResult GetMemberDetails(int id)
        {
            var memberDetails = MemberDetailsService.GetMemberDetails(id);
            if (memberDetails == null) return NotFound();
            return Ok(memberDetails);
        }

        [HttpDelete]
        [Route("delete-member-details")]
        public IHttpActionResult DeleteMemberDetails(int id)
        {
            var memberDetailsInDb = MemberDetailsService.GetMemberDetails(id);
            if (memberDetailsInDb == null) return NotFound();
            var result = MemberDetailsService.Delete(id);
            return Ok(result);
        }
    }
}