using System;
using System.Collections.Generic;
using System.Text;
using NPoco;

namespace GDHOTE.Hub.CoreObject.Models
{
    [TableName("HUB_RefreshTokens")]
    [PrimaryKey("Id")]
    public class RefreshToken
    {
        public long Id { get; set; }
        public string Token { get; set; }
        public string Subject { get; set; }
        public string UserType { get; set; }
        public string ClientId { get; set; }
        public DateTime IssuedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }
        public string ProtectedTicket { get; set; }
    }
}
