using System;
using System.Collections.Generic;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class SubMenuResponse
    {
        public string Id { get; set; }
        public string MenuId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int DisplaySequence { get; set; }
    }
}
