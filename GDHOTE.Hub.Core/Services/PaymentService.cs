using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Models;

namespace GDHOTE.Hub.Core.Services
{
    public class PaymentService : BaseService
    {
        public static string SavePayment(Payment payment)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(payment);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert payment";
            }
        }
        public static IEnumerable<Payment> GetPayments()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var Payments = db.Fetch<Payment>();
                    return Payments;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                if (ex.Message.Contains("The duplicate key")) throw new Exception("Cannot Insert duplicate record");
                throw new Exception("Error occured while trying to get payment");
            }
        }
    }
}
