﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Models;

namespace GDHOTE.Hub.Core.Services
{
    public class RoleSubMenuViewService : BaseService
    {
        public static IEnumerable<RoleSubMenuView> GetRoleMenu()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var roleSubMenus = db.Fetch<RoleSubMenuView>();
                    return roleSubMenus;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new List<RoleSubMenuView>();
            }
        }
        public static IEnumerable<RoleSubMenuView> GetRoleMenuByRole(string roleId)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var roleSubMenus = db.Fetch<RoleSubMenuView>().Where(r => r.RoleId == roleId);
                    return roleSubMenus;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new List<RoleSubMenuView>();
            }
        }
    }
}