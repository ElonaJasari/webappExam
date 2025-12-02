using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirstMVC.Models;
using FirstMVC.Data;
using Microsoft.AspNetCore.Authorization;

namespace FirstMVC.Controllers
{
    /// <summary>
    /// Controller for read-only Character operations.
    /// Characters are now a fixed cast in the story (Friend 1, Friend 2, Parent, Principal, Teacher)
    /// and can no longer be created, edited or deleted through the admin interface.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class CharacterController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CharacterController> _logger;

        public CharacterController(ApplicationDbContext context, ILogger<CharacterController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Displays list of all characters with error handling
        /// </summary>
        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Retrieving all characters for index view");
                var characters = await _context.Characters.ToListAsync();
                return View("~/Views/Admin/Character/Index.cshtml", characters);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while loading character index");
                TempData["Error"] = "Unable to load characters. Please try again.";
                return View("~/Views/Admin/Character/Index.cshtml", new List<Characters>());
            }
        }

        /// <summary>
        /// Displays details for a specific character with comprehensive error handling
        /// </summary>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                _logger.LogWarning("Details action called with null ID");
                return NotFound();
            }

            try
            {
                var character = await _context.Characters
                    .Include(c => c.StoryActs)
                    .FirstOrDefaultAsync(c => c.CharacterID == id.Value);
                if (character == null)
                {
                    _logger.LogWarning("Character with ID {CharacterId} not found", id);
                    return NotFound();
                }
                return View("~/Views/Admin/Character/Details.cshtml", character);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while loading character details for ID: {CharacterId}", id);
                TempData["Error"] = "Unable to load character details. Please try again.";
                return RedirectToAction(nameof(Index));
            }
        }

        // NOTE:
        // Characters are now treated as fixed system data.
        // All write operations (Create/Edit/Delete) have been removed from this controller
        // to ensure the core cast used in the visual novel cannot be modified from the UI.
    }
}
