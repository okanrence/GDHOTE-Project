using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GDHOTE.Hub.BusinessCore.BusinessLogic;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.CoreObject.ViewModels;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class PublicationAccessRightService : BaseService

    {
        public static string Save(PublicationAccessRight publicationAccessRight)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(publicationAccessRight);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert record";
            }
        }

        public static List<PublicationAccessRightViewModel> GetAllPublicationAccessRights()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var publicationAccessRights = db.Fetch<PublicationAccessRightViewModel>()
                        .OrderBy(c => c.Name)
                        .ToList();
                    return publicationAccessRights;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<PublicationAccessRightViewModel>();
            }
        }

        public static List<PublicationAccessRight> GetActivePublicationAccessRights()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var publicationAccessRights = db.Fetch<PublicationAccessRight>()
                        .Where(c => c.StatusId == (int)CoreObject.Enumerables.Status.Active && c.DateDeleted == null)
                        .OrderBy(c => c.Name)
                        .ToList();
                    return publicationAccessRights;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<PublicationAccessRight>();
            }
        }

        public static PublicationAccessRight GetPublicationAccessRight(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var publicationAccessRight = db.Fetch<PublicationAccessRight>().SingleOrDefault(c => c.Id == id);
                    return publicationAccessRight;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new PublicationAccessRight();
            }
        }

        public static string Update(PublicationAccessRight publicationAccessRight)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(publicationAccessRight);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return "Error occured while trying to update record";
            }
        }

        public static Response Delete(int id, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var response = new Response();

                    var publicationAccessRight = db.Fetch<PublicationAccessRight>().SingleOrDefault(c => c.Id == id);
                    if (publicationAccessRight == null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Record does not exist"
                        };
                    }

                    //Get User Initiating Creation Request
                    var user = UserService.GetUserByUserName(currentUser);
                    if (user == null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Record does not exist"
                        };
                    }


                    //Delete PublicationAccessRight
                    publicationAccessRight.StatusId = (int)CoreObject.Enumerables.Status.Deleted;
                    publicationAccessRight.DeletedById = user.UserId;
                    publicationAccessRight.DateDeleted = DateTime.Now;
                    db.Update(publicationAccessRight);
                    response = new Response
                    {
                        ErrorCode = "00",
                        ErrorMessage = "Successful"
                    };
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new Response
                {
                    ErrorCode = "01",
                    ErrorMessage = "Error occured while trying to delete record"
                };
            }
        }

        public static Response CreatePublicationAccessRight(CreatePublicationAccessRightRequest request, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var response = new Response();

                    //Get User Initiating Creation Request
                    var user = UserService.GetUserByUserName(currentUser);

                    if (user == null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Unable to validate User"
                        };
                    }

                    //Check if name already exist
                    var publicationAccessRightExist = db.Fetch<PublicationAccessRight>()
                        .SingleOrDefault(c => c.Name.ToLower().Equals(request.Name.ToLower()));
                    if (publicationAccessRightExist != null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Record already exist"
                        };
                    }

                    string accessRightName = StringCaseService.TitleCase(request.Name);

                    var publicationAccessRight = new PublicationAccessRight
                    {
                        Name = accessRightName,
                        StatusId = (int)CoreObject.Enumerables.Status.Active,
                        CreatedById = user.UserId,
                        DateCreated = DateTime.Now,
                        RecordDate = DateTime.Now
                    };

                    db.Insert(publicationAccessRight);
                    response = new Response
                    {
                        ErrorCode = "00",
                        ErrorMessage = "Successful"
                    };
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                var response = new Response
                {
                    ErrorCode = "01",
                    ErrorMessage = "Error occured while trying to insert record"
                };
                return response;
            }

        }
    }
}
