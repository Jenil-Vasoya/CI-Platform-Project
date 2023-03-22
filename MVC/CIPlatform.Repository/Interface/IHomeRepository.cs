﻿using CIPlatform.Entities.Models;
using CIPlatform.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Interface
{
    public interface IHomeRepository
    {
        public List<User> UserList();

        public List<Country> CountryList();

        public List<City> CityList(List<string> CountryID);

        public List<MissionTheme> MissionThemeList();

        public List<Skill> SkillList();

        public List<Mission> MissionList();

        public string GetCityName(long cityId);

        public string GetMissionThemes(long themeID);

        public int TotalMissions();

        public string MediaByMissionId(long missionID);

        public int MissionRatings(long missionID);

        public List<MissionData> GetMissionList(string? search, string[] countries, string[] cities, string[] themes, string[] skills, int sort);

        public List<MissionData> GetMissionCardsList();

        public bool AddFavouriteMission(long userId, long missionId);

        public bool CheckFavMission(long userId, long missionId);

        public void AddComment(string comment, long UserId, long MissionId);

        public List<CommentViewModel> GetComment(long missionID);

        public bool ApplyMission(long UserId, long MissionId);

        public List<MissionData> GetRecentVolunteer(long missionId);

        public bool InviteWorker(List<long> CoWorker, long UserId, long MissionId);
    }
}
