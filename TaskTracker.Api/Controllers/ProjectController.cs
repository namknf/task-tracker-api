using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Entities.DataTransferObjects;
using TaskTracker.Entities.Models;
using TaskTracker.Api.ActionFilters;
using System.Net;
using TaskTracker.Entities.RequestFeatures;
using Newtonsoft.Json;
using TaskTracker.Contract.Service;

namespace TaskTracker.Api.Controllers
{
    [Route("api/projects/")]
    [ApiController]
    [Produces("application/json")]
    [Authorize]
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
        /// <response code="200">Successfully got</response>
        /// <returns>List of projects</returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpGet]
        public async Task<ActionResult<List<ProjectDto>>> GetProjects([FromQuery] ProjectParameters parms)
        {
            var projectsFromDb = await _dataContextService.GetProjectsAsync(UserId, parms);
            var projects = _mapper.Map<List<ProjectDto>>(projectsFromDb);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(projectsFromDb.MetaData));
            return Ok(projects);
        }

        /// <summary>
        /// Get project by id
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <response code="200">Successfully got</response>
        /// <response code="404">Project not found</response>
        /// <returns>Project</returns>
        [HttpGet("{projectId}")]
        [ServiceFilter(typeof(ValidateProjectExistsAttribute))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public ActionResult<ProjectDto> GetProject(Guid projectId)
        {
            var projectEntity = HttpContext.Items["project"] as Project;
            var projectDto = _mapper.Map<ProjectDto>(projectEntity);
            return Ok(projectDto);
        }

        /// <summary>
        /// Create project
        /// </summary>
        /// <param name="projectDto">Project model with parameters</param>
        /// <response code="400">Project dto is null</response>
        /// <response code="201">Project was successfully created</response>
        /// <returns>Created project</returns>
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [HttpPost(Name = "ProjectById")]
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
        /// <param name="projectId">Project id</param>
        /// <response code="204">Project was successfully deleted</response>
        /// <response code="404">Project not found</response>
        /// <returns>No content</returns>
        [HttpDelete("{projectId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ServiceFilter(typeof(ValidateProjectExistsAttribute))]
        public async Task<IActionResult> DeleteProject(Guid projectId)
        {
            var project = HttpContext.Items["project"] as Project;
            _dataContextService.DeleteProject(project);
            await _dataContextService.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Update project information
        /// </summary>
        /// <param name="projectId">Project id</param>
        /// <param name="projectDto">New project information</param>
        /// <response code="204">Project was successfully updated</response>
        /// <response code="404">Project not found</response>
        /// <returns>No content</returns>
        [HttpPut("{projectId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ServiceFilter(typeof(ValidateProjectExistsAttribute))]
        public async Task<IActionResult> UpdateProject(Guid projectId, [FromBody] ProjectForUpdateDto projectDto)
        {
            var projectEntity = HttpContext.Items["project"] as Project;
            var updatedProject = _mapper.Map(projectDto, projectEntity);
            _dataContextService.UpdateProject(updatedProject);
            await _dataContextService.SaveChangesAsync();
            return NoContent();
        }
    }
}
