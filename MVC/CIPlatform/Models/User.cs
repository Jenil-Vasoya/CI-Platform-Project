using CIPlatform.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIPlatform.Models;

public partial class User
{
    public long UserId { get; set; }

    [Required(ErrorMessage = "Please enter the first name")]
    public string? FirstName { get; set; }


    public string? LastName { get; set; }

    [Required(ErrorMessage = "Please enter the email")]
    [EmailAddress(ErrorMessage = "Please enter the valid email")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Please enter the password")]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one number and one special character.")]
    public string Password { get; set; } = null!;


    [NotMapped] // Does not effect with your database
    [Compare("Password", ErrorMessage = "Please enter the same password")]
    public string? ConfirmPassword { get; set; }


    public int PhoneNumber { get; set; }

    public string? Avatar { get; set; }

    public string? WhyIvolunteer { get; set; }

    public string? EmployeeId { get; set; }

    public string? Department { get; set; }

    public long? CityId { get; set; }

    public long? CountryId { get; set; }

    public string? ProfileText { get; set; }

    public string? LinkedInUrl { get; set; }

    public string? Title { get; set; }

    public bool? Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual City? City { get; set; }

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual Country? Country { get; set; }

    public virtual ICollection<FavoriteMission> FavoriteMissions { get; } = new List<FavoriteMission>();

    public virtual ICollection<MissionApplication> MissionApplications { get; } = new List<MissionApplication>();

    public virtual ICollection<MissionInvite> MissionInvites { get; } = new List<MissionInvite>();

    public virtual ICollection<MissionRating> MissionRatings { get; } = new List<MissionRating>();

    public virtual ICollection<Story> Stories { get; } = new List<Story>();


    public virtual ICollection<StoryView> StoryViews { get; } = new List<StoryView>();

    public virtual ICollection<TimeSheet> TimeSheets { get; } = new List<TimeSheet>();

    public virtual ICollection<UserSkill> UserSkills { get; } = new List<UserSkill>();
}