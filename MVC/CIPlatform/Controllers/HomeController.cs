
using CIPlatform.Entities.Data;
using CIPlatform.Entities.Models;
using CIPlatform.Entities.ViewModel;
using CIPlatform.Repository.Interface;
using CIPlatform.Repository.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;

namespace CIPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeRepository _HomeRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration configuration;
        public readonly CiPlatformContext _DbContext;


        public HomeController(IHomeRepository HomeRepo, IHttpContextAccessor httpContextAccessor, IConfiguration _configuration, CiPlatformContext DbContext)
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


        //[HttpPost]
        //public IActionResult GetGridView(test test)
        //{
        //    var pageinfo = new Microsoft.Data.SqlClient.SqlParameter("@TotalCount", SqlDbType.BigInt) { Direction = ParameterDirection.Output };

        //    Pagination pagination = new Pagination();
        //    List<MissionListing> ViewItem = _dataBaseForCiPlatformContext.MissionListing.FromSqlInterpolated($"exec SP_GETDATAFORVIEW @searchCountry = {test.country} , @searchCity = {test.city} , @searchTheme = {test.theme} , @searchSkill = {test.skill} , @searchText = {test.searchText} , @SortBy = {test.SortBy}, @pageNumber = {test.pageNumber}, @TotalCount = {pageinfo} out").ToList();
        //    pagination.MissionListings = ViewItem;
        //    pagination.PageSize = 9;
        //    pagination.TotalItems = long.Parse(pageinfo.Value.ToString());
        //    pagination.ActivePage = test.pageNumber;
        //    return PartialView("_GridView", pagination);

        //}

        [HttpGet]
        public IActionResult MissionGrid()
        {
            //var pageinfo = new Microsoft.Data.SqlClient.SqlParameter("@TotalCount", SqlDbType.BigInt) { Direction = ParameterDirection.Output };

            //Pagination pagination = new Pagination();

            //List<MissionData> ViewItem = _DbContext.MissionDatas.FromSqlInterpolated($"exec SP_GETDATAFORVIEW @pageNumber = {test.pageNumber}, @TotalCount = {pageinfo} out").ToList();
          
            //pagination.MissionDatas = ViewItem;
            //pagination.PageSize = 4;
            //pagination.TotalItems = long.Parse(pageinfo.Value.ToString());
            //pagination.ActivePage = test.pageNumber;

            List<Mission> missions = _HomeRepo.MissionList();
            if (missions.Count == 0)
            {
                return RedirectToAction("MissionNotFound");
            }

            List<MissionData> missionDatas = _HomeRepo.GetMissionCardsList(); 
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


            List<MissionData> missionDatas = _HomeRepo.GetMissionCardsList();
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


        public JsonResult GetCity(List<string> countryId)
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


            List<MissionData> missionDatas = _HomeRepo.GetMissionCardsList(); ;
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

        
        public ActionResult SearchList(string? search, string[] countries, string[] cities, string[] themes, string[] skills, int sort)
        {


            search = string.IsNullOrEmpty(search) ? "" : search.ToLower();
            List<MissionData> missionDatas = _HomeRepo.GetMissionList(search, countries, cities, themes, skills, sort);

            ViewBag.missionDatas = missionDatas;

            var totalMission = missionDatas.Count.ToString();
            ViewBag.totalMission = totalMission;

            return PartialView("_MissionList");
        }


        public IActionResult MissionEmpty()
        {
            return View();
        }


        public IActionResult VolunteerMission(long id)
{
            List<MissionData> missionDatas = _HomeRepo.GetMissionCardsList();
            var missions = missionDatas.Where(x => x.MissionId == id).FirstOrDefault();
            ViewBag.missionDatas = missions;

            var relatedmission = missionDatas.Where(x => x.Theme == missions.Theme || x.CityName == missions.CityName || x.MissionType == missions.MissionType).Take(3).ToList();
            ViewBag.total = relatedmission.Count;
            ViewBag.relatedmission = relatedmission;

            ViewBag.Comments = _HomeRepo.GetComment(missions.MissionId);

            long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId")));
            ViewBag.UserId = UserId;
            ViewBag.Email = JsonConvert.DeserializeObject(HttpContext.Session.GetString("Email"));
            ViewBag.UserName = JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserName"));
            ViewBag.Avatar = JsonConvert.DeserializeObject(HttpContext.Session.GetString("Avatar"));

            return View();
        }


        [HttpPost]
        public ActionResult Search(string? search, string[] countries, string[] cities, string[] themes, string[] skills, int sort)
        {


            search = string.IsNullOrEmpty(search) ? "" : search.ToLower();
            List<MissionData> missionDatas = _HomeRepo.GetMissionList(search, countries, cities, themes, skills, sort);

            ViewBag.missionDatas = missionDatas;

            var totalMission = missionDatas.Count.ToString();
            ViewBag.totalMission = totalMission;


            return PartialView("_MissionGrid");
        }

        public ActionResult AddToFavourite( long missionId)
        {
            long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId")));

            var mission = _HomeRepo.AddFavouriteMission(UserId, missionId);
            return View();
        }

        public void AddComment(string comment, long MissionId)
        {
            long UserId = Convert.ToInt64(JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserId")));

            _HomeRepo.AddComment(comment, UserId, MissionId);
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}



        [HttpPost]
        public async Task<IActionResult> GetAll(PaginationRequest paginationRequest)
        {
            // ------------------------------------ Get The All Data-------------------
            var allData = await _DbContext.Missions.Include(m => m.MissionId).ToListAsync();

            // ------------------------------------ Search Parameter-------------------
           
            // ------------------------------------ Pagination ------------------------
            if (paginationRequest.PageNumber > 0 && paginationRequest.PageSize > 0)
            {
                var result = new PaginationResult<Mission>
                {
                    PageNumber = paginationRequest.PageNumber,
                    PageSize = paginationRequest.PageSize,
                    TotalCount = allData.Count(),
                    TotalPages = (int)Math.Ceiling(allData.Count() / (double)paginationRequest.PageSize),
                    Results = allData.Skip((paginationRequest.PageNumber - 1) * paginationRequest.PageSize).Take(paginationRequest.PageSize).ToList()
                };
                return Ok(result);
            }
            else
            {
                return Ok(allData.ToList());
            }
            return Ok();
        }




    }
}