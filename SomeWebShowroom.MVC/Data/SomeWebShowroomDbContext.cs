using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SomeWebShowroom.MVC.Models;

namespace SomeWebShowroom.MVC.Data
{
    public class SomeWebShowroomDbContext : IdentityDbContext<User>
    {
        public SomeWebShowroomDbContext(DbContextOptions<SomeWebShowroomDbContext> options)
            : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<UserProducts> UserProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProducts>()
        .HasKey(up => new { up.UserId, up.ProductId });

            modelBuilder.Entity<UserProducts>()
                .HasOne(up => up.User)
                .WithMany(b => b.UserProducts)
                .HasForeignKey(bc => bc.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<UserProducts>()
                .HasOne(up => up.Product)
                .WithMany(c => c.UserProducts)
                .HasForeignKey(up => up.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}
