using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HackGame.Api.Models
{
    [Keyless]
    public class UserExternalHarddriveData
    {
        [ForeignKey("user")]
        public Guid userId { get; set; }
        public UserData user { get; set; }
    }
}
