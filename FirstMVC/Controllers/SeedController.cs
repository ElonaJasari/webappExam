using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirstMVC.Data;
using FirstMVC.Models;

namespace FirstMVC.Controllers
{
    public class SeedController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SeedController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Navigate to /Seed/Characters to run this
        public async Task<IActionResult> Characters()
        {
            var charactersToAdd = new[]
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

            _context.Characters.AddRange(charactersToAdd);
            await _context.SaveChangesAsync();
            
            return Content($"Added 4 characters: Friend 1, Friend 2, Parent, Principal. Go to /Character to manage them.");
        }
    }
}
