﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using GDHOTE.Hub.CommonServices.BusinessLogic;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.CoreObject.ViewModels;
using GDHOTE.Hub.PortalCore.Models;
using Newtonsoft.Json;
using RestSharp;

namespace GDHOTE.Hub.PortalCore.Services
{
    public class PortalMemberService
    {
        public static List<MemberViewModel> GetAllMembers(Token token)
        {
            string fullUrl = ConfigService.ReturnBaseUrl() + "/member/get-all-members";
            var client = new RestClient(fullUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token.AuthToken);
            request.AddHeader("refresh_token", token.RefreshToken);
            request.RequestFormat = DataFormat.Json;

            var result = new List<MemberViewModel>();
            IRestResponse response = new RestResponse();
            try
            {
                response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(response));
                }
                result = JsonConvert.DeserializeObject<List<MemberViewModel>>(response.Content);
            }
            catch (Exception ex)
            {
                ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, ex);
            }
            return result;
        }

        public static List<MemberViewModel> GetMemberById(string id,Token token)
        {
            string fullUrl = $"{ConfigService.ReturnBaseUrl()}/member/{id}";
            var client = new RestClient(fullUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token.AuthToken);
            request.AddHeader("refresh_token", token.RefreshToken);
            //request.AddParameter("id", id);

            request.RequestFormat = DataFormat.Json;

            var result = new List<MemberViewModel>();
            IRestResponse response = new RestResponse();
            try
            {
                response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(response));
                }
                result = JsonConvert.DeserializeObject<List<MemberViewModel>>(response.Content);
            }
            catch (Exception ex)
            {
                ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, ex);
            }
            return result;
        }
        public static List<MemberViewModel> GetRecentMembers(Token token)
        {
            string fullUrl = ConfigService.ReturnBaseUrl() + "/member/recent";
            var client = new RestClient(fullUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token.AuthToken);
            request.AddHeader("refresh_token", token.RefreshToken);
            request.RequestFormat = DataFormat.Json;

            var result = new List<MemberViewModel>();
            IRestResponse response = new RestResponse();
            try
            {
                response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(response));
                }
                result = JsonConvert.DeserializeObject<List<MemberViewModel>>(response.Content);
            }
            catch (Exception ex)
            {
                ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, ex);
            }
            return result;
        }

        public static List<MemberViewModel> GetPendingApproval(Token token)
        {
            string fullUrl = ConfigService.ReturnBaseUrl() + "/member/get-pending-approval";
            var client = new RestClient(fullUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token.AuthToken);
            request.AddHeader("refresh_token", token.RefreshToken);
            request.RequestFormat = DataFormat.Json;

            var result = new List<MemberViewModel>();
            IRestResponse response = new RestResponse();
            try
            {
                response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(response));
                }
                result = JsonConvert.DeserializeObject<List<MemberViewModel>>(response.Content);
            }
            catch (Exception ex)
            {
                ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, ex);
            }
            return result;
        }
        public static List<Member> GetActiveMembers(Token token)
        {
            string fullUrl = ConfigService.ReturnBaseUrl() + "/member/get-active-members";
            var client = new RestClient(fullUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token.AuthToken);
            request.AddHeader("refresh_token", token.RefreshToken);
            request.RequestFormat = DataFormat.Json;

            var result = new List<Member>();
            IRestResponse response = new RestResponse();
            try
            {
                response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(response));
                }
                result = JsonConvert.DeserializeObject<List<Member>>(response.Content);
            }
            catch (Exception ex)
            {
                ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, ex);
            }
            return result;
        }

        public static Member GetMember(string id, Token token)
        {
            string fullUrl = ConfigService.ReturnBaseUrl() + "/member/get-member";
            var client = new RestClient(fullUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token.AuthToken);
            request.AddHeader("refresh_token", token.RefreshToken);
            request.AddParameter("id", id);
            request.RequestFormat = DataFormat.Json;

            var result = new Member();
            IRestResponse response = new RestResponse();
            try
            {
                response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(response));
                }
                result = JsonConvert.DeserializeObject<Member>(response.Content);
            }
            catch (Exception ex)
            {
                ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, ex);
            }
            return result;
        }
        public static Response CreateMember(CreateMemberRequest createRequest, Token token)
        {
            var channel = (int)CoreObject.Enumerables.Channel.Web;
            var requestData = JsonConvert.SerializeObject(createRequest);
            string fullUrl = ConfigService.ReturnBaseUrl() + "/member/create-member";
            var client = new RestClient(fullUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("channel", channel.ToString());
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

        public static Response ApproveMember(ApproveMemberRequest approveRequest, Token token)
        {
            var requestData = JsonConvert.SerializeObject(approveRequest);
            string fullUrl = ConfigService.ReturnBaseUrl() + "/member/approve-member";
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

        public static Response DeleteMember(string id, Token token)
        {

            string fullUrl = ConfigService.ReturnBaseUrl() + "/member/delete-member";
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

        public static List<MemberDetailsResponse> GetMembersByName(string query, Token token)
        {
            string fullUrl = ConfigService.ReturnBaseUrl() + "/member/get-members-by-search-query";
            var client = new RestClient(fullUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token.AuthToken);
            request.AddHeader("refresh_token", token.RefreshToken);
            request.AddParameter("seachQuery", query);
            request.RequestFormat = DataFormat.Json;

            var result = new List<MemberDetailsResponse>();
            IRestResponse response = new RestResponse();
            try
            {
                response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(response));
                }
                result = JsonConvert.DeserializeObject<List<MemberDetailsResponse>>(response.Content);
            }
            catch (Exception ex)
            {
                ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, ex);
            }
            return result;
        }

        public static List<MemberDetailsViewModel> GetMembersByMobileNumber(string query, Token token)
        {
            string fullUrl = ConfigService.ReturnBaseUrl() + "/member/get-members-by-mobile-number";
            var client = new RestClient(fullUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token.AuthToken);
            request.AddHeader("refresh_token", token.RefreshToken);
            request.AddParameter("seachQuery", query);
            request.RequestFormat = DataFormat.Json;

            var result = new List<MemberDetailsViewModel>();
            IRestResponse response = new RestResponse();
            try
            {
                response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(response));
                }
                result = JsonConvert.DeserializeObject<List<MemberDetailsViewModel>>(response.Content);
            }
            catch (Exception ex)
            {
                ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, ex);
            }
            return result;
        }

        public static MemberInfoResponse GetMemberInformation(string id, Token token)
        {
            string fullUrl = ConfigService.ReturnBaseUrl() + "/member/get-member-information";
            var client = new RestClient(fullUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token.AuthToken);
            request.AddHeader("refresh_token", token.RefreshToken);
            request.AddParameter("id", id);
            request.RequestFormat = DataFormat.Json;

            var result = new MemberInfoResponse();
            IRestResponse response = new RestResponse();
            try
            {
                response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(response));
                }
                result = JsonConvert.DeserializeObject<MemberInfoResponse>(response.Content);
            }
            catch (Exception ex)
            {
                ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, ex);
            }
            return result;
        }

        public static List<MemberDetailsViewModel> SearchMember(string searchTerm, Token token)
        {
            string fullUrl = ConfigService.ReturnBaseUrl() + "/member/search";
            var client = new RestClient(fullUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token.AuthToken);
            request.AddHeader("refresh_token", token.RefreshToken);
            request.AddParameter("searchterm", searchTerm);
            request.RequestFormat = DataFormat.Json;

            var result = new List<MemberDetailsViewModel>();
            IRestResponse response = new RestResponse();
            try
            {
                response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(response));
                }
                result = JsonConvert.DeserializeObject<List<MemberDetailsViewModel>>(response.Content);
            }
            catch (Exception ex)
            {
                ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, ex);
            }
            return result;
        }
        public static Response UpdateMember(UpdateMemberRequest updateRequest, Token token)
        {
            var requestData = JsonConvert.SerializeObject(updateRequest);
            string fullUrl = ConfigService.ReturnBaseUrl() + "/member/update-member";
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

        public static Response UploadMembers(UploadMemberRequest uploadRequest, Token token)
        {
            var requestData = JsonConvert.SerializeObject(uploadRequest);
            string fullUrl = ConfigService.ReturnBaseUrl() + "/member/upload-members";
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


        public static List<MemberViewModel> GetMembersByCriteria(string criteria, string startdate, string enddate, Token token)
        {
            string fullUrl = ConfigService.ReturnBaseUrl() + "/member/get-members-by-criteria";
            var client = new RestClient(fullUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token.AuthToken);
            request.AddHeader("refresh_token", token.RefreshToken);
            request.AddParameter("criteria", criteria, ParameterType.QueryString);
            request.AddParameter("startdate", startdate, ParameterType.QueryString);
            request.AddParameter("enddate", enddate, ParameterType.QueryString);
            request.RequestFormat = DataFormat.Json;

            var result = new List<MemberViewModel>();
            IRestResponse response = new RestResponse();
            try
            {
                response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(response));
                }
                result = JsonConvert.DeserializeObject<List<MemberViewModel>>(response.Content);
            }
            catch (Exception ex)
            {
                ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, ex);
            }
            return result;
        }

        public static List<MemberDetailsResponse> GetMembersByWeddingAnniversary(string weddingDate, Token token)
        {
            string fullUrl = ConfigService.ReturnBaseUrl() + "/member/get-members-by-wedding-anniversary";
            var client = new RestClient(fullUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token.AuthToken);
            request.AddHeader("refresh_token", token.RefreshToken);
            request.AddParameter("weddingDate", weddingDate);
            request.RequestFormat = DataFormat.Json;

            var result = new List<MemberDetailsResponse>();
            IRestResponse response = new RestResponse();
            try
            {
                response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(response));
                }
                result = JsonConvert.DeserializeObject<List<MemberDetailsResponse>>(response.Content);
            }
            catch (Exception ex)
            {
                ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, ex);
            }
            return result;
        }

        public static List<MemberDetailsResponse> GetMembersByBirthdayAnniversary(string dateOfBirth, Token token)
        {
            string fullUrl = ConfigService.ReturnBaseUrl() + "/member/get-members-by-birthday";
            var client = new RestClient(fullUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token.AuthToken);
            request.AddHeader("refresh_token", token.RefreshToken);
            request.AddParameter("dateOfBirth", dateOfBirth);
            request.RequestFormat = DataFormat.Json;

            var result = new List<MemberDetailsResponse>();
            IRestResponse response = new RestResponse();
            try
            {
                response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, JsonConvert.SerializeObject(response));
                }
                result = JsonConvert.DeserializeObject<List<MemberDetailsResponse>>(response.Content);
            }
            catch (Exception ex)
            {
                ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, ex);
            }
            return result;
        }
    }
}