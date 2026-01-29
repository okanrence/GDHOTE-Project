using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using GDHOTE.Hub.CommonServices.BusinessLogic;
using GDHOTE.Hub.PortalCore.Services;
using RestSharp;

namespace GDHOTE.Hub.PortalCore.Integrations
{
    public class BaseIntegration
    {
        private RestClient _client;

        public BaseIntegration()
        {
            _client = new RestClient(ConfigService.ReturnBaseUrl());
        }

        public T Execute<T>(RestRequest request) where T : new()
        {
            try
            {
                var response = _client.Execute<T>(request);
                return (T)response.Data;
            }
            catch (Exception ex)
            {
                ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, ex);
                throw;
            }
        }
    }
}
