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
using System.Web.Helpers;

namespace CIPlatform.Repository.Repository
{
    public  class AccountRepository : IAccountRepository
    {
        public readonly CIPlatformDbContext _DbContext;
        public AccountRepository(CIPlatformDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public List<User> UserList()
        {
            List<User> objUserList = _DbContext.Users.Where(u=>u.DeletedAt == null).ToList();
            return objUserList;
        }
        
        public List<Cmspage> CMSList()
        {
            List<Cmspage> objCMSList = _DbContext.Cmspages.Where(p=> p.DeletedAt == null).ToList();
            return objCMSList;
        }
        
        public List<Mission> MissionList()
        {
            List<Mission> objMissionList = _DbContext.Missions.Where(m=> m.DeletedAt == null).ToList();
            return objMissionList;
        }

        public List<MissionTheme> ThemeList()
        {
            List<MissionTheme> objThemeList = _DbContext.MissionThemes.Where(t=> t.DeletedAt == null).ToList();
            return objThemeList;
        }
        
        public List<Skill> SkillList()
        {
            List<Skill> objSkillList = _DbContext.Skills.Where(s=> s.DeletedAt == null).ToList();
            return objSkillList;
        }
        public List<Country> CountryList()
        {
            List<Country> objCountryList = _DbContext.Countries.ToList();
            return objCountryList;
        }
        
        public List<MissionApplicationModel> MissionApplicationList()
        {
            List<MissionApplication> missionApplications = _DbContext.MissionApplications.OrderByDescending(m => m.ApprovalStatus).ToList();

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
            List<Story> stories = _DbContext.Stories.Where(s=> s.DeletedAt == null).OrderByDescending(s => s.Status == "PENDING").ToList();

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

        public List<Banner> BannerList()
        {
            List<Banner> objBannerList = _DbContext.Banners.Where(b=> b.DeletedAt == null).ToList();
            return objBannerList;
        }

        public List<CommentModel> CommentList()
        {
            List<Comment> missionComments = _DbContext.Comments.OrderByDescending(m => m.ApprovalStatus).ToList();

            List<CommentModel> comments = new List<CommentModel>();

            foreach (var comment in missionComments)
            {
                User user = _DbContext.Users.Find(comment.UserId);
                Mission mission = _DbContext.Missions.Find(comment.MissionId);

                CommentModel model = new CommentModel();
                model.Mission = mission;
                model.User = user;
                model.UserId = comment.UserId;
                model.MissionId = comment.MissionId;
                model.User = comment.User;
                model.Comments = comment.Comments;
                model.ApprovalStatus = comment.ApprovalStatus;
                model.CommentId = comment.CommentId;
                comments.Add(model);
            }
            return comments;
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
            List<Cmspage> cmspages = CMSList();

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
            List<User> users = UserList();

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
            List<Mission> missions = MissionList();

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
            List<MissionTheme> themes = ThemeList();

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
            List<Skill> skills = SkillList();

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


        public List<Banner> BannerListSearch(string search, int pg)
        {
            var pageSize = 6;
            List<Banner> banners = BannerList();

            if (search != null)
            {
                banners = banners.Where(u => u.Text.ToLower().Contains(search.ToLower())).ToList();
            }

            if (pg != 0)
            {
                banners = banners.Skip((pg - 1) * pageSize).Take(pageSize).ToList();
            }
            return banners;
        }

        public List<CommentModel> CommentListSearch(string search, int pg)
        {
            var pageSize = 6;
            List<CommentModel> missions = CommentList();

            if (search != null)
            {
                missions = missions.Where(u => u.Mission.Title.ToLower().Contains(search.ToLower()) || u.User.FirstName.ToLower().Contains(search.ToLower()) || u.Comments.ToLower().Contains(search.ToLower())).ToList();
            }

            if (pg != 0)
            {
                missions = missions.Skip((pg - 1) * pageSize).Take(pageSize).Take(pageSize).ToList();
            }
            return missions;
        }

        public User UserData(long UserId)
        {
            var user = _DbContext.Users.Where(c => c.UserId == UserId).FirstOrDefault();


            return user;
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
                    mission = model;
                    mission.TotalSeats = model.TotalSeats;
                    mission.Availibility = model.Availibility;

                    _DbContext.Missions.Add(mission);
                    _DbContext.SaveChanges();
                }

                Notification notification = new Notification();
                {
                    notification.Text = "New Mission : " + mission.Title;
                    notification.Status = "Unseen";
                    _DbContext.Notifications.Add(notification);
                    _DbContext.SaveChanges();
                }
                if (model.MissionSkill != null)
                {
                    foreach (var skillId in model.MissionSkill)
                    {
                        MissionSkill skill = new MissionSkill();
                        {
                            skill.SkillId = skillId;
                            skill.MissionId = mission.MissionId;

                            _DbContext.MissionSkills.Add(skill);
                            _DbContext.SaveChanges();
                        }
                    }
                }

                if(model.GoalText != null && model.GoalValue != 0)
                {
                    GoalMission goalMission = new GoalMission();
                    goalMission.MissionId = mission.MissionId;
                    goalMission.GoalObjectiveText = model.GoalText;
                    goalMission.GoalValue = model.GoalValue;

                    _DbContext.GoalMissions.Add(goalMission);
                    _DbContext.SaveChanges();
                }

                if(model.Images.Count != 0)
                {
                    List<MissionMedium> missionMedia = _DbContext.MissionMedia.Where(a => a.MissionId == mission.MissionId).ToList();
                    foreach (var missionMediaItem in missionMedia)
                    {
                        _DbContext.MissionMedia.Remove(missionMediaItem);
                        _DbContext.SaveChanges();
                    }




                    var filePath = new List<string>();
                    foreach (var i in model.Images)
                    {
                        MissionMedium missionMedium = new MissionMedium();
                        missionMedium.MissionId = mission.MissionId;
                        missionMedium.MediaName = i.FileName;
                        missionMedium.MediaType = "png";
                        missionMedium.MediaPath = "~/Assets/StoryImages/" + i.FileName;
                        _DbContext.MissionMedia.Add(missionMedium);
                        _DbContext.SaveChanges();
                        if (i.Length > 0)
                        {
                            //string path = Server.MapPath("~/wwwroot/Assets/Story");
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Assets/StoryImages", i.FileName);
                            filePath.Add(path);
                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                i.CopyTo(stream);
                            }
                        }

                    }

                    if (model.Documents != null)
                    {
                        var docPath = new List<string>();
                        foreach (var i in model.Documents)
                        {
                            MissionDocument missionDocument = new MissionDocument();
                            missionDocument.MissionId = mission.MissionId;
                            missionDocument.DocumentName = i.FileName;
                            missionDocument.DocumentType = "pdf";
                            missionDocument.DocumentPath = "~/Assets/Document/" + i.FileName;
                            _DbContext.MissionDocuments.Add(missionDocument);
                            _DbContext.SaveChanges();
                            if (i.Length > 0)
                            {
                                //string path = Server.MapPath("~/wwwroot/Assets/Story");
                                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Assets/Document", i.FileName);
                                docPath.Add(path);
                                using (var stream = new FileStream(path, FileMode.Create))
                                {
                                    i.CopyTo(stream);
                                }
                            }

                        }
                    }

                }
                if (model.VideoUrl != null)
                {

                    MissionMedium medium = new MissionMedium();
                    medium.MissionId = mission.MissionId;
                    medium.MediaName = model.VideoUrl;
                    medium.MediaType = "mp4";
                    medium.MediaPath = model.VideoUrl;
                    _DbContext.MissionMedia.Add(medium);
                    _DbContext.SaveChanges();
                }

                return true;
            }
            else
            {
                var mission = _DbContext.Missions.Find(model.MissionId);
                var goalMission = _DbContext.GoalMissions.Where(g=> g.MissionId == model.MissionId).FirstOrDefault();
                if (mission != null)
                {

                    mission.Title = model.Title;
                    mission.Description = model.Description;
                    mission.Status = model.Status;
                    mission.CityId = model.CityId;
                    mission.CountryId = model.CountryId;
                    mission.EndDate = model.EndDate;
                    mission.Deadline = model.Deadline;
                    mission.StartDate = model.StartDate;
                    mission.MissionType = model.MissionType;
                    mission.MissionThemeId = model.MissionThemeId;
                    mission.OrganizationName = model.OrganizationName;
                    mission.OrganizationDetail = model.OrganizationDetail;
                    mission.ShortDescription = model.ShortDescription;
                    mission.TotalSeats = model.TotalSeats;
                    mission.UpdatedAt = DateTime.Now;

                    _DbContext.Missions.Update(mission);
                    _DbContext.SaveChanges();


                    if (model.MissionType == "Goal")
                    {
                        if (goalMission != null)
                        {

                            goalMission.MissionId = mission.MissionId;
                            goalMission.GoalObjectiveText = model.GoalText;
                            goalMission.GoalValue = model.GoalValue;

                            _DbContext.GoalMissions.Update(goalMission);
                            _DbContext.SaveChanges();
                        }
                        else
                        {
                            GoalMission goalMission1 = new GoalMission();
                            goalMission1.MissionId = mission.MissionId;
                            goalMission1.GoalObjectiveText = model.GoalText;
                            goalMission1.GoalValue = model.GoalValue;

                            _DbContext.GoalMissions.Add(goalMission1);
                            _DbContext.SaveChanges();
                        }
                    }
                    else
                    {
                        if (goalMission != null)
                        {
                            _DbContext.GoalMissions.Remove(goalMission);
                            _DbContext.SaveChanges();
                        }
                    }
                    //else if (model.GoalText != null && model.GoalValue != 0)
                    //{
                    //    GoalMission goalMissionAdd = new GoalMission();
                    //    goalMissionAdd.MissionId = mission.MissionId;
                    //    goalMissionAdd.GoalObjectiveText = model.GoalText;
                    //    goalMissionAdd.GoalValue = model.GoalValue;

                    //    _DbContext.GoalMissions.Add(goalMissionAdd);
                    //    _DbContext.SaveChanges();
                    //}

                    if (model.MissionSkill.Count != 0)
                    {

                        List<MissionSkill> missionSkills = _DbContext.MissionSkills.Where(a => a.MissionId == mission.MissionId).ToList();
                        foreach (var missionSkillItem in missionSkills)
                        {
                            _DbContext.MissionSkills.Remove(missionSkillItem);
                            _DbContext.SaveChanges();
                        }


                        foreach (var skillId in model.MissionSkill)
                        {
                            MissionSkill skill = new MissionSkill();
                            {
                                skill.SkillId = skillId;
                                skill.MissionId = mission.MissionId;

                                _DbContext.MissionSkills.Add(skill);
                                _DbContext.SaveChanges();
                            }
                        }
                    }

                    if (model.Images != null)
                    {
                        List<MissionMedium> missionMedia = _DbContext.MissionMedia.Where(a => a.MissionId == mission.MissionId && a.MediaType == "png").ToList();
                        foreach (var missionMediaItem in missionMedia)
                        {
                            _DbContext.MissionMedia.Remove(missionMediaItem);
                            _DbContext.SaveChanges();
                        }




                        var filePath = new List<string>();
                        foreach (var i in model.Images)
                        {
                            MissionMedium missionMedium = new MissionMedium();
                            missionMedium.MissionId = mission.MissionId;
                            missionMedium.MediaName = i.FileName;
                            missionMedium.MediaType = "png";
                            missionMedium.MediaPath = "~/Assets/StoryImages/" + i.FileName;
                            _DbContext.MissionMedia.Add(missionMedium);
                            _DbContext.SaveChanges();
                            if (i.Length > 0)
                            {
                                //string path = Server.MapPath("~/wwwroot/Assets/Story");
                                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Assets/StoryImages", i.FileName);
                                filePath.Add(path);
                                using (var stream = new FileStream(path, FileMode.Create))
                                {
                                    i.CopyTo(stream);
                                }
                            }

                        }

                    }
                    if (model.Documents != null)
                    {
                        List<MissionDocument> missionDocuments = _DbContext.MissionDocuments.Where(a => a.MissionId == mission.MissionId).ToList();
                        foreach (var document in missionDocuments)
                        {
                            _DbContext.MissionDocuments.Remove(document);
                            _DbContext.SaveChanges();
                        }


                        var docPath = new List<string>();
                        foreach (var i in model.Documents)
                        {
                            MissionDocument missionDocument = new MissionDocument();
                            missionDocument.MissionId = mission.MissionId;
                            missionDocument.DocumentName = i.FileName;
                            missionDocument.DocumentType = "pdf";
                            missionDocument.DocumentPath = "~/Assets/Document/" + i.FileName;
                            _DbContext.MissionDocuments.Add(missionDocument);
                            _DbContext.SaveChanges();
                            if (i.Length > 0)
                            {
                                //string path = Server.MapPath("~/wwwroot/Assets/Story");
                                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Assets/Document", i.FileName);
                                docPath.Add(path);
                                using (var stream = new FileStream(path, FileMode.Create))
                                {
                                    i.CopyTo(stream);
                                }
                            }

                        }
                    }
                    if (model.VideoUrl != null)
                    {
                        List<MissionMedium> missionMedia = _DbContext.MissionMedia.Where(a => a.MissionId == mission.MissionId && a.MediaType == "mp4").ToList();
                        foreach (var missionMediaItem in missionMedia)
                        {
                            _DbContext.MissionMedia.Remove(missionMediaItem);
                            _DbContext.SaveChanges();
                        }

                        MissionMedium medium = new MissionMedium();
                        medium.MissionId = mission.MissionId;
                        medium.MediaName = model.VideoUrl;
                        medium.MediaType = "mp4";
                        medium.MediaPath = model.VideoUrl;
                        _DbContext.MissionMedia.Add(medium);
                        _DbContext.SaveChanges();
                    }

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
            var goalMission = _DbContext.GoalMissions.Where(g=> g.MissionId == MissionId).FirstOrDefault();

            if (goalMission != null)
            {
                mission.GoalMissionId = goalMission.GoalMissionId;
                mission.GoalText = goalMission.GoalObjectiveText;
                mission.GoalValue = goalMission.GoalValue;
            }
            else
            {
                mission.StartDateEdit = mission.StartDate.Value.ToString("yyyy-MM-dd");
                mission.EndDateEdit = mission.EndDate.Value.ToString("yyyy-MM-dd");
            }

            mission.EditDeadline = mission.Deadline.Value.ToString("yyyy-MM-dd");
            var skill = _DbContext.MissionSkills.Where(u => u.MissionId == MissionId).ToList();
            var images = _DbContext.MissionMedia.Where(m => m.MissionId == MissionId).ToList();

            if (images.Count > 0)
            {
                var path = new List<string>();
                foreach (var i in images)
                {
                    if (i.MediaType == "png")
                    {
                        string path1 = i.MediaName;

                        path.Add(path1);
                    }

                }
                mission.MissionImages = path;
                
                var url = new List<string>();
                foreach (var i in images)
                {
                    if (i.MediaType == "mp4")
                    {
                        string path1 = i.MediaName;

                        url.Add(path1);
                    }

                }
                if (url.Count > 0)
                {
                    mission.VideoUrl = url[0];
                }
            }

            if (skill.Count > 0)
            {
                List<string> list = new List<string>();
                foreach (var skillItem in skill)
                {
                    var skillName = _DbContext.Skills.FirstOrDefault(s => s.SkillId == skillItem.SkillId).SkillName;
                    list.Add(skillName);
                }
                mission.skillNames = list;
            }
            return mission;
        }

        public bool DeleteMission(long MissionId)
        {
            var mission = _DbContext.Missions.Find(MissionId);
            mission.DeletedAt = DateTime.Now;
            _DbContext.Missions.Update(mission);
            _DbContext.SaveChanges();
            return true;
        }

        public bool AddTheme(MissionTheme model)
        {
            if (model.MissionThemeId == 0)
            {

                MissionTheme missionTheme = new MissionTheme();
                {
                    missionTheme.Titile = model.Titile;
                    missionTheme.Status = model.Status;

                    _DbContext.MissionThemes.Add(missionTheme);
                    _DbContext.SaveChanges();
                }
                return true;
            }
            else
            {
                var theme = _DbContext.MissionThemes.Find(model.MissionThemeId);
                if (theme != null)
                {
                    theme.Titile = model.Titile;
                    theme.Status = model.Status;
                    theme.UpdatedAt = DateTime.Now;

                    _DbContext.MissionThemes.Update(theme);
                    _DbContext.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public MissionTheme EditTheme(long ThemeId)
        {
            var theme = _DbContext.MissionThemes.Where(c => c.MissionThemeId == ThemeId).FirstOrDefault();


            return theme;
        }

        public bool DeleteTheme(long ThemeId)
        {
            var theme = _DbContext.MissionThemes.Find(ThemeId);
            theme.DeletedAt = DateTime.Now;
            _DbContext.MissionThemes.Update(theme);
            _DbContext.SaveChanges();
            return true;
        }
        
        public bool AddSkill(Skill model)
        {
            if (model.SkillId == 0)
            {

                Skill skill = new Skill();
                {
                    skill.SkillName = model.SkillName;
                    skill.Status = model.Status;

                    _DbContext.Skills.Add(skill);
                }
                    _DbContext.SaveChanges();
                return true;
            }
            else
            {
                var skill = _DbContext.Skills.Find(model.SkillId);
                if (skill != null)
                {
                    skill.SkillName = model.SkillName;
                    skill.Status = model.Status;
                    skill.UpdatedAt = DateTime.Now;

                    _DbContext.Skills.Update(skill);
                    _DbContext.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public Skill EditSkill(long SkillId)
        {
            var skill = _DbContext.Skills.Where(c => c.SkillId == SkillId).FirstOrDefault();


            return skill;
        }

        public bool DeleteSkill(long SkillId)
        {
            var skill = _DbContext.Skills.Where(s=> s.SkillId == SkillId).FirstOrDefault();
            skill.DeletedAt = DateTime.Now;
            _DbContext.Skills.Update(skill);
            _DbContext.SaveChanges();
            return true;
        }
        
        public bool StatusChangeApplication(long MissionApplicationId, string Result)
        {
            var missionApplication = _DbContext.MissionApplications.Where(s=> s.MissionApplicationId == MissionApplicationId).FirstOrDefault();
            var mission = _DbContext.Missions.Where(m=> m.MissionId == missionApplication.MissionId).FirstOrDefault();
            if (Result == "true")
            {
                missionApplication.ApprovalStatus = "Approve";
                mission.TotalSeats = mission.TotalSeats - 1;
                _DbContext.Missions.Update(mission);
            }
            else
            {
                if(missionApplication.ApprovalStatus == "Approve")
                {
                    mission.TotalSeats = mission.TotalSeats + 1;
                    _DbContext.Missions.Update(mission);
                }
                missionApplication.ApprovalStatus = "Decline";
            }
            _DbContext.MissionApplications.Update(missionApplication);
            _DbContext.SaveChanges();

            Notification notification = new Notification();
            {
                notification.MissionId = missionApplication.MissionId;
                notification.UserId = missionApplication.UserId;
                if (missionApplication.ApprovalStatus == "Approve")
                {
                    notification.Text = "Approved Mission : " + mission.Title;
                }
                else
                {
                    notification.Text = "Declined Mission : " + mission.Title;
                }
                notification.Status = "Unseen";
            }
            _DbContext.Notifications.Add(notification);
            _DbContext.SaveChanges();
            return true;
        }
        
        public bool StatusChangeStory(long StoryId, string Result)
        {
            var story = _DbContext.Stories.Where(s=> s.StoryId == StoryId).FirstOrDefault();
            if (Result == "true")
            {
                story.Status = "PUBLISHED";
            }
            else
            {
                story.Status = "Decline";
            }
            _DbContext.Stories.Update(story);
            _DbContext.SaveChanges();

            Notification notification = new Notification();
            {
                notification.StoryId = story.StoryId;
                notification.UserId = story.UserId;
                if (story.Status == "PUBLISHED")
                {
                    notification.Text = "Published Story : " + story.Title;
                }
                else
                {
                    notification.Text = "Declined Story : " + story.Title;
                }
                notification.Status = "Unseen";
            }
            _DbContext.Notifications.Add(notification);
            _DbContext.SaveChanges();

            return true;
        }

        public bool DeleteStory(long StoryId)
        {
            var story = _DbContext.Stories.Where(s => s.StoryId == StoryId).FirstOrDefault();
            story.DeletedAt = DateTime.Now;
            _DbContext.Stories.Update(story);
            _DbContext.SaveChanges();
            return true;
        }

        public bool AddBanner(Banner model)
        {
            if (model.BannerId == 0)
            {

                Banner banner = new Banner();
                {
                    banner.Text = model.Text;
                    banner.SortOrder = model.SortOrder;

                    if (model.BannerImg != null)
                    {
                        banner.Image = model.BannerImg.FileName;

                        _DbContext.Banners.Add(banner);
                        _DbContext.SaveChanges();
                    
                       
                        //string path = Server.MapPath("~/wwwroot/Assets/Story");
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Assets/StoryImages", model.BannerImg.FileName);
                       
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            model.BannerImg.CopyTo(stream);
                        }
                        return true;
                    }

                }
                return false;

            }
            else
            {
                var banner = _DbContext.Banners.Find(model.BannerId);
                if (banner != null)
                {

                    banner.Text = model.Text;
                    banner.SortOrder = model.SortOrder;

                    if (model.BannerImg != null)
                    {
                        banner.Image = model.BannerImg.FileName;
                    }
                    banner.UpdatedAt = DateTime.Now;

                    _DbContext.Banners.Update(banner);
                    _DbContext.SaveChanges();

                    if (model.BannerImg != null)
                    {

                        //string path = Server.MapPath("~/wwwroot/Assets/Story");
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Assets/StoryImages", model.BannerImg.FileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            model.BannerImg.CopyTo(stream);
                        }
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public Banner EditBanner(long BannerId)
        {
            var banner = _DbContext.Banners.Where(b => b.BannerId == BannerId).FirstOrDefault();

            return banner;
        }

        public bool DeleteBanner(long BannerId)
        {
            var banner = _DbContext.Banners.Where(s => s.BannerId == BannerId).FirstOrDefault();
            banner.DeletedAt = DateTime.Now;
            _DbContext.Banners.Update(banner);
            _DbContext.SaveChanges();
            return true;
        }

        public bool StatusChangeComment(long CommentId, string Result)
        {
            var missionComment = _DbContext.Comments.Where(s => s.CommentId == CommentId).FirstOrDefault();
            if (Result == "true")
            {
                missionComment.ApprovalStatus = "Approve";
            }
            else
            {
                missionComment.ApprovalStatus = "Decline";
            }
            _DbContext.Comments.Update(missionComment);
            _DbContext.SaveChanges();
            return true;
        }

        public bool AddUser(User model) 
        {
            if (model.UserId == 0)
            {

                User user = new User();
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.CityId = model.CityId;
                    user.CountryId = model.CountryId;
                    user.Email = model.Email;
                    user.Password = Crypto.HashPassword(model.Password);
                    user.EmployeeId = model.EmployeeId;
                    user.Department = model.Department;
                    user.Role = model.Role;
                    user.ProfileText = model.ProfileText;
                    if(model.UserImg != null)
                    user.Avatar = model.UserImg.FileName;

                    _DbContext.Users.Add(user);
                    _DbContext.SaveChanges();
                }

                if (model.UserImg != null)
                {

                    //string path = Server.MapPath("~/wwwroot/Assets/Story");
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Assets/StoryImages", model.UserImg.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        model.UserImg.CopyTo(stream);
                    }
                }

                return true;
            }
            else
            {
                var user = _DbContext.Users.Find(model.UserId);
                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.CityId = model.CityId;
                    user.CountryId = model.CountryId;
                    user.Email = model.Email;
                    user.Status = model.Status;
                    user.Password = model.Password;
                    user.EmployeeId = model.EmployeeId;
                    user.Department = model.Department;
                    user.Role = model.Role;
                    user.ProfileText = model.ProfileText;
                    if (model.UserImg != null)
                        user.Avatar = model.UserImg.FileName;
                    user.UpdatedAt = DateTime.Now;

                    _DbContext.Users.Update(user);
                    _DbContext.SaveChanges();

                    if (model.UserImg != null)
                    {

                        //string path = Server.MapPath("~/wwwroot/Assets/Story");
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Assets/StoryImages", model.UserImg.FileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            model.UserImg.CopyTo(stream);
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public User EditUser(long UserId)
        {
            var user = _DbContext.Users.Where(b => b.UserId == UserId).FirstOrDefault();

            return user;
        }

        public bool DeleteUser(long UserId)
        {
            var user = _DbContext.Users.Where(s => s.UserId == UserId).FirstOrDefault();
            user.DeletedAt = DateTime.Now;
            _DbContext.Users.Update(user);
            _DbContext.SaveChanges();
            return true;
        }

    }
}
