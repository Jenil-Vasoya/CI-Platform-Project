﻿
using CIPlatform.Entities.Models;
using CIPlatform.Entities.ViewModel;
using CIPlatform.Repository.Interface;
using CIPlatform.Repository.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace CIPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeRepository _HomeRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration configuration;

        public HomeController(IHomeRepository HomeRepo, IHttpContextAccessor httpContextAccessor, IConfiguration _configuration)
        {
            _HomeRepo = HomeRepo;
             _httpContextAccessor = httpContextAccessor;
            configuration = _configuration;
        }

 




        public IActionResult Index()
        {
            return View();
        }

        public IActionResult StoryList()
        {
            return View();
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

            List<Mission> missions = _HomeRepo.MissionList();
            if (missions.Count == 0)
            {
                return RedirectToAction("MissionNotFound");
            }


            List<MissionData> missionDatas = _HomeRepo.GetMissionCardsList(missions); 
            ViewBag.missionDatas = missionDatas;

            ViewBag.UserId = JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId"));
            ViewBag.Email = JsonConvert.DeserializeObject(HttpContext.Session.GetString("Email"));
            ViewBag.UserName = JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserName"));
            ViewBag.Avatar = JsonConvert.DeserializeObject(HttpContext.Session.GetString("Avatar"));

            List<Country> countries = _HomeRepo.CountryList();
            ViewBag.countries = countries;

            List<MissionTheme> themes = _HomeRepo.MissionThemeList();
            ViewBag.themes = themes;

            List<Skill> skills = _HomeRepo.SkillList();
            ViewBag.skills = skills;

            var totalMission = _HomeRepo.TotalMissions();
            ViewBag.totalMission = totalMission;


            return View();
        }

        [HttpPost]
        public IActionResult MissionGrid(string? search)
        {
            search = string.IsNullOrEmpty(search) ? "" : search.ToLower();

            ViewBag.UserId = JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId"));
            ViewBag.Email = JsonConvert.DeserializeObject(HttpContext.Session.GetString("Email"));
            ViewBag.UserName = JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserName"));
            ViewBag.Avatar = JsonConvert.DeserializeObject(HttpContext.Session.GetString("Avatar"));


            List<Mission> missions = _HomeRepo.MissionList();
            if (missions.Count == 0)
            {
                return RedirectToAction("MissionNotFound");
            }


            List<MissionData> missionDatas = _HomeRepo.GetMissionCardsList(missions);
            ViewBag.missionDatas = missionDatas;

           
            List<Country> countries = _HomeRepo.CountryList();
            ViewBag.countries = countries;

            List<MissionTheme> themes = _HomeRepo.MissionThemeList();
            ViewBag.themes = themes;

            List<Skill> skills = _HomeRepo.SkillList();
            ViewBag.skills = skills;

            var totalMission = _HomeRepo.TotalMissions();
            ViewBag.totalMission = totalMission;


            return View();
        }


        public JsonResult GetCity(int countryId)
        {
            List<City> city = _HomeRepo.CityList(countryId);
            var json = JsonConvert.SerializeObject(city);


            return Json(json);
        }

        [HttpGet]
        public IActionResult MissionList()
        {

            List<Mission> missions = _HomeRepo.MissionList();

            if (missions.Count == 0)
            {
                return RedirectToAction("MissionNotFound");
            }


            List<MissionData> missionDatas = _HomeRepo.GetMissionCardsList(missions); ;
            ViewBag.missionDatas = missionDatas;

            List<Country> countries = _HomeRepo.CountryList();
            ViewBag.countries = countries;

            List<MissionTheme> themes = _HomeRepo.MissionThemeList();
            ViewBag.themes = themes;

            List<Skill> skills = _HomeRepo.SkillList();
            ViewBag.skills = skills;

            var totalMission = _HomeRepo.TotalMissions();
            ViewBag.totalMission = totalMission;

            return View();
        }

        [HttpPost]
        public IActionResult MissionList(string? search, string? city, string? theme, string? country)
        {
            search = string.IsNullOrEmpty(search) ? "" : search.ToLower();

            List<Mission> missions = _HomeRepo.MissionList();

            if (missions.Count == 0)
            {
                return RedirectToAction("MissionNotFound");
            }


            List<MissionData> missionDatas = _HomeRepo.GetMissionCardsList(missions); ;
            ViewBag.missionDatas = missionDatas;

            List<Country> countries = _HomeRepo.CountryList();
            ViewBag.countries = countries;

            List<MissionTheme> themes = _HomeRepo.MissionThemeList();
            ViewBag.themes = themes;

            List<Skill> skills = _HomeRepo.SkillList();
            ViewBag.skills = skills;

            var totalMission = _HomeRepo.TotalMissions();
            ViewBag.totalMission = totalMission;

            return View();
        }

        public IActionResult MissionEmpty()
        {
            return View();
        }
        public IActionResult VolunteerMission()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string? search, string[] countries, string[] cities, string[] themes, string[] skills)
        {


            search = string.IsNullOrEmpty(search) ? "" : search.ToLower();
            List<Mission> missions = _HomeRepo.GetMissionList(search, countries, cities, themes, skills);
            List<MissionData> missionDatas = _HomeRepo.GetMissionCardsList(missions);

            ViewBag.missionDatas = missionDatas;


            return PartialView("_MissionGrid");
        }
        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}