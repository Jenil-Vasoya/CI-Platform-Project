using CIPlatform.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entities.ViewModel
{
    public class UserData
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? WhyIvolunteer { get; set; }

        public string? EmployeeId { get; set; }

        public string? Department { get; set; }

        public long? CityId { get; set; }

        public long? CountryId { get; set; }

        public string? ProfileText { get; set; }

        public string? LinkedInUrl { get; set; }

        public string? Title { get; set; }

        public bool? Status { get; set; }

        public List<UserSkill>? userSkill { get; set; }

        public string OldPassword { get; set; }

        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }

        public long UserId { get; set; }

        public string Email { get; set; }

        public int PhoneNumber { get; set; }

        public string? Avatar { get; set; }

        public List<Country> CountryList { get; set; }

        public List<City> CityList { get; set; }

        public List<Skill> SkillList { get; set; }
    }
}
