using CIPlatform.Data;
using CIPlatform.Entities.Models;
using CIPlatform.Repository.Interface;
using CIPlatform.Views.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CIPlatform.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _AccountRepo;

        public UserController(IUserRepository AccountRepo)
        {
            _AccountRepo = AccountRepo;
        }
        //private readonly CiPlatformContext _db;

        //public UserController(CiPlatformContext db)
        //{
        //    _db = db;
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
         {
            
            return View();
        }

        [HttpPost]
        public IActionResult Login(User objLogin)
        {
            //if (_db.Users.Any(u=> u.Email == objLogin.Email && u.Password == objLogin.Password)) 
            //{ return RedirectToAction("MissionGrid", "Home"); }
            return RedirectToAction("Login", "User");

          
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User objUser)
        {
            _AccountRepo.UserRegister(objUser);
            
            return RedirectToAction("Login", "User");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]

        //public IActionResult Register(User objUser)
        //{
        //    if (objUser.Password == objUser.ConfirmPassword)
        //    {
        //        _db.Users.Add(objUser);
        //        _db.SaveChanges();
        //        return RedirectToAction("MissionGrid", "Home");
        //    }
        //    return RedirectToAction("Login", "User");
        //}

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(User objPass)
        {
            //if (_db.Users.Any(u => u.Email == objPass.Email))
            //{ return RedirectToAction("ResetPassword", "User"); }
            return View();
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(User objResetPass)
        {
            //if (objResetPass.Password == objResetPass.ConfirmPassword)
            //{
            //    _db.Users.Add(objResetPass);
            //    _db.SaveChanges();
            //    return RedirectToAction("MissionGrid", "Home");
            //}
            return RedirectToAction("Login", "User");


        }

       

    }
}
