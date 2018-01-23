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
    [RoutePrefix(ConstantManager.ApiDefaultNamespace + "payment")]
    public class PaymentController : ApiController
    {
        [Route("get-payments")]
        public IHttpActionResult GetPayments()
        {
            var countries = PaymentService.GetPayments().ToList();
            if (countries.Count == 0) return NotFound();
            return Ok(countries);
        }
        [Route("get-payment")]
        public IHttpActionResult GetPayment(int id)
        {
            var Payment = PaymentService.GetPayment(id);
            if (Payment == null) return NotFound();
            return Ok(Payment);
        }

        [HttpDelete]
        [Route("delete-payment")]
        public IHttpActionResult DeletePayment(int id)
        {
            var PaymentInDb = PaymentService.GetPayment(id);
            if (PaymentInDb == null) return NotFound();
            var result = PaymentService.Delete(id);
            return Ok(result);
        }
    }
}
