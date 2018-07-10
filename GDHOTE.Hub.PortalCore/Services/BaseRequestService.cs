using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using GDHOTE.Hub.CommonServices.BusinessLogic;
using RestSharp;

namespace GDHOTE.Hub.PortalCore.Services
{
    public class BaseRequestService
    {
        private RestClient _client;
        public BaseRequestService()
        {
            _client = new RestClient(ConfigService.ReturnBaseUrl());
        }
        public static IRestResponse Execute(RestRequest request)
        {
            IRestResponse response = new RestResponse();
            try
            {
                var client = new RestClient();
                response = client.Execute(request);
                return response;
            }
            catch (Exception ex)
            {
                ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, ex);
                return response = new RestResponse();
            }
        }
    }
}
