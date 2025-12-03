using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using FirstMVC.Controllers;
using FirstMVC.Models;
using FirstMVC.DAL;
using FirstMVC.ViewModels;

namespace FirstMVC.Tests.Controller;


    public class AdminControllerTest : Controller
    
    private readonly IAdminRepository _adminRepository;
    private readonly IAdminRepository<AdminControllerTest> _logger;

    public AdminControllerTest(IAdminRepository adminRepository, ILogger<AdminControllerTest> logger)
    {
        _adminRepository = adminRepository;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var admins = await _adminRepository.GetAll();
        if (admins == null || !admins.Any())
        {
            _logger.LogWarning("No admins found in the database.");
            return View("Error", new ErrorViewModel { RequestId = "NoAdmins" });
        }
    }