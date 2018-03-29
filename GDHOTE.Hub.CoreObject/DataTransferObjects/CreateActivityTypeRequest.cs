using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class CreateActivityTypeRequest
    {
        [Required]
        public string Name { get; set; }
        [Display(Name = "Dependency Type")]
        //[Required(AllowEmptyStrings = true)]
        public int DependencyTypeId { get; set; }
    }
}
