using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class CreateYearGroupRequest
    {
        [Required]
        [Display(Name = "Year Group")]
        public string Name { get; set; }
    }
}
