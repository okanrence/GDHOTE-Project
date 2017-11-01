using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Models;

namespace GDHOTE.Hub.Core.Services
{
    public class CountryService : BaseService
    {
        public static string SaveCountry(Country country)
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
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                if (ex.Message.Contains("The duplicate key")) throw new Exception("Cannot Insert duplicate record");
                throw new Exception("Error occured while trying to insert country");
            }
        }
        public static IEnumerable<Country> GetCountries()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var countries = db.Fetch<Country>().Where(c => c.Status == "A");
                    return countries;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while trying to fetch countries");
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
                throw new Exception("Error occured while trying to fetch country");
            }
        }
        public static int UpdateCountry(Country country)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(country);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while trying to fetch country");
            }
        }
    }
}
