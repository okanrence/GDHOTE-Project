using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Models;
using GDHOTE.Hub.Core.Enumerables;

namespace GDHOTE.Hub.Core.Services
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
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert Currency";
            }
        }
        public static IEnumerable<Currency> GetCurrencies()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var countries = db.Fetch<Currency>().OrderBy(c => c.Description);
                    return countries;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new List<Currency>();
            }
        }
        public static IEnumerable<Currency> GetActiveCurrencies()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var countries = db.Fetch<Currency>().Where(c => c.Status == "A").OrderBy(c => c.Description);
                    return countries;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
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
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
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
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return "Error occured while trying to update Currency";
            }
        }
        public static string Delete(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Delete<Currency>(id);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return "Error occured while trying to delete record";
            }
        }
    }
}
