using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class CreateBankRequest
    {
        [Required(ErrorMessage = "Please specify Bank")]
        [Display(Name = "Bank")]
        public string Name { get; set; }
    }
}
