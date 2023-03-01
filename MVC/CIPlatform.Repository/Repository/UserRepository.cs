using CIPlatform.Entities.Data;
using CIPlatform.Entities.Models;
using CIPlatform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        

     

        public void UserRegister(User objUser)
        {

            _DbContext.Users.Add(objUser);
            _DbContext.SaveChanges();
                
            
        }

    }
}
