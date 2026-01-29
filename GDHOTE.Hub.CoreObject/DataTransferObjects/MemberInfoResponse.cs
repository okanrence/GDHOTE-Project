using System;
using System.Collections.Generic;
using System.Text;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.CoreObject.ViewModels;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class MemberInfoResponse
    {
        public MemberViewModel Member { get; set; }
        public MemberDetailsViewModel MemberDetails { get; set; }
        public List<ActivityViewModel> Activities { get; set; }
    }

    
}
