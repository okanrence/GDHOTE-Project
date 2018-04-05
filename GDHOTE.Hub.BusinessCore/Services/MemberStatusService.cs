using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.BusinessCore.BusinessLogic;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.CoreObject.ViewModels;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class MemberStatusService : BaseService
    {
        public static string Save(MemberStatus memberStatus)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(memberStatus);
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
        public static List<MemberStatusViewModel> GetAllMemberStatuses()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var memberStatuses = db.Fetch<MemberStatusViewModel>()
                       .OrderBy(c => c.Name)
                        .ToList();
                    return memberStatuses;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<MemberStatusViewModel>();
            }
        }
        public static List<MemberStatus> GetActiveMemberStatuses()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var memberStatuses = db.Fetch<MemberStatus>()
                        .Where(m => m.StatusId == (int)CoreObject.Enumerables.Status.Active && m.DateDeleted == null)
                        .OrderBy(m => m.Name)
                        .ToList();
                    return memberStatuses;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<MemberStatus>();
            }
        }
        public static MemberStatus GetMemberStatus(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var memberStatus = db.Fetch<MemberStatus>()
                        .SingleOrDefault(c => c.Id == id);
                    return memberStatus;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new MemberStatus();
            }
        }
        public static string Update(MemberStatus memberStatus)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(memberStatus);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return "Error occured while trying to update MemberStatus";
            }
        }
        public static string Delete(int id, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var memberStatus = db.Fetch<MemberStatus>().SingleOrDefault(c => c.Id == id);
                    if (memberStatus == null)
                    {
                        return "Record does not exist";
                    }

                    //Get User Initiating Creation Request
                    var user = UserService.GetUserByUserName(currentUser);


                    //Delete Member Status
                    memberStatus.StatusId = (int) CoreObject.Enumerables.Status.Deleted;
                    memberStatus.DeletedById = user.UserId;
                    memberStatus.DateDeleted = DateTime.Now;
                    db.Update(memberStatus);
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

        public static Response CreateMemberStatus(CreateMemberStatusRequest request, string currentUser)
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
                    var memberStatusExist = db.Fetch<MemberStatus>().SingleOrDefault(c => c.Name.ToLower().Equals(request.Name.ToLower()));
                    if (memberStatusExist != null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Record already exist"
                        };
                    }


                    string typeName = StringCaseManager.TitleCase(request.Name);


                    var memberStatus = new MemberStatus
                    {
                        Name = typeName,
                        StatusId = (int)CoreObject.Enumerables.Status.Active,
                        CreatedById = user.UserId,
                        DateCreated = DateTime.Now,
                        RecordDate = DateTime.Now
                    };

                    db.Insert(memberStatus);
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
