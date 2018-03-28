using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.CoreObject.Enumerables;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class MembershipStatusService : BaseService
    {
        public static string Save(MembershipStatus membershipStatus)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(membershipStatus);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
               LogService.Log(ex.Message);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert MembershipStatus";
            }
        }
        public static IEnumerable<MembershipStatus> GetCMembershipStatuses()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var countries = db.Fetch<MembershipStatus>().OrderBy(c => c.StatusDescription);
                    return countries;
                }
            }
            catch (Exception ex)
            {
               LogService.Log(ex.Message);
                return new List<MembershipStatus>();
            }
        }
        public static MembershipStatus GetMembershipStatus(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var MembershipStatus = db.Fetch<MembershipStatus>().SingleOrDefault(c => c.Id == id);
                    return MembershipStatus;
                }
            }
            catch (Exception ex)
            {
               LogService.Log(ex.Message);
                return new MembershipStatus();
            }
        }
        public static string Update(MembershipStatus membershipStatus)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(membershipStatus);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
               LogService.Log(ex.Message);
                return "Error occured while trying to update MembershipStatus";
            }
        }
        public static string Delete(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Delete<MembershipStatus>(id);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
               LogService.Log(ex.Message);
                return "Error occured while trying to delete record";
            }
        }
    }
}
