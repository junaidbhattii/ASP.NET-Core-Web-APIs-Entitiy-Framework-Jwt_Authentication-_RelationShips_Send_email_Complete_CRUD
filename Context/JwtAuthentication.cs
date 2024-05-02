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
    }
}
