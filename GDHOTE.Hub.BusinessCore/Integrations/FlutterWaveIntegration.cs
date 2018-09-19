using System;
using System.Net;
using System.Threading.Tasks;
using GDHOTE.Hub.BusinessCore.Services;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
namespace GDHOTE.Hub.BusinessCore.Integrations
{
    public class FlutterWaveIntegration
    {
        public static FlutterWaveVerifyPaymentResponse ConfirmGatewayReference(VerifyPaymentRequest verifyRequest)
        {
            var flwRequestequest = new FlutterWaveVerifyPaymentRequest
            {
                txref = verifyRequest.PaymentReference,
                SECKEY = ReturnSecretKey()
            };

            var requestData = JsonConvert.SerializeObject(flwRequestequest);
            string fullUrl = BaseService.Get("settings.getway.flutterwave.url");
            var client = new RestClient(fullUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", requestData, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;

            var result = new FlutterWaveVerifyPaymentResponse();
            IRestResponse response = new RestResponse();
            try
            {
                response = client.Execute<FlutterWaveVerifyPaymentResponse>(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    LogService.LogError(JsonConvert.SerializeObject(response));
                }
                result = JsonConvert.DeserializeObject<FlutterWaveVerifyPaymentResponse>(response.Content);
                result.details = JObject.Parse(response.Content).GetValue("data").ToString();

                //Log Gateway Resposnse
                new Task(() =>
                {
                    using (var db = BaseService.GdhoteConnection())
                    {
                        var paymentGatewayDetail = new PaymentGatewayDetail
                        {
                            GatewayId = verifyRequest.GatewayId,
                            PaymentReference = verifyRequest.PaymentReference,
                            MerchantReference = result.data.flwref,
                            GatewayResponseCode = result.status,
                            GatewayResponseMessage = result.message,
                            GatewayRequestDetails = requestData,
                            GatewayResponseDetails = result.details,
                            DateCreated = DateTime.Now,
                            RecordDate = DateTime.Now
                        };
                        db.Insert(paymentGatewayDetail);
                    }

                }).Start();
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
            }
            return result;
        }

        public static string ReturnSecretKey()
        {
            string secretKey = "";
            secretKey = BaseService.UseLive() == "Y"
            ? "FLWSECK-e6db11d1f8a6208de8cb2f94e293450e-X"
            : "FLWSECK-e6db11d1f8a6208de8cb2f94e293450e-X";
            return secretKey;
        }
    }
}
