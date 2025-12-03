using Bures.Models;

namespace Bures.Repositories
{
    /// <summary>
    /// Interface for TaskDB repository operations.
    /// Provides Data Access Layer abstraction for test tasks.
    /// </summary>
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskDB>> GetAllAsync();
        Task<TaskDB?> GetByIdAsync(int id);
        Task<TaskDB> AddAsync(TaskDB task);
        Task<TaskDB> UpdateAsync(TaskDB task);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}


