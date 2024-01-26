using System.ComponentModel.DataAnnotations;

namespace WebApplicationTraining.Models
{
    public class User
    {
        public long UserId { get; set; }

        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role {  get; set; }
    }
}
