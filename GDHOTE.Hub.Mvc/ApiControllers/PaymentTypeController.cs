using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GDHOTE.Hub.BusinessCore.BusinessLogic;
using GDHOTE.Hub.BusinessCore.Services;

namespace GDHOTE.Hub.Mvc.ApiControllers
{
    [RoutePrefix(ConstantManager.ApiDefaultNamespace + "payment")]

    public class PaymentTypeController : ApiController
    {
        [Route("get-payment-types")]
        public IHttpActionResult GetPaymentTypes()
        {
            var paymentTypes = PaymentTypeService.GetPaymentTypes().ToList();
            if (paymentTypes.Count == 0) return NotFound();
            return Ok(paymentTypes);
        }
        [Route("get-payment-type")]
        public IHttpActionResult GetPaymentType(int id)
        {
            var paymentType = PaymentTypeService.GetPaymentType(id);
            if (paymentType == null) return NotFound();
            return Ok(paymentType);
        }

        [HttpDelete]
        [Route("delete-payment-type")]
        public IHttpActionResult DeletePaymentType(int id)
        {
            var paymentTypeInDb = PaymentTypeService.GetPaymentType(id);
            if (paymentTypeInDb == null) return NotFound();
            var result = PaymentTypeService.Delete(id);
            return Ok(result);
        }
    }
}
