using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Contract;
using TaskTracker.Entities.DataTransferObjects;

namespace TaskTracker.Api.Controllers
{
    [Route("api/priorities")]
    [ApiController]
    [Produces("application/json")]
    public class PriorityController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IDataContextService _dataContextService;

        public PriorityController(IMapper mapper, IDataContextService dataContextService)
        {
            _mapper = mapper;
            _dataContextService = dataContextService;
        }

        /// <summary>
        /// Get all tasks priorities
        /// </summary>
        /// <returns>priorities</returns>
        [HttpGet, Authorize]
        public async Task<ActionResult<List<PriorityDto>>> GetPriorities()
        {
            var prioritiesFromDb = await _dataContextService.GetAllPriorities();
            var priorities = _mapper.Map<List<PriorityDto>>(prioritiesFromDb);
            return Ok(priorities);
        }
    }
}
