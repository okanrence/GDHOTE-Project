using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
        public static Member GetMember(string memberKey)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var member = db.Fetch<Member>()
                        .SingleOrDefault(m => m.MemberKey == memberKey);
                    return member;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new Member();
            }
        }

        public static Member GetMemberOld(int id)
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
        public static Response Delete(string memberKey, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var response = new Response();

                    var member = db.Fetch<Member>()
                        .SingleOrDefault(c => c.MemberKey == memberKey);
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
                    member.DeletedById = user.Id;
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
                                              && m.FirstName.ToLower().Equals(createRequest.FirstName.ToLower())
                                              && m.OtherNames.ToLower().Equals(createRequest.OtherNames.ToLower()));
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

                    string surname = StringCaseService.TitleCase(member.Surname);
                    string firstName = StringCaseService.TitleCase(member.FirstName);
                    string othertNames = StringCaseService.TitleCase(member.OtherNames);

                    member.Surname = surname;
                    member.FirstName = firstName;
                    member.OtherNames = othertNames;
                    member.ChannelId = channelCode;
                    member.MemberStatusId = (int)CoreObject.Enumerables.MemberStatus.Active;
                    member.ApprovedFlag = "N";
                    member.InitiationDate = member.InitiationStatus == false ? null : member.InitiationDate;
                    member.MagusDate = member.MagusStatus == false ? null : member.MagusDate;
                    member.MemberKey = Guid.NewGuid().ToString();
                    member.OfficerId = (int)OfficerType.NormalMember;
                    member.StatusId = (int)CoreObject.Enumerables.Status.Active;
                    member.CreatedById = user.Id;
                    member.OfficerDate = DateTime.Now;
                    member.DateCreated = DateTime.Now;
                    member.RecordDate = DateTime.Now;
                    
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
                                StatusId = (int)CoreObject.Enumerables.Status.Active,
                                MemberDetailsKey = Guid.NewGuid().ToString(),
                                CreatedById = user.Id,
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
                        }
                    }

                    //Insert member account details
                    if (result != null)
                    {
                        if (int.TryParse(result.ToString(), out var memberId))
                        {
                            var account = new Account
                            {
                                MemberId = memberId,
                                AccountName = surname + " " + firstName,
                                Balance = 0,
                                BankId = 0,
                                CurrencyId = (int)CoreObject.Enumerables.Currency.Naira,
                                StatusId = (int)CoreObject.Enumerables.Status.Active,
                                AccountTypeId = (int)CoreObject.Enumerables.AccountType.Member,
                                AccountKey = Guid.NewGuid().ToString(),
                                CreatedById = user.Id,
                                DateCreated = DateTime.Now,
                                RecordDate = DateTime.Now
                            };
                            db.Insert(account);
                        }
                    }

                    //Notify member by Sms
                    if (result != null)
                    {
                        if (int.TryParse(result.ToString(), out var memberId))
                        {
                            if (!string.IsNullOrEmpty(createRequest.MobileNumber))
                            {
                                new Task(() =>
                                {
                                    var req = new SmsMessageRequest
                                    {
                                        Message = "Dear " + createRequest.FirstName + " " + createRequest.Surname + ", your details has been successfully submitted.",
                                        MobileNumber = createRequest.MobileNumber,
                                        //Type = SmsType.CustomerProfile,
                                        //CustomerId = emailExist.CustomerId
                                    };
                                    SmsNotificationService.SendMessage(req, currentUser);
                                }).Start();
                            }

                        }
                    }


                    //Notify member by Mail
                    if (result != null)
                    {
                        if (int.TryParse(result.ToString(), out var memberId))
                        {
                            new Task(() =>
                            {
                                var req = new EmailRequest
                                {
                                    Type = EmailType.RegistrationConfirmation,
                                    Subject = "Welcome to " + Get("settings.organisation.name"),
                                    RecipientEmailAddress = createRequest.EmailAddress,
                                    Data = new Hashtable
                                    {
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
                    member.ApprovedById = user.Id;
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


        public static Response UpdateMember(UpdateMemberRequest updateRequest, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {

                    var response = new Response();

                    //Check member exist
                    var memberExist = db.Fetch<Member>()
                        .SingleOrDefault(m => m.MemberKey == updateRequest.MemberKey);

                    if (memberExist == null)
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

                    //Log audit trail

                    //Update
                    memberExist.Surname = StringCaseService.TitleCase(updateRequest.Surname);
                    memberExist.FirstName = StringCaseService.TitleCase(updateRequest.FirstName);
                    memberExist.OtherNames = StringCaseService.TitleCase(updateRequest.OtherNames);
                    memberExist.MaritalStatus = updateRequest.MaritalStatus;
                    memberExist.DateOfBirth = updateRequest.DateOfBirth;
                    memberExist.InitiationDate = updateRequest.InitiationDate;
                    memberExist.MagusDate = updateRequest.MagusDate;
                    memberExist.UpdatedById = user.Id;
                    memberExist.DateUpdated = DateTime.Now;
                    db.Update(memberExist);
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

                    //Get Member Statuses
                    var memberStatuses = db.Fetch<CoreObject.Models.MemberStatus>().ToList();

                    //Get Countries
                    var countries = db.Fetch<Country>().ToList();

                    //Get States
                    var states = db.Fetch<State>().ToList();


                    for (int i = workSheet.Dimension.Start.Column + 1; i <= workSheet.Dimension.End.Row; i++)
                    {
                        var memberCode = workSheet.Cells[i, 1].Value != null ? workSheet.Cells[i, 1].Value.ToString() : null;
                        var surname = workSheet.Cells[i, 2].Value != null ? workSheet.Cells[i, 2].Value.ToString() : null;
                        var firstName = workSheet.Cells[i, 3].Value != null ? workSheet.Cells[i, 3].Value.ToString() : null;
                        var othernames = workSheet.Cells[i, 4].Value != null ? workSheet.Cells[i, 4].Value.ToString() : null;
                        var dateOfBirthString = workSheet.Cells[i, 5].Value != null ? workSheet.Cells[i, 5].Value.ToString() : null;
                        var gender = workSheet.Cells[i, 6].Value != null ? workSheet.Cells[i, 6].Value.ToString() : null;
                        var maritalStatus = workSheet.Cells[i, 7].Value != null ? workSheet.Cells[i, 7].Value.ToString() : null;
                        var initiationDateString = workSheet.Cells[i, 8].Value != null ? workSheet.Cells[i, 8].Value.ToString() : null;
                        var magusDateString = workSheet.Cells[i, 9].Value != null ? workSheet.Cells[i, 9].Value.ToString() : null;
                        var guardianAngel = workSheet.Cells[i, 10].Value != null ? workSheet.Cells[i, 10].Value.ToString() : null;
                        var yeargroupString = workSheet.Cells[i, 11].Value != null ? workSheet.Cells[i, 11].Value.ToString() : null;
                        var memberStatusString = workSheet.Cells[i, 12].Value != null ? workSheet.Cells[i, 12].Value.ToString() : null;
                        var mobileNumber = workSheet.Cells[i, 13].Value != null ? workSheet.Cells[i, 13].Value.ToString() : null;
                        var alternateNumber = workSheet.Cells[i, 14].Value != null ? workSheet.Cells[i, 14].Value.ToString() : null;
                        var emailAddress = workSheet.Cells[i, 15].Value != null ? workSheet.Cells[i, 15].Value.ToString() : null;
                        var residenceAddress = workSheet.Cells[i, 16].Value != null ? workSheet.Cells[i, 16].Value.ToString() : null;
                        var residenceCountry = workSheet.Cells[i, 17].Value != null ? workSheet.Cells[i, 17].Value.ToString() : null;
                        var residenceState = workSheet.Cells[i, 18].Value != null ? workSheet.Cells[i, 18].Value.ToString() : null;
                        var dateWeddedString = workSheet.Cells[i, 19].Value != null ? workSheet.Cells[i, 19].Value.ToString() : null;
                        var highestDegreeObtained = workSheet.Cells[i, 20].Value != null ? workSheet.Cells[i, 20].Value.ToString() : null;
                        var currentPlaceOfWork = workSheet.Cells[i, 21].Value != null ? workSheet.Cells[i, 21].Value.ToString() : null;


                        if (string.IsNullOrEmpty(othernames)) othernames = "";

                        if (!string.IsNullOrEmpty(surname) && !string.IsNullOrEmpty(firstName))
                        {
                            //Check member already profiled
                            var memberExist = db.Fetch<Member>()
                             .SingleOrDefault(m => m.Surname.ToLower().Equals(surname.ToLower())
                                                  && m.FirstName.ToLower().Equals(firstName.ToLower())
                                                                                   && m.OtherNames.ToLower().Equals(othernames.ToLower()));
                            //Insert if new member
                            if (memberExist == null)
                            {
                                int memberStatusId = 1;
                                if (!string.IsNullOrEmpty(memberStatusString))
                                {
                                    var memberStatus = memberStatuses
                                        .SingleOrDefault(c => c.Name.ToLower().Equals(memberStatusString.ToLower()));
                                    if (memberStatus != null) memberStatusId = memberStatus.Id;
                                }

                                recordCount += 1;
                                firstName = StringCaseService.TitleCase(firstName);
                                surname = StringCaseService.TitleCase(surname);
                                gender = !string.IsNullOrEmpty(gender) ? gender.Substring(0, 1) : "M";


                                var member = new Member
                                {
                                    MemberCode = SequenceManager.PadGenderSequence(gender, memberCode),
                                    Surname = surname,
                                    FirstName = firstName,
                                    OtherNames = StringCaseService.TitleCase(othernames),
                                    Gender = gender,
                                    MaritalStatus = !string.IsNullOrEmpty(maritalStatus) ? maritalStatus.Substring(0, 1) : "S",
                                    DateOfBirth = DateTime.TryParse(dateOfBirthString, out var dateOfBirth)
                                        ? DateTime.Parse(dateOfBirthString) : (DateTime?)null,
                                    MemberStatusId = memberStatusId,//(int)CoreObject.Enumerables.MemberStatus.Active,
                                    ChannelId = channelId,
                                    ApprovedFlag = "N",
                                    MagusDate = DateTime.TryParse(magusDateString, out var magusDate)
                                        ? DateTime.Parse(magusDateString) : (DateTime?)null,
                                    MagusStatus = magusDate == null ? false : true,
                                    InitiationDate = DateTime.TryParse(initiationDateString, out var initiationDate)
                                        ? DateTime.Parse(initiationDateString) : (DateTime?)null,
                                    InitiationStatus = initiationDate == null ? false : true,
                                    MemberKey = Guid.NewGuid().ToString(),
                                    OfficerId = (int)OfficerType.NormalMember,
                                    OfficerDate = DateTime.Now,
                                    StatusId = (int)CoreObject.Enumerables.Status.Active,
                                    CreatedById = user.Id,
                                    DateCreated = DateTime.Now,
                                    RecordDate = DateTime.Now

                                };
                                var result = db.Insert(member);

                                long memberId = 0;
                                int residenceStateId = 1;
                                int residenceCountryId = 1;

                                if (result != null)
                                {
                                    if (long.TryParse(result.ToString(), out memberId))
                                    {
                                        if (!string.IsNullOrEmpty(residenceState))
                                        {
                                            var state = states.SingleOrDefault(s => s.Name.ToLower().Equals(residenceState.ToLower()));
                                            if (state != null) residenceStateId = state.Id;
                                        }

                                        if (!string.IsNullOrEmpty(residenceCountry))
                                        {
                                            var country = countries.SingleOrDefault(c => c.Name.ToLower().Equals(residenceCountry.ToLower()));
                                            if (country != null) residenceCountryId = country.Id;
                                        }
                                    }
                                }

                                //Insert member details
                                if (memberId > 0)
                                {
                                    var memberDetails = new MemberDetails
                                    {
                                        MemberId = memberId,
                                        MobileNumber = mobileNumber,
                                        AlternateNumber = alternateNumber,
                                        EmailAddress = emailAddress,
                                        StateOfOriginId = 1,
                                        CountryOfOriginId = 1,
                                        ResidenceStateId = residenceStateId,
                                        ResidenceCountryId = residenceCountryId,
                                        ResidenceAddress = StringCaseService.TitleCase(residenceAddress),
                                        HighestDegreeObtained = StringCaseService.TitleCase(highestDegreeObtained),
                                        CurrentWorkPlace = StringCaseService.TitleCase(currentPlaceOfWork),
                                        DateWedded = DateTime.TryParse(dateWeddedString, out var dateWedded)
                                            ? DateTime.Parse(dateWeddedString) : (DateTime?)null,
                                        GuardianAngel = StringCaseService.TitleCase(guardianAngel),
                                        StatusId = (int)CoreObject.Enumerables.Status.Active,
                                        MemberDetailsKey = Guid.NewGuid().ToString(),
                                        YearGroupId = Int16.TryParse(yeargroupString, out var yeargroup)
                                            ? Int16.Parse(yeargroupString) : 0,
                                        CreatedById = user.Id,
                                        DateCreated = DateTime.Now,
                                        RecordDate = DateTime.Now
                                    };
                                    db.Insert(memberDetails);
                                }

                                //Insert activity for Initiation Date
                                if (memberId > 0)
                                {
                                    if (member.InitiationDate != null)
                                    {
                                        var activity = new Activity
                                        {
                                            MemberId = memberId,
                                            ActivityTypeId = (int)CoreObject.Enumerables.ActivityType.Initiation,
                                            StartDate = member.InitiationDate.Value,
                                            StatusId = (int)CoreObject.Enumerables.Status.Active,
                                            ActivityKey = Guid.NewGuid().ToString(),
                                            CreatedById = user.Id,
                                            DateCreated = DateTime.Now,
                                            RecordDate = DateTime.Now
                                        };
                                        db.Insert(activity);
                                    }
                                }


                                //Insert activity for Magus Date
                                if (memberId > 0)
                                {
                                    if (member.MagusDate != null)
                                    {
                                        var activity = new Activity
                                        {
                                            MemberId = memberId,
                                            ActivityTypeId = (int)CoreObject.Enumerables.ActivityType.Maguship,
                                            StartDate = member.InitiationDate.Value,
                                            StatusId = (int)CoreObject.Enumerables.Status.Active,
                                            ActivityKey = Guid.NewGuid().ToString(),
                                            CreatedById = user.Id,
                                            DateCreated = DateTime.Now,
                                            RecordDate = DateTime.Now
                                        };
                                        db.Insert(activity);
                                    }
                                }


                                //Insert member account details
                                if (memberId > 0)
                                {
                                    var account = new Account
                                    {
                                        MemberId = memberId,
                                        AccountName = surname + " " + firstName,
                                        Balance = 0,
                                        BankId = 0,
                                        CurrencyId = (int)CoreObject.Enumerables.Currency.Naira,
                                        StatusId = (int)CoreObject.Enumerables.Status.Active,
                                        AccountTypeId = (int)CoreObject.Enumerables.AccountType.Member,
                                        AccountKey = Guid.NewGuid().ToString(),
                                        CreatedById = user.Id,
                                        DateCreated = DateTime.Now,
                                        RecordDate = DateTime.Now
                                    };
                                    db.Insert(account);
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

        public static List<MemberViewModel> GetMembersByCriteria(int criteria, string startdate, string enddate)
        {
            try
            {
                DateTime.TryParse(startdate, out var castStartDate);
                DateTime.TryParse(enddate, out var castEndDate);
                using (var db = GdhoteConnection())
                {
                    var members = new List<MemberViewModel>();
                    switch (criteria)
                    {
                        case 1:
                            members = db.Fetch<MemberViewModel>()
                              .Where(m => m.MagusDate >= castStartDate && m.MagusDate < castEndDate.AddDays(1))
                                .OrderBy(m => m.FirstName)
                                .ThenBy(m => m.Surname)
                              .ToList();
                            break;
                        case 2:
                            members = db.Fetch<MemberViewModel>()
                                .Where(m => m.InitiationDate >= castStartDate && m.InitiationDate < castEndDate.AddDays(1))
                                .OrderBy(m => m.FirstName)
                                .ThenBy(m => m.Surname)
                                .ToList();
                            break;
                        case 3:
                            members = db.Query<MemberViewModel>("exec sp_HUB_GetBirthdayMembers @startdate, @enddate", new { startdate = castStartDate, enddate = castEndDate })
                                .OrderBy(m => m.DateOfBirth)
                                .ThenBy(m => m.DateOfBirth.Value.Date.Month)
                                .ToList();
                            break;
                        case 4:
                            members = db.Fetch<MemberViewModel>()
                                .Where(m => m.OfficerDate >= castStartDate && m.OfficerDate < castEndDate.AddDays(1))
                                .OrderBy(m => m.FirstName)
                                .ThenBy(m => m.Surname)
                                .ToList();
                            break;
                        //case 5:
                        //    members = db.Query<MemberViewModel>("exec sp_HUB_GetWeddingAnnversaryMembers @startdate, @enddate", new { startdate = castStartDate, enddate = castEndDate })
                        //        .OrderBy(m => m.DateOfBirth).ThenBy(m => m.DateOfBirth.Value.Date.Month).ThenBy(m => m.DateOfBirth.Value.Date.Month)
                        //        .ToList();
                        //    break;
                        default:
                            members = db.Fetch<MemberViewModel>()
                                .Where(m => m.RecordDate >= castStartDate && m.RecordDate < castEndDate.AddDays(1))
                                .OrderBy(m => m.FirstName).ThenBy(m => m.Surname)
                                .ToList();
                            break;
                    }
                    return members;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<MemberViewModel>();
            }
        }
    }
}
