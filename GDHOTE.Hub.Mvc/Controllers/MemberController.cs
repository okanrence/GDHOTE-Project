using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.Core.BusinessLogic;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.Core.Services;
using GDHOTE.Hub.CoreObject.ViewModels;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class MemberController : BaseController
    {
        // GET: Member
        public ActionResult Index()
        {
            var members = MemberService.GetMembers().ToList();
            return View(members);
        }
        public ActionResult List()
        {
            var members = MemberService.GetMembers().ToList();
            return View("ReadOnlyList", members);
        }
        public ActionResult New()
        {
            var viewModel = ReturnMemberFormViewModel();
            return View("MemberForm", ReturnMemberFormViewModel());
        }
        public ActionResult Edit(int id)
        {
            var member = MemberService.GetMember(id);
            var genders = GenderService.GetGenders();
            var maritalStatuses = MaritalStatusService.GetMaritalStatuses();
            var viewModel = new MemberFormViewModel
            {
                Genders = genders,
                MaritalStatuses = maritalStatuses,
                //Member = member
            };
            return View("UpdateMemberForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CreateMemberRequest createRequest)
        {
            if (!ModelState.IsValid)
            {
                return View("MemberForm", ReturnMemberFormViewModel());
            }
            string currentUser = User.Identity.Name;
            int channelCode = (int)CoreObject.Enumerables.Channel.Web;
            var result = MemberManager.CreateMember(createRequest, currentUser, channelCode);
            if (result != null)
            {

                if (result.ErrorCode == "00")
                {
                    return RedirectToAction("Index", "Member");
                }
                else
                {
                    ViewBag.LoginError = result.ErrorMessage;
                }
            }
            else
            {
                //Display Error
                ViewBag.LoginError = "Unable to complete request";
            }
            return View("MemberForm", ReturnMemberFormViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UpdateMemberRequest updateRequest)
        {
            if (!ModelState.IsValid)
            {
                return View("MemberForm", ReturnMemberFormViewModel());
            }
            string currentUser = User.Identity.Name;

            return View();
            //if (member.MemberKey == 0)
            //{
            //    //Validate DOB
            //    var dob = member.DateOfBirth;
            //    //if (DateTime.TryParse(member.DateOfBirth, out temp))
            //    //{

            //    //}
            //    member.CreatedBy = User.Identity.Name;
            //    member.StatusCode = "A";
            //    member.DeleteFlag = "N";
            //    member.ApprovedFlag = "N";
            //    member.RecordDate = DateTime.Now;
            //    member.PostedDate = DateTime.Now;
            //    member.OfficerId = (int)EnumsService.OfficerType.NormalMember;
            //    member.OfficerDate = DateTime.Now;
            //    var result = MemberService.Save(member);
            //}
            //else
            //{
            //    var memberInDb = MemberService.GetMember(member.MemberKey);
            //    if (memberInDb == null) return HttpNotFound();
            //    memberInDb.FirstName = member.FirstName;
            //    memberInDb.Surname = member.Surname;
            //    memberInDb.MiddleName = member.MiddleName;
            //    memberInDb.DateOfBirth = member.DateOfBirth;
            //    memberInDb.ApprovedBy = User.Identity.Name;
            //    memberInDb.LastUpdatedDate = DateTime.Now;
            //    var result = MemberService.Update(memberInDb);
            //}

        }


        public ActionResult ApproveMember()
        {
            var members = MemberService.GetMembersPendingApproval().ToList();
            return View(members);
        }
        private static MemberFormViewModel ReturnMemberFormViewModel()
        {
            var genders = GenderService.GetGenders();
            var maritalStatuses = MaritalStatusService.GetMaritalStatuses();
            var viewModel = new MemberFormViewModel
            {
                Genders = genders,
                MaritalStatuses = maritalStatuses
            };
            return viewModel;
        }
    }
}