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
    public class PaymentModeService : BaseService
    {
        public static string Save(PaymentMode paymentMode)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(paymentMode);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert payment mode";
            }
        }
        public static IEnumerable<PaymentMode> GetPaymentModes()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var paymentModes = db.Fetch<PaymentMode>().OrderBy(pm => pm.Description);
                    return paymentModes;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new List<PaymentMode>();
            }
        }
        public static IEnumerable<PaymentMode> GetActivePaymentModes()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var paymentModes = db.Fetch<PaymentMode>().Where(p => p.Status == "A").OrderBy(pm => pm.Description);
                    return paymentModes;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new List<PaymentMode>();
            }
        }
        public static PaymentMode GetPaymentMode(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var paymentMode = db.Fetch<PaymentMode>().SingleOrDefault(pm => pm.PaymentModeId == id);
                    return paymentMode;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new PaymentMode();
            }
        }
        public static string Update(PaymentMode paymentMode)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(paymentMode);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return "Error occured while trying to update mode of payment";
            }
        }
        public static string Delete(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Delete<PaymentMode>(id);
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
