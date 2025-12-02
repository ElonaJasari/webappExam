using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirstMVC.Data;
using FirstMVC.Models;

namespace FirstMVC.Controllers
{
    /// <summary>
    /// Controller for managing dictionary words used in language tests.
    /// Teachers can add Northern Sami words/sentences and their translations here,
    /// which can later be used to generate end-of-game quizzes.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class DictionaryWordController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DictionaryWordController> _logger;

        public DictionaryWordController(ApplicationDbContext context, ILogger<DictionaryWordController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Displays all dictionary words that can be used for tests.
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var words = await _context.DictionaryWords
                .OrderBy(w => w.StoryActId)
                .ThenBy(w => w.Text)
                .ToListAsync();

            return View("~/Views/Admin/DictionaryWord/Index.cshtml", words);
        }

        /// <summary>
        /// Shows the form for adding a new test word or sentence.
        /// </summary>
        public IActionResult Create()
        {
            return View("~/Views/Admin/DictionaryWord/Create.cshtml");
        }

        /// <summary>
        /// Handles creation of a new dictionary entry with basic validation.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Text,Type,StoryActId,Description")] DictionaryWord word)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Admin/DictionaryWord/Create.cshtml", word);
            }

            try
            {
                _context.DictionaryWords.Add(word);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Word/sentence added to test dictionary.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating dictionary word");
                ModelState.AddModelError("", "Unable to save word. Please try again.");
                return View("~/Views/Admin/DictionaryWord/Create.cshtml", word);
            }
        }

        /// <summary>
        /// Shows the form for editing an existing word or sentence.
        /// </summary>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var word = await _context.DictionaryWords.FindAsync(id.Value);
            if (word == null)
            {
                return NotFound();
            }

            return View("~/Views/Admin/DictionaryWord/Edit.cshtml", word);
        }

        /// <summary>
        /// Handles updates to an existing dictionary entry.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Text,Type,StoryActId,Description")] DictionaryWord word)
        {
            if (id != word.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View("~/Views/Admin/DictionaryWord/Edit.cshtml", word);
            }

            try
            {
                _context.Update(word);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Dictionary entry updated.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.DictionaryWords.AnyAsync(w => w.Id == id))
                {
                    return NotFound();
                }

                ModelState.AddModelError("", "The entry was modified by another user. Please reload and try again.");
                return View("~/Views/Admin/DictionaryWord/Edit.cshtml", word);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating dictionary word");
                ModelState.AddModelError("", "Unable to update entry. Please try again.");
                return View("~/Views/Admin/DictionaryWord/Edit.cshtml", word);
            }
        }

        /// <summary>
        /// Confirms deletion of a dictionary entry.
        /// </summary>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var word = await _context.DictionaryWords.FirstOrDefaultAsync(w => w.Id == id.Value);
            if (word == null)
            {
                return NotFound();
            }

            return View("~/Views/Admin/DictionaryWord/Delete.cshtml", word);
        }

        /// <summary>
        /// Deletes a dictionary entry from the test database.
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var word = await _context.DictionaryWords.FindAsync(id);
            if (word == null)
            {
                TempData["Error"] = "Word not found or already deleted.";
                return RedirectToAction(nameof(Index));
            }

            _context.DictionaryWords.Remove(word);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Word removed from test dictionary.";
            return RedirectToAction(nameof(Index));
        }
    }
}


