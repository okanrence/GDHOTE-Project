using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    public class MemberDetailsFormModel : CreateMemberDetailsRequest
    {
        public List<CountryResponse> Countries { get; set; }
        public List<State> States { get; set; }
        public List<YearGroup> YearGroups { get; set; }

    }
}
