using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class AdminLoginRequest
    {
        [Required(ErrorMessage = "Enter a valid email")]
        [EmailAddress(ErrorMessage = "Please specify a valid email")]
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password, ErrorMessage = "Please specify a valid password")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
