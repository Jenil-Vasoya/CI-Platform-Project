using CIPlatform.Entities.Models;
using CIPlatform.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Interface
{
    public interface IStoryRepository
    {
        public User GetUserAvatar(long UserId);

        public List<User> UserList();

        public List<Country> CountryList();

        public List<MissionTheme> MissionThemeList();

        public List<Skill> SkillList();

        public string GetCityName(long cityId);

        public string MediaByMissionId(long missionID);

        public string GetMissionThemes(long themeID);

        public List<MissionData> GetStoryMissionList(string? search, string[] countries, string[] cities, string[] themes, string[] skills, int pg, long UserId);

        public List<MissionData> GetStoryCardsList(long UserId);

        public void StoryView(long StoryId, long UserId);

        public List<Mission> UserAppliedMissionList(long UserId);

        public long AddData(MissionData objStory,long UserId, string btn);

        public bool InviteWorker(List<long> CoWorker, long UserId, long StoryId);
    }
}
