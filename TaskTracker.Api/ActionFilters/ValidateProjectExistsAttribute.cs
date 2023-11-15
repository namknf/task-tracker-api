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
            var id = (Guid)context.ActionArguments["projectId"];
            var project = await _dataService.GetProjectAsync(id, false);
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
