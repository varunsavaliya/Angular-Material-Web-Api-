using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Demo.DataModels
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<SMSTemplate> SMSTemplates { get; set; }
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


            modelBuilder.Entity<SMSTemplate>(entity =>
            {

                entity.HasKey(s => s.SMSTemplateId);
                entity.Property(s => s.Name)
                    .IsRequired();
                entity.Property(s => s.Template)
                    .IsRequired();
                entity.Property(s => s.CreatedOn)
                    .IsRequired();

                entity.HasOne(s => s.CreatedByUser)
                    .WithMany(u => u.CreatedBySMSTemplate)
                    .HasForeignKey(s => s.CreatedBy)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK__CreatedBy__UserId__1CBC4616");

                entity.HasOne(s => s.UpdatedByUser)
                    .WithMany(u => u.UpdatedBySMSTemplate)
                    .HasForeignKey(s => s.UpdatedBy)
                    .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK__UpdatedBy__UserId__1CBC4616");
            });
        }

    }
}


//Scaffold - DbContext Name = ConnectionStrings:DefaultConnection Microsoft.EntityFrameworkCore.SqlServer -OutputDir DataModels -Context ApplicationDbContext -f