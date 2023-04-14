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
    public  class AccountRepository : IAccountRepository
    {
        public readonly CiPlatformContext _DbContext;
        public AccountRepository(CiPlatformContext DbContext)
        {
            _DbContext = DbContext;
        }

        public List<User> UserList()
        {
            List<User> objUserList = _DbContext.Users.ToList();
            return objUserList;
        }
        
        public List<Mission> MissionList()
        {
            List<Mission> objMissionList = _DbContext.Missions.ToList();
            return objMissionList;
        }

        public List<MissionTheme> ThemeList()
        {
            List<MissionTheme> objThemeList = _DbContext.MissionThemes.ToList();
            return objThemeList;
        }
        
        public List<Skill> SkillList()
        {
            List<Skill> objSkillList = _DbContext.Skills.ToList();
            return objSkillList;
        }

        public AdminModel adminModelList()
        {
            AdminModel admins = new AdminModel();
            admins.users=UserList();

            return admins;
        }

        public List<User> UserListSearch(string search, int pg)
        {
            var pageSize = 6;
            List<User> users = _DbContext.Users.ToList();

            if(search != null)
            {
                users = users.Where(u => u.FirstName.ToLower().Contains(search.ToLower())).ToList();
            }

            if (pg != 0)
            {
                users = users.Skip((pg - 1) * pageSize).Take(pageSize).ToList();
            }
            return users;
        }

        public List<Mission> MissionListSearch(string search, int pg)
        {
            var pageSize = 6;
            List<Mission> missions = _DbContext.Missions.ToList();

            if(search != null)
            {
                missions = missions.Where(u => u.Title.ToLower().Contains(search.ToLower())).ToList();
            }

            if (pg != 0)
            {
                missions = missions.Skip((pg - 1) * pageSize).Take(pageSize).Take(pageSize).ToList();
            }
            return missions;
        }
        
        public List<MissionTheme> ThemeListSearch(string search, int pg)
        {
            var pageSize = 6;
            List<MissionTheme> themes = _DbContext.MissionThemes.ToList();

            if(search != null)
            {
                themes = themes.Where(u => u.Titile.ToLower().Contains(search.ToLower())).ToList();
            }

            if (pg != 0)
            {
                themes = themes.Skip((pg - 1) * pageSize).Take(pageSize).Take(pageSize).ToList();
            }
            return themes;
        }
        
        public List<Skill> SkillListSearch(string search, int pg)
        {
            var pageSize = 6;
            List<Skill> skills = _DbContext.Skills.ToList();

            if(search != null)
            {
                skills = skills.Where(u => u.SkillName.ToLower().Contains(search.ToLower())).ToList();
            }

            if (pg != 0)
            {
                skills = skills.Skip((pg - 1) * pageSize).Take(pageSize).Take(pageSize).ToList();
            }
            return skills;
        }
    }
}
