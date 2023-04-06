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

        public List<MissionData> GetStoryMissionList(string? search, string[] countries, string[] cities, string[] themes, string[] skills, int pg, long UserId)
        {
            var pageSize = 6;
            List<MissionData> missions = GetStoryCardsList(UserId);
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

        public List<MissionData> GetStoryCardsList(long UserId)
        {
            var missions = _DbContext.Stories.Where(a => a.Status == "PUBLISHED" || (a.UserId == UserId && a.Status != "PENDING")).OrderBy(a => a.Status).ToList();


            

            List<MissionData> missionDatas = new List<MissionData>();

            foreach (var objMission in missions)
            {
                MissionData missionData = new MissionData();
                List<StoryMedium> storyMedium = _DbContext.StoryMedia.Where(a => a.StoryId == objMission.StoryId).ToList();

                var path = new List<string>();
                foreach (var i in storyMedium)
                {
                    StoryMedium story = new StoryMedium();
                    if (i.Type == "png")
                    {
                        string path1 = i.Path;

                        path.Add(path1);
                    }

                }
                missionData.StoryImages = path;
                if(path.Count > 0)
                missionData.MediaPath = path[0];


                var Videopath = new List<string>();
                foreach (var i in storyMedium)
                {
                    StoryMedium story = new StoryMedium();
                    if (i.Type == "mp4")
                    {
                        string path1 = i.Path;

                        Videopath.Add(path1);
                    }
                }
                if (Videopath.Count != 0)
                {
                    missionData.VideoUrl = Videopath;
                }
                else
                {
                    missionData.VideoUrl = null;
                }

                //var formFiles = new List<IFormFile>();

                //foreach (var imagePath in path)
                //{
                //    var fileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
                //    var fileInfo = fileProvider.GetFileInfo(imagePath);

                //    if (fileInfo.Exists)
                //    {
                //        var stream = fileInfo.CreateReadStream();
                //        var formFile = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(imagePath))
                //        {
                //            Headers = new HeaderDictionary(),
                //            ContentType = "image/png" // set the content type to the appropriate value for your image type
                //        };

                //        formFiles.Add(formFile);
                //    }
                //}

                //missionData.images = formFiles;



                User user = _DbContext.Users.FirstOrDefault(a => a.UserId == objMission.UserId);

                Mission mission = _DbContext.Missions.FirstOrDefault(a => a.MissionId == objMission.MissionId);

                missionData.StoryId = objMission.StoryId;
                missionData.WhyIVolunteer = user.WhyIvolunteer;

                missionData.MissionId = objMission.MissionId;

                missionData.CityName = GetCityName(mission.CityId);

                missionData.OrganizationName = mission.OrganizationName;
                missionData.ShortDescription = mission.ShortDescription;
                missionData.MissionType = mission.MissionType;
                missionData.CreatedAt = objMission.PublishedAt;

                missionData.UserName = user.FirstName + " " + user.LastName;
                missionData.Avatar = user.Avatar;
                missionData.Views = _DbContext.StoryViews.Where(a=> a.StoryId == objMission.StoryId).Count();

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

        public void StoryView(long StoryId, long UserId)
        {

            var view1 = _DbContext.StoryViews.FirstOrDefault(s => s.StoryId == StoryId && s.UserId == UserId);
            if (view1 == null)
            {
                StoryView sv = new StoryView();
                sv.UserId = UserId;
                sv.StoryId = StoryId;
                _DbContext.StoryViews.Add(sv);
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

        public long AddData(MissionData objStory, long UserId, string btn)
        {
            var editStory = _DbContext.Stories.Where(a=> a.StoryId == objStory.StoryId).FirstOrDefault();
            if (editStory != null)
            {
                editStory.UserId = UserId;
                editStory.Views = 0;
                editStory.Title = objStory.Title;
                editStory.Description = objStory.Description;
                editStory.PublishedAt = objStory.CreatedAt;
                editStory.MissionId = objStory.MissionId;
                if (btn == "Submit")
                {
                    editStory.Status = "PENDING";
                }
                _DbContext.Stories.Update(editStory);
                _DbContext.SaveChanges();

                List<StoryMedium> storyMedia = _DbContext.StoryMedia.Where(a => a.StoryId == editStory.StoryId).ToList();
                foreach (var storyMediaItem in storyMedia)
                {
                    _DbContext.StoryMedia.Remove(storyMediaItem);
                    _DbContext.SaveChanges();
                }




                var filePath = new List<string>();
                foreach (var i in objStory.images)
                {
                    StoryMedium storyMedium = new StoryMedium();
                    storyMedium.StoryId = editStory.StoryId;
                    storyMedium.Type = "png";
                    storyMedium.Path = i.FileName;
                    _DbContext.StoryMedia.Add(storyMedium);
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
                if (objStory.VideoUrl != null)
                {
                    foreach (var i in objStory.VideoUrl)
                    {
                        StoryMedium storyMediumurl = new StoryMedium();
                        storyMediumurl.StoryId = editStory.StoryId;
                        storyMediumurl.Type = "mp4";
                        storyMediumurl.Path = i;
                        _DbContext.StoryMedia.Add(storyMediumurl);
                    }
                    _DbContext.SaveChanges();
                }

                _DbContext.SaveChanges();
                return editStory.StoryId;

            }
            else
            {
                Story story = new Story();
                {
                    story.UserId = UserId;
                    story.Views = 0;
                    story.Title = objStory.Title;
                    story.Description = objStory.Description;
                    story.PublishedAt = objStory.CreatedAt;
                    story.MissionId = objStory.MissionId;
                    if (btn == "Submit")
                    {
                        story.Status = "PENDING";
                    }
                }

                _DbContext.Stories.Add(story);
                _DbContext.SaveChanges();




                var filePath = new List<string>();
                foreach (var i in objStory.images)
                {
                    StoryMedium storyMedium = new StoryMedium();
                    storyMedium.StoryId = story.StoryId;
                    storyMedium.Type = "png";
                    storyMedium.Path = i.FileName;
                    _DbContext.StoryMedia.Add(storyMedium);
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

                if (objStory.VideoUrl[0] != null)
                {
                    foreach (var i in objStory.VideoUrl)
                    {
                        StoryMedium storyMediumurl = new StoryMedium();
                        storyMediumurl.StoryId = story.StoryId;
                        storyMediumurl.Type = "mp4";
                        storyMediumurl.Path = i;
                        _DbContext.StoryMedia.Add(storyMediumurl);
                    }
                    _DbContext.SaveChanges();
                }


                _DbContext.SaveChanges();
                return story.StoryId;

            }

              
            
        }
        public bool InviteWorker(List<long> CoWorker, long UserId, long StoryId)
        {
            foreach (var user in CoWorker)
            {
                _DbContext.StoryInvites.Add(new StoryInvite
                {
                    FromUserId = UserId,
                    ToUserId = Convert.ToInt64(user),
                    StoryId = StoryId
                });
            }
            _DbContext.SaveChanges();

            User? from_user = _DbContext.Users.FirstOrDefault(c => c.UserId.Equals(UserId));
            List<string> Email_users = (from u in _DbContext.Users
                                        where CoWorker.Contains(u.UserId)
                                        select u.Email).ToList();
            foreach (var email in Email_users)
            {
                var senderEmail = new MailAddress("josephlal3eie@gmail.com", "CI-Platform");
                var receiverEmail = new MailAddress(email, "Receiver");
                var password = "oxdijqngsiewiooq";
                var sub = "Recommendation for see Mission Story";
                var body = "Recommend By " + from_user?.FirstName + " " + from_user?.LastName + "\n" + "You can join through below link" + "\n" + $"https://localhost:7097/Home/VolunteerMission/{StoryId}";
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

    }
}
