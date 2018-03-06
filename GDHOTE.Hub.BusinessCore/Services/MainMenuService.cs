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
    public class MainMenuService : BaseService
    {
        public static string Save(MainMenu mainMenu)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(mainMenu);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert MainMenu";
            }
        }
        public static IEnumerable<MainMenu> GetMainMenus()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var countries = db.Fetch<MainMenu>().Where(c => c.Status == "A").OrderBy(c => c.DisplaySequence);
                    return countries;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new List<MainMenu>();
            }
        }
        public static MainMenu GetMainMenu(string menuId)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var mainMenu = db.Fetch<MainMenu>().SingleOrDefault(c => c.MenuId == menuId);
                    return mainMenu;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new MainMenu();
            }
        }
        public static string Update(MainMenu mainMenu)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(mainMenu);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return "Error occured while trying to update MainMenu";
            }
        }
        public static string Delete(string menuId)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Delete<MainMenu>(menuId);
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
