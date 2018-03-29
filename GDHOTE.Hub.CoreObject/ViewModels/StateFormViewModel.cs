using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    public class StateFormViewModel : CreateStateRequest
    {
        public List<Country> Countries { get; set; }
        public List<Status> Statuses { get; set; }

    }
}
