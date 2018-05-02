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
                        .OrderBy(m => m.MemberId)
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
                        .SingleOrDefault(m => m.MemberId == memberKey);
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

                    var memberDetails = db.Fetch<MemberDetails>().SingleOrDefault(c => c.MemberId == id);
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
                    memberDetails.DeletedById = user.Id;
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
                    var memberExist = db.Fetch<Member>().SingleOrDefault(m => m.Id == createRequest.MemberId);
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

                    memberDetails.CreatedById = user.Id;
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

                    //check member details exist
                    var detailsExist = db.Fetch<MemberDetails>()
                        .SingleOrDefault(m => m.Id == updateRequest.Id);
                    if (detailsExist == null)
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

                    //Update member details
                    detailsExist.MobileNumber = updateRequest.MobileNumber;
                    detailsExist.AlternateNumber = updateRequest.AlternateNumber;
                    detailsExist.EmailAddress = updateRequest.EmailAddress;
                    detailsExist.StateOfOriginId = updateRequest.StateOfOriginId;
                    detailsExist.CountryOfOriginId = updateRequest.CountryOfOriginId;
                    detailsExist.ResidenceStateId = updateRequest.ResidenceStateId;
                    detailsExist.ResidenceCountryId = updateRequest.ResidenceCountryId;
                    detailsExist.ResidenceAddress = updateRequest.ResidenceAddress;
                    detailsExist.DateWedded = updateRequest.DateWedded;
                    detailsExist.HighestDegreeObtained = updateRequest.HighestDegreeObtained;
                    detailsExist.CurrentWorkPlace = updateRequest.CurrentWorkPlace;
                    detailsExist.UpdatedById = user.Id;
                    detailsExist.DateUpdated = DateTime.Now;
                    db.Update(detailsExist);

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
                    ErrorMessage = "Error occured while trying to update record"
                };
                return response;
            }
        }

        public static List<MemberDetailsViewModel> GetMembersDetailsByCriteria(int criteria, string startdate, string enddate)
        {
            try
            {
                DateTime.TryParse(startdate, out var castStartDate);
                DateTime.TryParse(enddate, out var castEndDate);
                using (var db = GdhoteConnection())
                {
                    var details = new List<MemberDetailsViewModel>();
                    switch (criteria)
                    {
                        case 1:
                            details = db.Fetch<MemberDetailsViewModel>()
                                .Where(m => m.MagusDate >= castStartDate && m.MagusDate < castEndDate.AddDays(1))
                                .OrderBy(m => m.FirstName).ThenBy(m => m.Surname)
                                .ToList();
                            break;
                    }
                    return details;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<MemberDetailsViewModel>();
            }

        }
    }
}
