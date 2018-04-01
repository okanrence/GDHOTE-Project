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
    public class PublicationService : BaseService
    {
        public static string Save(Publication publication)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(publication);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(ex.Message);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert Publication";
            }
        }
        public static List<PublicationViewModel> GetAllPublications()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var publications = db.Fetch<PublicationViewModel>()
                        .OrderBy(c => c.Name)
                        .ToList();
                    return publications;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(ex.Message);
                return new List<PublicationViewModel>();
            }
        }
        public static List<Publication> GetActivePublications()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var publications = db.Fetch<Publication>()
                        .Where(c => c.StatusId == (int)CoreObject.Enumerables.Status.Active && c.DateDeleted == null)
                        .OrderBy(c => c.Name)
                        .ToList();
                    return publications;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(ex.Message);
                return new List<Publication>();
            }
        }
        public static Publication GetPublication(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var publication = db.Fetch<Publication>().SingleOrDefault(c => c.Id == id);
                    return publication;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(ex.Message);
                return new Publication();
            }
        }
        public static string Update(Publication publication)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(publication);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(ex.Message);
                return "Error occured while trying to update Publication";
            }
        }
        public static string Delete(int id, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {


                    var publication = db.Fetch<Publication>().SingleOrDefault(c => c.Id == id);
                    if (publication == null)
                    {
                        return "Record does not exist";
                    }

                    //Get User Initiating Creation Request
                    var user = UserService.GetUserByUserName(currentUser);

                    //Delete Publication
                    publication.StatusId = (int)CoreObject.Enumerables.Status.Deleted;
                    publication.DeletedById = user.UserId;
                    publication.DateDeleted = DateTime.Now;
                    db.Update(publication);
                    var result = "Operation Successful";
                    return result;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(ex.Message);
                return "Error occured while trying to delete record";
            }
        }

        public static Response CreatePublication(CreatePublicationRequest request, string currentUser)
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
                    var publicationExist = db.Fetch<Publication>()
                        .SingleOrDefault(c => c.Name.ToLower().Equals(request.Name.ToLower()));
                    if (publicationExist != null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Record already exist"
                        };
                    }

                    string publicationName = StringCaseManager.TitleCase(request.Name);

                    var publication = new Publication
                    {
                        Name = publicationName,
                        StatusId = (int)CoreObject.Enumerables.Status.Active,
                        CreatedById = user.UserId,
                        DateCreated = DateTime.Now,
                        RecordDate = DateTime.Now
                    };

                    db.Insert(publication);
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
                LogService.Log(ex.Message);
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
