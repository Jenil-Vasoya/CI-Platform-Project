using CIPlatform.Entities.Models;
using CIPlatform.Entities.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Interface
{
    public interface IUserRepository
    {
        public User GetUserAvatar(long UserId);

        public List<User> UserList();

        public Boolean Register(User objUser);

        public Boolean IsEmailAvailable(string email);

        public User IsPasswordAvailable(string password, string email);

        public long GetUserID(string Email);

        public bool ResetPassword(long userId, string OldPassword, string NewPassword);

        public Boolean ChangePassword(long UserId, Reset_Password model);

        public UserData GetUserlist(long UserId);

        public List<Country> GetCountryList();

        public List<City> GetCities();

        public List<Skill> GetSkills();

        public List<UserSkill> GetUserSkills(long UserId);

        public List<City> CityList(long CountryID);

        public bool ChangePasswordUser(long UserId, string OldPassword, string Password);

        public bool ChangeSkills(List<long> skills, long UserId);

        public bool EditAvatar(IFormFile Profileimg, long UserId);

        public bool EditProfile(UserData userData, long UserId);
    }
}
