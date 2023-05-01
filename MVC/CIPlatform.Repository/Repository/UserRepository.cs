using CIPlatform.Entities.Data;
using CIPlatform.Entities.Models;
using CIPlatform.Entities.ViewModel;
using CIPlatform.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace CIPlatform.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        public readonly CIPlatformDbContext _DbContext;

        public UserRepository(CIPlatformDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public User GetUserAvatar(long UserId)
        {
            var user = _DbContext.Users.FirstOrDefault(i => i.UserId == UserId && i.DeletedAt == null);
            return user ;
        }

        public List<User> UserList()
        {
            List<User> objUserList = _DbContext.Users.Where(u=> u.DeletedAt == null).ToList();
            return objUserList;
        }
        
        public List<Banner> BannerList()
        {
            List<Banner> objBannerList = _DbContext.Banners.Where(u=> u.DeletedAt == null).ToList();
            return objBannerList;
        }

    

        public Boolean IsEmailAvailable(string email)
        {
            return _DbContext.Users.Any(u => u.Email == email && u.DeletedAt == null);
        }

        public User IsPasswordAvailable(string password, string email)
        {
            return _DbContext.Users.Where(u => u.Password == password && u.Email == email && u.DeletedAt == null).FirstOrDefault();
        }



        public long GetUserID(string Email)
        {
            User user = _DbContext.Users.Where(u => u.Email == Email && u.DeletedAt == null ).FirstOrDefault();
            if (user == null)
            {

                return -1;
            }
            else
            {

                return user.UserId;
            }
        }

       
        public bool ResetPassword(long userId, string OldPassword, string NewPassword)
        {
            try
            {
                User user = _DbContext.Users.Find(userId);
                string pass = (user.Password);
                if (pass == OldPassword)
                {
                    user.Password = (NewPassword);
                    _DbContext.Users.Update(user);
                    _DbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public Boolean ChangePassword(long UserId, Reset_Password model)
        {
            User user = _DbContext.Users.FirstOrDefault(x => x.UserId == model.UserId);
            user.Password = Crypto.HashPassword(model.Password); ;
            user.UpdatedAt = DateTime.Now;
            _DbContext.Users.Update(user);
            _DbContext.SaveChanges();
            return true;

        }
        public User GetUser(int userID)
        {

            User user = _DbContext.Users.Where(u => u.UserId == userID && u.DeletedAt == null).FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            else
            {

                return null;

            }

        }

        public bool Register(User objUser)
        {
            if(_DbContext.Users.Any(u => u.Email != objUser.Email))
            {
                objUser.Password = Crypto.HashPassword(objUser.Password);
                _DbContext.Users.Add(objUser);
                _DbContext.SaveChanges();
                return true;
            }
            return false;

        }

        public void Login(User objLogin)
        {
            if (_DbContext.Users.Any(u => u.Email == objLogin.Email && u.Password == objLogin.Password && u.DeletedAt == null))
            {

            }

        }

        public UserData GetUserlist(long UserId)
        {
            var getuser = _DbContext.Users.FirstOrDefault(u=> u.UserId == UserId && u.DeletedAt == null);
            if (getuser != null)
            {
                UserData userData = new UserData();
                userData.UserId = getuser.UserId;
                userData.FirstName = getuser.FirstName;
                userData.LastName = getuser.LastName;
                userData.Email = getuser.Email;
                userData.PhoneNumber = getuser.PhoneNumber;
                userData.Avatar = getuser.Avatar;
                userData.WhyIvolunteer = getuser.WhyIvolunteer;
                userData.EmployeeId = getuser.EmployeeId;
                userData.Department = getuser.Department;
                userData.CityId = getuser.CityId;
                userData.CountryId = getuser.CountryId;
                userData.ProfileText = getuser.ProfileText;
                userData.LinkedInUrl = getuser.LinkedInUrl;
                userData.Title = getuser.Title;
                userData.Status = getuser.Status;

                userData.CountryList = _DbContext.Countries.ToList();
                userData.CityList = _DbContext.Cities.ToList();
                userData.SkillList = _DbContext.Skills.ToList();
                userData.userSkill = _DbContext.UserSkills.Where(u => u.UserId == UserId).ToList();
                return userData;
            }
            return null;
        }

        public List<Country> GetCountryList()
        {
            var getCountry = _DbContext.Countries.ToList();
            return getCountry;
        }

        public List<City> GetCities()
        {
            var getcity = _DbContext.Cities.ToList();
            return getcity;
        }
        
        public List<Skill> GetSkills()
        {
            var getskills = _DbContext.Skills.Where(s=> s.DeletedAt == null).ToList();
            return getskills;
        }
        
        public List<UserSkill> GetUserSkills(long UserId)
        {
            var getUserSkills = _DbContext.UserSkills.Where(u=> u.UserId == UserId && u.DeletedAt == null).ToList();
            return getUserSkills;
        }

        public List<City> CityList(long CountryID)
        {
            List<City> objCityList = _DbContext.Cities.Where(i => i.CountryId == CountryID).ToList();
            return objCityList;
        }

        public bool ChangePasswordUser(long UserId, string OldPassword, string Password)
        {
            User user = _DbContext.Users.FirstOrDefault(x => x.UserId == UserId);
            if (Crypto.VerifyHashedPassword(user.Password, OldPassword))
            {
                user.Password = Crypto.HashPassword(Password);
                user.UpdatedAt = DateTime.Now;
                _DbContext.Users.Update(user);
                _DbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool ChangeSkills(List<long> skills, long UserId)
        {
            List<UserSkill> userSkills = _DbContext.UserSkills.Where(a => a.UserId == UserId && a.DeletedAt == null).ToList();
            foreach (var userSkill in userSkills)
            {
                _DbContext.UserSkills.Remove(userSkill);
                _DbContext.SaveChanges();
            }

            foreach (var skill in skills)
            {
                UserSkill userSkill = new UserSkill();
                {
                    userSkill.UserId = UserId;
                    userSkill.SkillId = (int)skill;
                    userSkill.UpdatedAt = DateTime.Now;

                    _DbContext.UserSkills.Add(userSkill);
                    _DbContext.SaveChanges();

                }

            }
            return true;

        }

        public bool EditAvatar(IFormFile Profileimg, long UserId)
        {
            var CheckProfile = _DbContext.Users.FirstOrDefault(x => x.UserId == UserId && x.DeletedAt == null);


            // Delete the image file from the file system
            
            var filepath = Path.Combine("wwwroot/Assets/StoryImages", Profileimg.FileName);
            using (var filestream = new FileStream(filepath, FileMode.Create))
            {
                Profileimg.CopyTo(filestream);
            }

            CheckProfile.Avatar = Profileimg.FileName;

            _DbContext.Users.Update(CheckProfile);
            _DbContext.SaveChanges();

            return true;
        }

        public bool EditProfile(UserData userData, long UserId)
        {
            User getUser = _DbContext.Users.FirstOrDefault(u=> u.UserId == UserId && u.DeletedAt == null);
            if(getUser != null)
            {
                getUser.FirstName = userData.FirstName;
                getUser.LastName = userData.LastName;   
               // getUser.Avatar = userData.Avatar;
                getUser.WhyIvolunteer = userData.WhyIvolunteer;
                getUser.EmployeeId = userData.EmployeeId;
                getUser.Department = userData.Department;
                getUser.CityId = userData.CityId;
                getUser.CountryId = userData.CountryId;
                getUser.ProfileText = userData.ProfileText;
                getUser.LinkedInUrl = userData.LinkedInUrl;
                getUser.Title = userData.Title;
                getUser.UpdatedAt = DateTime.Now;

                _DbContext.Users.Update(getUser);
                _DbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<VolunteerTimeSheet> GetVolunteerSheetData(long UserId)
        {

            List<VolunteerTimeSheet> volunteerSheets = new List<VolunteerTimeSheet>();

            var timeSheet = _DbContext.TimeSheets.Where(u => u.UserId == UserId && u.DeletedAt == null).ToList();

            foreach (var sheet in timeSheet)
            {

                VolunteerTimeSheet volunteerSheet = new VolunteerTimeSheet();
                volunteerSheet.TimesheetId = sheet.TimeSheetId;
                volunteerSheet.UserId = sheet.UserId;
                volunteerSheet.MissionId = sheet.MissionId;
                volunteerSheet.MissionTitle = _DbContext.Missions.Where(m => m.MissionId == sheet.MissionId).FirstOrDefault().Title;
                volunteerSheet.Time = sheet.Time.ToString();
                volunteerSheet.Status = sheet.Status;
                var newdate = sheet.DateVolunteered;
                var year = newdate.Year;
                var month = newdate.Month.ToString("D2");
                var day = newdate.Day.ToString("D2");
                volunteerSheet.DateVolunteered = year+"-"+month + "-" + day;
                volunteerSheet.Notes = sheet.Notes;

                if (sheet.Time != null)
                {
                    TimeSpan timeSpanValue = TimeSpan.Parse(sheet.Time.ToString() ?? "");
                    volunteerSheet.Hours = timeSpanValue.Hours;
                    volunteerSheet.Minutes = timeSpanValue.Minutes;
                }
                else
                {
                    volunteerSheet.Action = sheet.Action;
                }
                

                volunteerSheets.Add(volunteerSheet);
            }
            
            return volunteerSheets;
        }

        public List<Mission> UserAppliedMissionList(long UserId)
        {
            var validUser = _DbContext.MissionApplications.Where(a => a.UserId == UserId && a.ApprovalStatus == "Approve" && a.DeletedAt == null).ToList();

            var list = new List<long>();

            foreach (var app in validUser)
            {
                list.Add(app.MissionId);
            }

            var missions = _DbContext.Missions.Where(a => list.Contains(a.MissionId) && a.DeletedAt == null).ToList();

            return missions;
        }

        public bool AddTimeSheet(VolunteerTimeSheet volunteerSheet)
        {
            TimeSheet timeSheet = new TimeSheet();
            {
                timeSheet.UserId = volunteerSheet.UserId;
                timeSheet.MissionId = volunteerSheet.MissionId;
                if(volunteerSheet.Hours != 0 || volunteerSheet.Minutes != 0)
                {
                    TimeSpan timeSpan = new TimeSpan(volunteerSheet.Hours, volunteerSheet.Minutes, 0);
                    timeSheet.Time = timeSpan;
                }
                else
                {
                    timeSheet.Action = volunteerSheet.Action;
                }
                DateTime dateTime = DateTime.Parse(volunteerSheet.DateVolunteered);
                timeSheet.DateVolunteered = dateTime;
                timeSheet.Notes = volunteerSheet.Notes;
                
                _DbContext.TimeSheets.Add(timeSheet);
                _DbContext.SaveChanges();

                return true;
            }
            
        }
        
        public bool EditTimeSheet(VolunteerTimeSheet volunteerSheet)
        {
            var timeSheet = _DbContext.TimeSheets.Where(t => t.TimeSheetId == volunteerSheet.TimesheetId && t.DeletedAt == null).FirstOrDefault();
            if(timeSheet != null)
            {
                timeSheet.MissionId = volunteerSheet.MissionId;
                if(volunteerSheet.Hours != 0 || volunteerSheet.Minutes != 0)
                {
                    TimeSpan timeSpan = new TimeSpan(volunteerSheet.Hours, volunteerSheet.Minutes, 0);
                    timeSheet.Time = timeSpan;
                }
                else
                {
                    timeSheet.Action = volunteerSheet.Action;
                }
                timeSheet.DateVolunteered = volunteerSheet.sendDateVolunteered;
                timeSheet.Notes = volunteerSheet.Notes;
                
                _DbContext.TimeSheets.Update(timeSheet);
                _DbContext.SaveChanges();

                return true;
            }
            return false;
            
        }
        
        public bool DeleteTimeSheet(long TimesheetId)
        {
            var timeSheet = _DbContext.TimeSheets.Where(t => t.TimeSheetId == TimesheetId && t.DeletedAt == null).FirstOrDefault();
            if(timeSheet != null)
            {
                _DbContext.TimeSheets.Remove(timeSheet);
                _DbContext.SaveChanges();

                return true;
            }
            return false;
            
        }

        public bool AddContactUs(ContactU model)
        {
            ContactU contactU = new ContactU();
            contactU.Message = model.Message;
            contactU.UserName = model.UserName;
            contactU.Email = model.Email;
            contactU.Subject = model.Subject;
            contactU.UserId = model.UserId;
            _DbContext.ContactUs.Add(contactU);
            _DbContext.SaveChanges();

            return true;
        }
       
    }
}
