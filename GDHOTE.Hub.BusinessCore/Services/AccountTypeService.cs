using System;
using System.Collections.Generic;
using System.Linq;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.CoreObject.ViewModels;
using Newtonsoft.Json;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class AccountTypeService : BaseService
    {
        public static string Save(AccountType accountType)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(accountType);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert AccountType";
            }
        }
        public static List<AccountTypeViewModel> GetAllAccountTypes()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var accountTypes = db.Fetch<AccountTypeViewModel>()
                        .OrderBy(c => c.Name)
                        .ToList();
                    return accountTypes;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<AccountTypeViewModel>();
            }
        }
        public static List<AccountTypeResponse> GetActiveAccountTypes()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var accountTypes = db.Fetch<AccountType>()
                        .Where(c => c.StatusId == (int)CoreObject.Enumerables.Status.Active && c.DateDeleted == null)
                        .OrderBy(c => c.Name)
                        .ToList();
                    var item = JsonConvert.SerializeObject(accountTypes);
                    var response = JsonConvert.DeserializeObject<List<AccountTypeResponse>>(item);
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<AccountTypeResponse>();
            }
        }
        public static AccountType GetAccountType(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var accountType = db.Fetch<AccountType>().SingleOrDefault(c => c.Id == id);
                    return accountType;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new AccountType();
            }
        }
        public static string Update(AccountType accountType)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(accountType);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return "Error occured while trying to update AccountType";
            }
        }
        public static Response Delete(int id, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var response = new Response();

                    var accountType = db.Fetch<AccountType>().SingleOrDefault(c => c.Id == id);
                    if (accountType == null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Record does not exist"
                        };
                    }

                    //Get User Initiating Creation Request
                    var user = UserService.GetUserByUserName(currentUser);
                    if (user == null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Record does not exist"
                        };
                    }


                    //Delete AccountType
                    accountType.StatusId = (int)CoreObject.Enumerables.Status.Deleted;
                    accountType.DeletedById = user.Id;
                    accountType.DateDeleted = DateTime.Now;
                    db.Update(accountType);
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

        public static Response CreateAccountType(CreateAccountTypeRequest request, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var response = new Response();

                    //Get User Initiating Creation Request
                    var user = UserService.GetUserByUserName(currentUser);

                    if (user == null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Unable to validate User"
                        };
                    }

                    //Check if name already exist
                    var accountTypeExist = db.Fetch<AccountType>()
                        .SingleOrDefault(c => c.Name.ToLower().Equals(request.Name.ToLower()));
                    if (accountTypeExist != null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Record already exist"
                        };
                    }

                    string accountTypeName = StringCaseService.TitleCase(request.Name);

                    var accountType = new AccountType
                    {
                        Name = accountTypeName,
                        StatusId = (int)CoreObject.Enumerables.Status.Active,
                        CreatedById = user.Id,
                        DateCreated = DateTime.Now,
                        RecordDate = DateTime.Now
                    };

                    db.Insert(accountType);
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
