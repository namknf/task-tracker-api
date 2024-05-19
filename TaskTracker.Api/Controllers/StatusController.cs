using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Contract.Service;
using TaskTracker.Entities.DataTransferObjects;

namespace TaskTracker.Api.Controllers
{
    [Route("api/statuses")]
    [ApiController]
    [Produces("application/json")]
    [Authorize]
    public class StatusController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDataContextService _dataContextService;

        public StatusController(IMapper mapper, IDataContextService dataContextService)
        {
            _mapper = mapper;
            _dataContextService = dataContextService;
        }

        /// <summary>
        /// Получение списка всех статусов задач
        /// </summary>
        /// <response code="401">Пользователь не авторизован</response>
        /// <response code="200">Список статусов успешно загружен из БД</response>
        /// <returns>statuses</returns>
        [HttpGet]
        public async Task<ActionResult<List<StatusDto>>> GetStatuses()
        {
            var statusesFromDb = await _dataContextService.GetAllStatuses();
            var statuses = _mapper.Map<List<StatusDto>>(statusesFromDb);
            return Ok(statuses);
        }
    }
}
