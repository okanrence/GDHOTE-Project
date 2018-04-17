using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.CoreObject.ViewModels;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class MemberInfoService : BaseService
    {
        public static List<MemberDetailsViewModel> GetMembersByBirthday(string dateOfBirth)
        {
            try
            {
                DateTime.TryParse(dateOfBirth, out var castDateOfBirth);
                using (var db = GdhoteConnection())
                {
                    var members = db.Fetch<MemberDetailsViewModel>()
                        .Where(m => m.DateOfBirth.Date.Month == castDateOfBirth.Date.Month 
                                    && m.DateOfBirth.Date.Day == castDateOfBirth.Date.Day)
                        .OrderBy(m => m.FirstName).ToList();
                    return members;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<MemberDetailsViewModel>();
            }
        }
        public static List<MemberDetailsViewModel> GetMembersByName(string searchQuery)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    searchQuery = !string.IsNullOrEmpty(searchQuery) ? searchQuery.ToLower() : searchQuery;
                    //searchQuery="%" + searchQuery + "%";
                    var members = db.Fetch<MemberDetailsViewModel>()
                        .Where(m => m.FirstName.ToLower().Contains(searchQuery) || m.Surname.ToLower().Contains(searchQuery))
                        .OrderBy(m => m.FirstName).ToList();

                    //var members = db.Fetch<MemberDetailsViewModel>()
                    //    .Where(m => m.Name.Contains(searchQuery)).ToList();
                    return members;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<MemberDetailsViewModel>();
            }
        }

        public static List<MemberDetailsViewModel> GetMembersByMobileNumber(string mobileNumber)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var members = db.Fetch<MemberDetailsViewModel>()
                        .Where(m => m.MobileNumber == mobileNumber)
                        .OrderBy(m => m.FirstName).ToList();
                    return members;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<MemberDetailsViewModel>();
            }
        }

        public static List<MemberDetailsViewModel> GetMembersByWeddingAnniversary(string weddingDate)
        {
            try
            {
                DateTime.TryParse(weddingDate, out var castWeddingDate);
                using (var db = GdhoteConnection())
                {
                    var members = db.Fetch<MemberDetailsViewModel>()
                        .Where(m => m.DateWedded != null
                                   && m.DateWedded.Value.Date.Month == castWeddingDate.Date.Month
                                   && m.DateWedded.Value.Date.Day == castWeddingDate.Date.Day)
                        .OrderBy(m => m.FirstName).ToList();
                    return members;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<MemberDetailsViewModel>();
            }
        }


        public static MemberInfoResponse GetMemberInformation(long id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var member = db.Fetch<MemberViewModel>().SingleOrDefault(m => m.Id == id);
                    var memberDetails = db.Fetch<MemberDetailsViewModel>().SingleOrDefault(m => m.MemberId == id);
                    var activities = db.Fetch<ActivityViewModel>()
                        .Where(m => m.MemberId == id).OrderBy(m => m.StartDate).ToList();

                    var memberInfoResponse = new MemberInfoResponse
                    {
                        Member = member,
                        MemberDetails = memberDetails,
                        Activities = activities
                    };

                    return memberInfoResponse;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new MemberInfoResponse();
            }

        }
    }
}
