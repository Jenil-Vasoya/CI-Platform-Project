using CIPlatform.Entities.Data;
using CIPlatform.Entities.Models;
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

        public List<Admin> Index()
        {
            List<Admin> objAdminList = _DbContext.Admins.ToList();
            return objAdminList;
        }

    }
}
