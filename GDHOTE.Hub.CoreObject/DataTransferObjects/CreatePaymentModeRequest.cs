using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class CreatePaymentModeRequest
    {
        [Required(ErrorMessage = "Please specify Mode of Payment")]
        [Display(Name = "Mode of Payment")]
        public string Name { get; set; }
    }
}
