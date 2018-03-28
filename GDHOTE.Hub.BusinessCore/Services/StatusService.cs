using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.CoreObject.Models;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class StatusService
    {
        public static List<Status> GetStatuses()
        {
            var statuses = new List<Status>
            {
                new Status { Id = 1, Name = "Active"},
                new Status { Id = 2, Name = "De-activated"}
            };
            return statuses;
        }
    }
}
