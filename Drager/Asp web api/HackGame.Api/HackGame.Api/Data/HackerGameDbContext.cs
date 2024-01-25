using HackGame.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HackGame.Api.Data
{
    public class HackerGameDbContext : DbContext
    {
        public HackerGameDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserData> Login_Data { get; set; }
        public DbSet<Database> Databases { get; set; }
        public DbSet<Software> Software_Data { get; set; }
        public DbSet<UserInventoryData> Inventory_Data { get; set; }
        public DbSet<TimedTask> Timed_Tasks { get; set; }
        public DbSet<HackedDatabases> Hacked_Databases { get; set; }

    }
}
