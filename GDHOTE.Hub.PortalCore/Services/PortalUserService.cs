using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using Newtonsoft.Json;
using RestSharp;

namespace GDHOTE.Hub.PortalCore.Services
{
    public class PortalUserService
    {
        public static Response StartPasswordReset(PasswordResetRequest resetRequest)
        {
            var requestData = JsonConvert.SerializeObject(resetRequest);
            string fullUrl = ConfigService.ReturnBaseUrl() + "/user/start-reset-password";
            var client = new RestClient(fullUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("channel", "1");
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
    }
}
