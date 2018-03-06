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
    public class ActivityType
    {
        public int ActivityTypeId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Status { get; set; }
        [Display(Name = "Dependency Type")]
        [Required(AllowEmptyStrings = true)]
        public int DependencyTypeId { get; set; }
        public DateTime RecordDate { get; set; }
    }
}
