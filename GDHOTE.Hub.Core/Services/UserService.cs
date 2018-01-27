using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.BusinessLogic;
using GDHOTE.Hub.Core.DataTransferObjects;
using GDHOTE.Hub.Core.Models;
using Newtonsoft.Json;
using GDHOTE.Hub.Core.Enumerables;

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
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
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
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new User();
            }
        }

        private static User GetUser(string userName, string password)
        {
            try
            {
                var passwordHash = PasswordManager.ReturnHashPassword(password);
                using (var db = GdhoteConnection())
                {
                    var user = db.Fetch<User>().SingleOrDefault(u => u.UserName.ToLower().Equals(userName.ToLower())
                         && u.Password.SequenceEqual(passwordHash));
                    return user;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
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
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
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
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return "Error occured while trying to delete record";
            }
        }

        public static LoginResponse AuthenticateUser(LoginRequest loginRequest)
        {
            try
            {
                var loginResponse = new LoginResponse();
                var authenticatedUser = GetUser(loginRequest.UserName, loginRequest.Password);
                if (authenticatedUser == null)
                {
                    loginResponse.ErrorCode = "100";
                    loginResponse.ErrorMessage = "Invalid credentials";
                }
                else
                {
                    var usr = JsonConvert.SerializeObject(authenticatedUser);
                    loginResponse = JsonConvert.DeserializeObject<LoginResponse>(usr);
                    loginResponse.ErrorCode = "00";
                    loginResponse.ErrorMessage = "Login Successful";
                }
                return loginResponse;
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new LoginResponse();
            }
        }
        public static SignInStatus LoginUserOld(string username, string password, out User authenticatedUser)
        {
            try
            {
                authenticatedUser = GetUser(username, password);
                if (authenticatedUser == null) return SignInStatus.Failure;
                return authenticatedUser.UserId == null
                        ? SignInStatus.Failure
                        : SignInStatus.Success;
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                authenticatedUser = null;
                return SignInStatus.Failure;
            }
        }
    }
}
