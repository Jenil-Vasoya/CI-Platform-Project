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


        public List<MissionData> GetMissionList(string? search, string[] countries, string[] cities, string[] themes, string[] skills, int sort)
        {
            List<MissionData> missions = GetMissionCardsList();
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
                missionData.MissionType = objMission.MissionType;

                if (_DbContext.GoalMissions.Where(x => missionData.MissionId.Equals(x.MissionId)).Count() != 0)
                {
                    missionData.MissionGoalText = GetGoalMissionData(objMission.MissionId).GoalObjectiveText;
                    missionData.GoalValue1 = GetGoalMissionData(objMission.MissionId).GoalValue;

                }


                missionData.StartDate = objMission.StartDate;
                missionData.EndDate = objMission.EndDate;
              

                missionData.MediaPath = MediaByMissionId(objMission.MissionId);
                missionData.Title = objMission.Title;
                missionData.CreatedAt = objMission.CreatedAt;

                missionData.Rating = MissionRatings(objMission.MissionId);
                missionData.Theme = GetMissionThemes(objMission.MissionThemeId);
                missionData.Availability = objMission.Availability;
                
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

        public int CheckFavMission(long userId, long missionId)
        {
            if (_DbContext.FavoriteMissions.Any(a=> a.UserId == userId && a.MissionId == missionId))
            {
                return 1;
            }
            return 0;
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

        public void ApplyMission(long UserId, long MissionId)
        {
          MissionApplication missionApplication = new MissionApplication();
            missionApplication.UserId = UserId;
            missionApplication.MissionId = MissionId;
            missionApplication.AppliedAt = DateTime.Now;

            _DbContext.MissionApplications.Add(missionApplication);
            _DbContext.SaveChanges();


        }

    }
}
