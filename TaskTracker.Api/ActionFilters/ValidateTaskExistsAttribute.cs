using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TaskTracker.Contract.Service;

namespace TaskTracker.Api.ActionFilters
{
    public class ValidateTaskExistsAttribute : IAsyncActionFilter
    {
        private readonly IDataContextService _dataService;
        private readonly ILogger _logger;

        public ValidateTaskExistsAttribute(IDataContextService dataService, ILogger<ValidateTaskExistsAttribute> logger)
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
            var id = (Guid)context.ActionArguments["taskId"];
            var task = await _dataService.GetTaskByProjectAsync(projectId, id, false);

            if (task == null)
            {
                _logger.LogInformation($"Task with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("task", task);
                await next();
            }
        }
    }
}
