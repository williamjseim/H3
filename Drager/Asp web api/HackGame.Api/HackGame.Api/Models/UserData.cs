using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HackGame.Api.Models
{
    public class UserData
    {
        [Key]
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        //[JsonIgnore]
        //public DateTime? creationDate { get; set; } = DateTime.UtcNow;
    }
}
