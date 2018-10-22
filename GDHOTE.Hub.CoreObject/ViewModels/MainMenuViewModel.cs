using GDHOTE.Hub.CoreObject.Models;
using NPoco;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    [TableName("vx_HUB_MainMenus")]
    public class MainMenuViewModel : MainMenu
    {
        public string Status { get; set; }
        public string CreatedBy { get; set; }
    }
}
