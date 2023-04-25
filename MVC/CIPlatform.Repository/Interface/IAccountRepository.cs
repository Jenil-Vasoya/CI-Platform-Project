using CIPlatform.Entities.AdminViewModel;
using CIPlatform.Entities.Models;
using CIPlatform.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Interface
{
    public interface IAccountRepository
    {
        public List<User> UserList();

        public List<Cmspage> CMSList();

        public List<Mission> MissionList();

        public List<MissionTheme> ThemeList();

        public List<Skill> SkillList();

        public List<Country> CountryList();

        public List<MissionApplicationModel> MissionApplicationList();

        public List<StoryModel> StoryList();

        public AdminModel adminModelList();

        public List<User> UserListSearch(string search, int pg);

        public List<Cmspage> CMSListSearch(string search, int pg);

        public List<Mission> MissionListSearch(string search, int pg);

        public List<MissionTheme> ThemeListSearch(string search, int pg);

        public List<Skill> SkillListSearch(string search, int pg);

        public List<MissionApplicationModel> ApplicationListSearch(string search, int pg);

        public List<StoryModel> StoryListSearch(string search, int pg);

        public bool AddCMS(Cmspage model);

        //public AdminModel EditCMS(long CMSId);

        public Cmspage EditCMS(long CMSId);

        public bool DeleteCMS(long CMSId);

        public bool AddMission(Mission model);

        public Mission EditMission(long MissionId);

        public bool DeleteMission(long MissionId);

        public bool AddTheme(MissionTheme model);

        public MissionTheme EditTheme(long ThemeId);

        public bool DeleteTheme(long ThemeId);
    }
}
