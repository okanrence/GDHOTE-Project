using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    public class UserFormViewModel //: CreateAdminUserRequest
    {
        public IEnumerable<Role> Role { get; set; }
        public IEnumerable<UserStatus> UserStatus { get; set; }
        public User User { get; set; }
        [DisplayName("Confirm Password")]
        [DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
