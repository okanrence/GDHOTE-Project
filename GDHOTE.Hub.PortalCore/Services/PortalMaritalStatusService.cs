using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;
using GDHOTE.Hub.CommonServices.BusinessLogic;
using GDHOTE.Hub.CoreObject.Models;
using Newtonsoft.Json;
using RestSharp;

namespace GDHOTE.Hub.PortalCore.Services
{
    public class PortalMaritalStatusService
    {
        public static List<MaritalStatus> GetAllMaritalStatuses()
        {
            string fullUrl = ConfigService.ReturnBaseUrl() + "/martialstatus/get-all-martial-statuses";
            var client = new RestClient(fullUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;

            var result = new List<MaritalStatus>();
            IRestResponse response = new RestResponse();
            try
            {
                response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(response));
                }
                result = JsonConvert.DeserializeObject<List<MaritalStatus>>(response.Content);
            }
            catch (Exception ex)
            {
                ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, ex);
            }
            return result;
        }
    }
}
