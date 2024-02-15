using System.Text.Json.Serialization;

namespace HackGame.Api.Models
{
    public class Car
    {
        public Guid id { get; set; }
        public int rank { get; set; }
        public string model { get; set; } = string.Empty;
        public int numberSold { get; set; }
        public int percentageChange { get; set; }

    }
}
