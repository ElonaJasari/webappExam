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
                    Role = "Female Friend",
                    CharacterCode = "ID_FRIEND1",
                    Description = "Your first friend in the story",
                    Dialog = "",
                    ImageUrl = "",
                    Translate = ""
                },
                new Characters
                {
                    Name = "Friend 2",
                    Role = "Male Friend",
                    CharacterCode = "ID_FRIEND2",
                    Description = "Your second friend in the story",
                    Dialog = "",
                    ImageUrl = "",
                    Translate = ""
                },
                new Characters
                {
                    Name = "Parent",
                    Role = "Parent",
                    CharacterCode = "ID_PARENT",
                    Description = "The parent character",
                    Dialog = "",
                    ImageUrl = "",
                    Translate = ""
                },
                new Characters
                {
                    Name = "Principal",
                    Role = "Principal",
                    CharacterCode = "ID_PRINCIPAL",
                    Description = "The school principal",
                    Dialog = "",
                    ImageUrl = "",
                    Translate = ""
                },
                new Characters
                {
                    Name = "Teach",
                    Role = "Teacher",
                    CharacterCode = "ID_TEACHER",
                    Description = "The school teacher",
                    Dialog = "",
                    ImageUrl = "",
                    Translate = "" 
                }
            };

            _context.Characters.AddRange(charactersToAdd);
            await _context.SaveChangesAsync();
            
            return Content($"Added 5 characters: Friend 1, Friend 2, Parent, Principal, Teach. Go to /Character to manage them.");
        }

        // Navigate to /Seed/UpdateTeacherName to run this
        public async Task<IActionResult> UpdateTeacherName()
        {
            var teacher = await _context.Characters.FirstOrDefaultAsync(c => c.CharacterCode == "ID_TEACHER");
            
            if (teacher != null)
            {
                teacher.Name = "Teach";
                await _context.SaveChangesAsync();
                return Content("Updated Teacher name to 'Teach'. Go to /Character to verify.");
            }
            
            return Content("Teacher character not found.");
        }
    }
}
