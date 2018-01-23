using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.DataTransferObjects;
using GDHOTE.Hub.Core.Models;
using GDHOTE.Hub.Core.Services;
using Newtonsoft.Json;

namespace GDHOTE.Hub.Core.BusinessLogic
{
    public class MemberManager
    {
        public static CreateMemberResponse CreateMember(CreateMemberRequest createRequest, string currentUser)
        {
            var request = JsonConvert.SerializeObject(createRequest);
            var member = JsonConvert.DeserializeObject<Member>(request);
            var createResponse = new CreateMemberResponse();

           
            member.CreatedBy = currentUser;
            member.StatusCode = "A";
            member.DeleteFlag = "N";
            member.ApprovedFlag = "N";
            member.RecordDate = DateTime.Now;
            member.PostedDate = DateTime.Now;
            member.OfficerId = (int)EnumsService.OfficerType.NormalMember;
            member.OfficerDate = DateTime.Now;

            //Check user has not been profiled before
            var memberExist = MemberService.CheckIfMemberExist(member);
            if(memberExist != null)
            {
                createResponse.ErrorCode = "01";
                createResponse.ErrorMessage = "Member already exist";
                return createResponse;
            }

            var result = MemberService.Save(member);

            //Insert mobile details
            if (result != null)
            {
                int memberKey = 0;
                if (int.TryParse(result, out memberKey))
                {
                    var memberDetails = new MemberDetails
                    {
                        MemberKey = memberKey,
                        MobileNumber = createRequest.MobileNumber,
                        CreatedBy = member.CreatedBy,
                        RecordDate = DateTime.Now,
                        PostedDate = DateTime.Now,
                    };
                    var detailsResult = MemberDetailsService.Save(memberDetails);
                    createResponse.ErrorCode = "00";
                    createResponse.ErrorMessage = "Sucessful";
                    createResponse.Reference = memberKey.ToString();
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
