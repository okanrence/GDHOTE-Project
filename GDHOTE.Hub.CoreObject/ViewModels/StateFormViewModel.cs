using System.Collections.Generic;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    public class StateFormViewModel : CreateStateRequest
    {
        public List<CountryResponse> Countries { get; set; }
        public List<Status> Statuses { get; set; }
    }
}
