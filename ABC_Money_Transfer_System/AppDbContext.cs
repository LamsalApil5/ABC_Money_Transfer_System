﻿using ABC_Money_Transfer_System.Models;
using Microsoft.EntityFrameworkCore;

namespace ABC_Money_Transfer_System
{
    public class AppDbContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Transactions> Transactions { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
