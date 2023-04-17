﻿using CIPlatform.Data;
using CIPlatform.Entities.ViewModel;
using CIPlatform.Models;
using CIPlatform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.PortableExecutable;

namespace CIPlatform.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAccountRepository _AccountRepo;

        public AdminController(IAccountRepository AccountRepo)
        {
            _AccountRepo = AccountRepo;
        }

        public IActionResult Index()
        {
            //var model = _AccountRepo.adminModelList();

            ViewBag.CMSList = _AccountRepo.CMSList().Skip((1 - 1) * 6).Take(6).ToList();
            ViewBag.ttlCMS = Math.Ceiling(_AccountRepo.CMSList().Count() / 6.0);
            
            ViewBag.MissionList = _AccountRepo.MissionList().Skip((1 - 1) * 6).Take(6).ToList();
            ViewBag.ttlMissions = Math.Ceiling(_AccountRepo.MissionList().Count() / 6.0);

            ViewBag.ThemeList = _AccountRepo.ThemeList().Skip((1 - 1) * 6).Take(6).ToList();
            ViewBag.ttlThemes = Math.Ceiling(_AccountRepo.ThemeList().Count() / 6.0);

            ViewBag.SkillList = _AccountRepo.SkillList().Skip((1 - 1) * 6).Take(6).ToList();
            ViewBag.ttlSkills = Math.Ceiling(_AccountRepo.SkillList().Count() / 6.0);

            ViewBag.UserList = _AccountRepo.UserList().Skip((1 - 1) * 6).Take(6).ToList();
            ViewBag.ttlUsers = Math.Ceiling(_AccountRepo.UserList().Count() / 6.0);

            ViewBag.MissionApplicationList = _AccountRepo.MissionApplicationList().Skip((1 - 1) * 6).Take(6).ToList();
            ViewBag.ttlApplications = Math.Ceiling(_AccountRepo.MissionApplicationList().Count() / 6.0);
            
            ViewBag.StoryList = _AccountRepo.StoryList().Skip((1 - 1) * 6).Take(6).ToList();
            ViewBag.ttlStories = Math.Ceiling(_AccountRepo.StoryList().Count() / 6.0);

            ViewBag.pg_no = 1;

            return View();
        }

        public ActionResult Search(string? search, int pg, string who)
        {
            if (who == "mission")
            {
                var model = _AccountRepo.MissionListSearch(search, pg);

                ViewBag.MissionList = model;
                ViewBag.pg_no = pg;
                ViewBag.ttlMissions = Math.Ceiling(_AccountRepo.MissionListSearch(search, pg = 0).Count() / 6.0);



                return PartialView("_MissionData");
                
            }
            else if (who == "cms")
            {
                var model = _AccountRepo.CMSListSearch(search, pg);

                ViewBag.CMSList = model;
                ViewBag.pg_no = pg;
                ViewBag.ttlCMS = Math.Ceiling(_AccountRepo.CMSListSearch(search, pg = 0).Count() / 6.0);

                return PartialView("_CMSData");

            }
            else if (who == "theme")
            {
                var model = _AccountRepo.ThemeListSearch(search, pg);

                ViewBag.ThemeList = model;
                ViewBag.pg_no = pg;
                ViewBag.ttlThemes = Math.Ceiling(_AccountRepo.ThemeListSearch(search, pg = 0).Count() / 6.0);

                return PartialView("_ThemeData");

            }
            else if (who == "skill")
            {
                var model = _AccountRepo.SkillListSearch(search, pg);

                ViewBag.SkillList = model;
                ViewBag.pg_no = pg;
                ViewBag.ttlSkills = Math.Ceiling(_AccountRepo.SkillListSearch(search, pg = 0).Count() / 6.0);

                return PartialView("_SkillData");

            }
            else if(who == "application")
            {
                var model = _AccountRepo.ApplicationListSearch(search, pg);

                ViewBag.MissionApplicationList = model;
                ViewBag.pg_no = pg;
                ViewBag.ttlApplications = Math.Ceiling(_AccountRepo.ApplicationListSearch(search, pg = 0).Count() / 6.0);

                return PartialView("_ApplicationData");
            }
            else if(who == "story")
            {
                var model = _AccountRepo.StoryListSearch(search, pg);

                ViewBag.StoryList = model;
                ViewBag.pg_no = pg;
                ViewBag.ttlStories = Math.Ceiling(_AccountRepo.StoryListSearch(search, pg = 0).Count() / 6.0);

                return PartialView("_StoryData");
            }
            else 
            {
                var model = _AccountRepo.UserListSearch(search, pg);

                ViewBag.UserList = model;
                ViewBag.pg_no = pg;
                ViewBag.ttlUsers = Math.Ceiling(_AccountRepo.UserListSearch(search, pg = 0).Count() / 6.0);

                return PartialView("_UserData");
            }
        }

        public IActionResult AddCMS(AdminModel adminModel)
        {
           bool result = _AccountRepo.AddCMS(adminModel);
            ViewBag.CMSList = _AccountRepo.CMSList().Skip((1 - 1) * 6).Take(6).ToList();
            ViewBag.ttlCMS = Math.Ceiling(_AccountRepo.CMSList().Count() / 6.0);
            ViewBag.pg_no = 1;

            return PartialView("_CMSData");

        }
        //private readonly CiPlatformContext _db;

        //public AdminController(CiPlatformContext db)
        //{
        //    _db = db;
        //}

        //public IActionResult Index()
        //{
        //    IEnumerable<Admin> objAdminList = _db.Admins;
        //    return View(objAdminList);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Register(Admin objAdmin)
        {
            //_db.Admins.Add(objAdmin);
            //_db.SaveChanges();
            return RedirectToAction("Index","Admin");
        }
    }
}
