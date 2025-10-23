using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FirstMVC.Models; // for at Character, StoryAct osv. skal finnes

namespace FirstMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Characters> Characters { get; set; }
        public DbSet<StoryAct> StoryActs { get; set; }
        public DbSet<UserProgressDB> UserProgress { get; set; }

        public DbSet<Choice> Choices { get; set; }
    }
}
