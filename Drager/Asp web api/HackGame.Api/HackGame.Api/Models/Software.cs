using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Text.Json.Serialization;

namespace HackGame.Api.Models
{
    public enum SoftwareType
    {
        None,
        Firewall,
        Cracker,
        AntiVirus,
        Miner,
        Spammer,
        Virus,
        Hider,
        Seeker,
        Hasher,
    }

    public class Software
    {
        public Software()
        {
            Id = Guid.NewGuid();
            DatabaseId = Guid.Empty;
            Type = SoftwareType.Virus;
            IsInstalled = false;
            Strength = 1;
            this.IsDeleteable = true;
        }

        public Software(SoftwareType type, bool isInstalled, bool deletable, float strength, Guid databaseId, bool isStatic = false)
        {
            Id = Guid.NewGuid();
            this.Type = type;
            this.Strength = strength;
            this.Description = type.ToString();
            this.IsInstalled = isInstalled;
            this.DatabaseId = databaseId;
            this.IsDeleteable = deletable;
            this.StaticObject = isStatic;
        }

        public Software(SoftwareType type, bool isInstalled, bool deletable, float strength, string databaseId)
        {
            Id = Guid.NewGuid();
            this.Type = type;
            this.Strength = strength;
            this.Description = type.ToString();
            this.IsInstalled = isInstalled;
            this.DatabaseId = Guid.Parse(databaseId);
            this.IsDeleteable = deletable;
        }

        public Software(SoftwareType type, bool installed, bool deletable, string name, float strength, string description, Guid databaseId, Guid? uploaderId, bool isStatic = false)
        {
            Id = Guid.NewGuid();
            this.Type = type;
            this.IsInstalled = installed;
            this.Strength = strength;
            this.Description = description;
            this.DatabaseId = databaseId;
            this.IsDeleteable = deletable;
            this.UploadId = uploaderId;
            this.StaticObject = isStatic;
        }

        public Software(SoftwareType type, bool installed, bool deletable, string name, float strength, string description, Guid databaseId, bool isStatic = false)
        {
            Id = Guid.NewGuid();
            this.Type = type;
            this.IsInstalled = installed;
            this.Strength = strength;
            this.Description = description;
            this.DatabaseId = databaseId;
            this.IsDeleteable = deletable;
            this.UploadId = Guid.Empty;
            this.StaticObject = isStatic;
        }

        public void HideSoftware()
        {
            this.Strength = new Random().Next(0, 999);
            this.IsDeleteable = false;
            Description = string.Empty;
            Type = SoftwareType.None;
        }

        [Key]
        public Guid Id { get; set; }
        public SoftwareType Type { get; set; }
        public bool IsInstalled { get; set; } = false;
        [NotMapped]
        public string Name { get { return FixName(); } }
        public float Strength { get; set; }
        [NotMapped]
        public int Size { get { return CalculateSize(); } }
        public string Description { get; set; } = string.Empty;
        public bool IsDeleteable { get; set; }
        public Guid? UploadId { get; set; } //installer id
        public int HiddenStrength { get; set; } = 0; //how strong the hider software was that hid this software
        public int EncryptionStrength { get; set; } = 0;
        public bool StaticObject { get; set; } = false; //if true can only be download not edited or deleted or installed on target machine

        [ForeignKey(nameof(data))]
        [JsonIgnore]
        public Guid? DatabaseId { get; set; }

        [JsonIgnore]
        public Database? data { get; set; }

        private string FixName()
        {
            if(this.Strength <= 1)
            {
                return "Basic " + Type.ToString();
            }
            else if(this.Strength <= 2)
            {
                return "Simple " + Type.ToString();
            }
            else if (this.Strength <= 4)
            {
                return "Mediocre " + Type.ToString();
            }
            else if(this.Strength <= 6)
            {
                return "Good " + Type.ToString();
            }
            else if (this.Strength <= 8)
            {
                return "Advanced " + Type.ToString();
            }
            return "Basic " + Type.ToString();
        }

        private int CalculateSize()
        {
            if(Type != SoftwareType.Virus && Type != SoftwareType.Miner && Type != SoftwareType.Spammer)
            {
                if(Strength <= 1)
                {
                    return 28;
                }
                else if(Strength > 1)
                {
                    return (int)Math.Floor(28 * Strength);
                }
            }
            else
            {
                if(Strength <= 1)
                {
                    return 20;
                }
                else if (Strength > 1)
                {
                    return (int)Math.Floor(20 * Strength);
                }
            }
            return 28;
        }
    }
}
