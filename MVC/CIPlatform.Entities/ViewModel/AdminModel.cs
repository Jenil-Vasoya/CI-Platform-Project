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

        public long CmspageId { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string Slug { get; set; } = null!;

        public bool? Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
