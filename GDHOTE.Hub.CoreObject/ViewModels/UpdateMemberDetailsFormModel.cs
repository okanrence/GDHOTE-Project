using System.Collections.Generic;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    public class UpdateMemberDetailsFormModel : UpdateMemberDetailsRequest
    {
        public List<CountryResponse> Countries { get; set; }
        public List<State> States { get; set; }
        public List<YearGroup> YearGroups { get; set; }

    }
}
