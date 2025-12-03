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
            public int CharacterId { get; set; }
            public string CustomName { get; set; } = string.Empty;
        }

        [HttpPost]
        public async Task<IActionResult> SaveSelection([FromBody] SaveSelectionDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var selection = new UserCharacterSelection
            {
                UserId = userId,
                CharacterId = dto.CharacterId,
                CustomName = dto.CustomName
            };

            _context.UserCharacterSelection.Add(selection);
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