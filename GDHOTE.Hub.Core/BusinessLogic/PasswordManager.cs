using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Services;
using GDHOTE.Hub.Core.Enumerables;

namespace GDHOTE.Hub.Core.BusinessLogic
{
    public class PasswordManager
    {
        public static string ReturnHashPassword(string passwordString)
        {
            string result = "";
            if (string.IsNullOrEmpty(passwordString)) return result;
            result = CommonServices.CreateHash(passwordString, HashTypes.Sha256, HashEncoding.Hex);
            return result;
        }
    }
}
