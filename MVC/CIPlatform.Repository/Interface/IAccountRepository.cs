using CIPlatform.Entities.Models;
using CIPlatform.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Interface
{
    public interface IAccountRepository
    {
        public List<User> UserList();

        public List<Mission> MissionList();

        public List<MissionTheme> ThemeList();

        public List<Skill> SkillList();

        public AdminModel adminModelList();

        public List<User> UserListSearch(string search, int pg);

        public List<Mission> MissionListSearch(string search, int pg);

        public List<MissionTheme> ThemeListSearch(string search, int pg);

        public List<Skill> SkillListSearch(string search, int pg);
    }
}
