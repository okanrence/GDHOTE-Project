using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.CoreObject.ViewModels;
using Newtonsoft.Json;
using RestSharp;

namespace GDHOTE.Hub.PortalCore.Services
{
    public class PortalAccuralTypeService
    {
        public static List<AccuralTypeViewModel> GetAllAccuralTypes()
        {
            string fullUrl = ConfigService.ReturnBaseUrl() + "/accural/get-all-accural-types";
            var client = new RestClient(fullUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            //request.AddHeader("Authorization", "Bearer " + token.AuthToken);
            //request.AddHeader("refresh_token", token.RefreshToken);
            request.RequestFormat = DataFormat.Json;

            var result = new List<AccuralTypeViewModel>();
            IRestResponse response = new RestResponse();
            try
            {
                response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    //ErrorLogManager.LogError(callerFormName, computerDetails, "response.Content", JsonConvert.SerializeObject(response));
                }
                result = JsonConvert.DeserializeObject<List<AccuralTypeViewModel>>(response.Content);
            }
            catch (Exception ex)
            {
                //ErrorLogManager.LogError(callerFormName, computerDetails, "DoPayment", ex);
            }
            return result;
        }

        public static List<AccuralTypeResponse> GetActiveAccuralTypes()
        {
            string fullUrl = ConfigService.ReturnBaseUrl() + "/accural/get-active-accural-types";
            var client = new RestClient(fullUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            //request.AddHeader("Authorization", "Bearer " + token.AuthToken);
            //request.AddHeader("refresh_token", token.RefreshToken);
            request.RequestFormat = DataFormat.Json;

            var result = new List<AccuralTypeResponse>();
            IRestResponse response = new RestResponse();
            try
            {
                response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    //ErrorLogManager.LogError(callerFormName, computerDetails, "response.Content", JsonConvert.SerializeObject(response));
                }
                result = JsonConvert.DeserializeObject<List<AccuralTypeResponse>>(response.Content);
            }
            catch (Exception ex)
            {
                //ErrorLogManager.LogError(callerFormName, computerDetails, "DoPayment", ex);
            }
            return result;
        }

        public static AccuralType GetAccuralType(string id)
        {
            string fullUrl = ConfigService.ReturnBaseUrl() + "/accural/get-accural-type";
            var client = new RestClient(fullUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            //request.AddHeader("Authorization", "Bearer " + token.AuthToken);
            //request.AddHeader("refresh_token", token.RefreshToken);
            request.AddParameter("id", id);
            request.RequestFormat = DataFormat.Json;

            var result = new AccuralType();
            IRestResponse response = new RestResponse();
            try
            {
                response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    //ErrorLogManager.LogError(callerFormName, computerDetails, "response.Content", JsonConvert.SerializeObject(response));
                }
                result = JsonConvert.DeserializeObject<AccuralType>(response.Content);
            }
            catch (Exception ex)
            {
                //ErrorLogManager.LogError(callerFormName, computerDetails, "DoPayment", ex);
            }
            return result;
        }

        public static Response CreateAccuralType(CreateAccuralTypeRequest createRequest)
        {
            var requestData = JsonConvert.SerializeObject(createRequest);
            string fullUrl = ConfigService.ReturnBaseUrl() + "/accural/create-accural-type";
            var client = new RestClient(fullUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            //request.AddHeader("Authorization", "Bearer " + token.AuthToken);
            //request.AddHeader("refresh_token", token.RefreshToken);
            request.AddParameter("application/json", requestData, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;

            var result = new Response();
            IRestResponse response = new RestResponse();
            try
            {
                response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    //ErrorLogManager.LogError(callerFormName, computerDetails, "response.Content", JsonConvert.SerializeObject(response));
                }
                result = JsonConvert.DeserializeObject<Response>(response.Content);
            }
            catch (Exception ex)
            {
                //ErrorLogManager.LogError(callerFormName, computerDetails, "DoPayment", ex);
            }
            return result;
        }

        public static Response DeleteAccuralType(string id)
        {

            string fullUrl = ConfigService.ReturnBaseUrl() + "/accural/delete-accural-type";
            var client = new RestClient(fullUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            //request.AddHeader("Authorization", "Bearer " + token.AuthToken);
            //request.AddHeader("refresh_token", token.RefreshToken);
            request.AddParameter("id", id, ParameterType.QueryString);
            request.RequestFormat = DataFormat.Json;

            var result = new Response();
            IRestResponse response = new RestResponse();
            try
            {
                response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    //ErrorLogManager.LogError(callerFormName, computerDetails, "response.Content", JsonConvert.SerializeObject(response));
                }
                result = JsonConvert.DeserializeObject<Response>(response.Content);
            }
            catch (Exception ex)
            {
                //ErrorLogManager.LogError(callerFormName, computerDetails, "DoPayment", ex);
            }
            return result;
        }
    }
}
