using FirstMVC.Models;

namespace FirstMVC.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Ensure database is created
            context.Database.EnsureCreated();

            // Check if characters already exist
            if (context.Characters.Any())
            {
                return; // DB has been seeded
            }

            // Add the 4 core characters
            var characters = new Characters[]
            {
                new Characters
                {
                    Name = "Friend 1",
                    Role = "ID_FRIEND1",
                    Description = "Your first friend in the story",
                    Dialog = "",
                    ImageUrl = "",
                    Translate = ""
                },
                new Characters
                {
                    Name = "Friend 2",
                    Role = "ID_FRIEND2",
                    Description = "Your second friend in the story",
                    Dialog = "",
                    ImageUrl = "",
                    Translate = ""
                },
                new Characters
                {
                    Name = "Parent",
                    Role = "ID_PARENT",
                    Description = "The parent character",
                    Dialog = "",
                    ImageUrl = "",
                    Translate = ""
                },
                new Characters
                {
                    Name = "Principal",
                    Role = "ID_PRINCIPAL",
                    Description = "The school principal",
                    Dialog = "",
                    ImageUrl = "",
                    Translate = ""
                }
            };

            context.Characters.AddRange(characters);
            context.SaveChanges();
        }
    }
}
