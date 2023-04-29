using CIPlatform.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entities.AdminViewModel
{
    public class CommentModel
    {
        public long CommentId { get; set; }

        public long UserId { get; set; }

        public long MissionId { get; set; }

        public string ApprovalStatus { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string? Comments { get; set; }

        public virtual Mission Mission { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}
