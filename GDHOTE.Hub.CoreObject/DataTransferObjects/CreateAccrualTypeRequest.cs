using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class CreateAccrualTypeRequest
    {
        [Required(ErrorMessage = "Please specify Accrual Type")]
        [Display(Name = "Accrual Type")]
        public string Name { get; set; }
        //public int Period { get; set; }
    }
}
