using System.Text.Json.Serialization;

namespace HackGame.Api.Models
{
    public class Car
    {
        public Guid Id { get; set; }
        public int Rank { get; set; }
        public string Model { get; set; } = string.Empty;
        public int NumberSold { get; set; }
        public int PercentageChange { get; set; }

    }
}
