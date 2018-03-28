using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.BusinessCore.BusinessLogic;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.CoreObject.Enumerables;
using GDHOTE.Hub.CoreObject.ViewModels;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class CurrencyService : BaseService
    {
        public static string Save(Currency currency)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(currency);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(ex.Message);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert Currency";
            }
        }
        public static List<CurrencyViewModel> GetAllCurrencies()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var countries = db.Fetch<CurrencyViewModel>()
                        .OrderBy(c => c.Name).ToList();
                    return countries;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(ex.Message);
                return new List<CurrencyViewModel>();
            }
        }
        public static List<Currency> GetActiveCurrencies()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var countries = db.Fetch<Currency>()
                        .Where(c => c.StatusId == (int)CoreObject.Enumerables.Status.Active && c.DateDeleted == null)
                        .OrderBy(c => c.Name).ToList();
                    return countries;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(ex.Message);
                return new List<Currency>();
            }
        }
        public static Currency GetCurrency(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var currency = db.Fetch<Currency>().SingleOrDefault(c => c.CurrencyId == id);
                    return currency;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(ex.Message);
                return new Currency();
            }
        }
        public static string Update(Currency currency)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(currency);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(ex.Message);
                return "Error occured while trying to update Currency";
            }
        }
        public static string Delete(int id, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var currency = db.Fetch<Currency>().SingleOrDefault(c => c.CurrencyId == id);
                    if (currency == null)
                    {
                        return "Record does not exist";
                    }

                    //Get User Initiating Creation Request
                    var user = UserService.GetUserByUserName(currentUser);
                    

                    //Delete Currency
                    currency.StatusId = (int)CoreObject.Enumerables.Status.DeActivated;
                    currency.DeletedById = user.UserId;
                    currency.DateDeleted = DateTime.Now;
                    db.Update(currency);
                    var result = "Operation Successful";
                    return result;

                }
            }
            catch (Exception ex)
            {
                LogService.Log(ex.Message);
                return "Error occured while trying to delete record";
            }
        }

        public static Response CreateCurrency(CreateCurrencyRequest request, string currentUser)
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
                    var currencyExist = db.Fetch<Currency>()
                        .SingleOrDefault(c => c.Name.ToLower().Equals(request.Name.ToLower()) && c.CurrencyCode.ToLower().Equals(request.CurrencyCode.ToLower()));
                    if (currencyExist != null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Record already exist"
                        };
                    }

                    string currencyCode = request.CurrencyCode.ToUpper();
                    string currencyName = StringCaseManager.TitleCase(request.Name);


                    var activityType = new Currency
                    {
                        CurrencyCode = currencyCode,
                        Name = currencyName,
                        StatusId = (int)CoreObject.Enumerables.Status.Active,
                        CreatedById = user.UserId,
                        DateCreated = DateTime.Now,
                        RecordDate = DateTime.Now
                    };

                    db.Insert(activityType);
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
                LogService.Log(ex.Message);
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
