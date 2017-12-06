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
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
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
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new User();
            }
        }

        private static User GetUser(string emailAddress, string password)
        {
            try
            {
                var passwordHash = CommonServices.CreateHash(password, EnumsService.HashTypes.Sha256, EnumsService.HashEncoding.Hex);
                using (var db = GdhoteConnection())
                {
                    var user = db.Fetch<User>().SingleOrDefault(s => s.EmailAddress == emailAddress && s.Password.SequenceEqual(passwordHash));
                    return user;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
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
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
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
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return "Error occured while trying to delete record";
            }
        }

        public static EnumsService.SignInStatus LoginUser(string emailAddress, string password, out User authenticatedUser)
        {
            try
            {
                authenticatedUser = GetUser(emailAddress, password);

                return authenticatedUser == null
                    ? EnumsService.SignInStatus.Failure
                    : EnumsService.SignInStatus.Success;

            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                authenticatedUser = null;
                return EnumsService.SignInStatus.Failure;
            }
        }
    }
}
