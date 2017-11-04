﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.Core.Models;
using GDHOTE.Hub.Core.Services;
using GDHOTE.Hub.Core.ViewModels;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        public ActionResult Index()
        {
            var members = MemberService.GetMembers().ToList();
            return View(members);
        }
        public ActionResult New()
        {
            var sexList = SexService.GetSex().ToList();
            var maritalStatusList = MaritalStatusService.GetMaritalStatuses().ToList();
            var viewModel = new MemberFormViewModel
            {
                Sexs = sexList,
                MaritalStatus = maritalStatusList,
                Member = new Member()
            };

            return View("MemberForm", viewModel);
        }
        public ActionResult Edit(int id)
        {
            var member = MemberService.GetMember(id);
            var sexList = SexService.GetSex().ToList();
            var maritalStatusList = MaritalStatusService.GetMaritalStatuses().ToList();
            var viewModel = new MemberFormViewModel
            {
                Sexs = sexList,
                MaritalStatus = maritalStatusList,
                Member = member
            };
            return View("MemberForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Member member)
        {
            if (!ModelState.IsValid)
            {
                var sexList = SexService.GetSex().ToList();
                var maritalStatusList = MaritalStatusService.GetMaritalStatuses().ToList();
                var viewModel = new MemberFormViewModel
                {
                    Sexs = sexList,
                    MaritalStatus = maritalStatusList,
                    Member = member
                };
                return View("MemberForm", viewModel);
            }
            member.StatusCode = "A";
            member.DeleteFlag = "N";
            member.ApprovedFlag = "N";
            member.RecordDate = DateTime.Now;
            member.PostedDate = DateTime.Now;
            if (member.MemberKey == 0)
            {
                var result = MemberService.SaveMember(member);
            }
            else
            {
                var memberInDb = MemberService.GetMember(member.MemberKey);
                if (memberInDb == null) return HttpNotFound();
                memberInDb.FirstName = member.FirstName;
                memberInDb.Surname = member.Surname;
                memberInDb.MiddleName = member.MiddleName;
                memberInDb.DateOfBirth = member.DateOfBirth;
                memberInDb.LastUpdatedDate = DateTime.Now;
                var result = MemberService.UpdateState(memberInDb);
            }
            return RedirectToAction("Index", "Member");
            
        }
    }
}