using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.CoreObject.Enumerables;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class PaymentService : BaseService
    {
        public static string Save(Payment payment)
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
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
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
                    var payments = db.Fetch<Payment>();
                    return payments;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new List<Payment>();
            }
        }
        public static Payment GetPayment(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var payment = db.Fetch<Payment>().SingleOrDefault(p => p.PaymentId == id);
                    return payment;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new Payment();
            }
        }
        public static string Update(Payment payment)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(payment);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return "Error occured while trying to update payment";
            }
        }
        public static string Delete(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Delete<Payment>(id);
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
