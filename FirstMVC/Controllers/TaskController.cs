using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FirstMVC.Models;
using FirstMVC.Repositories;

namespace FirstMVC.Controllers
{
    /// <summary>
    /// Controller for TaskDB CRUD operations.
    /// Teachers (Admin role) use this to add and remove the words/sentences
    /// that will be used to test the students at the end of the game.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class TaskController : Controller
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ILogger<TaskController> _logger;

        public TaskController(ITaskRepository taskRepository, ILogger<TaskController> logger)
        {
            _taskRepository = taskRepository;
            _logger = logger;
        }

        /// <summary>
        /// Shows all test tasks (TaskDB) that will be used in quizzes.
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var tasks = await _taskRepository.GetAllAsync();
            return View("~/Views/Admin/Task/Index.cshtml", tasks);
        }

        /// <summary>
        /// Displays form to create a new test task (word or sentence).
        /// </summary>
        public IActionResult Create()
        {
            return View("~/Views/Admin/Task/Create.cshtml");
        }

        /// <summary>
        /// Handles creation of a new test task with validation.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Text,Type,StoryActId,Description")] TaskDB task)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Admin/Task/Create.cshtml", task);
            }

            try
            {
                await _taskRepository.AddAsync(task);
                TempData["Success"] = "Task added to TaskDB successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating task");
                ModelState.AddModelError("", "Unable to create task. Please try again.");
                return View("~/Views/Admin/Task/Create.cshtml", task);
            }
        }

        /// <summary>
        /// Displays form to edit an existing test task.
        /// </summary>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _taskRepository.GetByIdAsync(id.Value);
            if (task == null)
            {
                return NotFound();
            }

            return View("~/Views/Admin/Task/Edit.cshtml", task);
        }

        /// <summary>
        /// Handles updating an existing test task.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskId,Text,Type,StoryActId,Description")] TaskDB task)
        {
            if (id != task.TaskId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View("~/Views/Admin/Task/Edit.cshtml", task);
            }

            try
            {
                await _taskRepository.UpdateAsync(task);
                TempData["Success"] = "Task updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating task");
                ModelState.AddModelError("", "Unable to update task. Please try again.");
                return View("~/Views/Admin/Task/Edit.cshtml", task);
            }
        }

        /// <summary>
        /// Displays confirmation page for deleting a test task.
        /// </summary>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _taskRepository.GetByIdAsync(id.Value);
            if (task == null)
            {
                return NotFound();
            }

            return View("~/Views/Admin/Task/Delete.cshtml", task);
        }

        /// <summary>
        /// Deletes a test task from TaskDB.
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _taskRepository.DeleteAsync(id);
            if (!success)
            {
                TempData["Error"] = "Task not found or already deleted.";
                return RedirectToAction(nameof(Index));
            }

            TempData["Success"] = "Task deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}


