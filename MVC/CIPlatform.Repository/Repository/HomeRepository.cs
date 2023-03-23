using CIPlatform.Entities.Data;
using CIPlatform.Entities.Models;
using CIPlatform.Entities.ViewModel;
using CIPlatform.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
            return (_DbContext.Missions.Where(x => x.Title.Contains(search)).ToList());
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

        public List<MissionRating> MissionRatings(long missionID)
        {
            List<MissionRating> rating = _DbContext.MissionRatings.Where(a => a.MissionId == missionID).ToList();
            return rating;
        }

        public List<MissionData> GetMissionList(string? search, string[] countries, string[] cities, string[] themes, string[] skills, int sort, long UserId)
        {
            List<MissionData> missions = GetMissionCardsList(UserId);
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

            switch (sort)
            {
                case 1:
                    missions = missions.OrderBy(i => i.CreatedAt).ToList();
                    break;

                case 2:
                    missions = missions.OrderByDescending(i => i.CreatedAt).ToList();
                    break;

                case 3:
                    missions = missions.OrderByDescending(i => i.EndDate).ToList();
                    break;

                case 4:
                    missions = missions.OrderBy(i => i.Availability).ToList();
                    break;

                case 5:
                    missions = missions.OrderByDescending(i => i.Availability).ToList();
                    break;

                case 6:
                    missions = missions.OrderBy(i => i.Availability).ToList();
                    break;


            }
            return missions;
        }


        public List<MissionData> GetStoryMissionList(string? search, string[] countries, string[] cities, string[] themes, string[] skills, int sort, long UserId)
        {
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

            switch (sort)
            {
                case 1:
                    missions = missions.OrderBy(i => i.CreatedAt).ToList();
                    break;

                case 2:
                    missions = missions.OrderByDescending(i => i.CreatedAt).ToList();
                    break;

                case 3:
                    missions = missions.OrderByDescending(i => i.EndDate).ToList();
                    break;

                case 4:
                    missions = missions.OrderBy(i => i.Availability).ToList();
                    break;

                case 5:
                    missions = missions.OrderByDescending(i => i.Availability).ToList();
                    break;

                case 6:
                    missions = missions.OrderBy(i => i.Availability).ToList();
                    break;


            }
            return missions;
        }

        public List<MissionData> GetMissionCardsList(long UserId)
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
                missionData.MissionType = objMission.MissionType;

                if (_DbContext.GoalMissions.Where(x => missionData.MissionId.Equals(x.MissionId)).Count() != 0)
                {
                    missionData.MissionGoalText = GetGoalMissionData(objMission.MissionId).GoalObjectiveText;
                    missionData.GoalValue1 = GetGoalMissionData(objMission.MissionId).GoalValue;

                }

                var favmission = _DbContext.FavoriteMissions.Where(a => a.UserId == UserId && a.MissionId == objMission.MissionId).Count();
                    missionData.IsFavMission = favmission;
               


                missionData.StartDate = objMission.StartDate;
                missionData.EndDate = objMission.EndDate;


                missionData.MediaPath = MediaByMissionId(objMission.MissionId);
                missionData.Title = objMission.Title;
                missionData.CreatedAt = objMission.CreatedAt;

                missionData.Rating = (int)MissionRatings(objMission.MissionId).Average(a => a.Rating);
                missionData.CommentByUser = MissionRatings(objMission.MissionId).Count();

                missionData.Theme = GetMissionThemes(objMission.MissionThemeId);
                missionData.Availability = objMission.Availability;

                missionData.MissionThemeId = objMission.MissionThemeId;
                missionData.CountryId = objMission.CountryId;
                missionData.CityId = objMission.CityId;
                
                var missionSkill = _DbContext.MissionSkills.FirstOrDefault(s => s.MissionId == objMission.MissionId);
                missionData.SkillId = missionSkill.SkillId;

               var skillName = _DbContext.Skills.FirstOrDefault(a=> a.SkillId == missionSkill.SkillId);
                missionData.SkillName = skillName.SkillName;

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


        public List<MissionData> GetStoryCardsList()
        {
            var missions = _DbContext.Stories.ToList();

            List<MissionData> missionDatas = new List<MissionData>();

            foreach (var objMission in missions)
            {
                MissionData missionData = new MissionData();

                User user = _DbContext.Users.FirstOrDefault(a => a.UserId == objMission.UserId);

                Mission mission = _DbContext.Missions.FirstOrDefault(a => a.MissionId == objMission.MissionId);

                missionData.MissionId = objMission.MissionId;

                missionData.CityName = GetCityName(mission.CityId);

                missionData.OrganizationName = mission.OrganizationName;
                missionData.ShortDescription = mission.ShortDescription;
                missionData.MissionType = mission.MissionType;

                missionData.UserName = user.FirstName + " " + user.LastName;
                missionData.Avatar = user.Avatar;

                missionData.MediaPath = MediaByMissionId(objMission.MissionId);
                missionData.Title = objMission.Title;


                missionData.Theme = GetMissionThemes(mission.MissionThemeId);

                missionData.MissionThemeId = mission.MissionThemeId;
                missionData.CountryId = mission.CountryId;
                missionData.CityId = mission.CityId;

                var missionSkill = _DbContext.MissionSkills.FirstOrDefault(s => s.MissionId == objMission.MissionId);
                missionData.SkillId = missionSkill.SkillId;

                var skillName = _DbContext.Skills.FirstOrDefault(a => a.SkillId == missionSkill.SkillId);
                missionData.SkillName = skillName.SkillName;

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

        public GoalMission GetGoalMissionData(long missionId)
        {
            GoalMission goalMission = _DbContext.GoalMissions.FirstOrDefault(a => a.MissionId == missionId);
            if (goalMission == null)
            {
                return null;
            }
            return goalMission;
        }

        public bool AddFavouriteMission(long userId, long missionId)
        {

            FavoriteMission favoriteMission = new FavoriteMission();
            favoriteMission.UserId = userId;
            favoriteMission.MissionId = missionId;

            var favmission = _DbContext.FavoriteMissions.FirstOrDefault(s => s.UserId == userId && s.MissionId == missionId);
            if (favmission == null)
            {
                _DbContext.FavoriteMissions.Add(favoriteMission);
                _DbContext.SaveChanges();
                return true;
            }
            else
            {
                _DbContext.FavoriteMissions.Remove(favmission);
                _DbContext.SaveChanges();
                return false;
            }
        }

        public bool CheckFavMission(long userId, long missionId)
        {
            if (_DbContext.FavoriteMissions.FirstOrDefault(a => a.UserId == userId && a.MissionId == missionId) != null)
            {
                return true;
            }
            return false;
        }

        public void AddComment(string comment, long UserId, long MissionId)
        {
            Comment commentNew = new Comment();
            commentNew.Comments = comment;
            commentNew.MissionId = MissionId;
            commentNew.UserId = UserId;

            _DbContext.Comments.Add(commentNew);
            _DbContext.SaveChanges();


        }


        public List<CommentViewModel> GetComment(long missionID)
        {
            List<Comment> comments = _DbContext.Comments.Where(a => a.MissionId == missionID && a.ApprovalStatus == "Approve").ToList();
            List<CommentViewModel> commentView = new List<CommentViewModel>();

            foreach (var comment in comments)
            {
                CommentViewModel commentViews = new CommentViewModel();

                User user = _DbContext.Users.FirstOrDefault(a => a.UserId == comment.UserId);

                commentViews.Comment = comment.Comments;
                commentViews.Month = comment.CreatedAt.ToString("MMMM");
                commentViews.Time = comment.CreatedAt.ToString("h:mm tt");
                commentViews.Day = comment.CreatedAt.Day.ToString();
                commentViews.WeekDay = comment.CreatedAt.DayOfWeek.ToString();
                commentViews.Year = comment.CreatedAt.Year.ToString();
                commentViews.Avatar = user.Avatar;
                commentViews.UserName = user.FirstName + " " + user.LastName;
                commentView.Add(commentViews);

            }
            return commentView;
        }

        public bool ApplyMission(long UserId, long MissionId)
        {
            if (_DbContext.MissionApplications.FirstOrDefault(a => a.UserId == UserId && a.MissionId == MissionId) != null)
            { return false; }

            else
            {
                MissionApplication missionApplication = new MissionApplication();
                missionApplication.UserId = UserId;
                missionApplication.MissionId = MissionId;
                missionApplication.AppliedAt = DateTime.Now;

                _DbContext.MissionApplications.Add(missionApplication);
                _DbContext.SaveChanges();
                return true;
            }

        }


        public List<MissionData> GetRecentVolunteer(long missionId)
        {
            List<MissionApplication> missionApplication = _DbContext.MissionApplications.Where(a => a.MissionId == missionId).ToList();


            List<MissionData> recentVolunteer = new List<MissionData>();

            foreach (MissionApplication application in missionApplication)
            {
                MissionData missionVolunteer = new MissionData();
                User user = _DbContext.Users.FirstOrDefault(a => a.UserId == application.UserId);
                missionVolunteer.MissionId = missionId;
                missionVolunteer.Avatar = user.Avatar;
                missionVolunteer.UserName = user.FirstName + " " + user.LastName;

                recentVolunteer.Add(missionVolunteer);
            }
            return recentVolunteer;
        }

        
        public bool InviteWorker(List<long> CoWorker, long UserId, long MissionId)
        {
            foreach (var user in CoWorker)
            {
                _DbContext.MissionInvites.Add(new MissionInvite
                {
                    FromUserId = UserId,
                    ToUserId = Convert.ToInt64(user),
                    MissionId = MissionId
                });
            }
            _DbContext.SaveChanges();

            User from_user = _DbContext.Users.FirstOrDefault(c => c.UserId.Equals(UserId));
            List<string> Email_users = (from u in _DbContext.Users
                                        where CoWorker.Contains(u.UserId)
                                        select u.Email).ToList();
            foreach (var email in Email_users)
            {
                var senderEmail = new MailAddress("josephlal3eie@gmail.com", "CI-Platform");
                var receiverEmail = new MailAddress(email, "Receiver");
                var password = "oxdijqngsiewiooq";
                var sub = "Recommendation for join a new Mission";
                var body = "Recommend By " + from_user?.FirstName + " " + from_user?.LastName + "\n" + "You can join through below link" + "\n" + $"https://localhost:7097/Home/VolunteerMission/{MissionId}";
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = sub,
                    Body = body
                })
                {
                    smtp.Send(mess);
                }
            }
            return true;
        }

        public bool PostRating(byte rate, long missionId, long userId)
        {
            var entry = _DbContext.MissionRatings.Where(m => m.MissionId == missionId && m.UserId == userId);

            if (entry.ToList().Count == 0)
            {
                var data = new MissionRating()
                {
                    UserId = userId,
                    MissionId = missionId,
                    Rating = rate
                };
                _DbContext.MissionRatings.Add(data);
                _DbContext.SaveChanges();

                return true;
            }
            else
            {
                var data = new MissionRating()
                {
                    Rating = rate
                };
                entry.First().Rating = rate;
                entry.First().UpdatedAt = DateTime.Now;
                _DbContext.SaveChanges();

                return true;
            }

        }

    }
}
