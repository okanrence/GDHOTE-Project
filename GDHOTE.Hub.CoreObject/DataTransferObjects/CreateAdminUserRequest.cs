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
        [DisplayName("Firstname")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please specify lastname")]
        [DisplayName("Lastname")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please specify email")]
        [EmailAddress]
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Please specify password")]
        [DataType(DataType.Password)]
        [RegularExpression("^[0-9A-Za-z@_!]+$", ErrorMessage = "Please enter a valid Password")]
        [MinLength(8, ErrorMessage = "Password should be a minimum of 8 characters")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Password does not match")]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Please specify Role")]
        [DisplayName("Role")]
        public string RoleId { get; set; }

    }
}
