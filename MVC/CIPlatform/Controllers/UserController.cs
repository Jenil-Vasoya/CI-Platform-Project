
using CIPlatform.Entities.Models;
using CIPlatform.Entities.ViewModel;
using CIPlatform.Repository.Interface;
using CIPlatform.Views.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using SendGrid.Helpers.Mail;
using System.Data;
using MailHelper = CIPlatform.Entities.ViewModel.MailHelper;

namespace CIPlatform.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _UserRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration configuration;

        public UserController(IUserRepository UserRepo, IHttpContextAccessor httpContextAccessor, IConfiguration _configuration)
        {
            _UserRepo = UserRepo;
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
            
            if (objLogin.Email != null && objLogin.Password != null)
            {

                var objUser = _UserRepo.UserList().FirstOrDefault(u => u.Email == objLogin.Email && u.Password == objLogin.Password);

                if (_UserRepo.UserList().Any(u => u.Email == objLogin.Email))
                {
                    if(_UserRepo.UserList().Any(u => u.Password == objLogin.Password))
                    { 
                   
                        HttpContext.Session.SetString("UserId", JsonConvert.SerializeObject(objUser.UserId.ToString()));
                        HttpContext.Session.SetString("Email", JsonConvert.SerializeObject(objUser.Email.ToString()));
                        HttpContext.Session.SetString("UserName", JsonConvert.SerializeObject(objUser.FirstName.ToString() + " " + objUser.LastName.ToString()));
                        HttpContext.Session.SetString("Avatar", JsonConvert.SerializeObject(objUser.Avatar.ToString()));




                        TempData["Success"] = "Login Successfully";
                        return RedirectToAction("MissionGrid", "Home");

                    }
                    TempData["Fail"] = "Please enter correct password";
                    return View();
                }
                TempData["Fail"] = "Don't have any account please register your account";
                return View();

            }
            else
            {
               
                return View();
            }

        }

        public IActionResult LogOut()
        {
           
            return RedirectToAction("Login", "User");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User objUser)
        {

            if ((objUser.Password == objUser.ConfirmPassword) && objUser.FirstName != null && objUser.Email != null && objUser.Password != null)
            {
                if (_UserRepo.UserList().Any(u => u.Email == objUser.Email) == false)
                {

                    var isValid = _UserRepo.Register(objUser);
                    {
                        if (isValid == true)
                        {
                            TempData["RegisterSuccess"] = "Account Created Successfully";
                            return RedirectToAction("Login", "User");

                        }
                        TempData["RegisterFail"] = "Registarion is fail";
                        return View();

                    }
                }
                TempData["RegisterFail"] = "This email is already registered";
                return View();

            }
            TempData["RegisterFail"] = "Registarion is fail";
            return View();
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
            ViewBag.sessionV = HttpContext.Session.GetString("UserId");
            ViewBag.session = HttpContext.Session.GetString("UserName");
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(ForgotPassword objForgotPass)
        {
            if(objForgotPass.email == null)
            {
                return View();
            }

            if (_UserRepo.IsEmailAvailable(objForgotPass.email))
            {
                try
                {
                    long UserId = _UserRepo.GetUserID(objForgotPass.email);
                    string welcomeMessage = "Welcome to CI platform, <br/> You can Reset your password using below link. <br/>";
                    // string path = "<a href=\"" + " https://" + _httpContextAccessor.HttpContext.Request.Host.Value + "/Account/Reset_Password/" + UserId.ToString() + " \"  style=\"font-weight:500;color:blue;\" > Reset Password </a>";
                    string path = "<a href=\"https://" + _httpContextAccessor.HttpContext.Request.Host.Value + "/User/ResetPassword/" + UserId.ToString() + "\"> Reset Password </a>";
                    MailHelper mailHelper = new MailHelper(configuration);
                    ViewBag.sendMail = mailHelper.Send(objForgotPass.email, welcomeMessage + path);
                    ModelState.Clear();
                    TempData["LinkSent"] = "ResetPassword link is sent on your registered email";
                    return RedirectToAction("Login", new { UserId = UserId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            else
            {
                TempData["InvalidEmail"] = "This email in not registered";
                return View();
            }
            return View();
            
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

                if (_UserRepo.ChangePassword(id, model))
                {
                    ModelState.Clear();
                    TempData["ResetSuccess"] = "Your password has been updated";
                    return RedirectToAction("Login", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Enter Same Password");
                }
            }

            return View();
        }

        public IActionResult EditProfile(long id)
        {
            ViewBag.Uid = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? ""));
            ViewBag.Email = JsonConvert.DeserializeObject(HttpContext.Session.GetString("Email"));
            ViewBag.UserName = JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserName"));
            ViewBag.Avatar = JsonConvert.DeserializeObject(HttpContext.Session.GetString("Avatar"));
          

            UserData userEditProfile = new UserData();
            {
                userEditProfile.users = _UserRepo.GetUserlist(id);
                userEditProfile.country = _UserRepo.GetCountryList();
                userEditProfile.city = _UserRepo.GetCities();
                userEditProfile.skill = _UserRepo.GetSkills();
            }

            return View(userEditProfile);
        }

        public JsonResult GetCity(long countryId)
        {
            List<City> city = _UserRepo.CityList(countryId);
            var json = JsonConvert.SerializeObject(city);


            return Json(json);
        }

        //[HttpPost]
        //public IActionResult ForgotPassword(User objPass)
        //{
        //    var objUser = _UserRepo.UserList().Exists;
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