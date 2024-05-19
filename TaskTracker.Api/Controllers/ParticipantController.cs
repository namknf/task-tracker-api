using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TaskTracker.Contract.Service;
using TaskTracker.Entities.DataTransferObjects;
using TaskTracker.Entities.RequestFeatures;

namespace TaskTracker.Api.Controllers
{
    [Route("api/participants")]
    [Produces("application/json")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDataContextService _dataService;

        public ParticipantController(IMapper mapper, IDataContextService dataService)
        {
            _mapper = mapper;
            _dataService = dataService;
        }

        /// <summary>
        /// Получение списка пользователей для добавления участников проекта или задачи
        /// </summary>
        /// <response code="200">Пользователи успешно загружены из БД</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <returns></returns>
        [HttpGet, Authorize]
        public async Task<ActionResult<List<ParticipantForGetDto>>> GetParticipants([FromQuery] ParticipantParameters parms)
        {
            var participantsFromDb = await _dataService.GetParticipantsAsync(parms);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(participantsFromDb.MetaData));
            var participants = _mapper.Map<List<ParticipantForGetDto>>(participantsFromDb);
            return Ok(participants);
        }
    }
}
