using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Models;

namespace GDHOTE.Hub.Core.Services
{
    public class CountryService : BaseService
    {
        public void SaveCountry(Country country)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    db.Insert(country);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while trying to insert country");
            }
        }
        public static IEnumerable<Country> GetCountries()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var countries = db.Fetch<Country>();
                    return countries;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while trying to fetch countries");
            }
        }
    }
}
