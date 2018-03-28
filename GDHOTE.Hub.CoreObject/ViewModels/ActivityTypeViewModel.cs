using System;
using System.Collections.Generic;
using System.Text;
using GDHOTE.Hub.CoreObject.Models;
using NPoco;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    [TableName("vx_HUB_ActivityTypes")]
    public class ActivityTypeViewModel : ActivityType
    {
        public string Status { get; set; }
        public string Dependency { get; set; }
    }
}
