using CIPlatform.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entities.ViewModel
{
    public class AdminModel
    {
        public List<User>? users { get; set; }

        public List<MissionApplication>? missionApplications { get; set; }
    }
}
