using Moq;
using Domain.Repositories;
using Domain;
using Service.Services;
using Domain.Models;


namespace TypingWeb.Tests.Services.ProgressServiceTest
{
    public class GetLessonProgressByUserId
    {
        private readonly Mock<IProgressRepository> _mockLessonProgressRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly ProgressService _progressService;

        public GetLessonProgressByUserId()
        {
            _mockLessonProgressRepository = new Mock<IProgressRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _progressService = new ProgressService(_unitOfWork.Object);
        }
        [Fact]
        public async Task GetLessonProgressByUserId_ShouldReturnProgress_WhenUserIdIsValid()
        {
            // Arrange
            var userId = "test-user-id";
            var lessonId = 1;
            var lessonProgress = new LessonProgress
            {
                UserId = userId,
                LessonId = lessonId,
                BestWpm = 50,
                BestRaw = 200,
                BestAccuracy = 95
            };
            _mockLessonProgressRepository.Setup(repo => repo.GetByUserAsync(userId))
                .ReturnsAsync(new List<LessonProgress> { lessonProgress });
            _unitOfWork.Setup(uow => uow.Progress).Returns(_mockLessonProgressRepository.Object);
            // Act
            var response = await _progressService.GetByUserIdAsync(userId);
            // Assert
            Assert.True(response.Success, "Response should be successful.");
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public async Task GetLessonProgressByUserId_ShouldReturnFailure_WhenUserIdIsInvalid(string userId)
        {
            // Act
            var response = await _progressService.GetByUserIdAsync(userId);
            // Assert
            Assert.False(response.Success, "Response should not be successful for invalid userId.");
            Assert.Equal("userId empty", response.Errors.FirstOrDefault());
        }
    }
}
