using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIPlatform.Models1
{
    public class User
    {
        [Key]
        public Int64 UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        [NotMapped] // Does not effect with your database
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public int PhoneNumber {get; set; }

        public string Avatar { get; set; }

        public string WhyIVolunteer { get; set; }

        public string EmployeeID { get; set; }

        public string Department { get; set; }

        public Int64 CityID {
            get { return 1; }
            set
            {
                this.CityID = 1;
            } }

        public Int64 CountryID
        {
            get { return 1; }
            set
            {
                this.CountryID = 1;
            }
        }

        public string ProfileText { get; set; }

        public string LinkedInUrl { get; set; }
        public string Title { get; set; }
        public bool Status { get; set; }



        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; } 
        public DateTime DeletedAt { get; set; } 


    }
}
