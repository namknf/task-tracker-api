using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Contract;
using TaskTracker.Entities.DataTransferObjects;

namespace TaskTracker.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ProjectController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IDataContextService _dataContextService;

        public ProjectController(ILogger<ProjectController> logger, IMapper mapper, IDataContextService dataContextService)
        {
            _logger = logger;
            _mapper = mapper;
            _dataContextService = dataContextService;
        }

        [HttpGet("projects"), Authorize]
        [Produces("application/json")]
        public async Task<ActionResult<List<ProjectDto>>> GetProjects()
        {
            var projectsFromDb = await _dataContextService.GetProjectsAsync();
            var projects = _mapper.Map<List<ProjectDto>>(projectsFromDb);
            return Ok(projects);
        }
    }
}
