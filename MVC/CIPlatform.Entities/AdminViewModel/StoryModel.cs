using CIPlatform.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entities.AdminViewModel
{
    public class StoryModel
    {
        public long StoryId { get; set; }

        public long UserId { get; set; }

        public long MissionId { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string Status { get; set; } = null!;

        public DateTime? PublishedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public long Views { get; set; }

        public virtual Mission Mission { get; set; } = null!;

        public virtual ICollection<StoryInvite> StoryInvites { get; } = new List<StoryInvite>();

        public virtual ICollection<StoryMedium> StoryMedia { get; } = new List<StoryMedium>();

        public virtual ICollection<StoryView> StoryViews { get; } = new List<StoryView>();

        public virtual User User { get; set; } = null!;
    }
}
