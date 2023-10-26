using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Contract;
using TaskTracker.Entities.DataTransferObjects;
using TaskTracker.Entities.Models;

namespace TaskTracker.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    [Produces("application/json")]
    public class ProjectController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IDataContextService _dataContextService;
        private readonly UserManager<User> _userManager;

        public ProjectController(ILogger<ProjectController> logger, IMapper mapper, IDataContextService dataContextService, UserManager<User> manager)
        {
            _logger = logger;
            _mapper = mapper;
            _dataContextService = dataContextService;
            _userManager = manager;
        }

        /// <summary>
        /// Get all projects of current user
        /// </summary>
        /// <returns>List of projects</returns>
        [HttpGet("projects"), Authorize]
        public async Task<ActionResult<List<ProjectDto>>> GetProjects()
        {
            var projectsFromDb = await _dataContextService.GetProjectsAsync(UserId);
            var projects = _mapper.Map<List<ProjectDto>>(projectsFromDb);
            return Ok(projects);
        }

        /// <summary>
        /// Create project
        /// </summary>
        /// <param name="projectDto">Project model with parameters</param>
        /// <returns>Created project</returns>
        [HttpPost("project"), Authorize]
        public async Task<IActionResult> CreateProject([FromBody] ProjectForCreationDto projectDto)
        {
            if (projectDto == null)
            {
                _logger.LogError("ProjectForCreationDto is null");
                return BadRequest("ProjectForCreationDto is null");
            }

            var projectEntity = _mapper.Map<Project>(projectDto);
            await _dataContextService.CreateProjectAsync(projectEntity, projectDto.Participants);
            await _dataContextService.SaveChangesAsync();
            var projectToReturn = _mapper.Map<ProjectDto>(projectEntity);
            return CreatedAtRoute("ProjectById", new { id = projectToReturn.Id }, projectToReturn);
        }
    }
}
