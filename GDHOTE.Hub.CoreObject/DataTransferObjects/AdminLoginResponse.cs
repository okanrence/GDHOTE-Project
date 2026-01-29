using System;
using System.Collections.Generic;
using System.Text;
using GDHOTE.Hub.CoreObject.ViewModels;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class AdminLoginResponse : Response
    {
        public UserViewModel User { get; set; }
    }
}
