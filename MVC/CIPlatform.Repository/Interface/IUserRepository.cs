using CIPlatform.Entities.Models;
using CIPlatform.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Interface
{
    public interface IUserRepository
    {
        public List<User> UserList();

        
        public Boolean Register(User objUser);

        public Boolean IsEmailAvailable(string email);

        public User IsPasswordAvailable(string password, string email);

        public long GetUserID(string Email);

        public bool ResetPassword(long userId, string OldPassword, string NewPassword);

        public Boolean ChangePassword(long UserId, Reset_Password model);
    }
}
