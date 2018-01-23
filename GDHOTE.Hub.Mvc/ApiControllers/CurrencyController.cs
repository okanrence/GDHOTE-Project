using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GDHOTE.Hub.Core.BusinessLogic;
using GDHOTE.Hub.Core.Services;

namespace GDHOTE.Hub.Mvc.ApiControllers
{
    [RoutePrefix(ConstantManager.ApiDefaultNamespace + "currency")]

    public class CurrencyController : ApiController
    {
        [Route("get-currencies")]
        public IHttpActionResult GetCountries()
        {
            var currencies = CurrencyService.GetActiveCurrencies().ToList();
            if (currencies.Count == 0) return NotFound();
            return Ok(currencies);
        }
        [Route("get-currency")]
        public IHttpActionResult GetCurrency(int id)
        {
            var currency = CurrencyService.GetCurrency(id);
            if (currency == null) return NotFound();
            return Ok(currency);
        }

        [HttpDelete]
        [Route("delete-currency")]
        public IHttpActionResult DeleteCurrency(int id)
        {
            var currencyInDb = CurrencyService.GetCurrency(id);
            if (currencyInDb == null) return NotFound();
            var result = CurrencyService.Delete(id);
            return Ok(result);
        }
    }
}
