using System;
using System.Collections.Generic;
using System.Text;
using NPoco;

namespace GDHOTE.Hub.CoreObject.Models
{
    [TableName("HUB_RequestResponseEntries")]
    [PrimaryKey("Id")]
    public class RequestResponseEntry
    {
        public long Id { get; set; }
        public string RequestHeaders { get; set; }
        public string Username { get; set; }
        public string RequestIpAddress { get; set; }
        public string RequestContentType { get; set; }
        public string RequestUri { get; set; }
        public string RequestMethod { get; set; }
        public string OperationName { get; set; }
        public string OperationVersion { get; set; }
        public DateTime RequestTimestamp { get; set; }
        public string ResponseContentType { get; set; }
        public int ResponseStatusCode { get; set; }
        public string ResponseHeaders { get; set; }
        public DateTime? ResponseTimestamp { get; set; }
        public string Token { get; set; }
        public string Hash { get; set; }
        public string QueryString { get; set; }
        public string RequestContentBody { get; set; }
        public string ResponseContentBody { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
