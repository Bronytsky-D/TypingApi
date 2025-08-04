using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TypingWeb.Common;
using TypingWeb.Aplication.Abstractions.UseCases;
using TypingWeb.Common.DTOs;

namespace TypingWebApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private readonly IAddRecordUseCase _addRecordUseCase;
        private readonly IGetRecordUseCase _getRecordUseCase;

        public RecordController(IAddRecordUseCase addRecordUseCase,
            IGetRecordUseCase getRecordUseCase)
        {
            _getRecordUseCase = getRecordUseCase;
            _addRecordUseCase = addRecordUseCase;
        }

        [HttpPost]
        public async Task<IExecutionResponse> PostRecord(RecordWriteRequestDto recordDto)
        {
            return await _addRecordUseCase.ExecuteAsync(recordDto); ;
        }

        [HttpGet("{userId}")]
        public async Task<IExecutionResponse> GetRecordsByUser(string userId)
        {
            return await _getRecordUseCase.ExecuteAsync(userId);
        }

    } 
}
