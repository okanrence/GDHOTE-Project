﻿
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GDHOTE.Hub.BusinessCore.BusinessLogic;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.BusinessCore.Exceptions;
using GDHOTE.Hub.BusinessCore.Services;

namespace GDHOTE.Hub.Mvc.ApiControllers
{
    [RoutePrefix(ConstantManager.ApiDefaultNamespace + "member")]
    public class MemberController : ApiController
    {
        [Route("get-members")]
        public IHttpActionResult GetMembers()
        {
            var members = MemberService.GetMembers().ToList();
            return Ok(members);
        }
        [Route("get-member")]
        public IHttpActionResult GetMember(int id)
        {
            var member = MemberService.GetMember(id);
            if (member == null) return NotFound();
            return Ok(member);
        }

        [HttpDelete]
        [Route("delete-member")]
        public IHttpActionResult DeleteMember(int id)
        {
            var memberInDb = MemberService.GetMember(id);
            if (memberInDb == null) return NotFound();
            var result = MemberService.Delete(id);
            return Ok(result);
        }
        [HttpPost]
        [Route("create-member")]
        public IHttpActionResult CreateNewMember(CreateMemberRequest memberRequest)
        {
            //var req = new EmailRequest
            //{
            //    Type = EmailType.RegistrationConfirmation,
            //    RecipientEmailAddress = "abrabfun@yahoo.com",
            //    Data = new Hashtable
            //    {
            //        ["Subject"] = "Welcome to " + BaseService.Get("settings.organisation.name"),
            //        ["FirstName"] = "Olufunso",
            //        ["LastName"] = "Olufunso",
            //    }
            //};
            //EmailManager.SendForEmailConfirmation(req);

            try
            {
                string headerKey = "channel";
                var headers = Request.Headers.GetValues(headerKey);
                var headerValue = headers.FirstOrDefault();

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                string currentUser = "";
                //int channelCode = 0;
                if (!int.TryParse(headerValue, out var channelCode)) channelCode = 3;

                var result = MemberManager.CreateMember(memberRequest, currentUser, channelCode);
                if (result == null) return BadRequest();
                return Ok(result);
            }
            catch (InvalidRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnableToCompleteException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("approve-member")]
        public IHttpActionResult ApproveMember(UpdateMemberRequest updateRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = MemberManager.ApproveMember(updateRequest);
                if (string.IsNullOrEmpty(result)) return BadRequest();
                if (string.IsNullOrEmpty(result)) return InternalServerError();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}