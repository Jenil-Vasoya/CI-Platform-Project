
using CIPlatform.Entities.Models;
using CIPlatform.Entities.ViewModel;
using CIPlatform.Repository.Interface;
using CIPlatform.Views.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SendGrid.Helpers.Mail;
using System.Data;
using MailHelper = CIPlatform.Entities.ViewModel.MailHelper;

namespace CIPlatform.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _AccountRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration configuration;

        public UserController(IUserRepository AccountRepo, IHttpContextAccessor httpContextAccessor, IConfiguration _configuration)
        {
            _AccountRepo = AccountRepo;
            _httpContextAccessor = httpContextAccessor;
            configuration = _configuration;
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
            var objUser = _AccountRepo.UserList().FirstOrDefault(u => u.Email == objLogin.Email && u.Password == objLogin.Password);

                if (objUser != null)
                {
                    return RedirectToAction("MissionGrid", "Home");

                }

            

            //if (_db.Users.Any(u=> u.Email == objLogin.Email && u.Password == objLogin.Password)) 
            //{ return RedirectToAction("MissionGrid", "Home"); }
            return View();

          
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User objUser)
        {

            if (objUser.Password == objUser.ConfirmPassword && objUser.FirstName != null && objUser.Email != null && objUser.PhoneNumber != 0)
            {
               var isValid = _AccountRepo.Register(objUser);
                {
                    if (isValid == true)
                    {
                        return RedirectToAction("Login", "User");

                    }
                    return RedirectToAction("Register", "User");

                }
            }
            
            return RedirectToAction("Register", "User");
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
        public IActionResult ValidateForgotDetails(ForgotPassword objForgotPass)
        {
            if (_AccountRepo.IsEmailAvailable(objForgotPass.email))
            {
                try
                {
                    long UserId = _AccountRepo.GetUserID(objForgotPass.email);
                    string welcomeMessage = "Welcome to CI platform, <br/> You can Reset your password using below link. <br/>";
                    // string path = "<a href=\"" + " https://" + _httpContextAccessor.HttpContext.Request.Host.Value + "/Account/Reset_Password/" + UserId.ToString() + " \"  style=\"font-weight:500;color:blue;\" > Reset Password </a>";
                    string path = "<a href=\"https://" + _httpContextAccessor.HttpContext.Request.Host.Value + "/User/ResetPassword/" + UserId.ToString() + "\"> Reset Password </a>";
                    MailHelper mailHelper = new MailHelper(configuration);
                    ViewBag.sendMail = mailHelper.Send(objForgotPass.email, welcomeMessage + path);
                    ModelState.Clear();
                    return RedirectToAction("Login", new { UserId = UserId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            else
            {
                ModelState.AddModelError("email", "Plase Enter Register Email Address..");
                ViewBag.isForgetPasswordOpen = true;
                return View("ForgotPassword");
            }
            return View("Login");
        }
        [HttpGet]
        public IActionResult ResetPassword(long id)
        {
            Reset_Password model = new Reset_Password();
            model.UserId = id;
            return View(model);
        }

        [HttpPost]
        public IActionResult ResetPassword(Reset_Password model, long id)
        {
            if (ModelState.IsValid)
            {

                if (_AccountRepo.ChangePassword(id, model))
                {
                    ModelState.Clear();
                    return RedirectToAction("Login", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Enter Same Password");
                }
            }

            return View();
        }



        //[HttpPost]
        //public IActionResult ForgotPassword(User objPass)
        //{
        //    var objUser = _AccountRepo.UserList().Exists;
        //    //if (_db.Users.Any(u => u.Email == objPass.Email))
        //    //{ return RedirectToAction("ResetPassword", "User"); }
        //    return View();
        //}

        //public IActionResult ResetPassword()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult ResetPassword(User objResetPass)
        //{
        //    User user = new User();
        //    user = objResetPass;

        //    //if (objResetPass.Password == objResetPass.ConfirmPassword)
        //    //{
        //    //    _db.Users.Add(objResetPass);
        //    //    _db.SaveChanges();
        //    //    return RedirectToAction("MissionGrid", "Home");
        //    //}
        //    return RedirectToAction("Login", "User");


        //}

       

    }
}
