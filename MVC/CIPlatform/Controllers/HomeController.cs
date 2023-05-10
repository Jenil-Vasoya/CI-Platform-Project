
using CIPlatform.Data;
using CIPlatform.Entities.Data;
using CIPlatform.Entities.Models;
using CIPlatform.Entities.ViewModel;
using CIPlatform.Repository.Interface;
using CIPlatform.Repository.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;

namespace CIPlatform.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IHomeRepository _HomeRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration configuration;
        public readonly CIPlatformDbContext _DbContext;


        public HomeController(IHomeRepository HomeRepo, IHttpContextAccessor httpContextAccessor, IConfiguration _configuration, CIPlatformDbContext DbContext)
        {
            _HomeRepo = HomeRepo;
            _httpContextAccessor = httpContextAccessor;
            configuration = _configuration;
            _DbContext = DbContext;
        }



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddStory()
        {
            return View();
        }

        public IActionResult Policy()
        {
            long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? ""));

            //if (UserId > 0)
            //{
            ViewBag.CMSList = _HomeRepo.getCMS();
            if (UserId > 0)
            {
                ViewBag.UserName = _HomeRepo.GetUserAvatar(UserId).FirstName + " " + _HomeRepo.GetUserAvatar(UserId).LastName;
                ViewBag.Avatar = _HomeRepo.GetUserAvatar(UserId).Avatar;
            }
            return View();
            //}
            //else
            //{
            //    return RedirectToAction("Login","User");
            //}
        }

        public IActionResult Header()
        {
            return View();
        }

        public IActionResult Header1()
        {
            return View();
        }


        [HttpGet]
        public IActionResult MissionGrid()
        {
            try
            {
                long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? ""));

                
                    List<Mission> missions = _HomeRepo.MissionList();
                    if (missions.Count <= 0)
                    {
                        return RedirectToAction("MissionNotFound");
                    }

                    List<MissionData> missionDatas = _HomeRepo.GetMissionCardsList(UserId);


                    ViewBag.UserId = UserId;
                    ViewBag.Email = JsonConvert.DeserializeObject(HttpContext.Session.GetString("Email") ?? "");
                    ViewBag.UserName = _HomeRepo.GetUserAvatar(UserId).FirstName + " " + _HomeRepo.GetUserAvatar(UserId).LastName;
                    ViewBag.Avatar = _HomeRepo.GetUserAvatar(UserId).Avatar;

                    List<User> users = _HomeRepo.UserList();
                    ViewBag.Users = users;

                    //List<MissionInvite> missionInvites = _HomeRepo.InvitedUserList(UserId);
                    //ViewBag.Invites = missionInvites;

                    List<Country> countries = _HomeRepo.CountryList();
                    ViewBag.countries = countries;

                    List<MissionTheme> themes = _HomeRepo.MissionThemeList();
                    ViewBag.themes = themes;

                    List<Skill> skills = _HomeRepo.SkillList();
                    ViewBag.skills = skills;

                    var totalMission = _HomeRepo.TotalMissions();
                    ViewBag.totalMission = totalMission;

                    ViewBag.NotificationSetting = _HomeRepo.GetNotificationSetting(UserId) ?? null;
                    //ViewBag.NotificationCount = _HomeRepo.GetNotifications(UserId).Where(n=> n.Status == "Unseen").Count();
                    ViewBag.Notifications = _HomeRepo.GetNotifications(UserId).Where(n=> n.MissionId != null && (n.Text.Contains("Approved Mission") || n.Text.Contains("Declined Mission")));
                    ViewBag.Story = _HomeRepo.GetNotifications(UserId).Where(n=> n.StoryId != null && (n.Text.Contains("Published Story") || n.Text.Contains("Declined Story"))) ?? null;
                    ViewBag.NewMissions = _HomeRepo.GetNotifications(UserId).Where(n=> n.MissionId == null && n.StoryId == null) ?? null;
                    ViewBag.RecommandedMissions = _HomeRepo.GetNotifications(UserId).Where(n=> n.MissionId != null && n.Text.Contains("Recommanded")) ?? null;
                    ViewBag.RecommandedStory = _HomeRepo.GetNotifications(UserId).Where(n=> n.StoryId != null && n.Text.Contains("Recommanded")) ?? null;

                    ViewBag.Totalpages = Math.Ceiling(missionDatas.Count() / 6.0);
                    ViewBag.missionDatas = missionDatas.Skip((1 - 1) * 6).Take(6).ToList();
                    ViewBag.pg_no = 1;



                    return View();
                
            }
            catch (Exception ex)
            {
                TempData["Fail"] = ex.Message;
                return RedirectToAction("Login", "User");
            }
        }


        public JsonResult GetCity(List<string> countryId)
        {
            List<City> city = _HomeRepo.CityList(countryId);
            var json = JsonConvert.SerializeObject(city);


            return Json(json);
        }



        public ActionResult SearchList(string? search, string[] countries, string[] cities, string[] themes, string[] skills, int sort, int pg)
        {
            try
            {
                long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? ""));

                
                    search = string.IsNullOrEmpty(search) ? "" : search.ToLower();
                    List<MissionData> missionDatas = _HomeRepo.GetMissionList(search, countries, cities, themes, skills, sort, UserId, pg);

                    ViewBag.missionDatas = missionDatas;

                    var totalMission = missionDatas.Count.ToString();
                    ViewBag.totalMission = totalMission;

                    List<User> users = _HomeRepo.UserList();
                    ViewBag.Users = users;

                    ViewBag.UserId = UserId;
                    ViewBag.pg_no = pg;
                    ViewBag.Totalpages = Math.Ceiling(_HomeRepo.GetMissionList(search, countries, cities, themes, skills, sort, UserId, pg = 0).Count() / 6.0);
                    ViewBag.missionDatas = missionDatas.Skip((1 - 1) * 6).Take(6).ToList();


                    if (totalMission != "0")
                    {
                        return PartialView("_MissionList");
                    }
                    else
                    {
                        return PartialView("_MissionEmpty");
                    }
                
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return PartialView("_MissionList");
            }
        }


        public IActionResult MissionEmpty()
        {
            return View();
        }

            
        public IActionResult VolunteerMission(long id, long? NotificationId)
            {
            try
            {
                long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? ""));


                if (UserId > 0)
                {
                    if(NotificationId != null)
                    {
                        var notification = _HomeRepo.UpdateNotification(NotificationId);
                    }
                    List<User> users = _HomeRepo.UserList();
                    ViewBag.Users = users;


                    List<MissionData> missionDatas = _HomeRepo.GetMissionCardsList(UserId);

                    var missions = missionDatas.Where(x => x.MissionId == id).FirstOrDefault();
                    ViewBag.missionDatas = missions;

                    if (missions != null)
                    {

                        var relatedmission = missionDatas.Where(x => (x.Theme == missions.Theme || x.CityName == missions.CityName || x.MissionType == missions.MissionType) && x.MissionId != id).Take(3).ToList();
                        ViewBag.total = relatedmission.Count;
                        ViewBag.relatedmission = relatedmission;

                        ViewBag.Comments = _HomeRepo.GetComment(missions.MissionId);
                        ViewBag.CheckFavMisson = _HomeRepo.CheckFavMission(UserId, missions.MissionId);


                        var recentVolunteer = _HomeRepo.GetRecentVolunteer(missions.MissionId);
                        ViewBag.RecentVolunteer = recentVolunteer;


                        ViewBag.Totalpages = Math.Ceiling(recentVolunteer.Count() / 3.0);
                        ViewBag.RecentVolunteer = recentVolunteer.Skip((1 - 1) * 3).Take(3).ToList();
                        ViewBag.pg_no = 1;

                    }
                    ViewBag.UserId = UserId;
                    ViewBag.Email = JsonConvert.DeserializeObject(HttpContext.Session.GetString("Email") ?? "");
                    ViewBag.UserName = _HomeRepo.GetUserAvatar(UserId).FirstName + " " + _HomeRepo.GetUserAvatar(UserId).LastName;
                    ViewBag.Avatar = _HomeRepo.GetUserAvatar(UserId).Avatar;

                    return View();
                }
                else
                {

                    return RedirectToAction("Login", "User", new { returnUrl = $"Home/VolunteerMission/{id}" });



                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View("MissionGrid");
            }
        }

        public ActionResult Volunteer(long MissionId, int pg)
        {
            long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? ""));
            
                List<RecentVolunteer> missionDatas = _HomeRepo.RecentVolunteer(MissionId, pg);
                ViewBag.RecentVolunteer = missionDatas;

                ViewBag.pg_no = pg;
                ViewBag.Totalpages = Math.Ceiling(_HomeRepo.RecentVolunteer(MissionId, pg = 0).Count() / 3.0);
                ViewBag.RecentVolunteer = missionDatas.Skip((1 - 1) * 3).Take(3).ToList();

                return PartialView("_RecentVolunteer");
            
        }

        [HttpPost]
        public ActionResult Search(string? search, string[] countries, string[] cities, string[] themes, string[] skills, int sort, int pg)
        {
            try
            {
                long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? ""));

                if (UserId > 0)
                {
                    search = string.IsNullOrEmpty(search) ? "" : search.ToLower();
                    List<MissionData> missionDatas = _HomeRepo.GetMissionList(search, countries, cities, themes, skills, sort, UserId, pg);

                    ViewBag.missionDatas = missionDatas;

                    var totalMission = missionDatas.Count.ToString();
                    ViewBag.totalMission = totalMission;

                    List<User> users = _HomeRepo.UserList();
                    ViewBag.Users = users;

                    ViewBag.UserId = UserId;
                    ViewBag.pg_no = pg;
                    ViewBag.Totalpages = Math.Ceiling(_HomeRepo.GetMissionList(search, countries, cities, themes, skills, sort, UserId, pg = 0).Count() / 6.0);
                    ViewBag.missionDatas = missionDatas.Skip((1 - 1) * 6).Take(6).ToList();

                    if (totalMission != "0")
                    {
                        return PartialView("_MissionGrid");
                    }
                    else
                    {
                        return PartialView("_MissionEmpty");
                    }
                }
                else
                {
                    return RedirectToAction("Login", "User");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return PartialView("_MissionGrid");
            }
        }


        public JsonResult AddToFavourite(long missionId)
        {
            long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? ""));

            bool mission = _HomeRepo.AddFavouriteMission(UserId, missionId);
            if (mission == true)
            {
                return Json(mission);
            }

            return Json(null);
        }

        public void AddComment(string comment, long MissionId)
        {
            long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? ""));


            _HomeRepo.AddComment(comment, UserId, MissionId);

        }

        public JsonResult ApplyMission(long missionId)
        {
            long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? ""));

            int success = _HomeRepo.ApplyMission(UserId, missionId);
            return Json(success);

        }


        public JsonResult InviteWorker(long MissionId, List<long> CoWorkers)
        {
            long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? ""));

            bool mailSent = _HomeRepo.InviteWorker(CoWorkers, UserId, MissionId);
            if (mailSent == true)
            {
                return Json(mailSent);
            }

            return Json(null);
        }

        public int PostRating(byte rate, long missionId)
        {
            long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? ""));


            int UserValid = _HomeRepo.ApplyMissionCheck(UserId, missionId);

            if (UserValid == 1)
            {
                bool result = _HomeRepo.PostRating(rate, missionId, UserId);

                return 1;
            }
            else if (UserValid == 2)
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }

        public bool CheckUser(long userId, long missionId)
        {
            long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? ""));

            var invited = _HomeRepo.CheckUser(userId, UserId, missionId);

            return invited;

        }

        public ActionResult ClearNotification()
        {
            long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? ""));
            bool result = _HomeRepo.ClearNotification(UserId);

            return PartialView("_Notification");
        }
        
        public ActionResult UpdateSetting(List<string> type)
        {
            long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? ""));
            bool result = _HomeRepo.UpdateSetting(type,UserId);
            ViewBag.NotificationSetting = _HomeRepo.GetNotificationSetting(UserId) ?? null;
            ViewBag.Notifications = _HomeRepo.GetNotifications(UserId).Where(n => n.MissionId != null && (n.Text.Contains("Approved Mission") || n.Text.Contains("Declined Mission")));
            return PartialView("_NotificationSetting");
        }

    }
}