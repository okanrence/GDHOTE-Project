using System;
using System.Collections.Generic;
using System.Text;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    public class PublicationFormViewModel : CreatePublicationRequest
    {
        public List<PublicationAccessRight> PublicationAccessRights { get; set; }
        public List<PublicationCategory> PublicationCategories { get; set; }
    }
}
