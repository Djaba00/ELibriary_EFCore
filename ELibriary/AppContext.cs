using System;
using ELibriary.Configurations;
using ELibriary.Tables;
using Microsoft.EntityFrameworkCore;

namespace ELibriary
{
    public class AppContext : DbContext
    {
        public DbSet<User> Users {get; set; }
        public DbSet<Book> Books {get; set; }

        // public AppContext()
        // {
        //     Database.EnsureDeleted();
        //     Database.EnsureCreated();
        // }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString.MsSqlConnection);
        }
    }
}