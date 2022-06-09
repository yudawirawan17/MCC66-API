using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Profiling> Profilling { get; set; }
        public DbSet<University> University { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(a => a.Account)
                .WithOne(b => b.Employee)
                .HasForeignKey<Account>(b => b.NIK);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.Profiling)
                .WithOne(b => b.Account)
                .HasForeignKey<Profiling>(b => b.NIK);

            modelBuilder.Entity<Education>()
                .HasMany(a => a.Profilings)
                .WithOne(b => b.Education);

            modelBuilder.Entity<University>()
                .HasMany(u => u.Educations)
                .WithOne(ed => ed.University);

            // Many to Many Account Role
            modelBuilder.Entity<AccountRole>()
                .HasKey(ac => new { ac.NIK, ac.RoleId });
            modelBuilder.Entity<AccountRole>()
                .HasOne(ac => ac.Account)
                .WithMany(b => b.AccountRoles)
                .HasForeignKey(ac => ac.NIK);
            modelBuilder.Entity<AccountRole>()
                .HasOne(ac => ac.Role)
                .WithMany(c => c.AccountRoles)
                .HasForeignKey(ac => ac.RoleId);

        }
    }
    
}
