using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.BusinessCore.BusinessLogic;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.CoreObject.ViewModels;
using Newtonsoft.Json;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class PublicationCategoryService : BaseService
    {
        public static string Save(PublicationCategory publicationCategory)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(publicationCategory);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert PublicationCategory";
            }
        }
        public static List<PublicationCategoryViewModel> GetAllPublicationCategories()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var publicationCategories = db.Fetch<PublicationCategoryViewModel>()
                        .OrderBy(c => c.Name)
                        .ToList();
                    return publicationCategories;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<PublicationCategoryViewModel>();
            }
        }

        public static List<PublicationCategoryResponse> GetActivePublicationCategories()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var publicationCategories = db.Fetch<PublicationCategoryViewModel>()
                        .Where(c => c.StatusId == (int)CoreObject.Enumerables.Status.Active && c.DateDeleted == null)
                        .OrderBy(c => c.Name)
                        .ToList();
                    var response = new List<PublicationCategoryResponse>();
                    if (publicationCategories != null)
                    {
                        string imgageUrl = ReturnBaseUrl() + "/" + Get("settings.file.upload.folder");
                        foreach (var pubCategory in publicationCategories)
                        {
                            var categoryResponse = new PublicationCategoryResponse
                            {
                                Id = pubCategory.Id,
                                Name = pubCategory.Name,
                                DisplayImageUrl = imgageUrl + "/" + pubCategory.DisplayImageFile
                            };
                            response.Add(categoryResponse);
                        }
                    }
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<PublicationCategoryResponse>();
            }
        }

        public static PublicationCategory GetPublicationCategory(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var publicationCategory = db.Fetch<PublicationCategory>()
                        .SingleOrDefault(c => c.Id == id);
                    return publicationCategory;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new PublicationCategory();
            }
        }
        public static string Update(PublicationCategory publicationCategory)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(publicationCategory);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return "Error occured while trying to update Publication Category";
            }
        }
        public static string Delete(int id, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {


                    var publicationCategory = db.Fetch<PublicationCategory>().SingleOrDefault(c => c.Id == id);
                    if (publicationCategory == null)
                    {
                        return "Record does not exist";
                    }

                    //Get User Initiating Creation Request
                    var user = UserService.GetUserByUserName(currentUser);

                    //Delete Publication Category
                    publicationCategory.StatusId = (int)CoreObject.Enumerables.Status.Deleted;
                    publicationCategory.DeletedById = user.UserId;
                    publicationCategory.DateDeleted = DateTime.Now;
                    db.Update(publicationCategory);
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

        public static Response CreatePublicationCategory(CreatePublicationCategoryRequest request, string currentUser)
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
                    var publicationCategoryExist = db.Fetch<PublicationCategory>()
                        .SingleOrDefault(c => c.Name.ToLower().Equals(request.Name.ToLower()));
                    if (publicationCategoryExist != null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Record already exist"
                        };
                    }

                    //Save File to Disk
                    var fileExt = Path.GetExtension(request.DisplayImageFile);
                    var newFileName = Guid.NewGuid() + fileExt;
                    string uploadPath = ReturnFileUploadPath();
                    //string uploadPath =  AppDomain.CurrentDomain.BaseDirectory + Get("settings.file.upload.folder");
                    LogService.LogError(uploadPath);
                    if (request.DisplayImageFileContent != null)
                    {
                        new Task(() =>
                        {
                            string outpath = uploadPath + "\\" + newFileName;
                            File.WriteAllBytes(outpath, request.DisplayImageFileContent);
                        }).Start();
                    }

                    //Save File Property to Db
                    string categoryName = StringCaseManager.TitleCase(request.Name);
                    var publicationCategory = new PublicationCategory
                    {
                        Name = categoryName,
                        DisplayImageFile = newFileName,
                        StatusId = (int)CoreObject.Enumerables.Status.Active,
                        CreatedById = user.UserId,
                        DateCreated = DateTime.Now,
                        RecordDate = DateTime.Now
                    };

                    db.Insert(publicationCategory);
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
