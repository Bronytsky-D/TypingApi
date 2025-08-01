using Moq;


namespace TypingWeb.Tests.Services.RecordServiceTests
{
    //public class GetRecordsTests
    //{
    //    private readonly RecordService _recordService;
    //    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    //    private readonly Mock<IRecordRepository> _recordRepositoryMock;

    //    public GetRecordsTests()
    //    {
    //        _recordRepositoryMock = new Mock<IRecordRepository>();
    //        _unitOfWorkMock = new Mock<IUnitOfWork>();
    //        _recordService = new RecordService(_unitOfWorkMock.Object);
    //    }

    //    [Fact]
    //    public async Task GetUserByUserId_ShouldReturnRecords_WhenUserIdIsValid()
    //    {
    //        // Arrange
    //        var userId = "valid-user-id";
    //        var records = new List<MyRecord> { new MyRecord() };

    //        _recordRepositoryMock
    //            .Setup(repo => repo.GetRecordsByUserIdAsync(userId))
    //            .ReturnsAsync(records);
    //        _unitOfWorkMock.Setup(uow => uow.Record).Returns(_recordRepositoryMock.Object);

    //        // Act
    //        var result = await _recordService.GetRecordsByUserIdAsync(userId);
    //        // Assert
    //        Assert.True(result.Success, "Records should be retrieved successfully.");
    //    }

    //    [Theory]
    //    [InlineData("")]
    //    [InlineData(null)]
    //    [InlineData("   ")]
    //    public async Task GetRecordsByUserId_ShouldReturnFailure_WhenUserIdIsInvalid(string invalidUserId)
    //    {
    //        // Act
    //        var result = await _recordService.GetRecordsByUserIdAsync(invalidUserId);

    //        // Assert
    //        Assert.False(result.Success);
    //        Assert.Equal("userId is null or empty", result.Errors.First());
    //        Assert.Null(result.Result);
    //    }

    //}
}
