using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GDHOTE.Hub.BusinessCore.BusinessLogic;
using GDHOTE.Hub.BusinessCore.Services;

namespace GDHOTE.Hub.WebApi.Controllers
{
    [RoutePrefix(ConstantManager.ApiDefaultNamespace + "menu")]
    public class SubMenuController : ApiController
    {
        [Route("get-sub-menus")]
        public IHttpActionResult GetSubMenus()
        {
            var subMenus = SubMenuService.GetSubMenus().ToList();
            if (subMenus.Count == 0) return NotFound();
            return Ok(subMenus);
        }
        [Route("get-sub-menu")]
        public IHttpActionResult GetSubMenu(string id)
        {
            var subMenu = SubMenuService.GetSubMenu(id);
            if (subMenu == null) return NotFound();
            return Ok(subMenu);
        }

        [HttpDelete]
        [Route("delete-sub-menu")]
        public IHttpActionResult DeleteSubMenu(string id)
        {
            var subMenuInDb = SubMenuService.GetSubMenu(id);
            if (subMenuInDb == null) return NotFound();
            var result = SubMenuService.Delete(id);
            return Ok(result);
        }
    }
}
