using System;
using System.Collections.Generic;
using System.Text;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    public class InternalAccountFormViewModel : CreateInternalAccountRequest
    {
        public List<AccountTypeResponse> AccountTypes { get; set; }
        public List<BankResponse> Banks { get; set; }
        public List<CurrencyResponse> Currencies { get; set; }
    }
}
