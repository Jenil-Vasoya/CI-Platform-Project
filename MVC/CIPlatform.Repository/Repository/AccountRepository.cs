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
    public  class AccountRepository : IAccountRepository
    {
        public readonly CiPlatformContext _DbContext;
        public AccountRepository(CiPlatformContext DbContext)
        {
            _DbContext = DbContext;
        }

        public List<User> UserList()
        {
            List<User> objUserList = _DbContext.Users.ToList();
            return objUserList;
        }
        
        public List<Mission> MissionList()
        {
            List<Mission> objMissionList = _DbContext.Missions.ToList();
            return objMissionList;
        }

        public AdminModel adminModelList()
        {
            AdminModel admins = new AdminModel();
            admins.users=UserList();

            return admins;
        }

        public List<User> UserListSearch(string search, int pg)
        {
            var pageSize = 6;
            List<User> users = _DbContext.Users.ToList();

            if(search != null)
            {
                users = users.Where(u => u.FirstName.ToLower().Contains(search)).ToList();
            }

            if (pg != 0)
            {
                users = users.Skip((pg - 1) * pageSize).Take(pageSize).Take(pageSize).ToList();
            }
            return users;
        }
    }
}
