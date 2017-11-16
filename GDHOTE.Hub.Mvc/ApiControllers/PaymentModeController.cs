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
    [RoutePrefix(ConstantManager.ApiDefaultNamespace + "paymentmode")]
    public class PaymentModeController : ApiController
    {
        [Route("getpaymentmodes")]
        public IHttpActionResult GetPaymentModes()
        {
            var paymentModes = PaymentModeService.GetPaymentModes().ToList();
            if (paymentModes.Count == 0) return NotFound();
            return Ok(paymentModes);
        }
        [Route("getpaymentmode")]
        public IHttpActionResult GetPaymentMode(int id)
        {
            var paymentMode = PaymentModeService.GetPaymentMode(id);
            if (paymentMode == null) return NotFound();
            return Ok(paymentMode);
        }

        [HttpDelete]
        [Route("deletepaymentmode")]
        public IHttpActionResult DeletePaymentMode(int id)
        {
            var paymentModeInDb = PaymentModeService.GetPaymentMode(id);
            if (paymentModeInDb == null) return NotFound();
            var result = PaymentModeService.Delete(id);
            return Ok(result);
        }
    }
}
