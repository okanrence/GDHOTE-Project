using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class CreateActivityRequest
    {
        [Required(ErrorMessage = "Please specify activity")]
        [Display(Name = "Activity")]
        public long ActivityTypeId { get; set; }
        [Required(ErrorMessage = "Please specify member")]
        [Display(Name = "Member")]
        public long MemberId { get; set; }
        [Required(ErrorMessage = "Please specify start date")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
