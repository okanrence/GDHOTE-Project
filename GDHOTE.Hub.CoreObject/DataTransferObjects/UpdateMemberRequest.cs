using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class UpdateMemberRequest : CreateMemberRequest
    {
        public int MemberKey { get; set; }
        public string MemberCode { get; set; }
        public string DeleteFlag { get; set; }
        public string ApprovedFlag { get; set; }
    }
}
