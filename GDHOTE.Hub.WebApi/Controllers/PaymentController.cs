using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GDHOTE.Hub.BusinessCore.BusinessLogic;
using GDHOTE.Hub.BusinessCore.Services;

namespace GDHOTE.Hub.WebApi.Controllers
{
    [RoutePrefix(ConstantManager.ApiDefaultNamespace + "payment")]
    public class PaymentController : ApiController
    {
        [Route("get-payments")]
        public IHttpActionResult GetPayments()
        {
            var payments = PaymentService.GetPayments().ToList();
            if (payments.Count == 0) return NotFound();
            return Ok(payments);
        }
        [Route("get-payment")]
        public IHttpActionResult GetPayment(int id)
        {
            var payment = PaymentService.GetPayment(id);
            if (payment == null) return NotFound();
            return Ok(payment);
        }

        [HttpDelete]
        [Route("delete-payment")]
        public IHttpActionResult DeletePayment(int id)
        {
            var paymentInDb = PaymentService.GetPayment(id);
            if (paymentInDb == null) return NotFound();
            var result = PaymentService.Delete(id);
            return Ok(result);
        }
    }
}
