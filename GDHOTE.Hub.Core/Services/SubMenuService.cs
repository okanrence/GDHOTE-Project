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
    public class SubMenuService : BaseService
    {
        public static string Save(SubMenu subMenu)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(subMenu);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert SubMenu";
            }
        }
        public static IEnumerable<SubMenu> GetSubMenus()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var countries = db.Fetch<SubMenu>().Where(c => c.Status == "A").OrderBy(c => c.DisplaySequence);
                    return countries;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new List<SubMenu>();
            }
        }
        public static IEnumerable<SubMenu> GetSubMenusByRoleId()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var countries = db.Fetch<SubMenu>().Where(c => c.Status == "A").OrderBy(c => c.DisplaySequence);
                    return countries;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new List<SubMenu>();
            }
        }
        public static SubMenu GetSubMenu(string id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var subMenu = db.Fetch<SubMenu>().SingleOrDefault(c => c.SubMenuId == id);
                    return subMenu;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new SubMenu();
            }
        }
        public static string Update(SubMenu subMenu)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(subMenu);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return "Error occured while trying to update SubMenu";
            }
        }
        public static string Delete(string id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Delete<SubMenu>(id);
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
