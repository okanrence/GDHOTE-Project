﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.CoreObject.Enumerables;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class UserViewService : BaseService
    {
        public static IEnumerable<UserView> GetUsers()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var userViews = db.Fetch<UserView>().OrderBy(u => u.EmailAddress);
                    return userViews;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new List<UserView>();
            }
        }
    }
}