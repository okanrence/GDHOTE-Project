﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GDHOTE.Hub.Core.BusinessLogic;
using GDHOTE.Hub.Core.Services;

namespace GDHOTE.Hub.Mvc.ApiControllers
{
    [RoutePrefix(ConstantManager.ApiDefaultNamespace + "MainMenu")]

    public class MainMenuController : ApiController
    {
        [Route("getmainmenus")]
        public IHttpActionResult GetMainMenus()
        {
            var countries = MainMenuService.GetMainMenus().ToList();
            if (countries.Count == 0) return NotFound();
            return Ok(countries);
        }
        [Route("getmainmenu")]
        public IHttpActionResult GetMainMenu(string id)
        {
            var mainMenu = MainMenuService.GetMainMenu(id);
            if (mainMenu == null) return NotFound();
            return Ok(mainMenu);
        }

        [HttpDelete]
        [Route("deletemainmenu")]
        public IHttpActionResult DeleteMainMenu(string id)
        {
            var mainMenuInDb = MainMenuService.GetMainMenu(id);
            if (mainMenuInDb == null) return NotFound();
            var result = MainMenuService.Delete(id);
            return Ok(result);
        }
    }
}