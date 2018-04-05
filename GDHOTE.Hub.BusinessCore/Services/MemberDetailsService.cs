using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.CoreObject.Enumerables;
using GDHOTE.Hub.CoreObject.ViewModels;
using Newtonsoft.Json;
using MemberStatus = GDHOTE.Hub.CoreObject.Enumerables.MemberStatus;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class MemberDetailsService : BaseService
    {
        public static string Save(MemberDetails memberDetails)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(memberDetails);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert member";
            }
        }
        public static List<MemberDetailsViewModel> GetMembersDetails()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var membersdetails = db.Fetch<MemberDetailsViewModel>()
                        .OrderBy(m => m.MemberKey)
                        .ToList();
                    return membersdetails;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<MemberDetailsViewModel>();
            }
        }
        public static MemberDetails GetMemberDetails(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var memberDetails = db.Fetch<MemberDetails>()
                        .SingleOrDefault(m => m.Id == id);
                    return memberDetails;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new MemberDetails();
            }
        }
        public static MemberDetails GetMemberDetailsByMemberKey(int memberKey)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var memberDetails = db.Fetch<MemberDetails>()
                        .SingleOrDefault(m => m.MemberKey == memberKey);
                    return memberDetails;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new MemberDetails();
            }
        }

        public static Response Delete(int id, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var response = new Response();

                    var memberDetails = db.Fetch<MemberDetails>().SingleOrDefault(c => c.MemberKey == id);
                    if (memberDetails == null)
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
                            ErrorMessage = "User does not exist"
                        };
                    }

                    //Delete Member Details
                    memberDetails.MemberStatusId = (int)MemberStatus.Deleted;
                    memberDetails.DeletedById = user.UserId;
                    memberDetails.DateDeleted = DateTime.Now;
                    db.Update(memberDetails);

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
        public static Response CreateMemberDetails(CreateMemberDetailsRequest createRequest, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var response = new Response();

                    //check member exist
                    var memberExist = db.Fetch<Member>().SingleOrDefault(m => m.MemberKey == createRequest.MemberKey);
                    if (memberExist == null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Member doesn't already exist"
                        };
                    }


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


                    var request = JsonConvert.SerializeObject(createRequest);
                    var memberDetails = JsonConvert.DeserializeObject<MemberDetails>(request);

                    memberDetails.CreatedById = user.UserId;
                    memberDetails.DateCreated = DateTime.Now;
                    memberDetails.RecordDate = DateTime.Now;

                    db.Insert(memberDetails);
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

        public static Response UpdateMemberDetails(UpdateMemberDetailsRequest updateRequest, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var response = new Response();


                    //check member exist


                    //check member details exist
                    var memberDetailsExist = db.Fetch<MemberDetails>().SingleOrDefault(m => m.MemberKey == updateRequest.MemberKey);
                    if (memberDetailsExist == null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Member Details doesn't already exist"
                        };
                    }


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


                    var request = JsonConvert.SerializeObject(updateRequest);
                    memberDetailsExist = JsonConvert.DeserializeObject<MemberDetails>(request);

                    memberDetailsExist.UpdatedById = user.UserId;
                    memberDetailsExist.DateUpdated = DateTime.Now;
                    db.Update(memberDetailsExist);
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
