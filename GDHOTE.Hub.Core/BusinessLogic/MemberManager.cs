﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.Core.Services;
using Newtonsoft.Json;
using GDHOTE.Hub.CoreObject.Enumerables;
using System.Collections;

namespace GDHOTE.Hub.Core.BusinessLogic
{
    public class MemberManager
    {
        public static CreateMemberResponse CreateMember(CreateMemberRequest createRequest, string currentUser, int channelCode)
        {
            var request = JsonConvert.SerializeObject(createRequest);
            var member = JsonConvert.DeserializeObject<Member>(request);
            var createResponse = new CreateMemberResponse();


            member.CreatedBy = currentUser;
            member.ChannelCode = channelCode;
            member.StatusCode = "A";
            member.DeleteFlag = "N";
            member.ApprovedFlag = "N";
            member.RecordDate = DateTime.Now;
            member.PostedDate = DateTime.Now;
            member.OfficerId = (int)OfficerType.NormalMember;
            member.OfficerDate = DateTime.Now;

            //Check user has not been profiled before
            var memberExist = MemberService.CheckIfMemberExist(member);
            if (memberExist != null)
            {
                createResponse.ErrorCode = "01";
                createResponse.ErrorMessage = "Member already exist";
                return createResponse;
            }

            var result = MemberService.Save(member);

            //Insert mobile details
            if (result != null)
            {
                if (int.TryParse(result, out var memberKey))
                {
                    var memberDetails = new MemberDetails
                    {
                        MemberKey = memberKey,
                        MobileNumber = createRequest.MobileNumber,
                        EmailAddress = createRequest.EmailAddress,
                        CreatedBy = member.CreatedBy,
                        RecordDate = DateTime.Now,
                        PostedDate = DateTime.Now,
                    };

                    var detailsResult = MemberDetailsService.Save(memberDetails);
                    createResponse.ErrorCode = "00";
                    createResponse.ErrorMessage = "Successful";
                    createResponse.Reference = memberKey.ToString();

                    //Notify member
                    new Task(() =>
                    {
                        var req = new EmailRequest
                        {
                            Type = EmailType.RegistrationConfirmation,
                            RecipientEmailAddress = createRequest.EmailAddress,
                            Data = new Hashtable
                            {
                                ["Subject"] = "Welcome to " + BaseService.Get("settings.organisation.name"),
                                ["FirstName"] = createRequest.FirstName,
                                ["LastName"] = createRequest.Surname,
                            }
                        };

                        try
                        {
                            EmailManager.SendForEmailConfirmation(req);
                        }
                        catch (Exception ex)
                        {

                        }
                    }).Start();
                }
            }


            return createResponse;
        }

        public static string ApproveMember(UpdateMemberRequest updateRequest)
        {
            string result = "";
            string gender = updateRequest.Gender;
            var member = MemberService.GetMember(updateRequest.MemberKey);
            string nextsequence = SequenceManager.ReturnNextSequence(updateRequest.Gender);
            if (string.IsNullOrEmpty(nextsequence)) return "";

            member.MemberCode = gender + nextsequence;
            member.ApprovedBy = updateRequest.ApprovedBy;
            member.ApprovedFlag = "Y";
            member.ApprovedDate = DateTime.Now;
            member.LastUpdatedDate = DateTime.Now;

            result = MemberService.Update(member);
            return result;
        }
    }
}
