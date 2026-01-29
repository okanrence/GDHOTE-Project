using System;
using System.Collections.Generic;
using System.Linq;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.CoreObject.ViewModels;
using Newtonsoft.Json;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class BankService : BaseService
    {
        public static string Save(Bank bank)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(bank);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert Bank";
            }
        }
        public static List<BankViewModel> GetAllBanks()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var countries = db.Fetch<BankViewModel>()
                        .OrderBy(c => c.Name)
                        .ToList();
                    return countries;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<BankViewModel>();
            }
        }
        public static List<BankResponse> GetActiveBanks()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var banks = db.Fetch<Bank>()
                        .Where(c => c.StatusId == (int)CoreObject.Enumerables.Status.Active && c.DateDeleted == null)
                        .OrderBy(c => c.Name)
                        .ToList();
                    var item = JsonConvert.SerializeObject(banks);
                    var response = JsonConvert.DeserializeObject<List<BankResponse>>(item);
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<BankResponse>();
            }
        }
        public static Bank GetBank(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var bank = db.Fetch<Bank>()
                        .SingleOrDefault(b => b.Id == id);
                    return bank;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new Bank();
            }
        }
        public static string Update(Bank bank)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(bank);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return "Error occured while trying to update Bank";
            }
        }
        public static Response Delete(int id, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {

                    var response = new Response();

                    var bank = db.Fetch<Bank>().SingleOrDefault(c => c.Id == id);
                    if (bank == null)
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

                    //Delete Bank
                    bank.StatusId = (int)CoreObject.Enumerables.Status.Deleted;
                    bank.DeletedById = user.Id;
                    bank.DateDeleted = DateTime.Now;
                    db.Update(bank);
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

        public static Response CreateBank(CreateBankRequest request, string currentUser)
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
                    var bankExist = db.Fetch<Bank>()
                        .SingleOrDefault(b => b.Name.ToLower().Equals(request.Name.ToLower()));
                    if (bankExist != null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Record already exist"
                        };
                    }

                    string bankName = StringCaseService.TitleCase(request.Name);

                    var bank = new Bank
                    {
                        Name = bankName,
                        StatusId = (int)CoreObject.Enumerables.Status.Active,
                        CreatedById = user.Id,
                        DateCreated = DateTime.Now,
                        RecordDate = DateTime.Now
                    };

                    db.Insert(bank);
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
