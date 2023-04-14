using CIPlatform.Data;
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
            var model = _AccountRepo.adminModelList();

            ViewBag.MissionList = _AccountRepo.MissionList().Skip((1 - 1) * 6).Take(6).ToList();
            ViewBag.ThemeList = _AccountRepo.ThemeList().Skip((1 - 1) * 6).Take(6).ToList();
            ViewBag.SkillList = _AccountRepo.SkillList().Skip((1 - 1) * 6).Take(6).ToList();
            ViewBag.Totalpages = Math.Ceiling(_AccountRepo.UserList().Count() / 6.0);
            ViewBag.UserList = _AccountRepo.UserList().Skip((1 - 1) * 6).Take(6).ToList();
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
                ViewBag.Totalpages = Math.Ceiling(_AccountRepo.MissionListSearch(search, pg = 0).Count() / 6.0);



                return PartialView("_MissionData");
                
            }
            else if (who == "theme")
            {
                var model = _AccountRepo.ThemeListSearch(search, pg);

                ViewBag.ThemeList = model;
                ViewBag.pg_no = pg;
                ViewBag.Totalpages = Math.Ceiling(_AccountRepo.ThemeListSearch(search, pg = 0).Count() / 6.0);

                return PartialView("_ThemeData");

            }
            else if (who == "skill")
            {
                var model = _AccountRepo.SkillListSearch(search, pg);

                ViewBag.ThemeList = model;
                ViewBag.pg_no = pg;
                ViewBag.Totalpages = Math.Ceiling(_AccountRepo.SkillListSearch(search, pg = 0).Count() / 6.0);

                return PartialView("_SkillData");

            }
            else 
            {
                var model = _AccountRepo.UserListSearch(search, pg);

                ViewBag.UserList = model;
                ViewBag.pg_no = pg;
                ViewBag.Totalpages = Math.Ceiling(_AccountRepo.UserListSearch(search, pg = 0).Count() / 6.0);

                return PartialView("_UserData");
            }
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
