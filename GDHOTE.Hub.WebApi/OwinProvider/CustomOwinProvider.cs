﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using GDHOTE.Hub.BusinessCore.Exceptions;
using GDHOTE.Hub.BusinessCore.Services;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace GDHOTE.Hub.WebApi.OwinProvider
{
    public class CustomOwinProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {

            context.Validated();

        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            string userType = string.Empty;
            try
            {
                userType = context.Request.Headers["user_type"];
                if (string.IsNullOrEmpty(userType))
                {
                    context.SetError("invalid_grant", "user_type is missing in the header");
                    return;
                }
            }
            catch (Exception)
            {
                context.SetError("invalid_grant", "user_type is missing in the header");
                return;
            }

            if (string.IsNullOrEmpty(context.UserName))
            {
                context.SetError("invalid_grant", "Please specify your username");
                return;
            }
            if (string.IsNullOrEmpty(context.Password))
            {
                context.SetError("invalid_grant", "Please specify your password");
                return;
            }

            long id = 0;
            string role = string.Empty;

            AuthenticationProperties props = null;
            try
            {
                switch (userType)
                {
                    case "customer":
                        var loginRequest = new LoginRequest
                        {
                            UserName = context.UserName,
                            Password = context.Password
                        };
                        var customerUser = UserService.AuthenticateUser(loginRequest);
                        
                        //id = customer.Customer.Id;
                        //role = customer.CustomerUserViewModel.RoleName;
                        props = new AuthenticationProperties(new Dictionary<string, string>
                        {
                            { "as:clientRefreshTokenLifeTime","10"},
                            {"as:clientAllowedOrigin","*" },
                            {
                                "firstName",customerUser.FirstName
                            },
                            {
                                "lastName",customerUser.LastName//customer.Customer.LastName
                            },
                            {
                                "as:client_id",customerUser.UserId// customer.Customer.Id.ToString()
                            },
                            {
                                "userName", context.UserName
                            }
                        });
                        break;
                    case "administrator":
                        var adminloginRequest = new LoginRequest
                        {
                            UserName = context.UserName,
                            Password = context.Password
                        };
                        var adminUser = UserService.AuthenticateUser(adminloginRequest);
                        id = 1;// admin.User.Id;
                        role = adminUser.RoleId;
                        props = new AuthenticationProperties(new Dictionary<string, string>
                        {
                            { "as:clientRefreshTokenLifeTime","10"},
                            {"as:clientAllowedOrigin","*" },
                            {
                                "as:client_id", adminUser.RoleId 
                            },
                            {
                                "userName", context.UserName
                            }
                        });
                        break;
                    default:
                        context.SetError("invalid_grant", "Please specify your password");
                        return;


                }
                Claim claim1 = new Claim(ClaimTypes.Name, context.UserName);
                Claim[] claims = new Claim[] { claim1 };
                //string dateNow = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);
                var identity = new ClaimsIdentity(claims, context.Options.AuthenticationType);
                identity.AddClaim(new Claim("Name", context.UserName));
                identity.AddClaim(new Claim("id", id.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
                var ticket = new AuthenticationTicket(identity, props);
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
                context.Validated(ticket);

            }
            catch (InvalidRequestException ex)
            {
                context.SetError("invalid_grant", ex.ErrorMessage);
                return;
            }
            catch (UnableToCompleteException ex)
            {
                context.SetError("invalid_grant", ex.ErrorMessage);
                return;
            }


        }
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {

                context.AdditionalResponseParameters.Add(property.Key, property.Value);


            }

            return Task.FromResult<object>(null);
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
            var currentClient = context.ClientId;

            //if (originalClient != currentClient)
            //{
            //    context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.");
            //    return Task.FromResult<object>(null);
            //}

            // Change auth ticket for refresh token requests
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
            newIdentity.AddClaim(new Claim("newClaim", "newValue"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }


    }
}