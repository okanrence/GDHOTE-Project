using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.CoreObject.Models
{
    [TableName("HUB_MembershipStatus")]
    public class MembershipStatus
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Status Code")]
        public string StatusCode { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string StatusDescription { get; set; }
    }
}
