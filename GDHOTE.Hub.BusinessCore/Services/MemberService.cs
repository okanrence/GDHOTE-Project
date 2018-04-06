using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.BusinessCore.BusinessLogic;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.CoreObject.Enumerables;
using GDHOTE.Hub.BusinessCore.Exceptions;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.ViewModels;
using Newtonsoft.Json;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class MemberService : BaseService
    {
        public static string Save(Member member)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(member);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new UnableToCompleteException(ex.Message, MethodBase.GetCurrentMethod().Name);
            }
        }
        public static List<MemberViewModel> GetAllMembers()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var members = db.Fetch<MemberViewModel>().OrderBy(m => m.FirstName).ToList();
                    return members;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<MemberViewModel>();
            }
        }
        public static List<Member> GetActiveMembers()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var members = db.Fetch<Member>()
                        .Where(m => m.MemberStatusId == (int)CoreObject.Enumerables.MemberStatus.Active && m.DateDeleted == null)
                        .OrderBy(m => m.FirstName)
                        .ToList();
                    return members;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<Member>();
            }
        }
        public static Member CheckIfMemberExist(Member memberRequest)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var member = db.Fetch<Member>()
                        .SingleOrDefault(m => m.Surname.ToLower().Equals(memberRequest.Surname.ToLower())
                        && m.FirstName.ToLower().Equals(memberRequest.FirstName.ToLower()));
                    return member;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                throw new UnableToCompleteException(ex.Message, MethodBase.GetCurrentMethod().Name);
            }
        }
        public static Member GetMember(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var member = db.Fetch<Member>().SingleOrDefault(m => m.Id == id);
                    return member;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new Member();
            }
        }
        public static string Update(Member member)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(member);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return "Error occured while trying to update member";
            }
        }
        public static Response Delete(int id, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var response = new Response();

                    var member = db.Fetch<Member>().SingleOrDefault(c => c.Id == id);
                    if (member == null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "User does not exist"
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

                    //Delete Bank
                    member.MemberStatusId = (int)CoreObject.Enumerables.MemberStatus.Deleted;
                    member.DeletedById = user.UserId;
                    member.DateDeleted = DateTime.Now;
                    db.Update(member);
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
        public static List<MemberViewModel> GetMembersPendingApproval()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var members = db.Fetch<MemberViewModel>()
                        .Where(m => m.ApprovedFlag == "N" && m.DateDeleted == null)
                        .OrderBy(m => m.DateCreated).ThenBy(m => m.FirstName)
                        .ToList();
                    return members;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<MemberViewModel>();
            }
        }

        public static Response CreateMember(CreateMemberRequest createRequest, string currentUser, int channelCode)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var response = new Response();

                    //Check member already profiled
                    var memberExist = db.Fetch<Member>()
                        .SingleOrDefault(m => m.Surname.ToLower().Equals(createRequest.Surname.ToLower())
                                              && m.FirstName.ToLower().Equals(createRequest.FirstName.ToLower()));
                    if (memberExist != null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Member already exist"
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
                    var member = JsonConvert.DeserializeObject<Member>(request);

                    member.CreatedById = user.UserId;
                    member.ChannelId = channelCode;
                    member.MemberStatusId = (int)CoreObject.Enumerables.MemberStatus.Active;
                    member.ApprovedFlag = "N";
                    member.DateCreated = DateTime.Now;
                    member.RecordDate = DateTime.Now;
                    member.OfficerId = (int)OfficerType.NormalMember;
                    member.OfficerDate = DateTime.Now;


                    var result = db.Insert(member);
                    //Insert member details
                    if (result != null)
                    {
                        if (int.TryParse(result.ToString(), out var memberKey))
                        {
                            var memberDetails = new MemberDetails
                            {
                                MemberId = memberKey,
                                MobileNumber = createRequest.MobileNumber,
                                EmailAddress = createRequest.EmailAddress,
                                CreatedById = user.UserId,
                                RecordDate = DateTime.Now
                            };

                            db.Save(memberDetails);
                            response = new Response
                            {
                                ErrorCode = "00",
                                ErrorMessage = "Successful",
                                Reference = memberKey.ToString()
                            };


                            //Notify member
                            new Task(() =>
                            {
                                var req = new EmailRequest
                                {
                                    Type = EmailType.RegistrationConfirmation,
                                    Subject = "Welcome to " + Get("settings.organisation.name"),
                                    RecipientEmailAddress = createRequest.EmailAddress,
                                    Data = new Hashtable
                                    {
                                        //["Subject"] = "Welcome to " + Get("settings.organisation.name"),
                                        ["FirstName"] = createRequest.FirstName,
                                        ["LastName"] = createRequest.Surname,
                                    }
                                };

                                EmailNotificationService.SendRegistrationConfirmationEmail(req, currentUser);

                            }).Start();
                        }
                    }
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


        public static Response ApproveMember(ApproveMemberRequest approveRequest, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {

                    var response = new Response();

                    //Check member already profiled
                    var member = db.Fetch<Member>()
                        .SingleOrDefault(m => m.Id == approveRequest.MemberId);

                    if (member == null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Record not found"
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


                    string nextsequence = SequenceManager.ReturnNextSequence(member.Gender);
                    string gender = member.Gender;

                    if (string.IsNullOrEmpty(nextsequence))
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Unable to generate sequence"
                        };

                    }


                    member.MemberCode = gender + nextsequence;
                    member.ApprovedById = user.UserId;
                    member.ApprovedFlag = "Y";
                    member.DateApproved = DateTime.Now;
                    member.DateUpdated = DateTime.Now;

                    db.Update(member);
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
