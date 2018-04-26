using GDHOTE.Hub.CoreObject.Models;
using NPoco;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    [TableName("vx_HUB_MemberStatuses")]
    public class MemberStatusViewModel : MemberStatus
    {
        public string Status { get; set; }
        public string CreatedBy { get; set; }
    }
}
