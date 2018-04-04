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
                LogService.myLog(ex.Message);
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
                LogService.myLog(ex.Message);
                return new List<PublicationCategoryViewModel>();
            }
        }
        public static List<PublicationCategory> GetActivePublicationCategories()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var publicationCategories = db.Fetch<PublicationCategory>()
                        .Where(c => c.StatusId == (int)CoreObject.Enumerables.Status.Active && c.DateDeleted == null)
                        .OrderBy(c => c.Name)
                        .ToList();
                    return publicationCategories;
                }
            }
            catch (Exception ex)
            {
                LogService.myLog(ex.Message);
                return new List<PublicationCategory>();
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
                LogService.myLog(ex.Message);
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
                LogService.myLog(ex.Message);
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
                LogService.myLog(ex.Message);
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

                    string categoryName = StringCaseManager.TitleCase(request.Name);

                    var publicationCategory = new PublicationCategory
                    {
                        Name = categoryName,
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
                LogService.myLog(ex.Message);
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
