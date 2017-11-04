using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Models;

namespace GDHOTE.Hub.Core.Services
{
    public class PaymentTypeService : BaseService
    {
        public static string Save(PaymentType paymentType)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(paymentType);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert payment types";
            }
        }
        public static IEnumerable<PaymentType> GetPaymentTypes()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var paymentTypes = db.Fetch<PaymentType>();
                    return paymentTypes;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                if (ex.Message.Contains("The duplicate key")) throw new Exception("Cannot Insert duplicate record");
                throw new Exception("Error occured while trying to get payment types");
            }
        }
        public static PaymentType GetPaymentType(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var paymentType = db.Fetch<PaymentType>().SingleOrDefault(p=> p.PaymentTypeId ==id);
                    return paymentType;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                if (ex.Message.Contains("The duplicate key")) throw new Exception("Cannot Insert duplicate record");
                throw new Exception("Error occured while trying to get payment types");
            }
        }
        public static int Update(PaymentType paymentType)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(paymentType);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int Delete(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Delete<PaymentType>(id);
                    return result;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception("Error occured while trying to delete record");
            }
        }
    }
}
