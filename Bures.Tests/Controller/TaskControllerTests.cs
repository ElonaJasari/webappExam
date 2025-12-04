using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bures.Controllers;
using Bures.Models;
using Bures.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Bures.Tests
{
    public class TaskControllerTests
    {
        private readonly Mock<ITaskRepository> _mockRepo;
        private readonly Mock<ILogger<TaskController>> _mockLogger;
        private readonly TaskController _controller;

        public TaskControllerTests()
        {
            _mockRepo = new Mock<ITaskRepository>();
            _mockLogger = new Mock<ILogger<TaskController>>();
            _controller = new TaskController(_mockRepo.Object, _mockLogger.Object);

            // 🔹 Viktig: sett opp TempData slik at TempData[...] fungerer i testene
            var httpContext = new DefaultHttpContext();
            var tempDataProvider = Mock.Of<ITempDataProvider>();
            _controller.TempData = new TempDataDictionary(httpContext, tempDataProvider);
        }

        // 1) INDEX: positiv test – returnerer view med liste
        [Fact]
        public async Task Index_ReturnsViewWithTasks()
        {
            // Arrange
            var tasks = new List<TaskDB>
            {
                new TaskDB { TaskId = 1, Text = "Test 1" },
                new TaskDB { TaskId = 2, Text = "Test 2" }
            };

            _mockRepo.Setup(r => r.GetAllAsync())
                     .ReturnsAsync(tasks);

            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<TaskDB>>(viewResult.Model);
            Assert.Equal(2, model.Count());
            _mockRepo.Verify(r => r.GetAllAsync(), Times.Once);
        }

        // 2) CREATE (GET): bare at den returnerer et view
        [Fact]
        public void Create_Get_ReturnsView()
        {
            // Act
            var result = _controller.Create();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        // 3) CREATE (POST): positiv test – gyldig model, redirect + AddAsync kalt
        [Fact]
        public async Task Create_Post_ValidModel_RedirectsAndCallsAdd()
        {
            // Arrange
            var task = new TaskDB
            {
                Text = "New task",
                Type = "Word",
                StoryActId = 1,
                Description = "Desc"
            };

            _mockRepo.Setup(r => r.AddAsync(task))
                     .ReturnsAsync(task);

            // Act
            var result = await _controller.Create(task);

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(TaskController.Index), redirect.ActionName);
            _mockRepo.Verify(r => r.AddAsync(It.IsAny<TaskDB>()), Times.Once);
        }

        // 4) CREATE (POST): negativ test – ugyldig modelstate, returnerer view og kaller IKKE AddAsync
        [Fact]
        public async Task Create_Post_InvalidModel_ReturnsView()
        {
            // Arrange
            var task = new TaskDB();
            _controller.ModelState.AddModelError("Text", "Required");

            // Act
            var result = await _controller.Create(task);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(task, viewResult.Model);
            _mockRepo.Verify(r => r.AddAsync(It.IsAny<TaskDB>()), Times.Never);
        }

        // 5) EDIT (GET): id == null -> NotFound
        [Fact]
        public async Task Edit_Get_IdNull_ReturnsNotFound()
        {
            // Act
            var result = await _controller.Edit(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        // 6) EDIT (GET): task ikke funnet -> NotFound
        [Fact]
        public async Task Edit_Get_TaskNotFound_ReturnsNotFound()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetByIdAsync(1))
                     .ReturnsAsync((TaskDB?)null);

            // Act
            var result = await _controller.Edit(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        // Ekstra: EDIT (POST): id mismatch -> NotFound
        [Fact]
        public async Task Edit_Post_IdMismatch_ReturnsNotFound()
        {
            // Arrange
            var task = new TaskDB
            {
                TaskId = 5,
                Text = "Updated",
                Type = "Sentence",
                StoryActId = 2
            };

            // Act
            var result = await _controller.Edit(10, task); // 10 != 5

            // Assert
            Assert.IsType<NotFoundResult>(result);
            _mockRepo.Verify(r => r.UpdateAsync(It.IsAny<TaskDB>()), Times.Never);
        }

        // 7) EDIT (POST): positiv test – gyldig, id matcher, kaller Update + redirect
        [Fact]
        public async Task Edit_Post_ValidModel_RedirectsAndCallsUpdate()
        {
            // Arrange
            var task = new TaskDB
            {
                TaskId = 5,
                Text = "Updated",
                Type = "Sentence",
                StoryActId = 2
            };

            _mockRepo.Setup(r => r.UpdateAsync(task))
                     .ReturnsAsync(task);

            // Act
            var result = await _controller.Edit(5, task);

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(TaskController.Index), redirect.ActionName);
            _mockRepo.Verify(r => r.UpdateAsync(It.IsAny<TaskDB>()), Times.Once);
        }

        // 8) DELETECONFIRMED: repo returnerer false -> TempData["Error"] settes
        [Fact]
        public async Task DeleteConfirmed_TaskNotFound_SetsErrorTempData()
        {
            // Arrange
            _mockRepo.Setup(r => r.DeleteAsync(1))
                     .ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteConfirmed(1);

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(TaskController.Index), redirect.ActionName);
            Assert.Equal("Task not found or already deleted.", _controller.TempData["Error"]);
        }

        // 9) DELETECONFIRMED: repo returnerer true -> TempData["Success"] settes
        [Fact]
        public async Task DeleteConfirmed_TaskDeleted_SetsSuccessTempData()
        {
            // Arrange
            _mockRepo.Setup(r => r.DeleteAsync(1))
                     .ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteConfirmed(1);

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(TaskController.Index), redirect.ActionName);
            Assert.Equal("Task deleted successfully.", _controller.TempData["Success"]);
        }
    }
}
