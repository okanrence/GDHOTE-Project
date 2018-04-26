using System;
using System.Collections;
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
using GDHOTE.Hub.CoreObject.ViewModels;
using NPoco;

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
                LogService.LogError(ex.Message);
                return ex.Message.Contains("The duplicate key") ? "Cannot Insert duplicate record" : "Error occured while trying to insert User";
            }
        }

        public static List<UserViewModel> GetAllUsers()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var user = db.Fetch<UserViewModel>()
                        .OrderBy(u => u.DateCreated)
                        .ToList();
                    return user;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<UserViewModel>();
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
                LogService.LogError(ex.Message);
                return new User();
            }
        }

        public static UserViewModel GetUser(string emailAddress, string password)
        {
            try
            {
                var passwordHash = PasswordManager.ReturnHashPassword(password);
                using (var db = GdhoteConnection())
                {
                    var user = db.Fetch<UserViewModel>().
                        SingleOrDefault(u => u.EmailAddress.ToLower().Equals(emailAddress.ToLower())
                                             && u.Password.Equals(passwordHash));
                    return user;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new UserViewModel();
            }
        }
        public static User GetUserByUserName(string emailaddress)
        {
            try
            {
                emailaddress = string.IsNullOrEmpty(emailaddress) ? "abrabfun@yahoo.com" : emailaddress;

                using (var db = GdhoteConnection())
                {
                    var user = db.Fetch<User>()
                        .SingleOrDefault(u => u.EmailAddress.ToLower().Equals(emailaddress.ToLower()));
                    return user;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
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
                LogService.LogError(ex.Message);
                return "Error occured while trying to update User";
            }
        }
      
        public static Response CreateUser(CreateAdminUserRequest createRequest, string currentUser, int channelCode)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var response = new Response();

                    //Check email exist
                    var adminUserExist = db.Fetch<User>()
                        .SingleOrDefault(m => m.EmailAddress.ToLower().Equals(createRequest.EmailAddress.ToLower()));

                    if (adminUserExist != null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Email Address already exist"
                        };
                    }


                    //Get User Initiating Creation Request
                    var user = GetUserByUserName(currentUser);

                    if (user == null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Unable to validate User"
                        };
                    }


                    var item = JsonConvert.SerializeObject(createRequest);
                    var adminUser = JsonConvert.DeserializeObject<User>(item);

                    
                    adminUser.UserId = Guid.NewGuid().ToString();
                    adminUser.UserName = createRequest.EmailAddress;
                    adminUser.Password = PasswordManager.ReturnHashPassword(createRequest.Password);
                    adminUser.CreatedById = user.UserId;
                    adminUser.ChannelId = channelCode;
                    adminUser.UserStatusId = (int)UserStatusEnum.Active;
                    adminUser.PasswordChange = true;
                    adminUser.DateCreated = DateTime.Now;
                    adminUser.RecordDate = DateTime.Now;

                    db.Insert(adminUser);

                    response = new Response
                    {
                        ErrorCode = "00",
                        ErrorMessage = "Successful"
                    };


                    //Notify Admin User
                    new Task(() =>
                    {
                        var req = new EmailRequest
                        {
                            Type = EmailType.NewAdminUser,
                            Subject = "Welcome to " + Get("settings.organisation.name"),
                            RecipientEmailAddress = createRequest.EmailAddress,
                            Data = new Hashtable
                            {
                                ["FirstName"] = createRequest.FirstName,
                                ["LastName"] = createRequest.LastName
                            }
                        };

                        EmailNotificationService.SendNewUserEmail(req, currentUser);

                    }).Start();

                    return response;
                }

            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                var response = new Response
                {
                    ErrorCode = "01",
                    ErrorMessage = "Error occured while trying to insert record"
                };
                return response;
            }
        }


        public static Response Delete(string userId, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var response = new Response();

                    var userExist = db.Fetch<User>().SingleOrDefault(u => u.UserId == userId);
                    if (userExist == null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Record does not exist"
                        };

                    }

                    //Get User Initiating Creation Request
                    var user = GetUserByUserName(currentUser);
                    if (user == null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Record does not exist"
                        };
                    }

                    //Delete User
                    userExist.UserStatusId = (int)UserStatusEnum.Deleted;
                    userExist.DeletedById = user.UserId;
                    userExist.DateDeleted = DateTime.Now;
                    db.Update(userExist);
                    response = new Response
                    {
                        ErrorCode = "00",
                        ErrorMessage = "Successful"
                    };
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new Response
                {
                    ErrorCode = "01",
                    ErrorMessage = "Error occured while trying to delete record"
                };
            }
        }
        public static Response RequestResetPassword(PasswordResetRequest request)
        {
            try
            {
                using (var db = GdhoteConnection())
                {

                    string emailAddress = request.EmailAddress.ToLower();

                    //Get user by username 
                    var userExist = db.Fetch<User>().SingleOrDefault(c => c.EmailAddress.ToLower().Equals(emailAddress));
                    if (userExist == null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "User not registered"
                        };
                    }

                    //string code = CommonServices.RandomString(5);
                    //var pwdReset = new PasswordReset();
                    //pwdReset.UserId = userExist.UserId;
                    //pwdReset.Status = "D";
                    //db.Update<PasswordReset>("where UserId =" + userExist.UserId);


                    ////Insert new Password Reset
                    //var pwd = new PasswordReset();
                    //code = CommonServices.RandomString(5);
                    //pwd.ResetCode = CommonServices.HashSha512(code);
                    //pwd.ChannelId = request.ChannelId;
                    //pwd.DateCreated = DateTime.Now;
                    //pwd.DateExpiry = DateTime.Now.AddDays(1);
                    //pwd.UserId = userExist.UserId;
                    //pwd.EmailAddress = userExist.EmailAddress;
                    //db.Insert(pwd);

                    //check password reset
                    var pwdReset = db.Query<PasswordReset>()
                            .SingleOrDefault(p => p.UserId == userExist.UserId && p.DateUpdated == null);

                    string code;
                    bool resend = false;
                    if (pwdReset == null)
                    {
                        var pwd = new PasswordReset();
                        code = CommonServices.RandomString(5);
                        pwd.ResetCode = CommonServices.HashSha512(code);
                        pwd.ChannelId = request.ChannelId;
                        pwd.DateCreated = DateTime.Now;
                        pwd.DateExpiry = DateTime.Now.AddDays(1);
                        pwd.UserId = userExist.UserId;
                        pwd.EmailAddress = userExist.EmailAddress;
                        db.Insert(pwd);
                    }
                    else
                    {

                        //code has expired, regenerate
                        code = CommonServices.RandomString(5);
                        resend = true;
                        pwdReset.ResetCode = CommonServices.HashSha512(code);
                        pwdReset.DateCreated = DateTime.Now;
                        pwdReset.DateExpiry = DateTime.Now.AddDays(1);
                        pwdReset.ChannelId = request.ChannelId;
                        db.Update(pwdReset);

                    }


                    new Task(() =>
                    {
                        var req = new EmailRequest
                        {
                            Type = EmailType.PasswordReset,
                            Subject = Get("settings.organisation.name") + " Password Reset",
                            RecipientEmailAddress = userExist.EmailAddress,
                            Data = new Hashtable
                            {
                                //["Subject"] = "Gdhote Password Reset",
                                ["Code"] = code,
                                ["Resend"] = resend
                            }
                        };
                        EmailNotificationService.SendPasswordResetEmail(req, emailAddress);
                    }).Start();



                    var response = new Response
                    {
                        ErrorCode = "00",
                        ErrorMessage = "Successful"
                    };

                    return response;
                }

            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                var response = new Response
                {
                    ErrorCode = "01",
                    ErrorMessage = "Error occured while trying to insert record"
                };
                return response;
            }

        }

    }
}
