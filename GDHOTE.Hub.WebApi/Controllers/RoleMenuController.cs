using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GDHOTE.Hub.BusinessCore.BusinessLogic;
using GDHOTE.Hub.BusinessCore.Exceptions;
using GDHOTE.Hub.BusinessCore.Services;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.ViewModels;
using GDHOTE.Hub.WebApi.OwinProvider;
using Newtonsoft.Json;

namespace GDHOTE.Hub.WebApi.Controllers
{
    [RoutePrefix(ConstantManager.ApiDefaultNamespace + "menu")]
    public class RoleMenuController : ApiController
    {
        [HttpGet]
        [Route("get-all-role-menus")]
        [UnAuthorized]
        public HttpResponseMessage GetAllRoleMenus()
        {
            try
            {
                var response = RoleMenuService.GetAllRoleMenus().ToList();
                if (response.Count > 0)
                {
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        RequestMessage = Request,
                        Content = new StringContent(
                            JsonConvert.SerializeObject(response, Formatting.Indented))
                    };
                }

                return new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    RequestMessage = Request,
                    Content = new StringContent(
                        JsonConvert.SerializeObject(response, Formatting.Indented))
                };
            }
            catch (UnableToCompleteException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.GetException());
            }
        }


        [HttpGet]
        [Route("get-active-role-menus")]
        [UnAuthorized]
        public HttpResponseMessage GetActiveRoleMenus()
        {
            try
            {
                var response = RoleMenuService.GetActiveRoleMenus().ToList();
                if (response.Count > 0)
                {
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        RequestMessage = Request,
                        Content = new StringContent(
                            JsonConvert.SerializeObject(response, Formatting.Indented))
                    };
                }

                return new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    RequestMessage = Request,
                    Content = new StringContent(
                        JsonConvert.SerializeObject(response, Formatting.Indented))
                };
            }
            catch (UnableToCompleteException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.GetException());
            }
        }

        [HttpGet]
        [Route("get-role-menu")]
        [UnAuthorized]
        public HttpResponseMessage GetRoleMenu(string id)
        {
            try
            {
                var response = RoleMenuService.GetRoleMenu(id);
                if (response != null)
                {
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        RequestMessage = Request,
                        Content = new StringContent(
                            JsonConvert.SerializeObject(response, Formatting.Indented))
                    };
                }
                return new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    RequestMessage = Request,
                    Content = new StringContent(
                        JsonConvert.SerializeObject(response, Formatting.Indented))
                };
            }
            catch (UnableToCompleteException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.GetException());
            }
        }


        [HttpGet]
        [Route("get-role-menus-by-role")]
        [UnAuthorized]
        public HttpResponseMessage GetRoleMenuByRole(string id)
        {
            try
            {
                var response = RoleMenuService.GetRoleMenuByRole(Convert.ToInt16(id)).ToList();
                if (response.Count > 0)
                {
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        RequestMessage = Request,
                        Content = new StringContent(
                            JsonConvert.SerializeObject(response, Formatting.Indented))
                    };
                }

                return new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    RequestMessage = Request,
                    Content = new StringContent(
                        JsonConvert.SerializeObject(response, Formatting.Indented))
                };
            }
            catch (UnableToCompleteException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.GetException());
            }
        }


        [HttpPost]
        [Route("create-role-menu")]
        [UnAuthorized(Roles = "Super Admin, Adminstrator")]
        public HttpResponseMessage CreateRoleMenu(CreateRoleMenuRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                string username = User.Identity.Name;
                var response = RoleMenuService.CreateRoleMenu(request, username);
                if (response != null)
                {
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        RequestMessage = Request,
                        Content = new StringContent(
                            JsonConvert.SerializeObject(response, Formatting.Indented))
                    };
                }
                return new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    RequestMessage = Request,
                    Content = new StringContent(
                        JsonConvert.SerializeObject(response, Formatting.Indented))
                };

            }
            catch (UnableToCompleteException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.GetException());
            }
        }


        [HttpPost]
        [Route("delete-role-menu")]
        [UnAuthorized(Roles = "Super Admin, Adminstrator")]
        public HttpResponseMessage DeleteRoleMenu(string id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                string username = User.Identity.Name;
                var response = RoleMenuService.Delete(id, username);
                if (response != null)
                {
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        RequestMessage = Request,
                        Content = new StringContent(
                            JsonConvert.SerializeObject(response, Formatting.Indented))
                    };
                }
                return new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    RequestMessage = Request,
                    Content = new StringContent(
                        JsonConvert.SerializeObject(response, Formatting.Indented))
                };

            }
            catch (UnableToCompleteException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.GetException());
            }
        }
    }
}
