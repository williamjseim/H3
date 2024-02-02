namespace DinDigitaleVerden.Models
{
    public enum Roles
    {//higher permission roles need higher value
        User = 0,
        Admin = 50,
    }
    public class UserModel
    {
        public UserModel()
        {
            this.Id = Guid.Empty;
            this.Role = Roles.User;
            this.Username = string.Empty;
            this.Password = string.Empty;
        }
        public UserModel(string username, string password, Roles role)
        {
            this.Id = Guid.NewGuid();
            this.Username = username;
            this.Password = password;
            this.Role = role;
        }

        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }
    }
}
