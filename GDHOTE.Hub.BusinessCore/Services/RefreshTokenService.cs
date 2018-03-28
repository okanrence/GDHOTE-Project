using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.BusinessCore.BusinessLogic;
using GDHOTE.Hub.CoreObject.Enumerables;
using GDHOTE.Hub.CoreObject.Models;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class RefreshTokenService : BaseService
    {
        public static async Task<RefreshToken> FindRefreshToken(string id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var token = db.Fetch<RefreshToken>().SingleOrDefault(r => r.Token == PasswordManager.ReturnHashPassword(id));
                    return token;
                }
            }
            catch (Exception ex)
            {
               LogService.Log(ex.Message);
                throw;
            }
        }

        public static async Task<bool> AddRefreshToken(RefreshToken token)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    db.Insert(token);
                    return true;
                }
            }
            catch (Exception ex)
            {
               LogService.Log(ex.Message);
                throw;
            }
        }

        public static async Task<bool> RemoveRefreshToken(string hashedTokenId)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var tokens = db.Fetch<RefreshToken>().SingleOrDefault(r => r.Token == PasswordManager.ReturnHashPassword(hashedTokenId));
                    if (tokens != null)
                    {
                        db.Delete(tokens);
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
               LogService.Log(ex.Message);
                throw;
            }
        }
    }
}
