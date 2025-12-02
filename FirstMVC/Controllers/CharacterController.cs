using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirstMVC.Models;
using FirstMVC.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace FirstMVC.Controllers
{
    /// <summary>
    /// Controller for Character CRUD operations
    /// Implements Repository pattern for data access and comprehensive error handling
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class CharacterController : Controller
    {
        private static readonly HashSet<string> CoreCharacterRoles = new(StringComparer.OrdinalIgnoreCase)
        {
            "ID_FRIEND1",
            "ID_FRIEND2",
            "ID_PARENT",
            "ID_PRINCIPAL"
        };

        private readonly ICharacterRepository _characterRepository;
        private readonly ILogger<CharacterController> _logger;

        public CharacterController(ICharacterRepository characterRepository, ILogger<CharacterController> logger)
        {
            _characterRepository = characterRepository;
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
                var characters = await _characterRepository.GetAllAsync();
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
                var character = await _characterRepository.GetByIdAsync(id.Value);
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

        /// <summary>
        /// Displays character creation form
        /// </summary>
        public IActionResult Create()
        {
            _logger.LogInformation("Displaying character creation form");
            return View("~/Views/Admin/Character/Create.cshtml");
        }

        /// <summary>
        /// Handles character creation with server-side validation and error handling
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CharacterID,Name,Description,Role,Dialog,ImageUrl,Translate")] Characters character)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for character creation: {CharacterName}", character.Name);
                return View("~/Views/Admin/Character/Create.cshtml", character);
            }

            try
            {
                await _characterRepository.AddAsync(character);
                TempData["Success"] = "Character created successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating character: {CharacterName}", character.Name);
                ModelState.AddModelError("", "Unable to create character. Please try again.");
                return View("~/Views/Admin/Character/Create.cshtml", character);
            }
        }

        /// <summary>
        /// Displays character edit form with error handling
        /// </summary>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                _logger.LogWarning("Edit action called with null ID");
                return NotFound();
            }

            try
            {
                var character = await _characterRepository.GetByIdAsync(id.Value);
                if (character == null)
                {
                    _logger.LogWarning("Character with ID {CharacterId} not found for editing", id);
                    return NotFound();
                }
                return View("~/Views/Admin/Character/Edit.cshtml", character);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while loading character for edit with ID: {CharacterId}", id);
                TempData["Error"] = "Unable to load character for editing. Please try again.";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// Handles character updates with comprehensive validation and error handling
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CharacterID,Name,Description,Role,Dialog,ImageUrl,Translate")] Characters character)
        {
            if (id != character.CharacterID)
            {
                _logger.LogWarning("ID mismatch in edit request: URL ID {UrlId}, Model ID {ModelId}", id, character.CharacterID);
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for character edit: {CharacterId}", character.CharacterID);
                return View("~/Views/Admin/Character/Edit.cshtml", character);
            }

            try
            {
                await _characterRepository.UpdateAsync(character);
                TempData["Success"] = "Character updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Concurrency error while updating character with ID: {CharacterId}", character.CharacterID);
                if (!await _characterRepository.ExistsAsync(id))
                {
                    return NotFound();
                }
                ModelState.AddModelError("", "The character was modified by another user. Please reload and try again.");
                return View("~/Views/Admin/Character/Edit.cshtml", character);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating character with ID: {CharacterId}", character.CharacterID);
                ModelState.AddModelError("", "Unable to update character. Please try again.");
                return View("~/Views/Admin/Character/Edit.cshtml", character);
            }
        }

        /// <summary>
        /// Displays character deletion confirmation with error handling
        /// </summary>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                _logger.LogWarning("Delete action called with null ID");
                return NotFound();
            }

            try
            {
                var character = await _characterRepository.GetByIdAsync(id.Value);
                if (character == null)
                {
                    _logger.LogWarning("Character with ID {CharacterId} not found for deletion", id);
                    return NotFound();
                }

                if (IsCoreCharacter(character))
                {
                    TempData["Error"] = "Cannot delete core characters (Friend 1, Friend 2, Parent, Principal). These are protected system characters.";
                    return RedirectToAction(nameof(Index));
                }

                return View("~/Views/Admin/Character/Delete.cshtml", character);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while loading character for delete with ID: {CharacterId}", id);
                TempData["Error"] = "Unable to load character for deletion. Please try again.";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// Handles character deletion with business logic validation and error handling
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var character = await _characterRepository.GetByIdAsync(id);
                if (character == null)
                {
                    TempData["Error"] = "Character not found.";
                    return RedirectToAction(nameof(Index));
                }

                if (IsCoreCharacter(character))
                {
                    TempData["Error"] = "Cannot delete core characters (Friend 1, Friend 2, Parent, Principal). These are protected system characters.";
                    return RedirectToAction(nameof(Index));
                }

                var success = await _characterRepository.DeleteAsync(id);
                if (!success)
                {
                    TempData["Error"] = "Cannot delete: character not found or is used by one or more story acts.";
                    return RedirectToAction(nameof(Index));
                }

                TempData["Success"] = "Character deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting character with ID: {CharacterId}", id);
                TempData["Error"] = "Unable to delete character. Please try again.";
                return RedirectToAction(nameof(Index));
            }
        }

        private static bool IsCoreCharacter(Characters character)
        {
            return !string.IsNullOrWhiteSpace(character.Role) &&
                   CoreCharacterRoles.Contains(character.Role);
        }
    }
}
