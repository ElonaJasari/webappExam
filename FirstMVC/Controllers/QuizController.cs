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
    [Authorize]
    public class QuizController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuizController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Render quiz for a given act number
        [HttpGet("/Quiz/Act/{act}")]
        public async Task<IActionResult> Act(int act)
        {
            // Expect TaskDB to have Act field; if not present, we load all for now
            var tasks = await _context.Tasks
                .OrderBy(t => t.TaskID)
                .ToListAsync();

            if (tasks.Count == 0)
            {
                ViewData["Error"] = "No quiz tasks available. Ask an admin to add tasks.";
            }

            ViewData["Act"] = act;
            return View(tasks);
        }

        public class QuizSubmissionDto
        {
            public int Act { get; set; }
            public int[] TaskIds { get; set; } = new int[0];
            public int[] SelectedOptionIndexes { get; set; } = new int[0];
        }

        // Submit quiz answers
        [HttpPost("/Quiz/Submit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit([FromForm] QuizSubmissionDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            int correct = 0;
            for (int i = 0; i < dto.TaskIds.Length && i < dto.SelectedOptionIndexes.Length; i++)
            {
                var taskId = dto.TaskIds[i];
                var selected = dto.SelectedOptionIndexes[i];
                var task = await _context.Tasks.FirstOrDefaultAsync(t => t.TaskID == taskId);
                if (task == null) continue;

                // Assuming TaskDB has CorrectOptionIndex (0-based) and Options serialized or fixed fields
                bool isCorrect = task.CorrectOptionIndex == selected;
                if (isCorrect) correct++;

                _context.UserTaskResults.Add(new UserTaskResult
                {
                    UserId = userId,
                    TaskId = taskId,
                    ActNumber = dto.Act,
                    IsCorrect = isCorrect
                });
            }

            await _context.SaveChangesAsync();

            int total = dto.TaskIds.Length;
            bool passed = total == 0 ? false : (correct * 100 / total) >= 70; // 70% pass

            // Mark pass flag on progress (optional future extension)
            var progress = await _context.UserProgress.FirstOrDefaultAsync(p => p.UserID == userId);
            if (progress != null)
            {
                progress.LastUpdated = System.DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }

            ViewData["Act"] = dto.Act;
            ViewData["Correct"] = correct;
            ViewData["Total"] = total;
            ViewData["Passed"] = passed;
            return View("Result");
        }
    }
}
