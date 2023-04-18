using CIPlatform.Entities.AdminViewModel;
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
        
        public List<Cmspage> CMSList()
        {
            List<Cmspage> objCMSList = _DbContext.Cmspages.ToList();
            return objCMSList;
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
        
        public List<MissionApplicationModel> MissionApplicationList()
        {
            List<MissionApplication> missionApplications = _DbContext.MissionApplications.ToList();

            List<MissionApplicationModel> missionsApplication = new List<MissionApplicationModel>();

            foreach (var application in missionApplications)
            {
                User user = _DbContext.Users.Find(application.UserId);
                Mission mission = _DbContext.Missions.Find(application.MissionId);

                MissionApplicationModel model = new MissionApplicationModel();
                model.Mission = mission;
                model.User = user;
                model.UserId = application.UserId;
                model.MissionId = application.MissionId;
                model.User = application.User;
                model.ApprovalStatus = application.ApprovalStatus;
                model.MissionApplicationId = application.MissionApplicationId;  
                model.AppliedAt = application.AppliedAt;
                missionsApplication.Add(model);
            }
            return missionsApplication;
        } 
        
        public List<StoryModel> StoryList()
        {
            List<Story> stories = _DbContext.Stories.ToList();

            List<StoryModel> storyModels = new List<StoryModel>();

            foreach (var story in stories)
            {
                User user = _DbContext.Users.Find(story.UserId);
                Mission mission = _DbContext.Missions.Find(story.MissionId);

                StoryModel model = new StoryModel();
                model.StoryId = story.StoryId;
                model.Mission = mission;
                model.User = user;

                model.Title = story.Title;
                model.Description = story.Description;
                model.PublishedAt = story.PublishedAt;
                model.CreatedAt = story.CreatedAt;
                model.UpdatedAt = story.UpdatedAt;
                model.Status = story.Status;
                storyModels.Add(model);
            }
            return storyModels;
        }

        public AdminModel adminModelList()
        {
            AdminModel admins = new AdminModel();
            admins.users=UserList();

            return admins;
        }

        public List<Cmspage> CMSListSearch(string search, int pg)
        {
            var pageSize = 6;
            List<Cmspage> cmspages = _DbContext.Cmspages.ToList();

            if(search != null)
            {
                cmspages = cmspages.Where(u => u.Title.ToLower().Contains(search.ToLower())).ToList();
            }

            if (pg != 0)
            {
                cmspages = cmspages.Skip((pg - 1) * pageSize).Take(pageSize).ToList();
            }
            return cmspages;
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
        
        
        public List<MissionApplicationModel> ApplicationListSearch(string search, int pg)
        {
            var pageSize = 6;
            List<MissionApplicationModel> missions = MissionApplicationList();

            if(search != null)
            {
                missions = missions.Where(u => u.Mission.Title.ToLower().Contains(search.ToLower())).ToList();
            }

            if (pg != 0)
            {
                missions = missions.Skip((pg - 1) * pageSize).Take(pageSize).Take(pageSize).ToList();
            }
            return missions;
        } 

        public List<StoryModel> StoryListSearch(string search, int pg)
        {
            var pageSize = 6;
            List<StoryModel> stories = StoryList();

            if(search != null)
            {
                stories = stories.Where(u => u.Mission.Title.ToLower().Contains(search.ToLower()) || u.Title.ToLower().Contains(search.ToLower())).ToList();
            }

            if (pg != 0)
            {
                stories = stories.Skip((pg - 1) * pageSize).Take(pageSize).Take(pageSize).ToList();
            }
            return stories;
        }


        public bool AddCMS(Cmspage model)
        {
            if (model.CmspageId == 0)
            {

                Cmspage cmspage = new Cmspage();
                {
                    cmspage.Title = model.Title;
                    cmspage.Description = model.Description;
                    cmspage.Slug = model.Slug;
                    cmspage.Status = model.Status;

                    _DbContext.Cmspages.Add(cmspage);
                    _DbContext.SaveChanges();
                }
                return true;
            }
            else
            {
                var cms = _DbContext.Cmspages.Find(model.CmspageId);
                if(cms != null)
                {
                    cms.Title = model.Title;
                    cms.Description = model.Description;
                    cms.Slug = model.Slug;
                    cms.Status = model.Status;
                    cms.UpdatedAt = DateTime.Now;

                    _DbContext.Cmspages.Update(cms);
                    _DbContext.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //public AdminModel EditCMS(long CMSId)
        //{
        //    var cmspage = _DbContext.Cmspages.Where(c => c.CmspageId == CMSId).FirstOrDefault();

        //    AdminModel adminModel = new AdminModel();
        //    {
        //        adminModel.Title = cmspage.Title;
        //        adminModel.Description = cmspage.Description;
        //        adminModel.Slug = cmspage.Slug;
        //        adminModel.Status = cmspage.Status;

        //    }
        //    return adminModel;
        //}
        
        public Cmspage EditCMS(long CMSId)
        {
            var cmspage = _DbContext.Cmspages.Where(c => c.CmspageId == CMSId).FirstOrDefault();

           
            return cmspage;
        }

        public bool DeleteCMS(long CMSId)
        {
            var cms = _DbContext.Cmspages.Find(CMSId);
            
            _DbContext.Cmspages.Remove(cms);
            _DbContext.SaveChanges();
            return true;
        }

        public bool AddMission(Mission model)
        {
            if (model.MissionId == 0)
            {

                Mission mission = new Mission();
                {
                    mission.Title = model.Title;
                    mission.Description = model.Description;
                    mission.Status = model.Status;

                    _DbContext.Missions.Add(mission);
                    _DbContext.SaveChanges();
                }
                return true;
            }
            else
            {
                var mission = _DbContext.Missions.Find(model.MissionId);
                if (mission != null)
                {
                    mission.Title = model.Title;
                    mission.Description = model.Description;
                    mission.Status = model.Status;
                    mission.UpdatedAt = DateTime.Now;

                    _DbContext.Missions.Update(mission);
                    _DbContext.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

         public Mission EditMission(long MissionId)
        {
            var mission = _DbContext.Missions.Where(c => c.MissionId == MissionId).FirstOrDefault();

           
            return mission;
        }

        public bool DeleteMission(long MissionId)
        {
            var mission = _DbContext.Missions.Find(MissionId);
            
            _DbContext.Missions.Remove(mission);
            _DbContext.SaveChanges();
            return true;
        }
    }
}
