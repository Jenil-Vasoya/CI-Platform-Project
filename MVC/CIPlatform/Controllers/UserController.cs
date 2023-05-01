
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
using System.Drawing;
using System.Web.Helpers;
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



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            ViewBag.Banner = _UserRepo.BannerList();
            return View();
        }

        public IActionResult VolunteerTimeSheet()
        {
            long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? ""));
            if(UserId != 0)
            {
            ViewBag.UserName = _UserRepo.GetUserAvatar(UserId).FirstName + " " + _UserRepo.GetUserAvatar(UserId).LastName;
            ViewBag.Avatar = _UserRepo.GetUserAvatar(UserId).Avatar;

            ViewBag.MissionList = _UserRepo.UserAppliedMissionList(UserId);
            List<VolunteerTimeSheet> sheets = _UserRepo.GetVolunteerSheetData(UserId);
            ViewBag.SheetData = sheets;
            return View();

            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public IActionResult AddTimeSheet(VolunteerTimeSheet volunteerSheet)
        {

            long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? ""));
            if (UserId != 0)
            {
                volunteerSheet.UserId = UserId;
                var result = _UserRepo.AddTimeSheet(volunteerSheet);

                if (result)
                {
                    TempData["SheetSuccess"] = "Your sheet added successfully🙍";
                }
                return RedirectToAction("VolunteerTimeSheet");
            }
            else
            {
                return RedirectToAction("Login");
            }
        } 
        
        public IActionResult EditTimeSheet(VolunteerTimeSheet volunteerSheet)
        {
            long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? ""));
            if (UserId != 0)
            {
                volunteerSheet.UserId = UserId;
                var result = _UserRepo.EditTimeSheet(volunteerSheet);

                if (result)
                {
                    TempData["SheetSuccess"] = "Your sheet update successfully🧖";
                }
                return RedirectToAction("VolunteerTimeSheet");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        
        public IActionResult DeleteTimeSheet(long id)
        {
            long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? ""));
            if (UserId != 0)
            {
                var result = _UserRepo.DeleteTimeSheet(id);

                if (result)
                {
                    TempData["SheetDelete"] = "Your TimeSheet delete successfully🤦";
                }
                return RedirectToAction("VolunteerTimeSheet");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }


        [HttpPost]
        public IActionResult Login(User objLogin)
        {
                ViewBag.Banner = _UserRepo.BannerList();
            try
            {
                if (objLogin.Email != null && objLogin.Password != null)
                {

                    var objUser = _UserRepo.UserList().FirstOrDefault(u => u.Email == objLogin.Email);


                    if (_UserRepo.UserList().Any(u => u.Email == objLogin.Email.ToLower()))
                    {
                        if (Crypto.VerifyHashedPassword(objUser.Password, objLogin.Password))
                        {

                            HttpContext.Session.SetString("UserId", JsonConvert.SerializeObject(objUser.UserId.ToString()));
                            HttpContext.Session.SetString("Email", JsonConvert.SerializeObject(objUser.Email.ToString()));
                            HttpContext.Session.SetString("UserName", JsonConvert.SerializeObject(objUser.FirstName.ToString() + " " + objUser.LastName.ToString()));

                            if (objUser.CountryId != null)
                            {

                                HttpContext.Session.SetInt32("userid", (int)objUser.UserId);
                                HttpContext.Session.SetString("FirstName", objUser.FirstName + " " + objUser.LastName);
                                HttpContext.Session.SetString("Country", objLogin.CountryId.ToString());
                                HttpContext.Session.SetString("Role", objUser.Role.ToString());


                                TempData["Success"] = "Login Successfully";
                                return RedirectToAction("MissionGrid", "Home");
                            }
                            else
                            {
                                HttpContext.Session.SetInt32("userid", (int)objUser.UserId);
                                HttpContext.Session.SetString("FirstName", objUser.FirstName + " " + objUser.LastName);

                                return RedirectToAction("EditProfile", "User");
                            }
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
            catch (Exception ex)
            {
                TempData["Fail"] = ex.Message;
                return View();
            }

        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "User");
        }

        public IActionResult Register()
        {
            ViewBag.Banner = _UserRepo.BannerList();
            return View();
        }


        [HttpPost]
        public IActionResult Register(User objUser)
        {
            try
            {
                ViewBag.Banner = _UserRepo.BannerList();
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
            catch (Exception e)
            {
                TempData["RegisterFail"] = e.Message;
                return View();
            }
        }


        public IActionResult ForgotPassword()
        {
            ViewBag.Banner = _UserRepo.BannerList();
            ViewBag.sessionV = HttpContext.Session.GetString("UserId");
            ViewBag.session = HttpContext.Session.GetString("UserName");
            return View();
        }


        [HttpPost]
        public IActionResult ForgotPassword(ForgotPassword objForgotPass)
        {
            if(objForgotPass.email == null)
            {
                TempData["InvalidEmail"] = "Please enter registered Email";
                return View();
            }

            try
            {
                if (_UserRepo.IsEmailAvailable(objForgotPass.email))
                {
                    try
                    {
                        long UserId = _UserRepo.GetUserID(objForgotPass.email);
                        string welcomeMessage = "Welcome to CI platform, <br/> You can Reset your password using below link. <br/>";
                        // string path = "<a href=\"" + " https://" + _httpContextAccessor.HttpContext.Request.Host.Value + "/Account/Reset_Password/" + UserId.ToString() + " \"  style=\"font-weight:500;color:blue;\" > Reset Password </a>";
                        string path = "<a href=\"https://" + _httpContextAccessor.HttpContext.Request.Host.Value + "/User/ResetPassword/" + UserId.ToString() + "\"> Reset Password </a>";
                        MailHelper mailHelper = new MailHelper(configuration);
                        ModelState.Clear();
                        ViewBag.sendMail = mailHelper.Send(objForgotPass.email, welcomeMessage + path);
                        TempData["LinkSent"] = "ResetPassword link is sent on your registered email";
                        return RedirectToAction("Login", new { UserId = UserId });
                    }
                    catch (Exception ex)
                    {
                        TempData["InvalidEmail"] = ex.Message;
                        return View();
                    }
                }
                else
                {
                    TempData["InvalidEmail"] = "This email in not registered";
                    return View();
                }
            }
            catch  (Exception ex)
            {
                TempData["InvalidEmail"] = ex.Message;
                return View();
            }
            return View();
            
        }


        [HttpGet]
        public IActionResult ResetPassword(long id)
        {
            ViewBag.Banner = _UserRepo.BannerList();
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
            try
            {
                long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? ""));

                if (UserId != 0)
                {

                    UserData model = _UserRepo.GetUserlist(UserId);
                    ViewBag.Email = _UserRepo.GetUserAvatar(UserId).Email;
                    ViewBag.UserName = _UserRepo.GetUserAvatar(UserId).FirstName + " " + _UserRepo.GetUserAvatar(UserId).LastName;
                    ViewBag.Avatar = _UserRepo.GetUserAvatar(UserId).Avatar;

                    return View(model);
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            catch  (Exception ex)
            {
                TempData["Fail"] = ex.Message;
                return RedirectToAction("Login");
            }
        }


        [HttpPost]
        public IActionResult EditProfile(UserData userData)
        {
            try
            {
                long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? ""));

                if (UserId != 0)
                {
                    UserData model1 = _UserRepo.GetUserlist(UserId);
                    if (userData.FirstName == model1.FirstName && userData.LastName == model1.LastName && userData.EmployeeId == model1.EmployeeId && userData.Title == model1.Title && userData.Department == model1.Department && userData.WhyIvolunteer == model1.WhyIvolunteer && userData.CityId == model1.CityId && userData.CountryId == model1.CountryId && userData.LinkedInUrl == model1.LinkedInUrl && userData.ProfileText == model1.ProfileText)
                    {
                        TempData["EditFail"] = "Please change the profile details for update details";

                        ViewBag.Email = JsonConvert.DeserializeObject(HttpContext.Session.GetString("Email") ?? "");
                        ViewBag.UserName = model1.FirstName + " " + model1.LastName;
                        ViewBag.Avatar = model1.Avatar;
                        return View(model1);
                    }
                    bool result = _UserRepo.EditProfile(userData, UserId);

                    UserData model = _UserRepo.GetUserlist(UserId);
                    if (result)
                    {
                        TempData["EditSuccess"] = "Your profile was updated";
                    }
                    else
                    {
                        TempData["EditFail"] = "Your profile was not updated";
                    }

                    ViewBag.Email = JsonConvert.DeserializeObject(HttpContext.Session.GetString("Email") ?? "");
                    ViewBag.UserName = _UserRepo.GetUserAvatar(UserId).FirstName + " " + _UserRepo.GetUserAvatar(UserId).LastName;
                    ViewBag.Avatar = _UserRepo.GetUserAvatar(UserId).Avatar;
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            catch (Exception ex)
            {
                TempData["EditFail"] =  ex.Message;
                return View();
            }
        }


        public JsonResult GetCity(long countryId)
            {
            List<City> city = _UserRepo.CityList(countryId);
            var json = JsonConvert.SerializeObject(city);


            return Json(json);
        }


        [HttpPost]
        public JsonResult ChangePassword(string OldPassword, string Password)
        {
            long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? ""));

            bool result = _UserRepo.ChangePasswordUser(UserId, OldPassword, Password);

            if (result == true)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }


        [HttpPost]
        public JsonResult ChangeSkill(List<long> skillIDs)
        {
            long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? ""));

            bool result = _UserRepo.ChangeSkills(skillIDs, UserId);
            return Json(result);
        }

       
        [HttpPost]
        public JsonResult EditAvatar(IFormFile Profileimg)
        {
            long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? ""));

            var changeimage = _UserRepo.EditAvatar(Profileimg, UserId);
            return Json(changeimage);
        }


        [HttpPost]
        public JsonResult AddMessage(ContactU model)
        {
            long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? ""));
            model.UserId = UserId;
            bool result = _UserRepo.AddContactUs(model);

            return Json(result);
        }

    }
}