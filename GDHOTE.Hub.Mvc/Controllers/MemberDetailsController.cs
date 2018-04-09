using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.BusinessCore.Services;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.ViewModels;
using GDHOTE.Hub.PortalCore.Services;
using Newtonsoft.Json;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class MemberDetailsController : BaseController
    {
        // GET: MemberDetails
        public ActionResult Index()
        {
            var memberDetails = PortalMemberDetailsService.GetMembersDetails().ToList();
            return View(memberDetails);
        }
        public ActionResult New()
        {
            return View("MemberDetailsForm", ReturnViewModel());
        }

        public ActionResult Edit(string id)
        {
            var memberDetails = PortalMemberDetailsService.GetMemberDetails(id);
            var viewModelTemp = ReturnViewModel();
            var item = JsonConvert.SerializeObject(memberDetails);
            var viewModel = JsonConvert.DeserializeObject<MemberDetailsFormModel>(item);
            viewModel.Countries = viewModelTemp.Countries;
            viewModel.States = viewModelTemp.States;
            viewModel.YearGroups = viewModelTemp.YearGroups;
            return View("MemberDetailsForm", viewModel);
        }

        public ActionResult Save(CreateMemberDetailsRequest createRequest)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = ReturnViewModel();
                var item = JsonConvert.SerializeObject(createRequest);
                viewModel = JsonConvert.DeserializeObject<MemberDetailsFormModel>(item);
                return View("MemberDetailsForm", viewModel);
            }

            var result = PortalMemberDetailsService.CreateMemberDetails(createRequest);
            if (result != null)
            {
                //Successful
                if (result.ErrorCode == "00")
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorBag = result.ErrorMessage;
                }

            }
            else
            {
                ViewBag.ErrorBag = "Unable to complete your request at the moment";
            }
            // If we got this far, something failed, redisplay form
            return View("MemberDetailsForm", ReturnViewModel());
        }

        private static MemberDetailsFormModel ReturnViewModel()
        {
            var states = PortalStateService.GetActiveStates().ToList();
            var countries = PortalCountryService.GetActiveCountries().ToList();
            var yearGroups = PortalYearGroupService.GetActiveYearGroups().ToList();
            var viewModel = new MemberDetailsFormModel
            {
                States = states,
                Countries = countries,
                YearGroups = yearGroups
            };
            return viewModel;
        }
    }
}