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
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert country";
            }
        }
        public static IEnumerable<Country> GetCountries()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var countries = db.Fetch<Country>().Where(c => c.Status == "A").OrderBy(c => c.CountryName);
                    return countries;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new List<Country>();
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
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
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
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return "Error occured while trying to update Country";
            }
        }
        public static string Delete(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Delete<Country>(id);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return "Error occured while trying to delete record";
            }
        }
    }
}
