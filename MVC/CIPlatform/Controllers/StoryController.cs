using CIPlatform.Entities.Models;
using CIPlatform.Entities.ViewModel;
using CIPlatform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CIPlatform.Controllers
{
    public class StoryController : Controller
    {
        private readonly IStoryRepository _StoryRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration configuration;

        public StoryController(IStoryRepository StoryRepo, IHttpContextAccessor httpContextAccessor, IConfiguration _configuration)
        {
            _StoryRepo = StoryRepo;
            _httpContextAccessor = httpContextAccessor;
            configuration = _configuration;
        }



        public IActionResult StoryList()
        {
            List<MissionData> missionDatas = _StoryRepo.GetStoryCardsList();
            ViewBag.missionDatas = missionDatas;

            ViewBag.UserId = JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? "");
            ViewBag.Email = JsonConvert.DeserializeObject(HttpContext.Session.GetString("Email") ?? "");
            ViewBag.UserName = JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserName") ?? "");
            ViewBag.Avatar = JsonConvert.DeserializeObject(HttpContext.Session.GetString("Avatar") ?? "");

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

        [HttpPost]
        public ActionResult SearchStory(string? search, string[] countries, string[] cities, string[] themes, string[] skills, int pg)
        {

            long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? ""));
            search = string.IsNullOrEmpty(search) ? "" : search.ToLower();
            List<MissionData> missionDatas = _StoryRepo.GetStoryMissionList(search, countries, cities, themes, skills, pg);

            ViewBag.missionDatas = missionDatas;

            ViewBag.pg_no = pg;
            ViewBag.Totalpages = Math.Ceiling(_StoryRepo.GetStoryMissionList(search, countries, cities, themes, skills, pg = 0).Count() / 6.0);
            ViewBag.missionDatas = missionDatas.Skip((1 - 1) * 6).Take(6).ToList();

            return PartialView("_StoryList");
        }


        public IActionResult StoryDetail(long id)
        {
            List<User> users = _StoryRepo.UserList();
            ViewBag.Users = users;

            _StoryRepo.StoryView(id);

            long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? ""));

            List<MissionData> missionDatas = _StoryRepo.GetStoryCardsList();

            var missions = missionDatas.Where(x => x.StoryId == id).FirstOrDefault();
            ViewBag.missionDatas = missions;

          
            ViewBag.UserId = UserId;
            ViewBag.Email = JsonConvert.DeserializeObject(HttpContext.Session.GetString("Email") ?? "");
            ViewBag.UserName = JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserName") ?? "");
            ViewBag.Avatar = JsonConvert.DeserializeObject(HttpContext.Session.GetString("Avatar") ?? "");

            return View();
        }

        public IActionResult AddStory(MissionData objStory)
        {
            long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId") ?? ""));

            ViewBag.MissionData = _StoryRepo.UserAppliedMissionList(UserId);
            if(objStory.MissionId != 0)
            _StoryRepo.AddData(objStory,UserId);
           
            ViewBag.Email = JsonConvert.DeserializeObject(HttpContext.Session.GetString("Email") ?? "");
            ViewBag.UserName = JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserName") ?? "");
            ViewBag.Avatar = JsonConvert.DeserializeObject(HttpContext.Session.GetString("Avatar") ?? "");

            return View();
        }

        //[HttpPost]
        //public ActionResult UploadImages(IEnumerable<HttpPostedFileBase> images)
        //{
        //    foreach (var image in images)
        //    {
        //        if (image != null && image.ContentLength > 0)
        //        {
        //            // Save the file to the server or process it as needed
        //            var fileName = Path.GetFileName(image.FileName);
        //            var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
        //            image.SaveAs(path);
        //        }
        //    }

        //    return RedirectToAction("Index");
        //}


    }
}
