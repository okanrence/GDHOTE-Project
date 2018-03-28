using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.BusinessCore.BusinessLogic;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;
using Newtonsoft.Json;
using GDHOTE.Hub.CoreObject.Enumerables;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class UserService : BaseService
    {
        public static string Save(User user)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(user);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
               LogService.Log(ex.Message);
                return ex.Message.Contains("The duplicate key") ? "Cannot Insert duplicate record" : "Error occured while trying to insert User";
            }
        }

        public static User GetUser(string id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var user = db.Fetch<User>().SingleOrDefault(s => s.UserId == id);
                    return user;
                }
            }
            catch (Exception ex)
            {
               LogService.Log(ex.Message);
                return new User();
            }
        }

        public static User GetUser(string userName, string password)
        {
            try
            {
                var passwordHash = PasswordManager.ReturnHashPassword(password);
                using (var db = GdhoteConnection())
                {
                    var user = db.Fetch<User>().SingleOrDefault(u => u.UserName.ToLower().Equals(userName.ToLower())
                         && u.Password.Equals(passwordHash));
                    return user;
                }
            }
            catch (Exception ex)
            {
               LogService.Log(ex.Message);
                return new User();
            }
        }
        public static User GetUserByUserName(string userName)
        {
            try
            {
                userName = string.IsNullOrEmpty(userName) ? "olufunso" : userName;

                using (var db = GdhoteConnection())
                {
                    var user = db.Fetch<User>()
                        .SingleOrDefault(u => u.UserName.ToLower().Equals(userName.ToLower()));
                    return user;
                }
            }
            catch (Exception ex)
            {
               LogService.Log(ex.Message);
                return new User();
            }
        }

        public static string Update(User user)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(user);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
               LogService.Log(ex.Message);
                return "Error occured while trying to update User";
            }
        }
        public static string Delete(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Delete<User>(id);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
               LogService.Log(ex.Message);
                return "Error occured while trying to delete record";
            }
        }
    }
}
