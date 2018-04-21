using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                LogService.LogError(ex.Message);
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
                        .OrderBy(c => c.DateCreated).ThenBy(c => c.Title)
                        .ToList();
                    return publications;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<PublicationViewModel>();
            }
        }

        public static List<PublicationResponse> GetActivePublications()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var publications = db.Fetch<Publication>()
                        .Where(c => c.StatusId == (int)CoreObject.Enumerables.Status.Active && c.DateDeleted == null)
                        .OrderBy(c => c.DateCreated).ThenBy(c => c.Title)
                        .ToList();
                    var response = new List<PublicationResponse>();
                    if (publications != null)
                    {
                        string urlPath = ReturnBaseUrl() + "/" + Get("settings.file.upload.folder");
                        foreach (var pub in publications)
                        {
                            var publicationResponse = new PublicationResponse
                            {
                                Id = pub.Id,
                                Title = pub.Title,
                                Description = pub.Description,
                                CategoryId = pub.CategoryId,
                                Author = pub.Author,
                                DatePublished = pub.DatePublished.ToString("dd-MMM-yyyy"),
                                UploadFileUrl = urlPath + "/" + pub.UploadFile,
                                DisplayImageUrl = urlPath + "/" + pub.DisplayImageFile
                            };
                            response.Add(publicationResponse);
                        }
                    }
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<PublicationResponse>();
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
                LogService.LogError(ex.Message);
                return new Publication();
            }
        }

        public static List<Publication> GetPublicationsByCategoryId(int categoryId)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var publications = db.Fetch<Publication>()
                        .Where(c => c.CategoryId == categoryId && c.StatusId == (int)CoreObject.Enumerables.Status.Active)
                        .OrderBy(c => c.Title)
                        .ToList();
                    return publications;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<Publication>();
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
                LogService.LogError(ex.Message);
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
                LogService.LogError(ex.Message);
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
                        .SingleOrDefault(c => c.Title.ToLower().Equals(request.Title.ToLower()) && c.CategoryId == request.CategoryId);
                    if (publicationExist != null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Record already exist for this Category"
                        };
                    }

                    //Save File to Disk
                    var fileExt = Path.GetExtension(request.UploadFile);
                    var newUploadFileName = Guid.NewGuid() + fileExt;
                    string uploadPath = AppDomain.CurrentDomain.BaseDirectory + Get("settings.file.upload.folder");
                    if (request.UploadFileContent != null)
                    {
                        new Task(() =>
                        {
                            string outpath = uploadPath + "\\" + newUploadFileName;
                            File.WriteAllBytes(outpath, request.UploadFileContent);
                        }).Start();
                    }
                    else
                    {
                        newUploadFileName = "NA";
                    }

                    //Save Display Image to Disk
                    var imageFileExt = Path.GetExtension(request.DisplayImageFile);
                    var newImageFileName = Guid.NewGuid() + imageFileExt;
                    if (request.DisplayImageFileContent != null)
                    {
                        new Task(() =>
                        {
                            string displayImagePath = uploadPath + "\\" + newImageFileName;
                            File.WriteAllBytes(displayImagePath, request.DisplayImageFileContent);
                        }).Start();
                    }
                    else
                    {
                        newImageFileName = "NA";
                    }
                    //Save File Property to Db
                    string publicationTitle = StringCaseManager.TitleCase(request.Title);
                    var publication = new Publication
                    {
                        Title = publicationTitle,
                        Description = request.Description,
                        StatusId = (int)CoreObject.Enumerables.Status.Active,
                        CategoryId = request.CategoryId,
                        AccessRightId = request.AccessRightId,
                        UploadFile = newUploadFileName,
                        DisplayImageFile = newImageFileName,
                        Author = request.Author,
                        CreatedById = user.UserId,
                        DatePublished = request.DatePublished,
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
                LogService.LogError(ex.Message);
                var response = new Response
                {
                    ErrorCode = "01",
                    ErrorMessage = "Error occured while trying to insert record"
                };
                return response;
            }
        }


        public static Response MailPublication(MailPublicationRequest request, string currentUser)
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

                    //Confirm publication exist  
                    var publicationExist = db.Fetch<Publication>().SingleOrDefault(c => c.Id == request.PublicationId);
                    if (publicationExist == null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Record does not exist"
                        };
                    }

                    //Confirm Member access right


                    //Mail Publication

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
                    ErrorMessage = "Error occured while trying to perform operation"
                };
                return response;
            }
        }
    }
}
