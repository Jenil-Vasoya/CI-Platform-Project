using CIPlatform.Entities.Models;
using CIPlatform.Entities.ViewModel;
using CIPlatform.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CIPlatform.Controllers
{

    [Authorize]
    public class StoryController : Controller
    {
        private readonly IStoryRepository _StoryRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public StoryController(IStoryRepository StoryRepo, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IWebHostEnvironment env)
        {
            _StoryRepo = StoryRepo;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _env = env;

        }



        public IActionResult StoryList()
        {
            try
            {
                long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? ""));
                string Role = HttpContext.Session.GetString("Role") ?? "";
                
                    List<MissionData> missionDatas = _StoryRepo.GetStoryCardsList(UserId,Role);
                    ViewBag.missionDatas = missionDatas;

                    ViewBag.UserId = JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? "");
                    ViewBag.Email = JsonConvert.DeserializeObject(HttpContext.Session.GetString("Email") ?? "");
                    ViewBag.UserName = _StoryRepo.GetUserAvatar(UserId).FirstName + " " + _StoryRepo.GetUserAvatar(UserId).LastName;
                    ViewBag.Avatar = _StoryRepo.GetUserAvatar(UserId).Avatar;

                    List<Country> countries = _StoryRepo.CountryList();
                    ViewBag.countries = countries;

                    List<MissionTheme> themes = _StoryRepo.MissionThemeList();
                    ViewBag.themes = themes;

                    List<Skill> skills = _StoryRepo.SkillList();
                    ViewBag.skills = skills;

                    ViewBag.Totalpages = Math.Ceiling(missionDatas.Count() / 6.0);
                    ViewBag.missionDatas = missionDatas.Skip((1 - 1) * 6).Take(6).ToList();
                    ViewBag.pg_no = 1;

                    return View();
                
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("MissionGrid", "Home");
            }
        }


        [HttpPost]
        public ActionResult SearchStory(string? search, string[] countries, string[] cities, string[] themes, string[] skills, int pg)
        {
            try
            {
                long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? ""));
                string Role = HttpContext.Session.GetString("Role") ?? "";
               
                    search = string.IsNullOrEmpty(search) ? "" : search.ToLower();
                    List<MissionData> missionDatas = _StoryRepo.GetStoryMissionList(search, countries, cities, themes, skills, pg, UserId, Role);

                    ViewBag.missionDatas = missionDatas;

                    ViewBag.UserId = UserId;
                    ViewBag.pg_no = pg;
                    ViewBag.Totalpages = Math.Ceiling(_StoryRepo.GetStoryMissionList(search, countries, cities, themes, skills, pg = 0, UserId,Role).Count() / 6.0);
                    ViewBag.missionDatas = missionDatas.Skip((1 - 1) * 6).Take(6).ToList();

                    return PartialView("_StoryList");
               
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return PartialView("_StoryList");
            }
        }


        public IActionResult StoryDetail(long id, long? NotificationId)
        {
            try
            {
                long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? ""));
                string Role = HttpContext.Session.GetString("Role")?? "";
                if (UserId > 0)
                {
                    List<User> users = _StoryRepo.UserList();
                    ViewBag.Users = users;

                    _StoryRepo.StoryView(id, UserId);
                    if (NotificationId != null)
                    {
                        var notification = _StoryRepo.UpdateNotification(NotificationId);
                    }

                    List<MissionData> missionDatas = _StoryRepo.GetStoryCardsList(UserId,Role);

                    var missions = missionDatas.Where(x => x.StoryId == id).FirstOrDefault();
                    ViewBag.missionDatas = missions;


                    ViewBag.UserId = UserId;
                    ViewBag.Email = JsonConvert.DeserializeObject(HttpContext.Session.GetString("Email") ?? "");
                    ViewBag.UserName = _StoryRepo.GetUserAvatar(UserId).FirstName + " " + _StoryRepo.GetUserAvatar(UserId).LastName;
                    ViewBag.Avatar = _StoryRepo.GetUserAvatar(UserId).Avatar;
                    
                    return View();
                }
                else
                {
                        return RedirectToAction("Login", "User", new { returnUrl = $"Story/StoryDetail/{id}" });
                    
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View();
            }
        }

        
        public IActionResult AddStory(long id)
        {
            try
            {
                long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? ""));
                string Role = HttpContext.Session.GetString("Role") ?? "";
                
                    List<MissionData> missionDatas = _StoryRepo.GetStoryCardsList(UserId,Role);

                    var missions = missionDatas.Where(x => x.StoryId == id).FirstOrDefault();
                    ViewBag.missionDatas = missions;

                    ViewBag.MissionData = _StoryRepo.UserAppliedMissionList(UserId);

                    ViewBag.UserId = UserId;
                    ViewBag.Email = JsonConvert.DeserializeObject(HttpContext.Session.GetString("Email") ?? "");
                    ViewBag.UserName = _StoryRepo.GetUserAvatar(UserId).FirstName + " " + _StoryRepo.GetUserAvatar(UserId).LastName;
                    ViewBag.Avatar = _StoryRepo.GetUserAvatar(UserId).Avatar;

                    return View(missions);
                
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View();
            }
        }


        [HttpPost]
        public IActionResult AddStory(MissionData objStory , string? submit)
        {
            try
            {
                long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? ""));
                string Role = HttpContext.Session.GetString("Role") ?? "";
                   if (ModelState.IsValid)
                    {
                        string btn = "";
                        long StoryId = 0;

                        if (submit != null)
                        {
                            btn = submit;
                            TempData["SubmitStory"] = "Story Sent for Approval";
                        }
                        else
                        {
                            TempData["SubmitStory"] = "Draft Story Saved";
                        }
                        if (objStory.MissionId != 0)
                        {
                            StoryId = _StoryRepo.AddData(objStory, UserId, btn);
                        }

                        if (objStory.images != null)
                        {
                            var path = new List<string>();
                            foreach (var i in objStory.images)
                            {
                                StoryMedium story = new StoryMedium();

                                string path1 = i.FileName;

                                path.Add(path1);

                            }
                            objStory.StoryImages = path;
                        }

                            var missions = objStory;
                        

                        List<MissionData> missionDatas = _StoryRepo.GetStoryCardsList(UserId,Role);

                        var storymission = missionDatas.Where(x => x.StoryId == (objStory.StoryId ?? StoryId)).FirstOrDefault();
                        ViewBag.missionDatas = storymission;

                        ViewBag.UserId = UserId;
                        ViewBag.Email = JsonConvert.DeserializeObject(HttpContext.Session.GetString("Email") ?? "");
                        ViewBag.UserName = _StoryRepo.GetUserAvatar(UserId).FirstName + " " + _StoryRepo.GetUserAvatar(UserId).LastName;
                        ViewBag.Avatar = _StoryRepo.GetUserAvatar(UserId).Avatar;

                        ViewBag.MissionData = _StoryRepo.UserAppliedMissionList(UserId);
                  
                            return RedirectToAction("StoryList");
                   
                    }
                    else
                    {
                        ViewBag.UserId = UserId;
                        ViewBag.Email = JsonConvert.DeserializeObject(HttpContext.Session.GetString("Email") ?? "");
                        ViewBag.UserName = _StoryRepo.GetUserAvatar(UserId).FirstName + " " + _StoryRepo.GetUserAvatar(UserId).LastName;
                        ViewBag.Avatar = _StoryRepo.GetUserAvatar(UserId).Avatar;

                        ViewBag.MissionData = _StoryRepo.UserAppliedMissionList(UserId);
                        return View();
                    }
               
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View();
            }
        }


        [HttpPost]
        public JsonResult InviteWorker(long StoryId, List<long> CoWorkers)
        {
            long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? ""));

            bool mailSent = _StoryRepo.InviteWorker(CoWorkers, UserId, StoryId);
            if (mailSent == true)
            {
                return Json(mailSent);
            }

            return Json(null);
        }
        


    }
}
