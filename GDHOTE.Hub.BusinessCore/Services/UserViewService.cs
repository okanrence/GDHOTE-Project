using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.BusinessCore.BusinessLogic;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.CoreObject.Enumerables;
namespace GDHOTE.Hub.BusinessCore.Services
{
    public class UserViewService : BaseService
    {
        public static UserView GetUser(string userName, string password)
        {
            try
            {
                var passwordHash = PasswordManager.ReturnHashPassword(password);
                using (var db = GdhoteConnection())
                {
                    var user = db.Fetch<UserView>().
                        SingleOrDefault(u => u.UserName.ToLower().Equals(userName.ToLower())
                                             && u.Password.Equals(passwordHash));
                    return user;
                }
            }
            catch (Exception ex)
            {
               LogService.myLog(ex.Message);
                return new UserView();
            }
        }
        public static IEnumerable<UserView> GetUsers()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var userViews = db.Fetch<UserView>().OrderBy(u => u.EmailAddress);
                    return userViews;
                }
            }
            catch (Exception ex)
            {
               LogService.myLog(ex.Message);
                return new List<UserView>();
            }
        }
    }
}
