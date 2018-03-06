using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;
namespace GDHOTE.Hub.BusinessCore.Exceptions
{
    public class InvalidRequestException : Exception
    {
        protected ILog _logger;
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public object GetException()
        {
            return new { ErrorCode, ErrorMessage };
        }
        public InvalidRequestException(string message, string methodName) : base(message)
        {
            ErrorCode = "100";
            ErrorMessage = message;
            _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            _logger.Fatal(GetBaseException());
        }
    }
}
