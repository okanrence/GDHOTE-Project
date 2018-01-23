using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.DataTransferObjects;
using GDHOTE.Hub.Core.Models;

namespace GDHOTE.Hub.Core.ViewModels
{
    public class MemberFormViewModel : CreateMemberRequest
    {
        public List<Gender> Genders { get; set; }
        public List<MaritalStatus> MaritalStatuses { get; set; }
    }
}
