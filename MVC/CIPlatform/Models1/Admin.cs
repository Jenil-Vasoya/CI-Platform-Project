using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace CIPlatform.Models
{
    public class Admin1
    {
        [Key]
        public Int64 AdminID { get; set; }
        public  string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
