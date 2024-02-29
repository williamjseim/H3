using System.ComponentModel.DataAnnotations;

namespace HackGame.Api.Models
{
    public class UserData
    {
        [Key]
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
