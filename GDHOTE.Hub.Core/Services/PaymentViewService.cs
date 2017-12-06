﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Models;

namespace GDHOTE.Hub.Core.Services
{
    public class PaymentViewService : BaseService
    {
        public static IEnumerable<PaymentView> GetPayments()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var PaymentViews = db.Fetch<PaymentView>().OrderBy(p => p.PostedDate);
                    return PaymentViews;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new List<PaymentView>();
            }
        }
        public static IEnumerable<PaymentView> GetPaymentsByDate(DateTime startDate)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var PaymentViews = db.Fetch<PaymentView>().Where(p => p.RecordDate == startDate.Date).OrderBy(p => p.PostedDate);
                    return PaymentViews;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new List<PaymentView>();
            }
        }
    }
}