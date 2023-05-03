using CIPlatform.Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entities.ViewModel
{
    public class MissionData
    {
        
        public long MissionId { get; set; }

        public long? StoryId { get; set; }

        public long? UserId { get; set; }

        public string? Theme { get; set; }

        public string? WhyIVolunteer { get; set; }

        public long CityId { get; set; }

        public long CountryId { get; set; }

        public int? SkillId { get; set; }

        public int IsApplied { get; set; }

        public string? SkillName { get; set; }

        public bool? Favorite { get; set; }

        public long MissionThemeId { get; set; }

        public string? CityName { get; set; } = null!;

        //[MaxLength(255, ErrorMessage = "Your Title Is Too Big!!!")]
        public string? Title { get; set; } 

        //[MinLength(150, ErrorMessage = "Please enter more details about your story")]
        //[MaxLength(40000, ErrorMessage = "Your Story Is Too Big!!!")]
        public string? Description { get; set; }
        
        public List<string>? StoryImages { get; set; }

        public List<string>? MissionImages { get; set; }

        public List<string>? MissionVideoUrl { get; set; }

        public List<string>? VideoUrl { get; set; }

        public List<string>? Documents { get; set; }

        //[MaxLength(20, ErrorMessage = "You can upload maximum 20 images")]
        public List<IFormFile>? images { get; set; }

        public string? ShortDescription { get; set; }

        public string? StoryStatus { get; set; }


        public DateTime? CreatedAt { get; set; }

        public int IsFavMission { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? MissionType { get; set; }

        public string? MissionGoalText { get; set; }

        public float? GoalValue1 { get; set; }

        public float? CompletedGoal { get; set; }


        public string? OrganizationName { get; set; }

        public string? MediaPath { get; set; }

        public string? MediaName { get; set; }

        public int AvgRating { get; set; }

        public int? UserRating { get; set; }


        public long CommentByUser { get; set; }

        public string? GoalObjectiveText { get; set; }

        public int? GoalValue { get; set; }

        public int? TotalSeats { get; set; }

        public string? Availability { get; set; }

        public DateTime? Deadline { get; set; }

        public string? UserName { get; set; }

        public string? Avatar { get; set; }

        public long? Views { get; set; }

    }
}
