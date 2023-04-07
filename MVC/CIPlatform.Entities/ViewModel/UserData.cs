using CIPlatform.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entities.ViewModel
{
    public class UserData
    {
        public List<User>? users { get; set; }

        public List<Country>? country { get; set; }

        public List<Skill>? skill { get; set; }

        public List<City>? city { get; set; }
    }
}
