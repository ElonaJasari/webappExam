using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FirstMVC.Models; // for at Character, StoryAct osv. skal finnes

namespace FirstMVC.Data
{
    // ApplicationDbContext inherits from IdentityDbContext to include ASP.NET Core Identity tables
    // (AspNetUsers, AspNetRoles, etc.) in the same DbContext as the application's own entities.
    public class ApplicationDbContext : IdentityDbContext
    {
        // The constructor injects DbContextOptions configured in Startup/Program.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        // DbSet for characters in the application.
        // Note: The name of the DbSet determines the table name by convention unless overridden.
        public DbSet<Characters> Characters { get; set; }

         // DbSet for story acts
        public DbSet<StoryAct> StoryActs { get; set; }

        //used for storing progress/state
        public DbSet<UserProgressDB> UserProgress { get; set; }

        // DbSet for choices
        public DbSet<Choice> Choices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Characters data
            modelBuilder.Entity<Characters>().HasData(
                new Characters
                {
                    CharacterID = 1,
                    Name = "Friend 1",
                    Role = "ID_FRIEND1",
                    Description = "Your first friend in the story",
                    Dialog = "",
                    ImageUrl = "",
                    Translate = ""
                },
                new Characters
                {
                    CharacterID = 2,
                    Name = "Friend 2",
                    Role = "ID_FRIEND2",
                    Description = "Your second friend in the story",
                    Dialog = "",
                    ImageUrl = "",
                    Translate = ""
                },
                new Characters
                {
                    CharacterID = 3,
                    Name = "Parent",
                    Role = "ID_PARENT",
                    Description = "The parent character",
                    Dialog = "",
                    ImageUrl = "",
                    Translate = ""
                },
                new Characters
                {
                    CharacterID = 4,
                    Name = "Principal",
                    Role = "ID_PRINCIPAL",
                    Description = "The school principal",
                    Dialog = "",
                    ImageUrl = "",
                    Translate = ""
                }
            );
        }
    }
}
