﻿using GDHOTE.Hub.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDHOTE.Hub.Core.DataTransferObjects
{
    public class LoginResponse : Response
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public int UserStatusId { get; set; }
        public string RoleId { get; set; }
    }
}