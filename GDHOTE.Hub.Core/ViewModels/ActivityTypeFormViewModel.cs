using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Models;

namespace GDHOTE.Hub.Core.ViewModels
{
    public class ActivityTypeFormViewModel
    {
        public ActivityType ActivityType { get; set; }
        public List<Status> Status { get; set; }
        public List<ActivityType> DependencyTypes { get; set; }
    }
}
