using CIPlatform.Entities.Data;
using CIPlatform.Entities.Models;
using CIPlatform.Entities.ViewModel;
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


        public List<City> CityList(List<string> CountryID)
        {
            List<City> objCityList = _DbContext.Cities.Where(i => CountryID.Contains(i.CountryId.ToString())).ToList();
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

        public List<Mission> MissionList(string search)
        {
            return (_DbContext.Missions.Where(x=> x.Title.Contains(search)).ToList());
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


        public List<MissionData> GetMissionList(string? search, string[] countries, string[] cities, string[] themes, string[] skills)
        {
            List<MissionData> missions = GetMissionCardsList();
            if (search != "")
            {
                missions = missions.Where(a => a.Title.Contains(search) || a.OrganizationName.Contains(search)).ToList();

            }
            if (countries.Length > 0)
            {
                    missions = missions.Where(a => countries.Contains(a.CountryId.ToString())).ToList();
                
            }
            if (cities.Length > 0)
            {

                missions = missions.Where(a => cities.Contains(a.CityId.ToString())).ToList();

            }
            if (themes.Length > 0)  
            {

                missions = missions.Where(a => themes.Contains(a.MissionThemeId.ToString())).ToList();

            }
            if (skills.Length > 0)
            {

                missions = missions.Where(a => skills.Contains(a.SkillId.ToString())).ToList();

            }
            return missions;
        }

        public List<MissionData> GetMissionCardsList()
        {
            var missions = _DbContext.Missions.ToList();

            List<MissionData> missionDatas = new List<MissionData>();

            foreach (var objMission in missions)
            {
                MissionData missionData = new MissionData();
               
                missionData.MissionId = objMission.MissionId;
                missionData.MissionType = objMission.MissionType.ToString();

                missionData.CityName = GetCityName(objMission.CityId);

                missionData.OrganizationName = objMission.OrganizationName;
                missionData.ShortDescription = objMission.ShortDescription;

                missionData.StartDate = objMission.StartDate;
                missionData.EndDate = objMission.EndDate;

                missionData.MediaPath = MediaByMissionId(objMission.MissionId);
                missionData.Title = objMission.Title;

                missionData.Rating = MissionRatings(objMission.MissionId);
                missionData.Theme = GetMissionThemes(objMission.MissionThemeId);
                
                missionData.MissionThemeId = objMission.MissionThemeId;
                missionData.CountryId = objMission.CountryId;
                missionData.CityId = objMission.CityId;

                var missionSkill = _DbContext.MissionSkills.FirstOrDefault(s => s.MissionId == objMission.MissionId);
                missionData.SkillId = missionSkill.SkillId;

                missionDatas.Add(missionData);
                //if (obj.MissionType)
                //{
                //    missionListing.TotalSeat = GetTotalSeat(obj.MissionId);
                //    missionListing.Deadline = GetDeadline(obj.MissionId);
                //}
                //if (!obj.MissionType)
                //{
                //    missionListing.GoalObjectiveText = GetGoalObjectiveText(obj.MissionId);
                //    missionListing.GoalValue = GetGoalValue(obj.MissionId);
                //}
            }
            return missionDatas;
        }







    }
}
