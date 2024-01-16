using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WEB_APP_PROJECT.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Models.FoodCourt>? FoodCourts { get; set; }
        public DbSet<Models.Order> Orders { get; set; }
        public DbSet<Models.UserState> UserStates { get; set; }
    }
}