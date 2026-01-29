using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;
using GDHOTE.Hub.CommonServices.BusinessLogic;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.CoreObject.ViewModels;
using GDHOTE.Hub.PortalCore.Models;
using Newtonsoft.Json;
using RestSharp;

namespace GDHOTE.Hub.PortalCore.Services
{
    public class PortalPaymentModeService
    {
        public static List<PaymentModeViewModel> GetAllPaymentModes(Token token)
        {
            string fullUrl = ConfigService.ReturnBaseUrl() + "/payment/get-all-payment-modes";
            var client = new RestClient(fullUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token.AuthToken);
            request.AddHeader("refresh_token", token.RefreshToken);
            request.RequestFormat = DataFormat.Json;

            var result = new List<PaymentModeViewModel>();
            IRestResponse response = new RestResponse();
            try
            {
                response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(response));
                }
                result = JsonConvert.DeserializeObject<List<PaymentModeViewModel>>(response.Content);
            }
            catch (Exception ex)
            {
                ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, ex);
            }
            return result;
        }

        public static List<PaymentModeResponse> GetActivePaymentModes(Token token)
        {
            string fullUrl = ConfigService.ReturnBaseUrl() + "/payment/get-active-payment-modes";
            var client = new RestClient(fullUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token.AuthToken);
            request.AddHeader("refresh_token", token.RefreshToken);
            request.RequestFormat = DataFormat.Json;

            var result = new List<PaymentModeResponse>();
            IRestResponse response = new RestResponse();
            try
            {
                response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(response));
                }
                result = JsonConvert.DeserializeObject<List<PaymentModeResponse>>(response.Content);
            }
            catch (Exception ex)
            {
                ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, ex);
            }
            return result;
        }

        public static PaymentMode GetPaymentMode(string id, Token token)
        {
            string fullUrl = ConfigService.ReturnBaseUrl() + "/payment/get-payment-mode";
            var client = new RestClient(fullUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token.AuthToken);
            request.AddHeader("refresh_token", token.RefreshToken);
            request.AddParameter("id", id);
            request.RequestFormat = DataFormat.Json;

            var result = new PaymentMode();
            IRestResponse response = new RestResponse();
            try
            {
                response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(response));
                }
                result = JsonConvert.DeserializeObject<PaymentMode>(response.Content);
            }
            catch (Exception ex)
            {
                ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, ex);
            }
            return result;
        }

        public static Response CreatePaymentMode(CreatePaymentModeRequest createRequest, Token token)
        {
            var requestData = JsonConvert.SerializeObject(createRequest);
            string fullUrl = ConfigService.ReturnBaseUrl() + "/payment/create-payment-mode";
            var client = new RestClient(fullUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token.AuthToken);
            request.AddHeader("refresh_token", token.RefreshToken);
            request.AddParameter("application/json", requestData, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;

            var result = new Response();
            IRestResponse response = new RestResponse();
            try
            {
                response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(response));
                }
                result = JsonConvert.DeserializeObject<Response>(response.Content);
            }
            catch (Exception ex)
            {
                ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, ex);
            }
            return result;
        }

        public static Response DeletePaymentMode(string id, Token token)
        {

            string fullUrl = ConfigService.ReturnBaseUrl() + "/payment/delete-payment-mode";
            var client = new RestClient(fullUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token.AuthToken);
            request.AddHeader("refresh_token", token.RefreshToken);
            request.AddParameter("id", id, ParameterType.QueryString);
            request.RequestFormat = DataFormat.Json;

            var result = new Response();
            IRestResponse response = new RestResponse();
            try
            {
                response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(response));
                }
                result = JsonConvert.DeserializeObject<Response>(response.Content);
            }
            catch (Exception ex)
            {
                ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, ex);
            }
            return result;
        }
    }
}
