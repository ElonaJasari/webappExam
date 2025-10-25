using FirstMVC.Models;

namespace FirstMVC.Repositories
{
    /// <summary>
    /// Interface for Character repository operations
    /// Provides Data Access Layer abstraction for Character entities
    /// </summary>
    public interface ICharacterRepository
    {
        Task<IEnumerable<Characters>> GetAllAsync();
        Task<Characters?> GetByIdAsync(int id);
        Task<Characters> AddAsync(Characters character);
        Task<Characters> UpdateAsync(Characters character);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}