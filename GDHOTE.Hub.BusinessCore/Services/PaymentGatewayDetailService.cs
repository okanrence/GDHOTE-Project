using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using GDHOTE.Hub.BusinessCore.Exceptions;
using GDHOTE.Hub.CoreObject.Models;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class PaymentGatewayDetailService : BaseService
    {
        public static string Save(PaymentGatewayDetail paymentGatewayDetail)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(paymentGatewayDetail);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert Checker";
            }
        }
    }
}
