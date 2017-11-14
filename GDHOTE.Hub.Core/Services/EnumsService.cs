using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDHOTE.Hub.Core.Services
{
    public class EnumsService
    {
        public enum LogType
        {
            Info = 1, Warning = 2, Error = 3, Fatal = 4
        }

<<<<<<< HEAD
        public enum SignInStatus
        {
            Success = 1, Failure = 2, LockedOut = 3
        }

        public enum HashTypes
        {
            Sha1 = 1,
            Sha256 = 2,
            Sha512 = 3,
            Sha384 = 4
        }
        public enum HashEncoding
        {
            Hex = 1,
            Base64 = 2,
        }

=======
        public enum OfficerType
        {
            NormalMember = 100
        }
>>>>>>> Sequence Logic started
    }
}
