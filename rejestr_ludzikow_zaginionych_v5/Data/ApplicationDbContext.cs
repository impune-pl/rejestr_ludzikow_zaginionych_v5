using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using rejestr_ludzikow_zaginionych_v5.Models;

namespace rejestr_ludzikow_zaginionych_v5.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
        public DbSet<User> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
               .Entity<Person>()
                   .HasOne<User>(p => p.Owner)
                       .WithMany(u => u.Entries)
                           .HasForeignKey(p => p.OwnerId);
            modelBuilder
                .Entity<User>()
                    .HasIndex(e => e.Email)
                        .IsUnique();
        }
    }
}
