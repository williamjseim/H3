using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Text.Json.Serialization;

namespace HackGame.Api.Models
{
    public enum Task
    {
        download,
        delete,
        install,
        Uninstall,
        Upload,
        RemoteInstall,
    }

    public class TimedTask
    {
        [Key]
        public Guid id { get; set; }
        public DateTime endTime { get; set; }

        [ForeignKey(nameof(software))]
        public Guid softwareId { get; set; }

        [ForeignKey(nameof(user)), JsonIgnore]
        public Guid userId { get; set; }
        [JsonIgnore]
        public Task type { get; set; }
        [JsonIgnore]
        public IPAddress targetIp { get; set; }
        [JsonIgnore]
        public UserData user { get; set; }
        [JsonIgnore]
        public Software software { get; set; }
    }
}
