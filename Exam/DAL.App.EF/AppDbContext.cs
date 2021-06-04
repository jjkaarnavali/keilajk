using System;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        
        public DbSet<Answer> Answers { get; set; } = default!;
        
        public DbSet<Category> Categories { get; set; } = default!;
        
        public DbSet<Game> Games { get; set; } = default!;
        
        public DbSet<Question> Questions { get; set; } = default!;
        
        public DbSet<Quiz> Quizzes { get; set; } = default!;

        
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}