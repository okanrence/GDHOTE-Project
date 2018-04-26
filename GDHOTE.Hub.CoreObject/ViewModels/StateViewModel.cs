using GDHOTE.Hub.CoreObject.Models;
using NPoco;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    [TableName("vx_HUB_States")]
   public class StateViewModel : State
    {
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public string Country { get; set; }
    }
}
