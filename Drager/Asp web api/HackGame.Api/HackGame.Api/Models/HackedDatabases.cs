using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HackGame.Api.Models
{
    public class HackedDatabases
    {
        [Key]
        public Guid id { get; set; }
        [ForeignKey(nameof(User))]
        public Guid userId { get; set; }
        [ForeignKey(nameof(Database))]
        public Guid databaseId { get; set; }
        public string SecretKey { get; set; } = string.Empty;
        [JsonIgnore]
        public Database? Database { get; set; }
        [JsonIgnore]
        public UserData? User { get; set; }
    }
}
