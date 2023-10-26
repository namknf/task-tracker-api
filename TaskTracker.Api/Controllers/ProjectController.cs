using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Contract;
using TaskTracker.Entities.DataTransferObjects;
using TaskTracker.Entities.Models;
using TaskTracker.Api.ActionFilters;

namespace TaskTracker.Api.Controllers
{
    [Route("api/projects/")]
    [ApiController]
    [Produces("application/json")]
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

        /// <summary>
        /// Get all projects of current user
        /// </summary>
        /// <returns>List of projects</returns>
        [HttpGet, Authorize]
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
        [HttpPost(Name = "ProjectById"), Authorize]
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

        /// <summary>
        /// Delete existing project
        /// </summary>
        /// <param name="id">Project id</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}"), Authorize]
        [ServiceFilter(typeof(ValidateProjectExistsAttribute))]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            var project = HttpContext.Items["project"] as Project;
            _dataContextService.DeleteProject(project);
            await _dataContextService.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Update project information
        /// </summary>
        /// <param name="id">Project id</param>
        /// <param name="projectDto">New project information</param>
        /// <returns>No content</returns>
        [HttpPut("{id}"), Authorize]
        [ServiceFilter(typeof(ValidateProjectExistsAttribute))]
        public async Task<IActionResult> UpdateProject(Guid id, [FromBody] ProjectForUpdateDto projectDto)
        {
            var projectEntity = HttpContext.Items["project"] as Project;
            _mapper.Map(projectDto, projectEntity);
            await _dataContextService.SaveChangesAsync();
            return NoContent();
        }
    }
}
