﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CIPlatform.Entities.Models;

public partial class Mission
{
    public long MissionId { get; set; }

    public long MissionThemeId { get; set; }

    public long CityId { get; set; }

    public long CountryId { get; set; }

    public string Title { get; set; } = null!;

    public string? ShortDescription { get; set; }

    public string? Description { get; set; }

    public DateTime? StartDate { get; set; }

    [NotMapped]
    public string StartDateEdit { get; set; }

    [NotMapped]
    public string EndDateEdit { get; set; }

    public DateTime? EndDate { get; set; }

    public string? MissionType { get; set; }

    public bool? Status { get; set; }

    public string? OrganizationName { get; set; }

    public string? OrganizationDetail { get; set; }

    public int? TotalSeats { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    [NotMapped]
    public List<int>? MissionSkill { get; set; }

    [NotMapped]
    public List<string>? StoryImages { get; set; }

    [NotMapped]
    public List<string>? skillNames { get; set; }

    [NotMapped]
    public string? VideoUrl { get; set; }

    [NotMapped]
    public List<IFormFile>? Images { get; set; }

    [NotMapped]
    public List<IFormFile>? Documents { get; set; }

    public string? Availibility { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<FavoriteMission> FavoriteMissions { get; } = new List<FavoriteMission>();

    public virtual ICollection<GoalMission> GoalMissions { get; } = new List<GoalMission>();

    public virtual ICollection<MissionApplication> MissionApplications { get; } = new List<MissionApplication>();

    public virtual ICollection<MissionDocument> MissionDocuments { get; } = new List<MissionDocument>();

    public virtual ICollection<MissionInvite> MissionInvites { get; } = new List<MissionInvite>();

    public virtual ICollection<MissionMedium> MissionMedia { get; } = new List<MissionMedium>();

    public virtual ICollection<MissionRating> MissionRatings { get; } = new List<MissionRating>();

    [JsonIgnore]
    public virtual ICollection<MissionSkill> MissionSkills { get; } = new List<MissionSkill>();

    public virtual MissionTheme MissionTheme { get; set; } = null!;

    public virtual ICollection<Story> Stories { get; } = new List<Story>();

    public virtual ICollection<TimeSheet> TimeSheets { get; } = new List<TimeSheet>();
}
