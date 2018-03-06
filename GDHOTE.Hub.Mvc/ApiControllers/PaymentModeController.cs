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
    public class PaymentModeController : ApiController
    {
        [Route("get-payment-modes")]
        public IHttpActionResult GetPaymentModes()
        {
            var paymentModes = PaymentModeService.GetPaymentModes().ToList();
            if (paymentModes.Count == 0) return NotFound();
            return Ok(paymentModes);
        }
        [Route("get-payment-mode")]
        public IHttpActionResult GetPaymentMode(int id)
        {
            var paymentMode = PaymentModeService.GetPaymentMode(id);
            if (paymentMode == null) return NotFound();
            return Ok(paymentMode);
        }

        [HttpDelete]
        [Route("delete-payment-mode")]
        public IHttpActionResult DeletePaymentMode(int id)
        {
            var paymentModeInDb = PaymentModeService.GetPaymentMode(id);
            if (paymentModeInDb == null) return NotFound();
            var result = PaymentModeService.Delete(id);
            return Ok(result);
        }
    }
}
