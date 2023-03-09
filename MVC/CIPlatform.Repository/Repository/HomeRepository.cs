using CIPlatform.Entities.Data;
using CIPlatform.Entities.Models;
using CIPlatform.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Repository
{
    public class HomeRepository : IHomeRepository
    {
        public readonly CiPlatformContext _DbContext;

        public HomeRepository(CiPlatformContext DbContext)
        {
            _DbContext = DbContext;
        }

        public List<Country> CountryList()
        {
            List<Country> objCountryList = _DbContext.Countries.ToList();
            return objCountryList;
        }


        public List<City> CityList(int CountryID)
        {
            List<City> objCityList = _DbContext.Cities.Where(i => i.CountryId == CountryID).ToList();
            return objCityList;
        }

        public List<MissionTheme> MissionThemeList()
        {
            List<MissionTheme> objMissionTheme = _DbContext.MissionThemes.ToList();
            return objMissionTheme;
        }

        public List<Skill> SkillList()
        {
            List<Skill> objSkill = _DbContext.Skills.ToList();
            return objSkill;
        }

        public List<Mission> MissionList()
        {
            return _DbContext.Missions.ToList();
        }

        public string GetCityName(long cityId)
        {

            City city = _DbContext.Cities.FirstOrDefault(i => i.CityId == cityId);
            return city.CityName;

        }


        public string GetMissionThemes(long themeID)
        {
            MissionTheme theme = _DbContext.MissionThemes.FirstOrDefault(a => a.MissionThemeId == themeID);
            return theme.Titile;
        }

        public int TotalMissions()
        {

            int totalMission = _DbContext.Missions.Count();
            return totalMission;

        }

        public string MediaByMissionId(long missionID)
        {

            MissionMedium media = _DbContext.MissionMedia.FirstOrDefault(a => a.MissionId == missionID);
            return media.MediaPath;

        }

        public int MissionRatings(long missionID)
        {
            MissionRating rating = _DbContext.MissionRatings.FirstOrDefault(a => a.MissionId == missionID);
            return rating.Rating;
        }




    }
}
