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
    public class PaymentViewService : BaseService
    {
        public static IEnumerable<PaymentView> GetPayments()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var paymentViews = db.Fetch<PaymentView>().OrderBy(p => p.PostedDate);
                    return paymentViews;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new List<PaymentView>();
            }
        }
        public static IEnumerable<PaymentView> GetPaymentsByDate(DateTime startDate)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var paymentViews = db.Fetch<PaymentView>().Where(p => p.RecordDate == startDate.Date).OrderBy(p => p.PostedDate);
                    return paymentViews;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new List<PaymentView>();
            }
        }
    }
}
