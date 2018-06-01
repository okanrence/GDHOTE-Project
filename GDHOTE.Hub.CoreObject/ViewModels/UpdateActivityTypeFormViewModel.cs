using System.Collections.Generic;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    public class UpdateActivityTypeFormViewModel : UpdateActivityTypeRequest
    {
        public List<Status> Statuses { get; set; }
        public List<ActivityType> DependencyTypes { get; set; }
    }
}
