using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.CoreObject.Enumerables;
namespace GDHOTE.Hub.BusinessCore.Services
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
               LogService.myLog(ex.Message);
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
               LogService.myLog(ex.Message);
                return new List<RoleSubMenuView>();
            }
        }
    }
}
