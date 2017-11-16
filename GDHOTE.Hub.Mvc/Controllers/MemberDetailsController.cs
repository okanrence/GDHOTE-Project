using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.Core.Models;
using GDHOTE.Hub.Core.Services;
using GDHOTE.Hub.Core.ViewModels;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class MemberDetailsController : Controller
    {
        // GET: MemberDetails
        public ActionResult Index()
        {
            var memberDetails = MemberDetailsService.GetMembersDetails().ToList();
            return View(memberDetails);
        }
        public ActionResult New()
        {
            var states = StateService.GetStates().ToList();
            var countries = CountryService.GetCountries().ToList();
            var yearGroups = YearGroupService.GetYearGroups().ToList();
            var viewModel = new MemberDetailsFormModel
            {
                States = states,
                Countries = countries,
                YearGroups = yearGroups,
                MemberDetails = new MemberDetails()
            };

            return View("MemberDetailsForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var memberDetails = MemberDetailsService.GetMemberDetails(id);
            //Revist this line.
            if (memberDetails == null) memberDetails = new MemberDetails { MemberKey = id };
            var states = StateService.GetStates().ToList();
            var countries = CountryService.GetCountries().ToList();
            var yearGroups = YearGroupService.GetYearGroups().ToList();
            var viewModel = new MemberDetailsFormModel
            {
                States = states,
                Countries = countries,
                YearGroups = yearGroups,
                MemberDetails = memberDetails// new MemberDetails()
            };
            return View("MemberDetailsForm", viewModel);
        }
        public ActionResult Save(MemberDetails memberDetails)
        {
            if (!ModelState.IsValid)
            {
                var states = StateService.GetStates().ToList();
                var countries = CountryService.GetCountries().ToList();
                var yearGroups = YearGroupService.GetYearGroups().ToList();
                var viewModel = new MemberDetailsFormModel
                {
                    States = states,
                    Countries = countries,
                    YearGroups = yearGroups,
                    MemberDetails = memberDetails
                };
                return View("MemberDetailsForm", viewModel);
            }
            memberDetails.CreatedBy = 0;
            memberDetails.RecordDate = DateTime.Now;
            memberDetails.PostedDate = DateTime.Now;

            if (memberDetails.MemberDetailsId == 0)
            {
                var result = MemberDetailsService.Save(memberDetails);
            }
            else
            {
                var memberDetailsInDb = MemberDetailsService.GetMemberDetails(memberDetails.MemberDetailsId);
                if (memberDetailsInDb == null) return HttpNotFound();
                memberDetailsInDb.YearGroupCode  = memberDetails.YearGroupCode;
                memberDetailsInDb.DateWedded = memberDetails.DateWedded;
                memberDetailsInDb.ResidenceAddress = memberDetails.ResidenceAddress;
                memberDetailsInDb.MobileNumber = memberDetails.MobileNumber;
                memberDetailsInDb.AlternateNumber = memberDetails.AlternateNumber;
                memberDetailsInDb.EmailAddress = memberDetails.EmailAddress;
                memberDetailsInDb.LastUpdatedDate = DateTime.Now;
                var result = MemberDetailsService.Update(memberDetailsInDb);
            }
            return RedirectToAction("Index", "MemberDetails");
        }
    }
}