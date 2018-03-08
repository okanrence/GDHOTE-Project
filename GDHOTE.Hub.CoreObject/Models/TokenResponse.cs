using System;
using System.Collections.Generic;
using System.Text;

namespace GDHOTE.Hub.CoreObject.Models
{
    public class TokenResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
}
