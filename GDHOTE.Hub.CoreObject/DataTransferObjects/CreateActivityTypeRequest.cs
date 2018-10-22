using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using NPoco;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class CreateActivityTypeRequest
    {
        [Required(ErrorMessage = "Please specify Activity Type")]
        [Display(Name = "Activity Type")]
        public string Name { get; set; }
        [Display(Name = "Dependency Type")]
        public int DependencyTypeId { get; set; }
    }
}
