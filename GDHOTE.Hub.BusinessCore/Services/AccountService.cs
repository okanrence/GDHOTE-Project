using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.CoreObject.ViewModels;
using Newtonsoft.Json;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class AccountService : BaseService
    {
        public static string Save(Account account)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(account);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert Account";
            }
        }
        public static List<AccountViewModel> GetAllAccounts()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var accounts = db.Fetch<AccountViewModel>()
                        .OrderBy(c => c.AccountName)
                        .ToList();
                    return accounts;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<AccountViewModel>();
            }
        }
        public static List<AccountViewModel> GetActiveAccounts()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var accounts = db.Fetch<AccountViewModel>()
                        .Where(c => c.StatusId == (int)CoreObject.Enumerables.Status.Active && c.DateDeleted == null)
                        .OrderBy(c => c.AccountName)
                        .ToList();
                    return accounts;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<AccountViewModel>();
            }
        }
        public static List<AccountViewModel> GetInternalAccounts()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var accounts = db.Fetch<AccountViewModel>()
                        .Where(a => a.AccountTypeId == (int)CoreObject.Enumerables.AccountType.InternalAccount)
                        .OrderBy(c => c.AccountName)
                        .ToList();
                    return accounts;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<AccountViewModel>();
            }
        }
        public static Account GetAccount(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var account = db.Fetch<Account>().SingleOrDefault(c => c.Id == id);
                    return account;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new Account();
            }
        }
        public static string Update(Account account)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(account);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return "Error occured while trying to update Account";
            }
        }
        public static string Delete(int id, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {


                    var account = db.Fetch<Account>().SingleOrDefault(c => c.Id == id);
                    if (account == null)
                    {
                        return "Record does not exist";
                    }

                    //Get User Initiating Creation Request
                    var user = UserService.GetUserByUserName(currentUser);

                    //Delete Account
                    account.StatusId = (int)CoreObject.Enumerables.Status.Deleted;
                    account.DeletedById = user.Id;
                    account.DateDeleted = DateTime.Now;
                    db.Update(account);
                    var result = "Operation Successful";
                    return result;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return "Error occured while trying to delete record";
            }
        }

        public static Response CreateAccount(CreateAccountRequest request, string currentUser)
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
                    var accountExist = db.Fetch<Account>()
                        .SingleOrDefault(c => c.MemberId == request.MemberId);
                    if (accountExist != null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Record already exist"
                        };
                    }

                    string accountName = StringCaseService.TitleCase(request.AccountName);

                    var account = new Account
                    {
                        AccountNumber = "",
                        AccountName = accountName,
                        BankId = 0,
                        MemberId = request.MemberId,
                        AccountTypeId = request.AccountTypeId,
                        StatusId = (int)CoreObject.Enumerables.Status.Active,
                        AccountKey = Guid.NewGuid().ToString(),
                        CreatedById = user.Id,
                        DateCreated = DateTime.Now,
                        RecordDate = DateTime.Now
                    };

                    db.Insert(account);
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


        public static Response CreateInternalAccount(CreateInternalAccountRequest request, string currentUser)
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

                    //Check if account already exist
                    var accountExist = db.Fetch<Account>()
                        .SingleOrDefault(a => a.BankId == request.BankId
                                              && a.AccountNumber == request.AccountNumber);
                    if (accountExist != null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Record already exist"
                        };
                    }

                    string accountName = StringCaseService.TitleCase(request.AccountName);

                    var account = new Account
                    {
                        AccountNumber = request.AccountNumber,
                        AccountName = accountName,
                        BankId = request.BankId,
                        MemberId = 0,
                        AccountTypeId = request.AccountTypeId,
                        StatusId = (int)CoreObject.Enumerables.Status.Active,
                        AccountKey = Guid.NewGuid().ToString(),
                        CreatedById = user.Id,
                        DateCreated = DateTime.Now,
                        RecordDate = DateTime.Now
                    };

                    db.Insert(account);
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
