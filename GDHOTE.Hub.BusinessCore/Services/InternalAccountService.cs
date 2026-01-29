using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class InternalAccountService : BaseService
    {
        public static PaymentType GetAccountByPaymentType(int currenyId, int paymentTypeId)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var paymentType = db.Fetch<PaymentType>().SingleOrDefault(p => p.Id == paymentTypeId);
                    if (paymentType == null)
                    {
                        //Use Default internal account
                        paymentType = new PaymentType
                        {
                            AccountId = 1
                        };
                    }
                    return paymentType;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new PaymentType();
            }
        }
        public static Account ReturnInternalAccount(int currenyId, int paymentType)
        {
            int accountId = 0;
            switch (paymentType)
            {
                case 0:
                    if (currenyId == 1) accountId = 1000;
                    break;
                default:
                    accountId = 1000;
                    break;
            }

            var account = new Account
            {
                Id = accountId
            };
            return account;
        }
    }
}
