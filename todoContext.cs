using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace TodoApi.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder){
            builder.Entity<User>().HasData(
                new User 
                {
                    Id = 1,
                    UserId = 10,
                    Name = "Omar Ramo"
                },
                new User 
                {
                    Id = 2,
                    UserId = 11,
                    Name = "Ana"
                },
                new User 
                {
                    Id = 3,
                    UserId = 12,
                    Name = "Tonathiu"
                },
                new User 
                {
                    Id = 4,
                    UserId = 13,
                    Name = "Hirales"
                }
            );
        }

        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<User> Users {get; set;}
       
    }
}
