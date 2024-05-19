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
        /// Получение проектов текущего пользователя
        /// </summary>
        /// <response code="200">Список проектов успешно загружен из БД</response>
        /// <response code="401">Пользователь не авторизован</response>
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
        /// Получение проекта
        /// </summary>
        /// <param name="projectId">Идентификатор проекта</param>
        /// <response code="200">Проект успешно загружен из БД</response>
        /// <response code="404">Проект не найден</response>
        /// <response code="401">Пользователь не авторизован</response>
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
        /// Создание проекта
        /// </summary>
        /// <param name="projectDto">Параметры для создания нового проекта</param>
        /// <response code="400">Некорректные параметры</response>
        /// <response code="201">Проект был успешно создан</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <returns>Created project</returns>
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [HttpPost(Name = "ProjectById")]
        public async Task<IActionResult> CreateProject([FromBody] ProjectForCreationDto projectDto)
        {
            if (projectDto == null)
            {
                _logger.LogError("Некорректные параметры для создания нового проекта");
                return BadRequest("Некорректные параметры для создания нового проекта");
            }

            var projectEntity = _mapper.Map<Project>(projectDto);
            await _dataContextService.CreateProjectAsync(projectEntity, projectDto.Participants);
            await _dataContextService.SaveChangesAsync();
            var projectToReturn = _mapper.Map<ProjectDto>(projectEntity);
            return CreatedAtRoute("ProjectById", new { id = projectToReturn.Id }, projectToReturn);
        }

        /// <summary>
        /// Удаление существующего проекта
        /// </summary>
        /// <param name="projectId">Идентификатор проекта</param>
        /// <response code="204">Проект был успешно удален</response>
        /// <response code="404">Проект не был найден</response>
        /// <response code="401">Пользователь не авторизован</response>
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
        /// Обновление информации о проекте
        /// </summary>
        /// <param name="projectId">Идентификатор проекта</param>
        /// <param name="projectDto">New project information</param>
        /// <response code="204">Проект был успешно обновлен</response>
        /// <response code="404">Проект не был найден</response>
        /// <response code="401">Пользователь не авторизован</response>
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
