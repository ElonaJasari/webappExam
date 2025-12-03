using Microsoft.EntityFrameworkCore;
using Bures.Data;
using Bures.Models;

namespace Bures.Repositories
{
    /// <summary>
    /// Repository implementation for TaskDB entity.
    /// Handles asynchronous CRUD operations for teacher-created test tasks.
    /// </summary>
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TaskRepository> _logger;

        public TaskRepository(ApplicationDbContext context, ILogger<TaskRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all tasks ordered by StoryAct and text.
        /// </summary>
        public async Task<IEnumerable<TaskDB>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all tasks from TaskDB");
                return await _context.Tasks
                    .OrderBy(t => t.StoryActId)
                    .ThenBy(t => t.Text)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all tasks");
                throw;
            }
        }

        /// <summary>
        /// Retrieves a specific task by ID.
        /// </summary>
        public async Task<TaskDB?> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Retrieving task with ID: {TaskId}", id);
                return await _context.Tasks.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving task with ID: {TaskId}", id);
                throw;
            }
        }

        /// <summary>
        /// Adds a new task to the database.
        /// </summary>
        public async Task<TaskDB> AddAsync(TaskDB task)
        {
            try
            {
                _logger.LogInformation("Adding new task: {TaskText}", task.Text);
                _context.Tasks.Add(task);
                await _context.SaveChangesAsync();
                return task;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding task: {TaskText}", task.Text);
                throw;
            }
        }

        /// <summary>
        /// Updates an existing task.
        /// </summary>
        public async Task<TaskDB> UpdateAsync(TaskDB task)
        {
            try
            {
                _logger.LogInformation("Updating task with ID: {TaskId}", task.TaskId);
                _context.Tasks.Update(task);
                await _context.SaveChangesAsync();
                return task;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Concurrency error while updating task with ID: {TaskId}", task.TaskId);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating task with ID: {TaskId}", task.TaskId);
                throw;
            }
        }

        /// <summary>
        /// Deletes a task from TaskDB.
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation("Attempting to delete task with ID: {TaskId}", id);
                var task = await _context.Tasks.FindAsync(id);
                if (task == null)
                {
                    _logger.LogWarning("Task with ID {TaskId} not found for deletion", id);
                    return false;
                }

                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Successfully deleted task with ID: {TaskId}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting task with ID: {TaskId}", id);
                throw;
            }
        }

        /// <summary>
        /// Checks if a task exists in TaskDB.
        /// </summary>
        public async Task<bool> ExistsAsync(int id)
        {
            try
            {
                return await _context.Tasks.AnyAsync(t => t.TaskId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking if task exists with ID: {TaskId}", id);
                throw;
            }
        }
    }
}


