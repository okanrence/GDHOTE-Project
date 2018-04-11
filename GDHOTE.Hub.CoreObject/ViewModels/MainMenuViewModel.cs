﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.CoreObject.Models;
using NPoco;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    [TableName("vx_HUB_MainMenus")]
    public class MainMenuViewModel : MainMenu
    {
        public string Status { get; set; }
        public string CreatedBy { get; set; }
    }
}
