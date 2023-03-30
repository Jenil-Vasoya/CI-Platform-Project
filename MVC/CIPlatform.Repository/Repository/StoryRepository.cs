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
    public class StoryRepository : IStoryRepository
    {
        public readonly CiPlatformContext _DbContext;

        public StoryRepository(CiPlatformContext DbContext)
        {
            _DbContext = DbContext;
        }

        public List<User> UserList()
        {
            List<User> users = _DbContext.Users.ToList();
            return users;
        }

        public List<Country> CountryList()
        {
            List<Country> objCountryList = _DbContext.Countries.ToList();
            return objCountryList;
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

        public string GetCityName(long cityId)
        {

            City city = _DbContext.Cities.FirstOrDefault(i => i.CityId == cityId);
            return city.CityName;

        }

        public string MediaByMissionId(long missionID)
        {

            MissionMedium media = _DbContext.MissionMedia.FirstOrDefault(a => a.MissionId == missionID);
            return media.MediaPath;

        }

        public string GetMissionThemes(long themeID)
        {
            MissionTheme theme = _DbContext.MissionThemes.FirstOrDefault(a => a.MissionThemeId == themeID);
            return theme.Titile;
        }

        public List<MissionData> GetStoryMissionList(string? search, string[] countries, string[] cities, string[] themes, string[] skills, int pg)
        {
            var pageSize = 6;
            List<MissionData> missions = GetStoryCardsList();
            if (search != "")
            {
                missions = missions.Where(a => a.Title.ToLower().Contains(search) || a.ShortDescription.ToLower().Contains(search)).ToList();

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
            if (pg != 0)
            {
                missions = missions.Skip((pg - 1) * pageSize).Take(pageSize).Take(pageSize).ToList();
            }


            return missions;
        }

        public List<MissionData> GetStoryCardsList()
        {
            var missions = _DbContext.Stories.ToList();

            List<MissionData> missionDatas = new List<MissionData>();

            foreach (var objMission in missions)
            {
                MissionData missionData = new MissionData();

                User user = _DbContext.Users.FirstOrDefault(a => a.UserId == objMission.UserId);

                Mission mission = _DbContext.Missions.FirstOrDefault(a => a.MissionId == objMission.MissionId);

                missionData.StoryId = objMission.StoryId;
                missionData.WhyIVolunteer = user.WhyIvolunteer;

                missionData.MissionId = objMission.MissionId;

                missionData.CityName = GetCityName(mission.CityId);

                missionData.OrganizationName = mission.OrganizationName;
                missionData.ShortDescription = mission.ShortDescription;
                missionData.MissionType = mission.MissionType;

                missionData.UserName = user.FirstName + " " + user.LastName;
                missionData.Avatar = user.Avatar;
                missionData.Views = objMission.Views;

                missionData.MediaPath = MediaByMissionId(objMission.MissionId);
                missionData.Title = objMission.Title;
                missionData.Description = objMission.Description;
                missionData.StoryStatus = objMission.Status;


                missionData.Theme = GetMissionThemes(mission.MissionThemeId);

                missionData.MissionThemeId = mission.MissionThemeId;
                missionData.CountryId = mission.CountryId;
                missionData.CityId = mission.CityId;

                var missionSkill = _DbContext.MissionSkills.FirstOrDefault(s => s.MissionId == objMission.MissionId);
                missionData.SkillId = missionSkill.SkillId;

                var skillName = _DbContext.Skills.FirstOrDefault(a => a.SkillId == missionSkill.SkillId);
                missionData.SkillName = skillName.SkillName;

                missionDatas.Add(missionData);
             
            }
            return missionDatas;
        }

        public void StoryView(long StoryId)
        {
           var entry= _DbContext.Stories.FirstOrDefault(a => a.StoryId == StoryId);
            if(entry != null)
            {
                entry.Views = entry.Views + 1;
                _DbContext.SaveChanges();
            }
           
        }

        public List<Mission> UserAppliedMissionList(long UserId)
        {
            var validUser = _DbContext.MissionApplications.Where(a=> a.UserId == UserId && a.ApprovalStatus == "Approve").ToList();

            var list = new List<long>();

            foreach (var app in validUser)
            {
                list.Add(app.MissionId);
            }

            var missions = _DbContext.Missions.Where(a=> list.Contains(a.MissionId)).ToList();

            return missions;
        }

        public void AddData(MissionData objStory, long UserId)
        {

            Story story = new Story();
            {
                story.UserId = UserId;
                story.Views = 0;
                story.Title = objStory.Title;
                story.Description = objStory.Description;
                story.PublishedAt = objStory.CreatedAt;
                story.MissionId = objStory.MissionId;
            }

                _DbContext.Stories.Add(story);
                _DbContext.SaveChanges();
              
            
        }
    }
}
