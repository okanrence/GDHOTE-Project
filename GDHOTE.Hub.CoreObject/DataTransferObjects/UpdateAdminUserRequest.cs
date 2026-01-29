using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class UpdateAdminUserRequest
    {
        public string UserId { get; set; }
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
        [Required(ErrorMessage = "Please specify Role")]
        [DisplayName("Role")]
        public int RoleId { get; set; }
        [Required(ErrorMessage = "Please specify user status")]
        [DisplayName("User Status")]
        public int UserStatusId { get; set; }
    }
}
