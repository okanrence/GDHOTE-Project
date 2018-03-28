using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.BusinessCore.BusinessLogic;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.BusinessCore.Services;
using GDHOTE.Hub.CoreObject.ViewModels;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class StateController : BaseController
    {
        // GET: State
        public ActionResult Index()
        {
            //var states = StateService.GetStates().ToList();
            //return View(states);
            return View("StateIndex");
        }
        public ActionResult New()
        {
            var countries = CountryService.GetAllCountries().ToList();
            var statuses = StatusService.GetStatuses().ToList();
            var viewModel = new StateFormViewModel
            {
                Status = statuses,
                Countries = countries,
                State = new State()
            };

            return View("StateForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(State state)
        {
            if (!ModelState.IsValid)
            {
                var countries = CountryService.GetAllCountries().ToList();
                var statuses = StatusService.GetStatuses().ToList();
                var viewModel = new StateFormViewModel
                {
                    Status = statuses,
                    Countries = countries,
                    State = state
                };
                return View("StateForm", viewModel);
            }
            state.StateCode = state.StateCode.ToUpper();
            state.StateName = StringCaseManager.TitleCase(state.StateName);
            if (state.StateCode == null)
            {
                state.RecordDate = DateTime.Now;
                var result = StateService.Save(state);
            }
            else
            {
                var stateInDb = StateService.GetState(state.StateId);
                if (stateInDb == null) return HttpNotFound();
                stateInDb.Status = state.Status;
                stateInDb.StateName = state.StateName;
                var result = StateService.Update(stateInDb);
            }
            return RedirectToAction("Index", "State");
        }
        public ActionResult Edit(int id)
        {
            var state = StateService.GetState(id);
            if (state == null) return HttpNotFound();
            var countries = CountryService.GetAllCountries().ToList();
            var statuses = StatusService.GetStatuses().ToList();
            var viewModel = new StateFormViewModel
            {
                Status = statuses,
                Countries = countries,
                State = state
            };
            return View("StateForm", viewModel);
        }
    }
}