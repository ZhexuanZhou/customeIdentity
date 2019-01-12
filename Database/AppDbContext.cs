using System;
using customeIdentity.Models;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace customeIdentity.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>().HasKey(k=>new {k.UserId, k.RoleId});
        }

        public DbSet<User> Users{get;set;}
        public DbSet<Role> Roles{get;set;}
        public DbSet<UserRole> UserRoles{get;set;}
    }
}