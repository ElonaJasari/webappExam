using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirstMVC.Data;
using FirstMVC.Models;

namespace FirstMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CharacterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CharacterController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Character
        public async Task<IActionResult> Index()
        {
            var characters = await _context.Characters.ToListAsync();
            return View(characters);
        }

        // GET: Admin/Character/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var character = await _context.Characters
                .Include(c => c.StoryActs)
                .FirstOrDefaultAsync(m => m.CharacterID == id);

            if (character == null) return NotFound();

            return View(character);
        }

        // GET: Admin/Character/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Character/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CharacterID,Name,Description,Role,Dialog,ImageUrl,Translate")] Characters character)
        {
            if (!ModelState.IsValid)
            {
                return View(character);
            }

            _context.Add(character);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Character created.";
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Character/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var character = await _context.Characters.FindAsync(id);
            if (character == null) return NotFound();
            return View(character);
        }

        // POST: Admin/Character/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CharacterID,Name,Description,Role,Dialog,ImageUrl,Translate")] Characters character)
        {
            if (id != character.CharacterID) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(character);
            }

            try
            {
                _context.Update(character);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Character updated.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Characters.AnyAsync(e => e.CharacterID == id))
                {
                    return NotFound();
                }
                throw;
            }
        }

        // GET: Admin/Character/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var character = await _context.Characters.FirstOrDefaultAsync(m => m.CharacterID == id);
            if (character == null) return NotFound();
            return View(character);
        }

        // POST: Admin/Character/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var character = await _context.Characters
                .Include(c => c.StoryActs)
                .FirstOrDefaultAsync(c => c.CharacterID == id);
            if (character == null) return NotFound();

            // Prevent delete if referenced by story acts
            if (character.StoryActs != null && character.StoryActs.Any())
            {
                TempData["Error"] = "Cannot delete: character is used by one or more story acts.";
                return RedirectToAction(nameof(Index));
            }

            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Character deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}
