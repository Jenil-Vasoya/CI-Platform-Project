using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entities.ViewModel
{
    public class RecentVolunteer
    {

        [Key]
        public long MissionId { get; set; }

        public string UserName { get; set; }

        public string? Avatar { get; set; }
    }
}
