using CIPlatform.Data;
using CIPlatform.Entities.Models;
using CIPlatform.Entities.ViewModel;
using CIPlatform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection.PortableExecutable;

namespace CIPlatform.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAccountRepository _AccountRepo;
        private readonly IHomeRepository _HomeRepo;

        public AdminController(IAccountRepository AccountRepo, IHomeRepository HomeRepo)
        {
            _AccountRepo = AccountRepo;
            _HomeRepo = HomeRepo;

        }

        public IActionResult Index()
        {
            //var model = _AccountRepo.adminModelList();
           
             var role =  HttpContext.Session.GetString("Role");
            try
            {
                if (role != null && role.Trim() == "Admin")
                {

                    long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? ""));

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

                    ViewBag.BannerList = _AccountRepo.BannerList().Skip((1 - 1) * 6).Take(6).ToList();
                    ViewBag.ttlBanners = Math.Ceiling(_AccountRepo.BannerList().Count() / 6.0);

                    ViewBag.CommentList = _AccountRepo.CommentList().Skip((1 - 1) * 6).Take(6).ToList();
                    ViewBag.ttlComments = Math.Ceiling(_AccountRepo.CommentList().Count() / 6.0);

                    ViewBag.CountryList = _AccountRepo.CountryList();
                    ViewBag.Themes = _AccountRepo.ThemeList();
                    ViewBag.Skills = _AccountRepo.SkillList();

                    ViewBag.UserName = _HomeRepo.GetUserAvatar(UserId).FirstName + " " + _HomeRepo.GetUserAvatar(UserId).LastName;
                    ViewBag.Avatar = _HomeRepo.GetUserAvatar(UserId).Avatar;

                    ViewBag.missionDatas = 1;

                    ViewBag.pg_no = 1;

                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "User");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("MissionGrid","Home");
            }
        }

        public ActionResult Search(string? search, int pg, string who)
        {
            ViewBag.CountryList = _AccountRepo.CountryList();
            ViewBag.Themes = _AccountRepo.ThemeList();
            ViewBag.Skills = _AccountRepo.SkillList();
            if (who == "mission")
            {
                var model = _AccountRepo.MissionListSearch(search, pg);
                ViewBag.CountryList = _AccountRepo.CountryList();
                ViewBag.Skills = _AccountRepo.SkillList();
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
            else if (who == "banner")
            {
                var model = _AccountRepo.BannerListSearch(search, pg);
                ViewBag.BannerList = model;
                ViewBag.pg_no = pg;
                ViewBag.ttlBanners = Math.Ceiling(_AccountRepo.BannerListSearch(search, pg = 0).Count() / 6.0);

                return PartialView("_BannerData");

            }
            else if (who == "comment")
            {
                var model = _AccountRepo.CommentListSearch(search, pg);

                ViewBag.CommentList = model;
                ViewBag.pg_no = pg;
                ViewBag.ttlComments = Math.Ceiling(_AccountRepo.CommentListSearch(search, pg = 0).Count() / 6.0);

                return PartialView("_CommentData");
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

        public JsonResult GetUserData(long UserId)
        {
            var adminModel = _AccountRepo.UserData(UserId);

            return Json(adminModel);
        }

        public ActionResult AddCMS(Cmspage model)
        {
           bool result = _AccountRepo.AddCMS(model);

            ViewBag.CMSList = _AccountRepo.CMSList().Skip((1 - 1) * 6).Take(6).ToList();
            ViewBag.ttlCMS = Math.Ceiling(_AccountRepo.CMSList().Count() / 6.0);
            ViewBag.pg_no = 1;

            return PartialView("_CMSData");

        }

        public JsonResult EditCMS(long CMSId)
        {
            var adminModel = _AccountRepo.EditCMS(CMSId);

            return Json(adminModel);
        }

        public ActionResult DeleteCMS(long CMSId)
        {
            bool result = _AccountRepo.DeleteCMS(CMSId);

            ViewBag.CMSList = _AccountRepo.CMSList().Skip((1 - 1) * 6).Take(6).ToList();
            ViewBag.ttlCMS = Math.Ceiling(_AccountRepo.CMSList().Count() / 6.0);
            ViewBag.pg_no = 1;

            return PartialView("_CMSData");
        } 
        
        public ActionResult AddMission(Mission model)
        {
           bool result = _AccountRepo.AddMission(model);

            ViewBag.Skills = _AccountRepo.SkillList();
            ViewBag.MissionList = _AccountRepo.MissionList().Skip((1 - 1) * 6).Take(6).ToList();
            ViewBag.ttlMissions = Math.Ceiling(_AccountRepo.MissionList().Count() / 6.0);
            ViewBag.pg_no = 1;

            return PartialView("_MissionData");

        }

        public JsonResult EditMission(long MissionId)
        {
            var adminModel = _AccountRepo.EditMission(MissionId);

            return Json(adminModel);
        }

        public ActionResult DeleteMission(long MissionId)
        {
            bool result = _AccountRepo.DeleteMission(MissionId);

            ViewBag.Skills = _AccountRepo.SkillList();
            ViewBag.MissionList = _AccountRepo.MissionList().Skip((1 - 1) * 6).Take(6).ToList();
            ViewBag.ttlMissions = Math.Ceiling(_AccountRepo.MissionList().Count() / 6.0);
            ViewBag.pg_no = 1;

            return PartialView("_MissionData");
        }


        public ActionResult AddTheme(MissionTheme model)
        {
            bool result = _AccountRepo.AddTheme(model);

            ViewBag.ThemeList = _AccountRepo.ThemeList().Skip((1 - 1) * 6).Take(6).ToList();
            ViewBag.ttlThemes = Math.Ceiling(_AccountRepo.ThemeList().Count() / 6.0);
            ViewBag.pg_no = 1;

            return PartialView("_ThemeData");

        }

        public JsonResult EditTheme(long ThemeId)
        {
            var adminModel = _AccountRepo.EditTheme(ThemeId);

            return Json(adminModel);
        }

        public ActionResult DeleteTheme(long ThemeId)
        {
            bool result = _AccountRepo.DeleteTheme(ThemeId);

            ViewBag.ThemeList = _AccountRepo.ThemeList().Skip((1 - 1) * 6).Take(6).ToList();
            ViewBag.ttlThemes = Math.Ceiling(_AccountRepo.ThemeList().Count() / 6.0);
            ViewBag.pg_no = 1;

            return PartialView("_ThemeData");
        }
        
        public ActionResult AddSkill(Skill model)
        {
            bool result = _AccountRepo.AddSkill(model);

            ViewBag.SkillList = _AccountRepo.SkillList().Skip((1 - 1) * 6).Take(6).ToList();
            ViewBag.ttlSkills = Math.Ceiling(_AccountRepo.SkillList().Count() / 6.0);
            ViewBag.pg_no = 1;

            return PartialView("_SkillData");

        }

        public JsonResult EditSkill(long SkillId)
        {
            var adminModel = _AccountRepo.EditSkill(SkillId);

            return Json(adminModel);
        }

        public ActionResult DeleteSkill(long SkillId)
        {
            bool result = _AccountRepo.DeleteSkill(SkillId);

            ViewBag.SkillList = _AccountRepo.SkillList().Skip((1 - 1) * 6).Take(6).ToList();
            ViewBag.ttlSkills = Math.Ceiling(_AccountRepo.SkillList().Count() / 6.0);
            ViewBag.pg_no = 1;

            return PartialView("_SkillData");
        }
        
        [HttpPost]
        public ActionResult StatusChangeApplication(long MissionApplicationId,string Result)
        {
            bool result = _AccountRepo.StatusChangeApplication(MissionApplicationId,Result);

            ViewBag.MissionApplicationList = _AccountRepo.MissionApplicationList().Skip((1 - 1) * 6).Take(6).ToList();
            ViewBag.ttlApplications = Math.Ceiling(_AccountRepo.MissionApplicationList().Count() / 6.0);
            ViewBag.pg_no = 1;

            return PartialView("_ApplicationData");
        }

        [HttpPost]
        public ActionResult StatusChangeStory(long StoryId, string Result)
        {
            bool result = _AccountRepo.StatusChangeStory(StoryId, Result);

            ViewBag.StoryList = _AccountRepo.StoryList().Skip((1 - 1) * 6).Take(6).ToList();
            ViewBag.ttlStories = Math.Ceiling(_AccountRepo.StoryList().Count() / 6.0);
            ViewBag.pg_no = 1;

            return PartialView("_StoryData");
        }

        public ActionResult DeleteStory(long StoryId)
        {
            bool result = _AccountRepo.DeleteStory(StoryId);

            ViewBag.StoryList = _AccountRepo.StoryList().Skip((1 - 1) * 6).Take(6).ToList();
            ViewBag.ttlStories = Math.Ceiling(_AccountRepo.StoryList().Count() / 6.0);
            ViewBag.pg_no = 1;

            return PartialView("_StoryData");
        }

        [HttpPost]
        public ActionResult AddBanner(Banner model)
        {
            bool result = _AccountRepo.AddBanner(model);

            ViewBag.BannerList = _AccountRepo.BannerList().Skip((1 - 1) * 6).Take(6).ToList();
            ViewBag.ttlBanners = Math.Ceiling(_AccountRepo.BannerList().Count() / 6.0);
            ViewBag.pg_no = 1;

            return PartialView("_BannerData");

        }

        public JsonResult EditBanner(long BannerId)
         {
            var adminModel = _AccountRepo.EditBanner(BannerId);

            return Json(adminModel);
        }

        public ActionResult DeleteBanner(long BannerId)
        {
            bool result = _AccountRepo.DeleteBanner(BannerId);

            ViewBag.BannerList = _AccountRepo.BannerList().Skip((1 - 1) * 6).Take(6).ToList();
            ViewBag.ttlBanners = Math.Ceiling(_AccountRepo.BannerList().Count() / 6.0);
            ViewBag.pg_no = 1;

            return PartialView("_BannerData");
        }

        [HttpPost]
        public ActionResult StatusChangeComment(long CommentId, string Result)
        {
            bool result = _AccountRepo.StatusChangeComment(CommentId, Result);

            ViewBag.CommentList = _AccountRepo.CommentList().Skip((1 - 1) * 6).Take(6).ToList();
            ViewBag.ttlComments = Math.Ceiling(_AccountRepo.CommentList().Count() / 6.0);
            ViewBag.pg_no = 1;

            return PartialView("_CommentData");
        }

        public ActionResult AddUser(User model)
        {
            bool result = _AccountRepo.AddUser(model);

            ViewBag.UserList = _AccountRepo.UserList().Skip((1 - 1) * 6).Take(6).ToList();
            ViewBag.ttlUsers = Math.Ceiling(_AccountRepo.UserList().Count() / 6.0);
            ViewBag.pg_no = 1;

            return PartialView("_UserData");

        }

        public ActionResult DeleteUser(long UserId)
        {
            bool result = _AccountRepo.DeleteUser(UserId);

            ViewBag.UserList = _AccountRepo.UserList().Skip((1 - 1) * 6).Take(6).ToList();
            ViewBag.ttlUsers = Math.Ceiling(_AccountRepo.UserList().Count() / 6.0);
            ViewBag.pg_no = 1;

            return PartialView("_UserData");
        }

       
        public JsonResult EditUser(long UserId)
        {
            var adminModel = _AccountRepo.EditUser(UserId);
            //ViewBag.CountryList = _AccountRepo.CountryList().Select(x=> new {x.CountryId,x.CountryName});

            return Json(adminModel);
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


        //public IActionResult StoryDetail(long id)
        //{

        //        List<User> users = _StoryRepo.UserList();
        //        ViewBag.Users = users;

        //        _StoryRepo.StoryView(id, UserId);


        //        List<MissionData> missionDatas = _StoryRepo.GetStoryCardsList(UserId);

        //        var missions = missionDatas.Where(x => x.StoryId == id).FirstOrDefault();
        //        ViewBag.missionDatas = missions;


        //        ViewBag.UserId = UserId;
        //        ViewBag.Email = JsonConvert.DeserializeObject(HttpContext.Session.GetString("Email") ?? "");
        //        ViewBag.UserName = _StoryRepo.GetUserAvatar(UserId).FirstName + " " + _StoryRepo.GetUserAvatar(UserId).LastName;
        //        ViewBag.Avatar = _StoryRepo.GetUserAvatar(UserId).Avatar;

        //        return View();

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
