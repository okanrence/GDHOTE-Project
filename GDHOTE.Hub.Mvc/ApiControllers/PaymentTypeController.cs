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
    [RoutePrefix(ConstantManager.ApiDefaultNamespace + "paymenttype")]

    public class PaymentTypeController : ApiController
    {
        [Route("getpaymenttypes")]
        public IHttpActionResult GetCountries()
        {
            var paymentTypes = PaymentTypeService.GetPaymentTypes().ToList();
            if (paymentTypes.Count == 0) return NotFound();
            return Ok(paymentTypes);
        }
        [Route("getpaymenttype")]
        public IHttpActionResult GetPaymentType(int id)
        {
            var paymentType = PaymentTypeService.GetPaymentType(id);
            if (paymentType == null) return NotFound();
            return Ok(paymentType);
        }

        [HttpDelete]
        [Route("deletepaymenttype")]
        public IHttpActionResult DeletePaymentType(int id)
        {
            var paymentTypeInDb = PaymentTypeService.GetPaymentType(id);
            if (paymentTypeInDb == null) return NotFound();
            var result = PaymentTypeService.Delete(id);
            return Ok(result);
        }
    }
}
