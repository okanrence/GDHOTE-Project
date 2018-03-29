using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class CreateMemberStatusRequest
    {
        [Required]
        [Display(Name = "Member Status")]
        public string Name { get; set; }
    }
}
