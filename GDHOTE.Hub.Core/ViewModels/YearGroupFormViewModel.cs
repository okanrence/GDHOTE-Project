﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Models;

namespace GDHOTE.Hub.Core.ViewModels
{
    public class YearGroupFormViewModel
    {
        public YearGroup YearGroup { get; set; }
        public List<Status> Status { get; set; }
    }
}