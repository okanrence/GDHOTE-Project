using System;
using System.Collections.Generic;
using System.Text;
using GDHOTE.Hub.CoreObject.Models;
using RestSharp;

namespace GDHOTE.Hub.PortalCore.Integrations
{
    public class LoginIntegration : BaseIntegration, IIntegration<TokenResponse>
    {
        private string _username;
        private string _password;

        public LoginIntegration(string user, string pass)
        {
            _username = user;
            _password = pass;
        }
        public TokenResponse Invoke()
        {
            var request = new RestRequest("/auth/token", Method.POST);
            request.AddHeader("user_type", "administrator");//"customer"
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "password");
            request.AddParameter("username", _username);
            request.AddParameter("password", _password);

            return Execute<TokenResponse>(request);
        }
    }
}
