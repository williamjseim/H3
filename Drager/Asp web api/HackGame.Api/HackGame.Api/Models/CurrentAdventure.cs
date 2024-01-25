using System.ComponentModel.DataAnnotations.Schema;

namespace HackGame.Api.Models
{
    public enum MissionLocation
    {
        MilitaryBase,
        SecuricoLocation,
        ScrapYard,
    }
    public class CurrentAdventureæ
    {
        public string missionLocation { get; set; } = string.Empty;

        public DateTime EndTime { get; set; }

        [ForeignKey("user")]
        public Guid UserId { get; set; }
        public UserData? User { get; set; }
    }
}

