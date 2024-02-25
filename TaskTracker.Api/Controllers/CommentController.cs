using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using TaskTracker.Api.ActionFilters;
using TaskTracker.Contract.Service;
using TaskTracker.Entities.DataTransferObjects;
using TaskTracker.Entities.Models;
using TaskTracker.Entities.RequestFeatures;

namespace TaskTracker.Api.Controllers
{
    [Route("api/projects/{projectId}/tasks/{taskId}/comments")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class CommentController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IDataContextService _dataContextService;

        public CommentController(ILogger<CommentController> logger, IMapper mapper, IDataContextService dataContextService)
        {
            _logger = logger;
            _mapper = mapper;
            _dataContextService = dataContextService;
        }

        /// <summary>
        /// Get all comments from task
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="taskId"> task id</param>
        /// <param name="parms">pagination params</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ServiceFilter(typeof(ValidateTaskExistsAttribute))]
        public async Task<ActionResult<List<CommentDto>>> GetAllCommentsForTask(Guid projectId, Guid taskId, [FromQuery] CommentParameters parms)
        {
            var commentsFromDb = await _dataContextService.GetTaskCommentsAsync(taskId, parms);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(commentsFromDb.MetaData));
            var commentsDto = _mapper.Map<IEnumerable<CommentDto>>(commentsFromDb);
            return Ok(commentsDto);
        }

        /// <summary>
        /// Get comment
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="taskId">task id</param>
        /// <param name="commentId">comment id</param>
        /// <returns></returns>
        [HttpGet("{commentId}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ServiceFilter(typeof(ValidateCommentExistsAttribute))]
        public ActionResult<CommentDto> GetComment(Guid projectId, Guid taskId, Guid commentId)
        {
            var comment = HttpContext.Items["comment"] as TaskComment;
            var commentDto = _mapper.Map<CommentDto>(comment);
            return Ok(commentDto);
        }

        /// <summary>
        /// Create new comment
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="taskId">task id</param>
        /// <param name="commentDto">comment for creation dto</param>
        /// <returns></returns>
        [HttpPost(Name = "CreateCommentForTask")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ServiceFilter(typeof(ValidateTaskExistsAttribute))]
        public async Task<IActionResult> CreateCommentForTask(Guid projectId, Guid taskId, [FromBody] CommentForCreationDto commentDto)
        {
            if (commentDto == null)
            {
                _logger.LogError("CommentForCreationDto is null");
                return BadRequest("CommentForCreationDto is null");
            }

            var commentEntity = _mapper.Map<TaskComment>(commentDto);
            _dataContextService.CreateComment(commentEntity, UserId, taskId);
            await _dataContextService.SaveChangesAsync();
            var commentToReturn = _mapper.Map<CommentDto>(commentEntity);
            return CreatedAtRoute("CreateCommentForTask", new { id = commentToReturn.Id }, commentToReturn);
        }

        /// <summary>
        /// Delete comment
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="taskId">task id</param>
        /// <param name="commentId">comment id</param>
        /// <returns></returns>
        [HttpDelete("{commentId}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ServiceFilter(typeof(ValidateCommentExistsAttribute))]
        public async Task<IActionResult> DeleteComment(Guid projectId, Guid taskId, Guid commentId)
        {
            var comment = HttpContext.Items["comment"] as TaskComment;
            _dataContextService.DeleteComment(comment);
            await _dataContextService.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Update comment information
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="taskId">task id</param>
        /// <param name="commentId">comment id</param>
        /// <returns></returns>
        [HttpPatch("{commentId}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ServiceFilter(typeof(ValidateCommentExistsAttribute))]
        public async Task<IActionResult> PartiallyUpdateComment(Guid projectId, Guid taskId, Guid commentId, [FromBody] JsonPatchDocument<CommentForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }

            var commentEntity = HttpContext.Items["comment"] as TaskComment;
            var commentToPatch = _mapper.Map<CommentForUpdateDto>(commentEntity);
            patchDoc.ApplyTo(commentToPatch, ModelState);
            TryValidateModel(commentToPatch);

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }

            _mapper.Map(commentToPatch, commentEntity);
            await _dataContextService.SaveChangesAsync();
            return NoContent();
        }
    }
}
