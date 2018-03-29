using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class CreateCurrencyRequest
    {
        [Required(ErrorMessage = "Please specify Currency Code")]
        [Display(Name = "Currency Code")]
        public string CurrencyCode { get; set; }
        [Required(ErrorMessage = "Please specify Currency")]
        [Display(Name = "Currency")]
        public string Name { get; set; }
    }
}
