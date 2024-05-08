using JwtAuthentication_Relations_Authorization.Models;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthentication_Relations_Authorization.Context
{
    public class JwtAuthentication : DbContext
    {
        public JwtAuthentication(DbContextOptions options) : base(options) { }
        
        public DbSet<User> Users { get; set; }  
        public DbSet<Role> Roles { get; set; }
        public DbSet<Employee> Employees { get; set; }  
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Admin> Admins { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.vendor)
                .WithOne(v => v.user)
                .HasForeignKey<Vendor>(v => v.UserId);
        }

    }
}
