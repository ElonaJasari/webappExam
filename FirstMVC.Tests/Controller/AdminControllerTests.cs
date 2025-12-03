using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
using FirstMVC.Data;
using FirstMVC.Models;

namespace FirstMVC.Tests
{
    public class AdminControllerTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

    
        //       CREATE 

        // 1) Create (GET) - positiv
        [Fact]
        public void Create_Get_ReturnsView()
        {
            // Arrange
            var context = GetDbContext();
            var controller = new AdminController(context);

            // Act
            var result = controller.Create() as ViewResult;

            // Assert
            Assert.NotNull(result);
            // null viewName betyr at default view ("Create") brukes
            Assert.Null(result.ViewName);
        }

        // 2) Create (POST) - positiv
        [Fact]
        public async Task Create_Post_ValidModel_RedirectsAndSaves()
        {
            // Arrange
            var context = GetDbContext();
            var controller = new AdminController(context);

            var story = new StoryAct
            {
                StoryActId = 1,
                Title = "Test Title",
                Content = "Some content",
                Description = "Desc"
            };

            // Act
            var result = await controller.Create(story);

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);

            var fromDb = await context.StoryActs.FirstOrDefaultAsync(s => s.StoryActId == 1);
            Assert.NotNull(fromDb);
            Assert.Equal("Test Title", fromDb!.Title);
        }

        // 3) Create (POST) - negativ
        [Fact]
        public async Task Create_Post_InvalidModel_ReturnsViewWithSameModel()
        {
            // Arrange
            var context = GetDbContext();
            var controller = new AdminController(context);

            var story = new StoryAct
            {
                // mangler Title (Required) -> vi gjør ModelState invalid
                Content = "Some content"
            };

            controller.ModelState.AddModelError("Title", "Required");

            // Act
            var result = await controller.Create(story) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Same(story, result!.Model);
        }

    
        //        READ (R)
        //  (Edit GET = Read One)

        // 4) Edit (GET) - positiv
        [Fact]
        public async Task Edit_Get_ExistingId_ReturnsViewWithModel()
        {
            // Arrange
            var context = GetDbContext();

            var story = new StoryAct
            {
                StoryActId = 1,
                Title = "Existing",
                Content = "Content"
            };

            context.StoryActs.Add(story);
            await context.SaveChangesAsync();

            var controller = new AdminController(context);

            // Act
            var result = await controller.Edit(1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = Assert.IsType<StoryAct>(result!.Model);
            Assert.Equal(1, model.StoryActId);
        }

        // 5) Edit (GET) - negativ
        [Fact]
        public async Task Edit_Get_NotExistingId_ReturnsNotFound()
        {
            // Arrange
            var context = GetDbContext();
            var controller = new AdminController(context);

            // Act
            var result = await controller.Edit(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

    
        //       UPDATE (U)
        //     (Edit POST)

        // 6) Edit (POST) - positiv
        [Fact]
        public async Task Edit_Post_ValidModel_UpdatesAndRedirects()
        {
            // Arrange
            var context = GetDbContext();

            var story = new StoryAct
            {
                StoryActId = 1,
                Title = "Old Title",
                Content = "Old Content"
            };

            context.StoryActs.Add(story);
            await context.SaveChangesAsync();

            // Simuler ny HTTP-request -> ingen entities trackes
            context.ChangeTracker.Clear();

            var controller = new AdminController(context);

            var updated = new StoryAct
            {
                StoryActId = 1,
                Title = "New Title",
                Content = "New Content"
            };

            // Act
            var result = await controller.Edit(updated);

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);

            var fromDb = await context.StoryActs.FirstOrDefaultAsync(s => s.StoryActId == 1);
            Assert.NotNull(fromDb);
            Assert.Equal("New Title", fromDb!.Title);
            Assert.Equal("New Content", fromDb.Content);
        }

        // 7) Edit (POST) - negativ
        [Fact]
        public async Task Edit_Post_InvalidModel_ReturnsViewWithSameModel()
        {
            // Arrange
            var context = GetDbContext();
            var controller = new AdminController(context);

            var story = new StoryAct
            {
                StoryActId = 1,
                // mangler Title -> invalid
                Content = "Content"
            };

            controller.ModelState.AddModelError("Title", "Required");

            // Act
            var result = await controller.Edit(story) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Same(story, result!.Model);
        }

    
        //       DELETE (D)
    

        // 8) Delete - positiv
        [Fact]
        public async Task Delete_ExistingId_DeletesAndRedirects()
        {
            // Arrange
            var context = GetDbContext();

            var story = new StoryAct
            {
                StoryActId = 1,
                Title = "To delete",
                Content = "Content"
            };

            context.StoryActs.Add(story);
            await context.SaveChangesAsync();

            var controller = new AdminController(context);

            // Act
            var result = await controller.Delete(1);

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);

            var fromDb = await context.StoryActs.FindAsync(1);
            Assert.Null(fromDb);
        }

        // 9) Delete - negativ
        [Fact]
        public async Task Delete_NotExistingId_ReturnsNotFound()
        {
            // Arrange
            var context = GetDbContext();
            var controller = new AdminController(context);

            // Act
            var result = await controller.Delete(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
