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

            ViewBag.MissionList = _AccountRepo.MissionList();
            ViewBag.Totalpages = Math.Ceiling(_AccountRepo.UserList().Count() / 6.0);
            ViewBag.UserList = _AccountRepo.UserList().Skip((1 - 1) * 6).Take(6).ToList();
            ViewBag.pg_no = 1;

            return View();
        }

        public ActionResult Search(string? search, int pg)
        {
            var userlist = _AccountRepo.UserList();
            var model = _AccountRepo.UserListSearch(search,pg);

            ViewBag.pg_no = pg;
            ViewBag.Totalpages = Math.Ceiling(_AccountRepo.UserListSearch(search,pg=0).Count() / 6.0);
            ViewBag.model = model.Skip((1 - 1) * 6).Take(6).ToList();

            
            return PartialView("_UserData");
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
