using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Models;

namespace GDHOTE.Hub.Core.ViewModels
{
    public class UserFormViewModel
    {
        public IEnumerable<Role> Role { get; set; }
        public IEnumerable<UserStatus> UserStatus { get; set; }
        public User User { get; set; }
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
