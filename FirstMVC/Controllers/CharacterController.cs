using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirstMVC.Data;
using FirstMVC.Models;

namespace FirstMVC.Controllers
{
    public class CharacterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CharacterController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Character or /Admin/Character via explicit view path
        public async Task<IActionResult> Index()
        {
            var characters = await _context.Characters.ToListAsync();
            return View("~/Views/Admin/Character/Index.cshtml", characters);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var character = await _context.Characters
                .Include(c => c.StoryActs)
                .FirstOrDefaultAsync(c => c.CharacterID == id);
            if (character == null) return NotFound();
            return View("~/Views/Admin/Character/Details.cshtml", character);
        }

        public IActionResult Create()
        {
            return View("~/Views/Admin/Character/Create.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CharacterID,Name,Description,Role,Dialog,ImageUrl,Translate")] Characters character)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Admin/Character/Create.cshtml", character);
            }
            _context.Add(character);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var character = await _context.Characters.FindAsync(id);
            if (character == null) return NotFound();
            return View("~/Views/Admin/Character/Edit.cshtml", character);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CharacterID,Name,Description,Role,Dialog,ImageUrl,Translate")] Characters character)
        {
            if (id != character.CharacterID) return NotFound();
            if (!ModelState.IsValid)
            {
                return View("~/Views/Admin/Character/Edit.cshtml", character);
            }
            try
            {
                _context.Update(character);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Characters.AnyAsync(e => e.CharacterID == id)) return NotFound();
                throw;
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var character = await _context.Characters.FirstOrDefaultAsync(c => c.CharacterID == id);
            if (character == null) return NotFound();
            return View("~/Views/Admin/Character/Delete.cshtml", character);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var character = await _context.Characters
                .Include(c => c.StoryActs)
                .FirstOrDefaultAsync(c => c.CharacterID == id);
            if (character == null) return NotFound();

            if (character.StoryActs != null && character.StoryActs.Any())
            {
                TempData["Error"] = "Cannot delete: character is used by one or more story acts.";
                return RedirectToAction(nameof(Index));
            }

            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
