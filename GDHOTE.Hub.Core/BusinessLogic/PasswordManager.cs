using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Services;

namespace GDHOTE.Hub.Core.BusinessLogic
{
    public class PasswordManager
    {
        public static string ReturnHashPassword(string passwordString)
        {
            string result = "";
            if (string.IsNullOrEmpty(passwordString)) return result;
            result = CommonServices.CreateHash(passwordString, EnumsService.HashTypes.Sha256, EnumsService.HashEncoding.Hex);
            return result;
        }
    }
}
