﻿using System;
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
    public class PortalAccrualTypeService
    {
        public static List<AccrualTypeViewModel> GetAllAccrualTypes()
        {
            string fullUrl = ConfigService.ReturnBaseUrl() + "/accrual/get-all-accrual-types";
            var client = new RestClient(fullUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            //request.AddHeader("Authorization", "Bearer " + token.AuthToken);
            //request.AddHeader("refresh_token", token.RefreshToken);
            request.RequestFormat = DataFormat.Json;

            var result = new List<AccrualTypeViewModel>();
            IRestResponse response = new RestResponse();
            try
            {
                response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    //ErrorLogManager.LogError(callerFormName, computerDetails, "response.Content", JsonConvert.SerializeObject(response));
                }
                result = JsonConvert.DeserializeObject<List<AccrualTypeViewModel>>(response.Content);
            }
            catch (Exception ex)
            {
                //ErrorLogManager.LogError(callerFormName, computerDetails, "DoPayment", ex);
            }
            return result;
        }

        public static List<AccrualTypeResponse> GetActiveAccrualTypes()
        {
            string fullUrl = ConfigService.ReturnBaseUrl() + "/accrual/get-active-accrual-types";
            var client = new RestClient(fullUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            //request.AddHeader("Authorization", "Bearer " + token.AuthToken);
            //request.AddHeader("refresh_token", token.RefreshToken);
            request.RequestFormat = DataFormat.Json;

            var result = new List<AccrualTypeResponse>();
            IRestResponse response = new RestResponse();
            try
            {
                response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    //ErrorLogManager.LogError(callerFormName, computerDetails, "response.Content", JsonConvert.SerializeObject(response));
                }
                result = JsonConvert.DeserializeObject<List<AccrualTypeResponse>>(response.Content);
            }
            catch (Exception ex)
            {
                //ErrorLogManager.LogError(callerFormName, computerDetails, "DoPayment", ex);
            }
            return result;
        }

        public static AccrualType GetAccrualType(string id)
        {
            string fullUrl = ConfigService.ReturnBaseUrl() + "/accrual/get-accrual-type";
            var client = new RestClient(fullUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            //request.AddHeader("Authorization", "Bearer " + token.AuthToken);
            //request.AddHeader("refresh_token", token.RefreshToken);
            request.AddParameter("id", id);
            request.RequestFormat = DataFormat.Json;

            var result = new AccrualType();
            IRestResponse response = new RestResponse();
            try
            {
                response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    //ErrorLogManager.LogError(callerFormName, computerDetails, "response.Content", JsonConvert.SerializeObject(response));
                }
                result = JsonConvert.DeserializeObject<AccrualType>(response.Content);
            }
            catch (Exception ex)
            {
                //ErrorLogManager.LogError(callerFormName, computerDetails, "DoPayment", ex);
            }
            return result;
        }

        public static Response CreateAccrualType(CreateAccrualTypeRequest createRequest)
        {
            var requestData = JsonConvert.SerializeObject(createRequest);
            string fullUrl = ConfigService.ReturnBaseUrl() + "/accrual/create-accrual-type";
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

        public static Response DeleteAccrualType(string id)
        {

            string fullUrl = ConfigService.ReturnBaseUrl() + "/accrual/delete-accrual-type";
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