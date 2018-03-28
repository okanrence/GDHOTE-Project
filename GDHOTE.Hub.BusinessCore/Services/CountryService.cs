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
    public class CountryService : BaseService
    {
        public static string Save(Country country)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(country);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(ex.Message);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert country";
            }
        }
        public static List<CountryViewModel> GetAllCountries()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var countries = db.Fetch<CountryViewModel>()
                        .OrderBy(c => c.Name)
                        .ToList();
                    return countries;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(ex.Message);
                return new List<CountryViewModel>();
            }
        }
        public static List<Country> GetCountries()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var countries = db.Fetch<Country>()
                        .Where(c => c.StatusId == (int)CoreObject.Enumerables.Status.Active && c.DateDeleted == null)
                        .OrderBy(c => c.Name)
                        .ToList();
                    return countries;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(ex.Message);
                return new List<Country>();
            }
        }
        public static Country GetCountry(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var country = db.Fetch<Country>().SingleOrDefault(c => c.CountryId == id);
                    return country;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(ex.Message);
                return new Country();
            }
        }
        public static string Update(Country country)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(country);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(ex.Message);
                return "Error occured while trying to update Country";
            }
        }
        public static string Delete(int id, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {


                    var country = db.Fetch<Country>().SingleOrDefault(c => c.CountryId == id);
                    if (country == null)
                    {
                        return "Country does not exist";
                    }

                    //Get User Initiating Creation Request
                    var user = UserService.GetUserByUserName(currentUser);

                    //Delete Country
                    country.StatusId = (int)CoreObject.Enumerables.Status.DeActivated;
                    country.DeletedBy = user.UserId;
                    country.DateDeleted = DateTime.Now;
                    db.Update(country);
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

        public static Response CreateCountry(CreateCountryRequest request, string currentUser)
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
                    var countryExist = db.Fetch<Country>()
                        .SingleOrDefault(c => c.Name.ToLower().Equals(request.Name.ToLower()) && c.CountryCode.ToLower().Equals(request.CountryCode.ToLower()));
                    if (countryExist != null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Record already exist"
                        };
                    }

                    string countryCode = request.CountryCode.ToUpper();
                    string countryName = StringCaseManager.TitleCase(request.Name);

                    var country = new Country
                    {
                        CountryCode = countryCode,
                        Name = countryName,
                        StatusId = (int)CoreObject.Enumerables.Status.Active,
                        CreatedBy = user.UserId,
                        DateCreated = DateTime.Now,
                        RecordDate = DateTime.Now
                    };

                    db.Insert(country);
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
