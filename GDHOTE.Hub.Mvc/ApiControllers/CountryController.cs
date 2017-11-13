using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GDHOTE.Hub.Core.BusinessLogic;
using GDHOTE.Hub.Core.Models;
using GDHOTE.Hub.Core.Services;

namespace GDHOTE.Hub.Mvc.ApiControllers
{
    [RoutePrefix(ConstantManager.ApiDefaultNamespace + "country")]

    public class CountryController : ApiController
    {
        [Route("getcountries")]
        public IHttpActionResult GetCountries()
        {
            var countries = new List<Country>();
            countries = CountryService.GetCountries().ToList();
            if (countries.Count == 0) return NotFound();
            return Ok(countries);
        }
        [Route("getcountry")]
        public IHttpActionResult GetCountry(int id)
        {
            var country = CountryService.GetCountry(id);
            if (country == null) return NotFound();
            return Ok(country);
        }

        [HttpDelete]
        [Route("deletecountry")]
        public IHttpActionResult DeleteCountry(int id)
        {
            var countryInDb = CountryService.GetCountry(id);
            if (countryInDb == null) return NotFound();
            var result = CountryService.Delete(id);
            return Ok(result);
        }
    }
}
