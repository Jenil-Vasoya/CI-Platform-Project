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

namespace CIPlatform.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        public readonly CiPlatformContext _DbContext;

        public UserRepository(CiPlatformContext DbContext)
        {
            _DbContext = DbContext;
        }

        public List<User> UserList()
        {
            List<User> objUserList = _DbContext.Users.ToList();
            return objUserList;
        }

    

        public Boolean IsEmailAvailable(string email)
        {
            return _DbContext.Users.Any(u => u.Email == email);
        }

        public User IsPasswordAvailable(string password, string email)
        {
            return _DbContext.Users.Where(u => u.Password == password && u.Email == email).FirstOrDefault();
        }



        public long GetUserID(string Email)
        {
            User user = _DbContext.Users.Where(u => u.Email == Email).FirstOrDefault();
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
            user.Password = model.Password;
            user.UpdatedAt = DateTime.Now;
            _DbContext.Users.Update(user);
            _DbContext.SaveChanges();
            return true;

        }
        public User GetUser(int userID)
        {
            try
            {

                User user = _DbContext.Users.Where(u => u.UserId == userID).FirstOrDefault();
                if (user != null)
                {
                    return user;
                }
                else
                {

                    return null;

                }


            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public bool Register(User objUser)
        {
            if(_DbContext.Users.Any(u => u.Email != objUser.Email))
            {
                _DbContext.Users.Add(objUser);
                _DbContext.SaveChanges();
                return true;
            }
            return false;

            
                
            
        }

        public void Login(User objLogin)
        {
            if (_DbContext.Users.Any(u => u.Email == objLogin.Email && u.Password == objLogin.Password))
            {

            }

        }

        public UserData GetUserlist(long UserId)
        {
            var getuser = _DbContext.Users.FirstOrDefault(u=> u.UserId == UserId);
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
            var getskills = _DbContext.Skills.ToList();
            return getskills;
        }
        
        public List<UserSkill> GetUserSkills(long UserId)
        {
            var getUserSkills = _DbContext.UserSkills.Where(u=> u.UserId == UserId).ToList();
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
            if (user.Password == OldPassword)
            {
                user.Password = Password;
                user.UpdatedAt = DateTime.Now;
                _DbContext.Users.Update(user);
                _DbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool ChangeSkills(List<long> skills, long UserId)
        {
            List<UserSkill> userSkills = _DbContext.UserSkills.Where(a => a.UserId == UserId).ToList();
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

        public bool EditAvatar(string base64Image, long UserId)
        {
            User user = _DbContext.Users.FirstOrDefault(a => a.UserId == UserId);
            if(user != null)
            {
                base64Image = base64Image.Replace("data:image/png;base64,", "");
                byte[] imageBytes1 = Convert.FromBase64String(base64Image);

                user.Avatar = base64Image;
                _DbContext.Users.Update(user);
                _DbContext.SaveChanges();
            }
            
            base64Image = base64Image.Replace("data:image/png;base64,", "");

            byte[] imageBytes = Convert.FromBase64String(base64Image);

            using (var ms = new MemoryStream(imageBytes))
            {
                var image = Image.FromStream(ms);

            }
            return true;
        }
    }
}
