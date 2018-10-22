using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.CoreObject.Models
{
    [TableName("HUB_MainMenus")]
    public class MainMenu : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MenuKey { get; set; }
        public int StatusId { get; set; }
        public int DisplaySequence { get; set; }
    }
}
