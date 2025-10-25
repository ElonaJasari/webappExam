using Microsoft.EntityFrameworkCore;
using FirstMVC.Data;
using FirstMVC.Models;

namespace FirstMVC.Repositories
{
    /// <summary>
    /// Repository implementation for Character entity
    /// Implements Data Access Layer (DAL) pattern with asynchronous database operations
    /// </summary>
    public class CharacterRepository : ICharacterRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CharacterRepository> _logger;

        public CharacterRepository(ApplicationDbContext context, ILogger<CharacterRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all characters from database asynchronously
        /// </summary>
        public async Task<IEnumerable<Characters>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all characters from database");
                return await _context.Characters.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all characters");
                throw;
            }
        }

        /// <summary>
        /// Retrieves a specific character by ID, including related story acts
        /// </summary>
        public async Task<Characters?> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Retrieving character with ID: {CharacterId}", id);
                return await _context.Characters
                    .Include(c => c.StoryActs)
                    .FirstOrDefaultAsync(c => c.CharacterID == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving character with ID: {CharacterId}", id);
                throw;
            }
        }

        /// <summary>
        /// Adds a new character to the database asynchronously
        /// </summary>
        public async Task<Characters> AddAsync(Characters character)
        {
            try
            {
                _logger.LogInformation("Adding new character: {CharacterName}", character.Name);
                _context.Characters.Add(character);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Successfully added character with ID: {CharacterId}", character.CharacterID);
                return character;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding character: {CharacterName}", character.Name);
                throw;
            }
        }

        /// <summary>
        /// Updates an existing character in the database asynchronously
        /// </summary>
        public async Task<Characters> UpdateAsync(Characters character)
        {
            try
            {
                _logger.LogInformation("Updating character with ID: {CharacterId}", character.CharacterID);
                _context.Characters.Update(character);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Successfully updated character with ID: {CharacterId}", character.CharacterID);
                return character;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Concurrency error while updating character with ID: {CharacterId}", character.CharacterID);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating character with ID: {CharacterId}", character.CharacterID);
                throw;
            }
        }

        /// <summary>
        /// Deletes a character from the database if not referenced by story acts
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation("Attempting to delete character with ID: {CharacterId}", id);
                var character = await _context.Characters
                    .Include(c => c.StoryActs)
                    .FirstOrDefaultAsync(c => c.CharacterID == id);

                if (character == null)
                {
                    _logger.LogWarning("Character with ID {CharacterId} not found for deletion", id);
                    return false;
                }

                // Check if character is referenced by story acts
                if (character.StoryActs != null && character.StoryActs.Any())
                {
                    _logger.LogWarning("Cannot delete character with ID {CharacterId}: referenced by story acts", id);
                    return false;
                }

                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Successfully deleted character with ID: {CharacterId}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting character with ID: {CharacterId}", id);
                throw;
            }
        }

        /// <summary>
        /// Checks if a character exists in the database
        /// </summary>
        public async Task<bool> ExistsAsync(int id)
        {
            try
            {
                return await _context.Characters.AnyAsync(c => c.CharacterID == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking if character exists with ID: {CharacterId}", id);
                throw;
            }
        }
    }
}