﻿using System;
using System.Collections.Generic;
using System.Linq;
using GDHOTE.Hub.BusinessCore.BusinessLogic;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.CoreObject.ViewModels;
using Newtonsoft.Json;

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
                LogService.LogError(ex.Message);
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
                LogService.LogError(ex.Message);
                return new List<CountryViewModel>();
            }
        }
        public static List<CountryResponse> GetActiveCountries()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var countries = db.Fetch<Country>()
                        .Where(c => c.StatusId == (int)CoreObject.Enumerables.Status.Active && c.DateDeleted == null)
                        .OrderBy(c => c.Name)
                        .ToList();
                    var item = JsonConvert.SerializeObject(countries);
                    var response = JsonConvert.DeserializeObject<List<CountryResponse>>(item);
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<CountryResponse>();
            }
        }
        public static Country GetCountry(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var country = db.Fetch<Country>().SingleOrDefault(c => c.Id == id);
                    return country;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
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
                LogService.LogError(ex.Message);
                return "Error occured while trying to update Country";
            }
        }
        public static Response Delete(int id, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var response = new Response();

                    var country = db.Fetch<Country>().SingleOrDefault(c => c.Id == id);
                    if (country == null)
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

                    //Delete Country
                    country.StatusId = (int)CoreObject.Enumerables.Status.Deleted;
                    country.DeletedById = user.Id;
                    country.DateDeleted = DateTime.Now;
                    db.Update(country);
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
                        .SingleOrDefault(c => c.Name.ToLower().Equals(request.Name.ToLower()));
                    if (countryExist != null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Record already exist"
                        };
                    }

                    string countryName = StringCaseService.TitleCase(request.Name);

                    var country = new Country
                    {
                        Name = countryName,
                        StatusId = (int)CoreObject.Enumerables.Status.Active,
                        CreatedById = user.Id,
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
