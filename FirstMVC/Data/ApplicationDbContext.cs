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

        // DbSet for test tasks (words / sentences) that the teacher chooses for quizzes.
        // This is the TaskDB table that powers the end-of-game language tests.
        public DbSet<TaskDB> Tasks { get; set; }

        // DbSet for dictionary words
        public DbSet<DictionaryWord> DictionaryWords { get; set; }

       public DbSet<UserCharacterSelection> UserCharacterSelection { get; set;} 

       // Records of user quiz submissions and correctness
       public DbSet<UserTaskResult> UserTaskResults { get; set; }
    }
}
