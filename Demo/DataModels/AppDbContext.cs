using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Demo.DataModels
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);
            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.DOB)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.Address)
                .IsRequired();
        }

    }
}
