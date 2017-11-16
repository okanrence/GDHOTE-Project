using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Models;

namespace GDHOTE.Hub.Core.Services
{
    public class StatusService
    {
        public static IEnumerable<Status> GetStatus()
        {
            var statuses = new List<Status>
            {
                new Status { Code = "A", Description = "Active"},
                new Status { Code = "D", Description = "De-activated"}
            };
            return statuses;
        }
    }
}
