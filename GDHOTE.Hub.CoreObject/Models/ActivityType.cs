using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.CoreObject.Models
{
    [TableName("HUB_ActivityTypes")]
    [PrimaryKey("ActivityTypeId")]
    public class ActivityType : BaseModel
    {
        public int ActivityTypeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Display(Name = "Dependency Type")]
        [Required(AllowEmptyStrings = true)]
        public int DependencyTypeId { get; set; }
    }
}
