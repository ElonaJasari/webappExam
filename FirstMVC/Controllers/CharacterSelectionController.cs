using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FirstMVC.Data;
using FirstMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FirstMVC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // require logged-in user
    public class CharacterSelectionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CharacterSelectionController(ApplicationDbContext context)
        {
            _context = context;
        }

        public class SaveSelectionDto
        {
            // Front-end sends 1..5. We'll map to DB by code.
            public int CharacterId { get; set; }
            public string CustomName { get; set; } = string.Empty;
        }

        [HttpPost]
        public async Task<IActionResult> SaveSelection([FromBody] SaveSelectionDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            // Map client id (1..5) to CharacterCode in DB
            string code = dto.CharacterId switch
            {
                1 => "ID_COOL_DUDE",      // Eiven Nordflamme
                2 => "ID_CONFIDENT_DUDE", // Vargár Ravdna
                3 => "ID_TUNG_TUNG",      // TUNG TUNG SAMUR
                4 => "ID_AURORA",         // Aurora Borealis
                5 => "ID_CHLOEKELLY",     // Chloe Kelly
            };

            // Prefer CharacterCode lookup; fall back to numeric when present
            var character = await _context.Characters
                .FirstOrDefaultAsync(c => c.CharacterCode == code);

            if (character == null)
            {
                // If not found by code, try using numeric id directly
                character = await _context.Characters
                    .FirstOrDefaultAsync(c => c.CharacterID == dto.CharacterId);
            }

            if (character == null)
            {
                return BadRequest($"Character not found for selection id {dto.CharacterId}.");
            }

            // Upsert: keep only one selection per user
            var existing = await _context.UserCharacterSelection
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync();

            if (existing == null)
            {
                var selection = new UserCharacterSelection
                {
                    UserId = userId,
                    CharacterId = character.CharacterID,
                    CustomName = dto.CustomName
                };
                _context.UserCharacterSelection.Add(selection);
            }
            else
            {
                existing.CharacterId = character.CharacterID;
                existing.CustomName = dto.CustomName;
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetLatestSelection()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var selection = await _context.UserCharacterSelection
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync();

            if (selection == null) return NotFound();

            return Ok(new
            {
                characterId = selection.CharacterId,
                customName = selection.CustomName
            });
        }
    }
}