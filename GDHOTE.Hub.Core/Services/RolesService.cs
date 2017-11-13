using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Dtos;
using GDHOTE.Hub.Core.Models;

namespace GDHOTE.Hub.Core.Services
{
    public class RolesService : BaseService
    {

        public static string Save(Role role)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(role);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return ex.Message.Contains("The duplicate key") ? "Cannot Insert duplicate record" : "Error occured while trying to insert Role";
            }
        }

        public static IEnumerable<Role> GetRoles()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var roles = db.Fetch<Role>();
                    return roles;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new List<Role>();
            }
        }
        public static Role GetRole(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var Role = db.Fetch<Role>().SingleOrDefault(s => s.RoleId == id);
                    return Role;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new Role();
            }
        }

        public static string Update(Role role)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(role);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return "Error occured while trying to update Role";
            }
        }
        public static string Delete(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Delete<Role>(id);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return "Error occured while trying to delete record";
            }
        }
    }
}
