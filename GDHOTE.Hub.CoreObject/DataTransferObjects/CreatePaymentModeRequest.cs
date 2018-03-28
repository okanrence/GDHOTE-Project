using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class CreatePaymentModeRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
