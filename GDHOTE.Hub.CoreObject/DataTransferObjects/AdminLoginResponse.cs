using System;
using System.Collections.Generic;
using System.Text;
using GDHOTE.Hub.CoreObject.ViewModels;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class AdminLoginResponse : Response
    {
        public AdminUserViewModel User { get; set; }
    }
}
