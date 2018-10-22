using System;
using System.Collections.Generic;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class UpdateMemberDetailsRequest : CreateMemberDetailsRequest
    {
        public string MemberDetailsKey { get; set; }
        //public long Id { get; set; }
    }
}
