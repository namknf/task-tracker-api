using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Contract.Service;

namespace TaskTracker.Api.ActionFilters
{
    public class ValidateCommentExistsAttribute
    {
        private readonly IDataContextService _dataService;
        private readonly ILogger _logger;

        public ValidateCommentExistsAttribute(IDataContextService dataService, ILogger<ValidateTaskExistsAttribute> logger)
        {
            _dataService = dataService;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var projectId = (Guid)context.ActionArguments["projectId"];
            var project = await _dataService.GetProjectAsync(projectId, false);
            if (project == null)
            {
                _logger.LogInformation($"Project with id: {projectId} doesn't exist in the database.");
                context.Result = new NotFoundResult();
                return;
            }

            var taskId = (Guid)context.ActionArguments["taskId"];
            var task = await _dataService.GetTaskAsync(projectId, taskId, false);
            if (task == null)
            {
                _logger.LogInformation($"Task with id: {taskId} doesn't exist in the database.");
                context.Result = new NotFoundResult();
                return;
            }

            var commentId = (Guid)context.ActionArguments["commentId"];
            var comment = await _dataService.GetCommentAsync(taskId, commentId, false);
            if (comment == null)
            {
                _logger.LogInformation($"Comment with id: {commentId} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("comment", comment);
                await next();
            }
        }
    }
}
