using Service.Services;
using Microsoft.AspNetCore.Identity;
using Moq;
using TypingWeb.Tests.Utils;
using Domain.Models;

namespace TypingWeb.Tests.Services.UserServiceTest
{
    public class CreateUserTests
    {
        private readonly UserService _userService;
        private readonly Mock<UserManager<User>> _userManager;
        public CreateUserTests()
        {
            _userManager = UserManagerMockFactory.Create<User>();
            _userService = new UserService(_userManager.Object);
        }
        [Fact]
        public async Task CreateUserWithPassword_ShouldReturnSuccess_WhenUserIsCreated()
        {
            // Arrange
            var newUser = new User { Email = "test@example.com", UserName = "testuser" };
            _userManager.Setup(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            _userManager.Setup(m => m.AddToRoleAsync(It.IsAny<User>(), "User"))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _userService.CreateUser(newUser, "P@ssw0rd");

            // Assert
            Assert.True(result.Success, "Користувач має бути створений успішно.");
            _userManager.Verify(m => m.AddToRoleAsync(newUser, "User"), Times.Once, "Користувача має бути додано до ролі 'User'.");
        }

        [Fact]
        public async Task CreateUserWithPassword_ShouldReturnFailure_WhenUserCreationFails()
        {
            // Arrange
            var newUser = new User { Email = "test@example.com", UserName = "testuser" };
            _userManager.Setup(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Помилка створення" }));

            // Act
            var result = await _userService.CreateUser(newUser, "P@ssw0rd");

            // Assert
            Assert.False(result.Success, "Створення користувача має завершитися невдачею.");
            _userManager.Verify(m => m.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never, "Роль не має додаватися при невдачі.");
        }

        [Fact]
        public async Task CreateUserWithoutPassword_ShouldReturnSuccess_WhenUserIsCreated()
        {
            // Arrange
            var newUser = new User { Email = "test@example.com", UserName = "testuser" };
            _userManager.Setup(m => m.CreateAsync(It.IsAny<User>()))
                .ReturnsAsync(IdentityResult.Success);
            _userManager.Setup(m => m.AddToRoleAsync(It.IsAny<User>(), "User"))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _userService.CreateUser(newUser);

            // Assert
            Assert.True(result.Success, "Користувач має бути створений успішно.");
            _userManager.Verify(m => m.AddToRoleAsync(newUser, "User"), Times.Once, "Користувача має бути додано до ролі 'User'.");
        }

        [Fact]
        public async Task CreateUserWithoutPassword_ShouldReturnFailure_WhenUserCreationFails()
        {
            // Arrange
            var newUser = new User { Email = "test@example.com", UserName = "testuser" };
            _userManager.Setup(m => m.CreateAsync(It.IsAny<User>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Помилка створення" }));

            // Act
            var result = await _userService.CreateUser(newUser);

            // Assert
            Assert.False(result.Success, "Створення користувача має завершитися невдачею.");
            _userManager.Verify(m => m.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never, "Роль не має додаватися при невдачі.");
        }
    }
}
