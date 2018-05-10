using GDHOTE.Hub.CoreObject.Models;
using NPoco;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    [TableName("vx_HUB_Activities")]
    public class ActivityViewModel : Activity
    {
        public string MemberCode { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string OtherNames { get; set; }
        public string Status { get; set; }
        public string ActivityType { get; set; }
        public string CreatedBy { get; set; }
    }
}
