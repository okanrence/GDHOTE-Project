using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.Core.Models
{
    [TableName("HUB_MainMenus")]
    [PrimaryKey("MenuId", AutoIncrement = false)]
    public class MainMenu
    {
        public string MenuId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public string DisplaySequence { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
