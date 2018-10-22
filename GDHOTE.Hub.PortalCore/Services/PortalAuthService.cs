using System;
using System.Net;
using System.Reflection;
using GDHOTE.Hub.CommonServices.BusinessLogic;
using GDHOTE.Hub.CoreObject.Models;
using Newtonsoft.Json;
using RestSharp;

namespace GDHOTE.Hub.PortalCore.Services
{
    public class PortalAuthService
    {
        public static TokenResponse Login(string username, string password)
        {
            string fullUrl = ConfigService.ReturnBaseUrl() + "/auth/token";
            var client = new RestClient(fullUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("user_type", "administrator");//"customer"
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "password");
            request.AddParameter("username", username);
            request.AddParameter("password", password);
            try
            {
                var response = client.Execute<TokenResponse>(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(response));
                }
                return response.Data;
            }
            catch (Exception ex)
            {
                ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, ex);
            }
            return new TokenResponse();
        }

        public static TokenResponse LoginOld(string username, string password)
        {
            string fullUrl = ConfigService.ReturnBaseUrl() + "/auth/token";
            var client = new RestClient(fullUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("user_type", "administrator");//"customer"
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "password");
            request.AddParameter("username", username);
            request.AddParameter("password", password);

            var result = new TokenResponse();
            IRestResponse response = new RestResponse();
            try
            {
                response = client.Execute<TokenResponse>(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(response));
                }
                result = JsonConvert.DeserializeObject<TokenResponse>(response.Content);
            }
            catch (Exception ex)
            {
                ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, ex);
            }
            return result;
        }
    }
}
