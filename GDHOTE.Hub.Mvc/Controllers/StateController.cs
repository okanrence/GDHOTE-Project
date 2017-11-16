using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.Core.BusinessLogic;
using GDHOTE.Hub.Core.Models;
using GDHOTE.Hub.Core.Services;
using GDHOTE.Hub.Core.ViewModels;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class StateController : Controller
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
            var countries = CountryService.GetCountries().ToList();
            var statuses = StatusService.GetStatus().ToList();
            var viewModel = new StateFormViewModel
            {
                Status =  statuses,
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
                var countries = CountryService.GetCountries().ToList();
                var statuses = StatusService.GetStatus().ToList();
                var viewModel = new StateFormViewModel
                {
                    Status =statuses,
                    Countries = countries,
                    State = state
                };
                return View("StateForm", viewModel);
            }
            state.RecordDate = DateTime.Now;
            state.StateCode =  state.StateCode.ToUpper();
            state.StateName = StringCaseManager.TitleCase(state.StateName);
            if (state.Id == 0)
            {
                var result = StateService.Save(state);
            }
            else
            {
                var stateInDb = StateService.GetState(state.Id);
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
            var countries = CountryService.GetCountries().ToList();
            var statuses = StatusService.GetStatus().ToList();
            var viewModel = new StateFormViewModel
            {
                Status =statuses,
                Countries = countries,
                State = state
            };
            return View("StateForm", viewModel);
        }
    }
}