using CIPlatform.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Interface
{
    public interface IHomeRepository
    {

        public List<Country> CountryList();

        public List<City> CityList(int CountryID);

        public List<MissionTheme> MissionThemeList();

        public List<Skill> SkillList();

    }
}
