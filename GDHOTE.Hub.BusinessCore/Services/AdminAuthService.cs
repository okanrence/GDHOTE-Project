using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using GDHOTE.Hub.BusinessCore.Exceptions;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Enumerables;
using GDHOTE.Hub.CoreObject.ViewModels;
using Newtonsoft.Json;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class AdminAuthService
    {
        public static AdminLoginResponse AuthenticateUser(AdminLoginRequest loginRequest)
        {
            try
            {

                LogService.myLog("Testing");


                var userView = UserViewService.GetUser(loginRequest.UserName, loginRequest.Password);

                if (userView == null)
                    return new AdminLoginResponse
                    {
                        ErrorCode = "01",
                        ErrorMessage = "Invalid username or password"
                    }; ;

                if (userView.UserStatusId != 1)
                    return new AdminLoginResponse
                    {
                        ErrorCode = "01",
                        ErrorMessage = "User has been disabled. Please contact super admin"
                    };


                var adminUser = new AdminLoginResponse();
                var item = JsonConvert.SerializeObject(userView);
                adminUser.User = JsonConvert.DeserializeObject<AdminUserViewModel>(item);
                adminUser.ErrorCode = "00";

                return adminUser;
            }
            catch (Exception ex)
            {
                LogService.myLog(ex.Message);

                if (ex is InvalidRequestException) throw;
                throw new UnableToCompleteException(ex.Message, MethodBase.GetCurrentMethod().Name);
            }
        }
    }
}
