using System;
using System.Collections.Generic;
using System.Text;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    public class AccountFormViewModel : CreateAccountRequest
    {
        public List<AccountTypeResponse> AccountTypes { get; set; }
    }
}
