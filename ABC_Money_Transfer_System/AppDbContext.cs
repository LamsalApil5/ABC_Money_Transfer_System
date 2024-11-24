using ABC_Money_Transfer_System.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ABC_Money_Transfer_System
{
    public class AppDbContext : DbContext
    {
        public DbSet<Users> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
