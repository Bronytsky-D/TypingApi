using Domain;
using Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypingWebApi.Service;
using MyRecord = TypingWebApi.Data.Models.Record;


namespace TypingWeb.Tests.Services.RecordServiceTests
{
    public class AddRecordTest
    {
        private readonly RecordService _recordService;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IRecordRepository> _recordRepositoryMock;
        public AddRecordTest()
        {
            _recordRepositoryMock = new Mock<IRecordRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _recordService = new RecordService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task AddRecord_ShouldReturnSuccess_WhenUserIdIsValid()
        {
            // Arrange
            var record = new MyRecord
            {
                Wpm = 50,
                Raw = 200,
                Accuracy = 95,
                Chars = 1000,
                MatchTime = 60,
                Language = "en",
                UserId = "test-user-id"
            };
            _recordRepositoryMock.Setup(repo => repo.AddRecordAsync(It.IsAny<MyRecord>()))
                .Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(uow => uow.Record).Returns(_recordRepositoryMock.Object);
            // Act
            var result = await _recordService.AddRecordAsync(record);
            // Assert
            Assert.True(result.Success, "Record should be added successfully.");
        }
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public async Task AddRecord_ShouldReturnFailure_WhenUserIdIsInvalid(string invalidUserId)
        {
            // Arrange
            var record = new MyRecord
            {
                Wpm = 50,
                Raw = 200,
                Accuracy = 95,
                Chars = 1000,
                MatchTime = 60,
                Language = "en",
                UserId = invalidUserId
            };
            // Act
            var result = await _recordService.AddRecordAsync(record);
            // Assert
            Assert.False(result.Success);
            Assert.Equal("userId is null or empty", result.Errors.First());
            Assert.Null(result.Result);
        }
    }
}
