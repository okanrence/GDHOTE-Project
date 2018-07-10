using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.CoreObject.ViewModels;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class CheckerService : BaseService
    {
        public static string Save(Checker checker)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(checker);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert Checker";
            }
        }

        public static List<Checker> GetAllCheckers()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var checkers = db.Fetch<Checker>()
                        .OrderBy(c => c.ApplicationId)
                        .ToList();
                    return checkers;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<Checker>();
            }
        }

        public static List<Checker> GetActiveCheckers()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var checkers = db.Fetch<Checker>()
                        .Where(c => c.StatusId == (int)CoreObject.Enumerables.Status.Active)
                        .OrderBy(c => c.ApplicationId)
                        .ToList();
                    return checkers;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<Checker>();
            }
        }

        public static Checker GetCheckerByAppId(string applicationId)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var checker = db.Fetch<Checker>()
                        .SingleOrDefault(c => c.ApplicationId == applicationId);
                    return checker;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new Checker();
            }
        }

        public static Response Delete(string id, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var response = new Response();

                    var checker = db.Fetch<Checker>().SingleOrDefault(c => c.ApplicationId == id);
                    if (checker == null)
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

                    //Delete Checker
                    checker.StatusId = (int)CoreObject.Enumerables.Status.Deleted;
                    //checker.DeletedById = user.Id;
                    //checker.DateDeleted = DateTime.Now;
                    db.Update(checker);
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

        public static Response UpdateCheckerByAppId(string appid, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var response = new Response();

                    var checker = db.Fetch<Checker>()
                        .SingleOrDefault(c => c.ApplicationId == appid);
                    if (checker == null)
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

                    //Delete Checker
                    checker.StatusId = (int)CoreObject.Enumerables.Status.Deleted;
                    checker.LastCheckDate = DateTime.Now;
                    checker.CheckDate = DateTime.Now.Date;
                    db.Update(checker);
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

        public static string Update(Checker checker)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(checker);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                if (ex.Message.Contains("The duplicate key")) return "Cannot update duplicate record";
                return "Error occured while trying to update Checker";
            }
        }

        public static Response CreateChecker(CreateCheckerRequest request, string currentUser)
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
                    var checkerExist = db.Fetch<Checker>()
                        .SingleOrDefault(c => c.ApplicationId.ToLower().Equals(request.ApplicationId.ToLower()));
                    if (checkerExist != null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Record already exist"
                        };
                    }

                    string applicationId = StringCaseService.TitleCase(request.ApplicationId);

                    var checker = new Checker
                    {
                        ApplicationId = applicationId,
                        StatusId = (int)CoreObject.Enumerables.Status.Active,
                        StartDate = request.StartDate,
                        EndDate = request.EndDate,
                        CheckDate = DateTime.Now,
                        LastCheckDate = DateTime.Now
                    };

                    db.Insert(checker);
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
