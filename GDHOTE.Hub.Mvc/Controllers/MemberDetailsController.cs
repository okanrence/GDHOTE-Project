using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.BusinessCore.Services;
using GDHOTE.Hub.CoreObject.ViewModels;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class MemberDetailsController : BaseController
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
            var yearGroups = YearGroupService.GetActiveYearGroups().ToList();
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
            var yearGroups = YearGroupService.GetActiveYearGroups().ToList();
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
                var yearGroups = YearGroupService.GetActiveYearGroups().ToList();
                var viewModel = new MemberDetailsFormModel
                {
                    States = states,
                    Countries = countries,
                    YearGroups = yearGroups,
                    MemberDetails = memberDetails
                };
                return View("MemberDetailsForm", viewModel);
            }
            
            if (memberDetails.MemberDetailsId == 0)
            {
                memberDetails.CreatedBy = User.Identity.Name;
                memberDetails.RecordDate = DateTime.Now;
                memberDetails.PostedDate = DateTime.Now;
                var result = MemberDetailsService.Save(memberDetails);
            }
            else
            {
                var memberDetailsInDb = MemberDetailsService.GetMemberDetails(memberDetails.MemberDetailsId);
                if (memberDetailsInDb == null) return HttpNotFound();
                memberDetailsInDb.YearGroupCode = memberDetails.YearGroupCode;
                memberDetailsInDb.DateWedded = memberDetails.DateWedded;
                memberDetailsInDb.ResidenceAddress = memberDetails.ResidenceAddress;
                memberDetailsInDb.MobileNumber = memberDetails.MobileNumber;
                memberDetailsInDb.AlternateNumber = memberDetails.AlternateNumber;
                memberDetailsInDb.EmailAddress = memberDetails.EmailAddress;
                memberDetailsInDb.LastUpdatedBy = User.Identity.Name;
                memberDetailsInDb.LastUpdatedDate = DateTime.Now;
                var result = MemberDetailsService.Update(memberDetailsInDb);
            }
            return RedirectToAction("Index", "MemberDetails");
        }
    }
}