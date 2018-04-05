using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using GDHOTE.Hub.BusinessCore.Exceptions;
using GDHOTE.Hub.CoreObject.Enumerables;
using GDHOTE.Hub.CoreObject.Models;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class RequestResponseEntryService : BaseService
    {
        public static void Save(RequestResponseEntry entry)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    entry.DateCreated = DateTime.Now;
                    var result = db.Insert(entry);
                }
            }
            catch (Exception ex)
            {
               LogService.LogError(ex.Message);
            }
        }
    }
}
