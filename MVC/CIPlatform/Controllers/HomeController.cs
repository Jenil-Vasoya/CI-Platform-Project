
using CIPlatform.Entities.Models;
using CIPlatform.Entities.ViewModel;
using CIPlatform.Repository.Interface;
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
        public IActionResult MissionGrid()
        {
            List<MissionData> missionDatas = new List<MissionData>();

            List<Mission> mission = _HomeRepo.MissionList();

         foreach (var objMission in mission)
            {
                MissionData missionData = new MissionData();

                missionData.MissionId = objMission.MissionId;
                missionData.MissionType = objMission.MissionType.ToString();

                missionData.Availability = objMission.Availability.ToString();

                missionData.CityId = objMission.CityId;
                missionData.CityName = _HomeRepo.GetCityName(objMission.CityId);

                missionData.OrganizationName = objMission.OrganizationName;
                missionData.ShortDescription = objMission.ShortDescription;

                missionData.StartDate = objMission.StartDate;
                missionData.EndDate = objMission.EndDate;

                missionData.MediaPath = _HomeRepo.MediaByMissionId(missionData.MissionId);
                missionData.Title = objMission.Title;

                missionData.Rating = _HomeRepo.MissionRatings(missionData.MissionId);
                missionData.Theme = _HomeRepo.GetMissionThemes(objMission.MissionThemeId);

                missionDatas.Add(missionData);
            }

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


        public JsonResult GetCity(int countryId)
        {
            List<City> city = _HomeRepo.CityList(countryId);
            var json = JsonConvert.SerializeObject(city);


            return Json(json);
        }


        public IActionResult MissionList()
        {
            List<MissionData> missionDatas = new List<MissionData>();

            List<Mission> mission = _HomeRepo.MissionList();

            foreach (var objMission in mission)
            {
                MissionData missionData = new MissionData();

                missionData.MissionId = objMission.MissionId;
                missionData.MissionType = objMission.MissionType.ToString();

                missionData.CityId = objMission.CityId;
                missionData.CityName = _HomeRepo.GetCityName(objMission.CityId);

                missionData.OrganizationName = objMission.OrganizationName;
                missionData.ShortDescription = objMission.ShortDescription;

                missionData.StartDate = objMission.StartDate;
                missionData.EndDate = objMission.EndDate;

                missionData.MediaPath = _HomeRepo.MediaByMissionId(missionData.MissionId);
                missionData.Title = objMission.Title;

                missionData.Rating = _HomeRepo.MissionRatings(missionData.MissionId);
                missionData.Theme = _HomeRepo.GetMissionThemes(objMission.MissionThemeId);

                missionDatas.Add(missionData);
            }

            ViewBag.missionDatas = missionDatas;


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
        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}