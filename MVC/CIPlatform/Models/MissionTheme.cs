using System;
using System.Collections.Generic;

namespace CIPlatform.Models;

public partial class MissionTheme
{
    public long MissionThemeId { get; set; }

    public string? Titile { get; set; }

    public byte Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Mission> Missions { get; } = new List<Mission>();
}
