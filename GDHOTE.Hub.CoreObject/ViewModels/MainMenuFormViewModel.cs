using System.Collections.Generic;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    public class MainMenuFormViewModel : CreateMainMenuRequest
    {
        public List<Status> Statuses { get; set; }
    }
}
