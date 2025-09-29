using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

public sealed class ValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray() ?? Array.Empty<string>());

            context.Result = new BadRequestObjectResult(new ValidationProblemDetails(errors)
            {
                Title = "One or more validation errors occurred.",
                Status = 400
            });
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
