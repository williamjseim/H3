using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HackGame.Api.Models
{
    public class UserInventoryData
    {
        [Key]
        public Guid id { get; set; }
        [ForeignKey("user")]
        public Guid userId { get; set; }
        public int highTechComps { get; set; }
        public int techComps { get; set; }
        public int microControllers { get; set; }
        public int MilitaryTechComps { get; set; }

        public UserData user { get; set; }
    }
}
