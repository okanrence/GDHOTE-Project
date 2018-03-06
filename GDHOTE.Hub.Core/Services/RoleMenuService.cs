using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.CoreObject.Enumerables;
namespace GDHOTE.Hub.Core.Services
{
    public class RoleMenuService : BaseService
    {
        public static string Save(RoleMenu roleMenu)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(roleMenu);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert RoleMenu";
            }
        }
        public static IEnumerable<RoleMenu> GetRoleMenus()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var roleMenus = db.Fetch<RoleMenu>().Where(c => c.Status == "A");
                    return roleMenus;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new List<RoleMenu>();
            }
        }
        public static RoleMenu GetRoleMenu(string id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var roleMenu = db.Fetch<RoleMenu>().SingleOrDefault(c => c.RoleMenuId == id);
                    return roleMenu;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new RoleMenu();
            }
        }
        public static string Update(RoleMenu roleMenu)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(roleMenu);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return "Error occured while trying to update RoleMenu";
            }
        }
        public static string Delete(string id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Delete<RoleMenu>(id);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return "Error occured while trying to delete record";
            }
        }
    }
}
