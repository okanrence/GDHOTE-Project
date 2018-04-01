using System;
using System.Collections.Generic;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class UpdateMemberDetailsRequest : CreateMemberDetailsRequest
    {
        public int Id { get; set; }
    }
}
