using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Dtos;
using GDHOTE.Hub.Core.Services;

namespace GDHOTE.Hub.Core.BusinessLogic
{
    public class ApproveMemberManager
    {
        public static string ApproveMember(MemberDto memberDto)
        {
            string result = "";
            string sex = memberDto.Sex;
            var member = MemberService.GetMember(memberDto.MemberKey);
            string nextsequence = SequenceManager.ReturnNextSequence(memberDto.Sex);
            if (string.IsNullOrEmpty(nextsequence)) return "";

            member.MemberCode = sex + nextsequence;
            member.ApprovedBy = memberDto.ApprovedBy;
            member.ApprovedFlag = "Y";
            member.ApprovedDate = DateTime.Now;
            member.LastUpdatedDate = DateTime.Now;

            result = MemberService.Update(member);
            return result;
        }
    }
}
