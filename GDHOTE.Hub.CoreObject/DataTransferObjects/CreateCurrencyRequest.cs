using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class CreateCurrencyRequest
    {
        [Required]
        [Display(Name = "Code")]
        public string CurrencyCode { get; set; }
        [Required]
        [Display(Name = "Currency")]
        public string Name { get; set; }
    }
}
