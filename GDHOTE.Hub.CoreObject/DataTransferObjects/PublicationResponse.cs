using System;
using System.Collections.Generic;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class PublicationResponse
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string UploadFileUrl { get; set; }
        public string DisplayImageUrl { get; set; }
        public string DatePublished { get; set; }
        public string Author { get; set; }
    }
}
