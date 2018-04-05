using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class CreateAdminUserRequest
    {
        [Required(ErrorMessage = "Please specify firstname")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please specify lastname")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please specify email")]
        [EmailAddress]
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Please specify password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Please specify Role")]
        [DisplayName("Role")]
        public string RoleId { get; set; }

    }
}
