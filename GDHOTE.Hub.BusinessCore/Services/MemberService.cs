using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc.Html;
using GDHOTE.Hub.BusinessCore.BusinessLogic;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.CoreObject.Enumerables;
using GDHOTE.Hub.BusinessCore.Exceptions;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.ViewModels;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;

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
                    member.InitiationDate = member.InitiationStatus == false ? null : member.InitiationDate;
                    member.MagusDate = member.MagusStatus == false ? null : member.MagusDate;
                    member.DateCreated = DateTime.Now;
                    member.RecordDate = DateTime.Now;
                    member.OfficerId = (int)OfficerType.NormalMember;
                    member.OfficerDate = DateTime.Now;

                    var result = db.Insert(member);

                    //Insert member details
                    if (result != null)
                    {
                        if (int.TryParse(result.ToString(), out var memberId))
                        {
                            var memberDetails = new MemberDetails
                            {
                                MemberId = memberId,
                                MobileNumber = createRequest.MobileNumber,
                                EmailAddress = createRequest.EmailAddress,
                                CreatedById = user.UserId,
                                DateCreated = DateTime.Now,
                                RecordDate = DateTime.Now
                            };

                            db.Insert(memberDetails);
                            response = new Response
                            {
                                ErrorCode = "00",
                                ErrorMessage = "Successful",
                                Reference = memberId.ToString()
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


        public static Response UploadMembers(UploadMemberRequest uploadRequest, string currentUser)
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


                    int recordCount = 0;

                    var stream = new MemoryStream(uploadRequest.UploadFileContent);
                    var package = new ExcelPackage(stream);

                    ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                    if (workSheet.Dimension.End.Row > 10000)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "We can only process 10000 records"
                        };
                    }

                    var channelId = uploadRequest.ChannelCode;

                    //Get Countries
                    var countries = db.Fetch<Country>().ToList();

                    //Get States
                    var states = db.Fetch<State>().ToList();


                    for (int i = workSheet.Dimension.Start.Column + 1; i <= workSheet.Dimension.End.Row; i++)
                    {
                        var surname = workSheet.Cells[i, 1].Value != null ? workSheet.Cells[i, 1].Value.ToString() : null;
                        var firstName = workSheet.Cells[i, 2].Value != null ? workSheet.Cells[i, 2].Value.ToString() : null;
                        var middleName = workSheet.Cells[i, 3].Value != null ? workSheet.Cells[i, 3].Value.ToString() : null;
                        var mobileNumber = workSheet.Cells[i, 4].Value != null ? workSheet.Cells[i, 4].Value.ToString() : null;
                        var alternateNumber = workSheet.Cells[i, 5].Value != null ? workSheet.Cells[i, 5].Value.ToString() : null;
                        var dateOfBirthString = workSheet.Cells[i, 6].Value != null ? workSheet.Cells[i, 6].Value.ToString() : null;
                        var gender = workSheet.Cells[i, 7].Value != null ? workSheet.Cells[i, 7].Value.ToString() : null;
                        var maritalStatus = workSheet.Cells[i, 8].Value != null ? workSheet.Cells[i, 8].Value.ToString() : null;
                        var emailAddress = workSheet.Cells[i, 9].Value != null ? workSheet.Cells[i, 9].Value.ToString() : null;
                        var initiationDateString = workSheet.Cells[i, 10].Value != null ? workSheet.Cells[i, 10].Value.ToString() : null;
                        var magusDateString = workSheet.Cells[i, 11].Value != null ? workSheet.Cells[i, 11].Value.ToString() : null;
                        var residenceAddress = workSheet.Cells[i, 12].Value != null ? workSheet.Cells[i, 12].Value.ToString() : null;
                        var residenceCountry = workSheet.Cells[i, 13].Value != null ? workSheet.Cells[i, 13].Value.ToString() : null;
                        var residenceState = workSheet.Cells[i, 14].Value != null ? workSheet.Cells[i, 14].Value.ToString() : null;
                        var dateWeddedString = workSheet.Cells[i, 15].Value != null ? workSheet.Cells[i, 15].Value.ToString() : null;
                        var highestDegreeObtained = workSheet.Cells[i, 16].Value != null ? workSheet.Cells[i, 16].Value.ToString() : null;
                        var currentPlaceOfWork = workSheet.Cells[i, 17].Value != null ? workSheet.Cells[i, 17].Value.ToString() : null;
                        var guardianAngel = workSheet.Cells[i, 18].Value != null ? workSheet.Cells[i, 18].Value.ToString() : null;
                        var yeargroup = workSheet.Cells[i, 19].Value != null ? workSheet.Cells[i, 19].Value.ToString() : null;


                        //Check member already profiled
                        var memberExist = db.Fetch<Member>().SingleOrDefault(m => m.Surname.ToLower().Equals(surname.ToLower())
                                                  && m.FirstName.ToLower().Equals(firstName.ToLower()));

                        //Insert if new member
                        if (memberExist == null)
                        {
                            recordCount += 1;
                            //DateTime.TryParse(magusDateString, out var magusDate);
                            //DateTime.TryParse(initiationDateString, out var initiationDate2);
                            //DateTime.TryParse(dateWeddedString, out var dateWedded);

                            var member = new Member
                            {
                                FirstName = StringCaseManager.TitleCase(firstName),
                                MiddleName = StringCaseManager.TitleCase(middleName),
                                Surname = StringCaseManager.TitleCase(surname),
                                Gender = !string.IsNullOrEmpty(gender) ? gender.Substring(0, 1) : "M",
                                MaritalStatus = !string.IsNullOrEmpty(maritalStatus) ? maritalStatus.Substring(0, 1) : "S",
                                DateOfBirth = DateTime.TryParse(dateOfBirthString, out var dateOfBirth)
                                    ? DateTime.Parse(dateOfBirthString) : DateTime.Now,
                                MemberStatusId = (int)CoreObject.Enumerables.MemberStatus.Active,
                                CreatedById = user.UserId,
                                ChannelId = channelId,
                                ApprovedFlag = "N",
                                MagusDate = DateTime.TryParse(magusDateString, out var magusDate)
                                    ? DateTime.Parse(magusDateString) : (DateTime?)null,
                                MagusStatus = magusDate == null ? false : true,
                                InitiationDate = DateTime.TryParse(initiationDateString, out var initiationDate)
                                    ? DateTime.Parse(magusDateString) : (DateTime?)null,
                                InitiationStatus = initiationDate == null ? false : true,
                                DateCreated = DateTime.Now,
                                RecordDate = DateTime.Now,
                                OfficerId = (int)OfficerType.NormalMember,
                                OfficerDate = DateTime.Now
                            };
                            var result = db.Insert(member);

                            int residenceStateId = 1;
                            if (!string.IsNullOrEmpty(residenceState))
                            {
                                var state = states.SingleOrDefault(s => s.Name.ToLower().Equals(residenceState.ToLower()));
                                if (state != null) residenceStateId = state.Id;
                            }

                            int residenceCountryId = 1;
                            if (!string.IsNullOrEmpty(residenceCountry))
                            {
                                var country = countries.SingleOrDefault(c => c.Name.ToLower().Equals(residenceCountry.ToLower()));
                                if (country != null) residenceCountryId = country.Id;
                            }
                            //Insert member details
                            if (result != null)
                            {
                                if (int.TryParse(result.ToString(), out var memberKey))
                                {
                                    var memberDetails = new MemberDetails
                                    {
                                        MemberId = memberKey,
                                        MobileNumber = mobileNumber,
                                        AlternateNumber = alternateNumber,
                                        EmailAddress = emailAddress,
                                        StateOfOriginId = 1,
                                        CountryOfOriginId = 1,
                                        ResidenceStateId = residenceStateId,
                                        ResidenceCountryId = residenceCountryId,
                                        ResidenceAddress = StringCaseManager.TitleCase(residenceAddress),
                                        HighestDegreeObtained = StringCaseManager.TitleCase(highestDegreeObtained),
                                        CurrentWorkPlace = StringCaseManager.TitleCase(currentPlaceOfWork),
                                        DateWedded = DateTime.TryParse(dateWeddedString, out var dateWedded)
                                            ? DateTime.Parse(dateWeddedString) : (DateTime?)null,
                                        GuardianAngel = StringCaseManager.TitleCase(guardianAngel),
                                        MemberStatusId = (int)CoreObject.Enumerables.MemberStatus.Active,
                                        CreatedById = user.UserId,
                                        DateCreated = DateTime.Now,
                                        RecordDate = DateTime.Now
                                    };
                                    db.Insert(memberDetails);
                                }
                            }

                        }
                    }
                    response = new Response
                    {
                        ErrorCode = "00",
                        ErrorMessage = recordCount.ToString() + " successful loaded"
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
                    ErrorMessage = "Error occured while trying to upload record(s)"
                };
                return response;
            }
        }
    }
}
