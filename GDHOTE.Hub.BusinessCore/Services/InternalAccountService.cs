using System;
using System.Collections.Generic;
using System.Text;
using GDHOTE.Hub.CoreObject.Models;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class InternalAccountService
    {
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
