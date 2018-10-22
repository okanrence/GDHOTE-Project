using System.Collections.Generic;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    public class MemberFormViewModel : CreateMemberRequest
    {
        public List<Gender> Genders { get; set; }
        public List<MaritalStatus> MaritalStatuses { get; set; }
    }
}
