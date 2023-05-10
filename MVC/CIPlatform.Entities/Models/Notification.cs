using System;
using System.Collections.Generic;

namespace CIPlatform.Entities.Models;

public partial class Notification
{
    public long NotificationId { get; set; }

    public long? MissionId { get; set; }

    public long? StoryId { get; set; }

    public string? Text { get; set; }

    public string? Status { get; set; }

    public long? UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Mission? Mission { get; set; }

    public virtual Story? Story { get; set; }

    public virtual User? User { get; set; }
}
