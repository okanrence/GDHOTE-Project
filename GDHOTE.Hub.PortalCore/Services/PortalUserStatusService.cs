using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using GDHOTE.Hub.CoreObject.Models;
using Newtonsoft.Json;
using RestSharp;

namespace GDHOTE.Hub.PortalCore.Services
{
    public class PortalUserStatusService
    {
        public static List<UserStatus> GetActiveUserStatuses()
        {
            string fullUrl = ConfigService.ReturnBaseUrl() + "/status/get-active-user-statuses";
            var client = new RestClient(fullUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            //request.AddHeader("Authorization", "Bearer " + token.AuthToken);
            //request.AddHeader("refresh_token", token.RefreshToken);
            request.RequestFormat = DataFormat.Json;

            var result = new List<UserStatus>();
            IRestResponse response = new RestResponse();
            try
            {
                response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    //ErrorLogManager.LogError(callerFormName, computerDetails, "response.Content", JsonConvert.SerializeObject(response));
                }
                result = JsonConvert.DeserializeObject<List<UserStatus>>(response.Content);
            }
            catch (Exception ex)
            {
                //ErrorLogManager.LogError(callerFormName, computerDetails, "DoPayment", ex);
            }
            return result;
        }
    }
}
