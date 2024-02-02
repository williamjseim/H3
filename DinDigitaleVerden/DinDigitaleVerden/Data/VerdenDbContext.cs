using DinDigitaleVerden.Models;
using Microsoft.EntityFrameworkCore;

namespace DinDigitaleVerden.Data
{
    public class VerdenDbContext : DbContext
    {
        public VerdenDbContext(DbContextOptions op) : base(op) { }
        public DbSet<UserModel> User_Data { get; set; }
    }
}
