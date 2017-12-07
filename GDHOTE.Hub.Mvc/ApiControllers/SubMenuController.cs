using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GDHOTE.Hub.Core.BusinessLogic;
using GDHOTE.Hub.Core.Services;

namespace GDHOTE.Hub.Mvc.ApiControllers
{
    [RoutePrefix(ConstantManager.ApiDefaultNamespace + "submenu")]

    public class SubMenuController : ApiController
    {
        [Route("getsubmenus")]
        public IHttpActionResult GetSubMenus()
        {
            var subMenus = SubMenuService.GetSubMenus().ToList();
            if (subMenus.Count == 0) return NotFound();
            return Ok(subMenus);
        }
        [Route("getsubmenu")]
        public IHttpActionResult GetSubMenu(string id)
        {
            var subMenu = SubMenuService.GetSubMenu(id);
            if (subMenu == null) return NotFound();
            return Ok(subMenu);
        }

        [HttpDelete]
        [Route("deletesubmenu")]
        public IHttpActionResult DeleteSubMenu(string id)
        {
            var subMenuInDb = SubMenuService.GetSubMenu(id);
            if (subMenuInDb == null) return NotFound();
            var result = SubMenuService.Delete(id);
            return Ok(result);
        }
    }
}
