using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class UpdateAccrualTypeRequest
    {
        [Required(ErrorMessage = "Please specify Accrual Type")]
        [Display(Name = "Accrual Type")]
        public string Name { get; set; }
        public int Id { get; set; }
        public int StatusId { get; set; }
    }
}
