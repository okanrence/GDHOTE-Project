using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;
using GDHOTE.Hub.CommonServices.BusinessLogic;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.PortalCore.Models;
using Newtonsoft.Json;
using RestSharp;

namespace GDHOTE.Hub.PortalCore.Services
{
   public class PortalStatusService
    {
        public static List<Status> GetStatuses(Token token)
        {
            string fullUrl = ConfigService.ReturnBaseUrl() + "/status/get-statuses";
            var client = new RestClient(fullUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token.AuthToken);
            request.AddHeader("refresh_token", token.RefreshToken);
            request.RequestFormat = DataFormat.Json;

            var result = new List<Status>();
            IRestResponse response = new RestResponse();
            try
            {
                response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(response));
                }
                result = JsonConvert.DeserializeObject<List<Status>>(response.Content);
            }
            catch (Exception ex)
            {
                ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, ex);
            }
            return result;
        }
    }
}
