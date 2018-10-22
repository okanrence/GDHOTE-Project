using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Routing;
using GDHOTE.Hub.BusinessCore.Services;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;
using Microsoft.Owin;
using Newtonsoft.Json;
using RestSharp;

namespace GDHOTE.Hub.WebApi.Handlers
{
    public class LogHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var routeData = request.RequestUri.AbsolutePath.ToLower();

            if (!routeData.StartsWith("/gdhotecore/api") && !routeData.StartsWith("/api"))
            {
                return await base.SendAsync(request, cancellationToken);
            }
            if (routeData.Contains("/api/v1/auth/token"))
            {
                return await base.SendAsync(request, cancellationToken);
            }

            //try and get the operation name and version
            string operationName = string.Empty;
            string operationVersion = string.Empty;
            try
            {
                var operationDetails = routeData.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                operationName = operationDetails[3];
                operationVersion = operationDetails[1];
            }
            catch (Exception)
            {
                //ignored
            }

            

            var entry = CreateRequestResponseMessage(request);


            if (entry == null)
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    RequestMessage = request,
                    Content = new StringContent(
                        JsonConvert.SerializeObject(
                        new Response
                        {
                            ErrorCode = "700",
                            ErrorMessage = "Your request could not be understood"
                        }, Formatting.Indented))
                };
            entry.OperationName = operationName;
            entry.OperationVersion = operationVersion;
            if (request.Content != null)
            {
                await request.Content.ReadAsStringAsync()
                    .ContinueWith(task =>
                    {
                        entry.RequestContentBody = task.Result;
                    }, cancellationToken);
            }

            return await base.SendAsync(request, cancellationToken)
            .ContinueWith(task =>
            {
                var response = task.Result;
                //response.Headers.Add("X-Dummy-Header", Guid.NewGuid().ToString());
                //call the refresh token and send it as part of the response
                var refreshToken = GetHeaderValue(request.Headers, "refresh_token");
                //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
                if (!string.IsNullOrEmpty(refreshToken))
                {
                    try
                    {
                        var client = new RestClient(ConfigurationManager.AppSettings["settings.base.url"]);
                        var req = new RestRequest(ConfigurationManager.AppSettings["settings.refresh.token.url"], Method.POST);
                        req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                        req.AddParameter("grant_type", "refresh_token");
                        req.AddParameter("refresh_token", refreshToken);
                        var resp = client.Execute<TokenResponse>(req);
                        if (resp?.Data != null)
                        {
                            response.Headers.Add("access_token", resp.Data.AccessToken);
                            response.Headers.Add("refresh_token", resp.Data.RefreshToken);
                        }
                    }
                    catch (Exception e)
                    {

                    }


                }
                entry.ResponseStatusCode = (int)response.StatusCode;
                entry.ResponseTimestamp = DateTime.Now;

                if (response.Content != null)
                {
                    entry.ResponseContentBody = response.Content.ReadAsStringAsync().Result;
                    entry.ResponseContentType = response.Content.Headers.ContentType.MediaType;
                    entry.ResponseHeaders = SerializeHeaders(response.Content.Headers);
                    entry.ResponseTimestamp = DateTime.Now;
                }
                new Task(() =>
                {
                    RequestResponseEntryService.Save(entry);
                }).Start();
                return response;
            }, cancellationToken);

        }

        private RequestResponseEntry CreateRequestResponseMessage(HttpRequestMessage request)
        {
            try
            {
                var context = ((OwinContext)request.Properties["MS_OwinContext"]);

                string queryString = string.Empty;
                if (context.Request.QueryString.Value != null)
                {
                    queryString = context.Request.QueryString.Value;
                }
                return new RequestResponseEntry
                {
                    Hash = GetHeaderValue(request.Headers, "Hash"),
                    Token = GetHeaderValue(request.Headers, "Authorization"),
                    Username = context.Authentication.User.Identity.Name,
                    RequestContentType = context.Request.ContentType,
                    RequestIpAddress = context.Request.RemoteIpAddress,
                    RequestMethod = request.Method.Method,
                    RequestHeaders = SerializeHeaders(request.Headers),
                    RequestTimestamp = DateTime.Now,
                    RequestUri = context.Request.Path.Value,
                    QueryString = queryString
                };
            }
            catch (Exception ex)
            {
                // ignored
                return null;
            }


        }

        private string SerializeRouteData(IHttpRouteData routeData)
        {
            return JsonConvert.SerializeObject(routeData, Formatting.Indented);
        }

        private string GetHeaderValue(HttpHeaders headers, string headerKey)
        {
            try
            {
                return headers.GetValues(headerKey).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }

        }

        private string SerializeHeaders(HttpHeaders headers)
        {
            var dict = new Dictionary<string, string>();

            foreach (var item in headers.ToList())
            {
                if (item.Value != null)
                {
                    var header = item.Value.Aggregate(String.Empty, (current, value) => current + (value + " "));
                    header = header.TrimEnd(" ".ToCharArray());
                    dict.Add(item.Key, header);
                }
            }

            return JsonConvert.SerializeObject(dict, Formatting.Indented);
        }
    }
}