using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GDHOTE.Hub.BusinessCore.BusinessLogic;
using GDHOTE.Hub.BusinessCore.Services;

namespace GDHOTE.Hub.Mvc.ApiControllers
{
    [RoutePrefix(ConstantManager.ApiDefaultNamespace + "menu")]

    public class MainMenuController : ApiController
    {
        [Route("get-main-menus")]
        public IHttpActionResult GetMainMenus()
        {
            var countries = MainMenuService.GetMainMenus().ToList();
            if (countries.Count == 0) return NotFound();
            return Ok(countries);
        }
        [Route("get-main-menu")]
        public IHttpActionResult GetMainMenu(string id)
        {
            var mainMenu = MainMenuService.GetMainMenu(id);
            if (mainMenu == null) return NotFound();
            return Ok(mainMenu);
        }

        [HttpDelete]
        [Route("delete-main-menu")]
        public IHttpActionResult DeleteMainMenu(string id)
        {
            var mainMenuInDb = MainMenuService.GetMainMenu(id);
            if (mainMenuInDb == null) return NotFound();
            var result = MainMenuService.Delete(id);
            return Ok(result);
        }
    }
}
