using GDHOTE.Hub.CoreObject.Models;
using NPoco;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    [TableName("vx_HUB_ActivityTypes")]
    public class ActivityTypeViewModel : ActivityType
    {
        public string Status { get; set; }
        public string Dependency { get; set; }
        public string CreatedBy { get; set; }
    }
}
