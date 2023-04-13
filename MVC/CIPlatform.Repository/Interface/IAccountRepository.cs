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

        public AdminModel adminModelList();

        public List<User> UserListSearch(string search, int pg);
    }
}
