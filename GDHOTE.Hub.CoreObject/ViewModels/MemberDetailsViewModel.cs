using System;
using System.Collections.Generic;
using System.Text;
using GDHOTE.Hub.CoreObject.Models;
using NPoco;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    [TableName("vx_HUB_MemberDetails")]
    public class MemberDetailsViewModel : MemberDetails
    {
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string MemberCode { get; set; }
        public string CreatedBy { get; set; }

    }
}
