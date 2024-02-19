using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TaskTracker.Contract.Service;

namespace TaskTracker.Api.ActionFilters
{
    public class ValidateFileExistsAttribute : IAsyncActionFilter
    {
        private readonly IDataContextService _dataService;
        private readonly ILogger _logger;

        public ValidateFileExistsAttribute(IDataContextService dataService, ILogger<ValidateProjectExistsAttribute> logger)
        {
            _dataService = dataService;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var id = (Guid)context.ActionArguments["fileId"];
            var file = await _dataService.GetFileAsync(id, false);
            if (file == null)
            {
                _logger.LogInformation($"File with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("file", file);
                await next();
            }
        }
    }
}
