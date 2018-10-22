using System.Collections.Generic;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    public class PublicationFormViewModel : CreatePublicationRequest
    {
        public List<PublicationAccessRight> PublicationAccessRights { get; set; }
        public List<PublicationCategoryResponse> PublicationCategories { get; set; }
    }
}
