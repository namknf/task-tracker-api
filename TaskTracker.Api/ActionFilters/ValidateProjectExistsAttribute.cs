using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Contract;

namespace TaskTracker.Api.ActionFilters
{
    public class ValidateProjectExistsAttribute : IAsyncActionFilter
    {
        private readonly IDataContextService _dataService;
        private readonly ILogger _logger;

        public ValidateProjectExistsAttribute(IDataContextService dataService, ILogger<ValidateProjectExistsAttribute> logger)
        {
            _dataService = dataService;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT");
            var id = (Guid)context.ActionArguments["projectId"];
            var project = await _dataService.GetProjectAsync(id, trackChanges);
            if (project == null)
            {
                _logger.LogInformation($"Project with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("project", project);
                await next();
            }
        }
    }
}
