using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TeachABit.Model.DTOs.Result.Message;

namespace TeachABit.API.Middleware
{
    public class ModelStateFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var modelStateError = context.ModelState.Values.First().Errors.FirstOrDefault();

            if (modelStateError != null)
            {
                context.Result = new BadRequestObjectResult(MessageDescriber.InvalidModelState(modelStateError.ErrorMessage));
            }
        }
    }
}
