using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.CoreObject.ViewModels;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class MemberInfoService : BaseService
    {
        public static List<Member> GetMembersByBirthday(string dateOfBirth)
        {
            try
            {
                DateTime.TryParse(dateOfBirth, out var castDateOfBirth);
                using (var db = GdhoteConnection())
                {
                    var members = db.Fetch<Member>()
                        .Where(m => m.DateOfBirth.Date.Month == castDateOfBirth.Date.Month && m.DateOfBirth.Date.Day == castDateOfBirth.Date.Day)
                        .OrderBy(m => m.FirstName).ToList();
                    return members;
                }
            }
            catch (Exception ex)
            {
                LogService.myLog(ex.Message);
                return new List<Member>();
            }
        }
        public static List<MemberDetailsViewModel> GetMembersByName(string searchQuery)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    searchQuery = !string.IsNullOrEmpty(searchQuery) ? searchQuery.ToLower() : searchQuery;
                    var members = db.Fetch<MemberDetailsViewModel>()
                        .Where(m => m.FirstName.ToLower().Contains(searchQuery) || m.Surname.ToLower().Contains(searchQuery))
                        .OrderBy(m => m.FirstName).ToList();
                    return members;
                }
            }
            catch (Exception ex)
            {
                LogService.myLog(ex.Message);
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
                LogService.myLog(ex.Message);
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
                        .Where(m =>m.DateWedded !=null 
                                   && m.DateWedded.Value.Date.Month == castWeddingDate.Date.Month 
                                   && m.DateWedded.Value.Date.Day == castWeddingDate.Date.Day)
                        .OrderBy(m => m.FirstName).ToList();
                    return members;
                }
            }
            catch (Exception ex)
            {
                LogService.myLog(ex.Message);
                return new List<MemberDetailsViewModel>();
            }
        }
    }
}
