﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Models;
using NPoco;
using NPoco.Expressions;
using GDHOTE.Hub.Core.Enumerables;

namespace GDHOTE.Hub.Core.Services
{
    public class RoleService : BaseService
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
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
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
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new List<Role>();
            }
        }
        public static IEnumerable<Role> GetActiveRoles()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var roles = db.Fetch<Role>().Where(r => r.Status == "A");
                    return roles;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new List<Role>();
            }
        }
        public static Role GetRole(string id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var role = db.Fetch<Role>().SingleOrDefault(s => s.RoleId == id);
                    return role;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
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
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return "Error occured while trying to update Role";
            }
        }
        public static string Delete(string roleId)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Delete<Role>("where RoleId = @0", roleId);
                    //var result = db.Delete<Role>(roleId);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return "Error occured while trying to delete record";
            }
        }
        public static string Delete(Role role)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Delete<Role>(role);
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
