using CIPlatform.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entities.ViewModel
{
    public class VolunteerTimeSheet
    {
            [Key]
            public long TimesheetId { get; set; }

            public long? UserId { get; set; }

            [Required(ErrorMessage = "Please Select a Mission")]
            public long? MissionId { get; set; }

            public string? MissionTitle { get; set; }

            [Required(ErrorMessage = "Please Enter The Hours You Volunteered")]
            [Range(0, 24, ErrorMessage = "Hours must be between 0 and 24")]
            public int Hours { get; set; }

            [Required(ErrorMessage = "Please Enter The Minutes You Volunteered")]
            [Range(0, 59, ErrorMessage = "Minutes must be between 0 and 59")]
            public int Minutes { get; set; }

            public string? Time { get; set; }

            [Required(ErrorMessage = "Please Enter a Date")]
            //[DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date and time")]
            public string DateVolunteered { get; set; }

            public DateTime sendDateVolunteered { get; set; }

            [Required(ErrorMessage = "Please Enter The Action")]
            [Range(0, 200, ErrorMessage = "Action Must be between 0 and 200")]
            public int? Action { get; set; }

        [Required(ErrorMessage = "Please Enter a Notes")]
        [MaxLength(500, ErrorMessage = "Message cannot exceed 500 characters")]
        public string? Notes { get; set; }

        public string? Status { get; set; }


    }
}
