﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.CoreObject.Models;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    public class RoleFormViewModel
    {
        public Role Role { get; set; }
        public List<Status> Statuses { get; set; }

    }
}