using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirstMVC.Models;
using FirstMVC.Data;

public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

    // LIST ALL STORY ACTS
    public async Task<IActionResult> Index()
    {
        var storyActs = await _context.StoryActs.Include(s => s.Choices).ToListAsync();
        return View(storyActs);
    }

    // CREATE NEW STORY ACT - GET
    public IActionResult Create()
    {
        return View();
    }

    // CREATE NEW STORY ACT - POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(StoryAct storyAct)
    {
        if (ModelState.IsValid)
        {
            _context.StoryActs.Add(storyAct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(storyAct);
    }

    // EDIT STORY ACT - GET
    public async Task<IActionResult> Edit(int id)
    {
        var storyAct = await _context.StoryActs.Include(s => s.Choices)
                                              .FirstOrDefaultAsync(s => s.StoryActId == id);
        if (storyAct == null) return NotFound();
        return View(storyAct);
    }

    // EDIT STORY ACT - POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(StoryAct storyAct)
    {
        if (ModelState.IsValid)
        {
            _context.Update(storyAct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(storyAct);
    }

    // DELETE STORY ACT
    public async Task<IActionResult> Delete(int id)
    {
        var storyAct = await _context.StoryActs.FindAsync(id);
        if (storyAct == null) return NotFound();

        _context.StoryActs.Remove(storyAct);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
